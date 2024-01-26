namespace PokemonAPI.Services;

public interface ITransactionService
{
        Task BeginTransaction();        
        Task CommitTransaction();
        Task RollbackTransaction();

}