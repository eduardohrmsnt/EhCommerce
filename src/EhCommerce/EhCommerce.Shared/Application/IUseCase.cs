using MediatR;

namespace EhCommerce.Shared.Application
{
    public interface IUseCase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IUseCaseRequest<TResponse>
    {
    }

    public interface IUseCase<TRequest> : IRequestHandler<TRequest> where TRequest : IUseCaseRequest
    {
    }
}
