using Castle.DynamicProxy;
using Galaxy.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.UnitOfWork
{
    public class UnitOfWorkInterceptor : IInterceptor
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public UnitOfWorkInterceptor(IUnitOfWorkAsync unitOfWorkAsync)
        {
            this._unitOfWorkAsync = unitOfWorkAsync ?? throw new ArgumentNullException(nameof(unitOfWorkAsync)) ;
        }
        
        public void Intercept(IInvocation invocation)
        {
            MethodInfo method;
            try
            {
                method = invocation.MethodInvocationTarget;
            }
            catch
            {
                method = invocation.GetConcreteMethod();
            }

            var disableUnitOfWorkAttribute = invocation.Method.GetCustomAttribute(typeof(DisableUnitOfWorkAttribute));

            if (disableUnitOfWorkAttribute != null)
            {
                invocation.Proceed();
                return;
            }

            var enableUnitofWorkAttribute = this.GetEnableUnitOfWorkAttributeOrNull(invocation.Method);

            if (enableUnitofWorkAttribute != null)
            {
                ProccessUnitOfWork(invocation, enableUnitofWorkAttribute.UnitOfWorkOptions);
            }
            else
            {
                invocation.Proceed();
                return;
            } 
        }

        private void ProccessUnitOfWork(IInvocation invocation, IUnitOfWorkOptions unitOfWorkOptions)
        {
            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                ProcceesAvailableTransactionsAsync(invocation, unitOfWorkOptions);
            }
            else
            {
                ProcceesAvailableTransactions(invocation, unitOfWorkOptions);
            }
        }

        private void ProcceesAvailableTransactions(IInvocation invocation, IUnitOfWorkOptions unitOfWorkOptions)
        {
            this._unitOfWorkAsync.BeginTransaction(unitOfWorkOptions);
            try
            {
                invocation.Proceed();
            }
            catch 
            {
                _unitOfWorkAsync.Dispose();
                throw;
            } 
            this._unitOfWorkAsync.Commit();
        }

        private void ProcceesAvailableTransactionsAsync(IInvocation invocation, IUnitOfWorkOptions unitOfWorkOptions)
        {
            this._unitOfWorkAsync.BeginTransaction(unitOfWorkOptions);
            try
            {
                invocation.Proceed();
            }
            catch
            {
                _unitOfWorkAsync.Dispose();
                throw;
            }

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = PublicAsyncHelper.AwaitTaskWithPostActionAndFinally(
                    (Task)invocation.ReturnValue,
                    async () => await _unitOfWorkAsync.CommitAsync(),
                    exception => _unitOfWorkAsync.Dispose()
                );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = PublicAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    async () => await _unitOfWorkAsync.CommitAsync(),
                    exception => _unitOfWorkAsync.Dispose()
                );
            }
        }

        private EnableUnitOfWorkAttribute GetEnableUnitOfWorkAttributeOrNull(MethodInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(true).OfType<EnableUnitOfWorkAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return attrs[0];
            }

            attrs = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true).OfType<EnableUnitOfWorkAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return attrs[0];
            } 
            return null;
        }

        private bool CheckIfThereIsAvailableTransaction()
        {
            return this._unitOfWorkAsync.CheckIfThereIsAvailableTransaction();
        }
    }
}
