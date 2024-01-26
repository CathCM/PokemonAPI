namespace PokemonAPI.Services;

public class TransactionService : ITransactionService
{
    private readonly PokemonDb dbContext;

    public TransactionService(PokemonDb dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task BeginTransaction()
    {
        await dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransaction()
    {
        await dbContext.Database.CurrentTransaction.CommitAsync();
    }

    public async Task RollbackTransaction()
    {
        await dbContext.Database.CurrentTransaction.RollbackAsync();
    }
}

