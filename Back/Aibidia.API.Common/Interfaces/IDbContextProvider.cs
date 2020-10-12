namespace Aibidia.API.Common.Interfaces
{
    public interface IDbContextProvider<TContext>
    {
        TContext GetCurrentDbContext();
        TContext CreateNewCurrentDbContext();
    }
}
