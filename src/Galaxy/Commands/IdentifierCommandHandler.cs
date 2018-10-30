using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.Commands
{
    public class IdentifierCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R>
       where T : IRequest<R>
    {
        public Task<R> Handle(IdentifiedCommand<T, R> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
