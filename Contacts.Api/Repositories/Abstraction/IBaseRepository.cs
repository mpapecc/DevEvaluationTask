namespace Contacts.Api.Repositories.Abstraction
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetById(Guid id);

        IQueryable<T> GetAll();

        Task<bool> AddAsync(T entity);

        Task<bool> AddRangeAsync(List<T> entities);

        Task<bool> UpdateAsync(T entity);

        Task<bool> UpdateRangeAsync(List<T> entities);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> DeleteRangeAsync(List<T> entities);

    }
}
