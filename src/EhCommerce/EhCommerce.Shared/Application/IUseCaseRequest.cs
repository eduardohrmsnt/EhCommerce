using MediatR;

namespace EhCommerce.Shared.Application
{
    public interface IUseCaseRequest<TResponse> : IRequest<TResponse>
    {
    }

    public interface IUseCaseRequest : IRequest
    {
    }
}
