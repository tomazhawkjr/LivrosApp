using LivrosAPI.Domain.Entities.Base;
using System.Linq.Expressions;

namespace LivrosAPI.Application.Contracts.Persistence.Repositories
{
    public interface IRepository<T> where T : Entidade
    {
        Task<List<T>> GetAllAsync();
        List<T> Find(Func<T, bool> predicate);
        Task<List<T>> FindQueryAsync(string query);
        Task<List<T>> FindQueryAsync(string query, params object[] key);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task UpdateAsync(T obj);
        Task<int> InsertAsync(T obj);
        Task DeleteAsync(Func<T, bool> predicate);
        Task DeleteAsync(T obj);
    }
}
