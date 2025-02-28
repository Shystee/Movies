using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Movies.Application;

namespace Movies.Infrastructure;

public class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>     
	where TRequest : IRequest<TResponse>
	where TResponse : IErrorOr
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ILogger<TransactionPipelineBehavior<TRequest, TResponse>> _logger;

	public TransactionPipelineBehavior(
		IUnitOfWork unitOfWork,
		ILogger<TransactionPipelineBehavior<TRequest, TResponse>> logger)
	{
		_unitOfWork = unitOfWork;
		_logger = logger;
	}
	
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		if (request is not ICommand<TResponse>)
		{
			return await next();
		}
		
		_logger.LogInformation("Beginning transaction for {RequestType}", typeof(TRequest).Name);

		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		try
		{
			var response = await next();
			if (response.IsError)
			{
				_logger.LogWarning("Rolling back transaction for {RequestType} due to errors", typeof(TRequest).Name);
				await _unitOfWork.RollbackTransactionAsync(cancellationToken);
				return response;
			}

			// Otherwise commit the transaction
			await _unitOfWork.CommitTransactionAsync(cancellationToken);
			_logger.LogInformation("Transaction committed for {RequestType}", typeof(TRequest).Name);
            
			return response;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error during transaction for {RequestType}", typeof(TRequest).Name);
			await _unitOfWork.RollbackTransactionAsync(cancellationToken);
			throw;
		}
	}
}