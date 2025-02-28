using Microsoft.EntityFrameworkCore.Storage;
using Movies.Application;

namespace Movies.Persistence;

public sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
	private IDbContextTransaction? _currentTransaction;

	public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
	{
		if (_currentTransaction != null)
		{
			return;
		}

		_currentTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
	}

	public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			await SaveChangesAsync(cancellationToken);

			if (_currentTransaction != null)
			{
				await _currentTransaction.CommitAsync(cancellationToken);
			}
		}
		finally
		{
			if (_currentTransaction != null)
			{
				await _currentTransaction.DisposeAsync();
				_currentTransaction = null;
			}
		}
	}

	public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			if (_currentTransaction != null)
			{
				await _currentTransaction.RollbackAsync(cancellationToken);
			}
		}
		finally
		{
			if (_currentTransaction != null)
			{
				await _currentTransaction.DisposeAsync();
				_currentTransaction = null;
			}
		}
	}

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
		await dbContext.SaveChangesAsync(cancellationToken);
}