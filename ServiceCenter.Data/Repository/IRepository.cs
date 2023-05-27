namespace ServiceCenter.Data.Repository
{
    public interface IRepository<T>
    {
        Task Create(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        IQueryable<T> Get();
        Task<T> GetById(uint id);
    }
}
