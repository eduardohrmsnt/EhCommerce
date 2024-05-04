using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.Shared.Application
{
    public interface IUseCase<TRequest, TResponse> : IRequestHandler<TRequest> where TRequest : IUseCaseRequest<TResponse>
    {
    }
}
