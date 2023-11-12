namespace DAL.Auth.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
