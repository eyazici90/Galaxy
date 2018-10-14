using Castle.DynamicProxy;
using Galaxy.Tasks;
using System;
using System.Collections.Generic;
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
            var disableValidationAttribute = invocation.Method.GetCustomAttribute(typeof(DisableUnitOfWorkAttribute));

            if (disableValidationAttribute != null)
            {
                invocation.Proceed();
                return;
            }

            ProccessUnitOfWork(invocation);
        }

        private void ProccessUnitOfWork(IInvocation invocation )
        {
            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                ProcceedAvailableTransactionsAsync(invocation);
            }
            else
            {
                ProcceedAvailableTransactions(invocation);
            }
        }

        private void ProcceedAvailableTransactions(IInvocation invocation )
        {
                invocation.Proceed();
                _unitOfWorkAsync.SaveChangesAsync().ConfigureAwait(false)
                .GetAwaiter().GetResult();   
        }

        private void ProcceedAvailableTransactionsAsync(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                _unitOfWorkAsync.Dispose();
                throw ex;
            }

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = PublicAsyncHelper.AwaitTaskWithPostActionAndFinally(
                    (Task)invocation.ReturnValue,
                    async () => await _unitOfWorkAsync.SaveChangesAsync(),
                    exception => _unitOfWorkAsync.Dispose()
                );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = PublicAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    async () => await _unitOfWorkAsync.SaveChangesAsync(),
                    exception => _unitOfWorkAsync.Dispose()
                );
            }
        }

    
        private  bool CheckIfThereIsAvailableTransaction()
        {
            return this._unitOfWorkAsync.CheckIfThereIsAvailableTransaction();
        }
    }
}
