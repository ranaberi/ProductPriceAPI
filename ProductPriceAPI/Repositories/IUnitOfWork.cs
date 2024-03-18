namespace ProductPriceAPI.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
