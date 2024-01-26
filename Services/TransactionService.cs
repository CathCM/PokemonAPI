namespace PokemonAPI.Services;

public class TransactionService : ITransactionService
{
    private readonly PokemonDb _dbContext;

    public TransactionService(PokemonDb dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task BeginTransaction()
    {
        await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransaction()
    {
        await _dbContext.Database.CurrentTransaction.CommitAsync();
    }

    public async Task RollbackTransaction()
    {
        await _dbContext.Database.CurrentTransaction.RollbackAsync();
    }
}

