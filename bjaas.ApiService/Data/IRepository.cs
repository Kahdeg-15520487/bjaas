
namespace bjaas.ApiService.Data
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> Update(T entity);
    }
}