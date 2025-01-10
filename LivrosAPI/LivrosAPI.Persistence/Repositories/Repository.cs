using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Linq.Expressions;


namespace LivrosAPI.Persistence.Repositories
{
    public class Repository<T> : IDisposable, IRepository<T> where T : Entidade, new()
    {       
        internal readonly BDContext _context;
        internal string ConnectionString => _context.Database.GetDbConnection().ConnectionString;

        public Repository(BDContext context)
        {            
            _context = context;            
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync<T>();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public async Task<List<T>> FindQueryAsync(string query)
        {
            return await _context.Set<T>().FromSqlRaw(query).ToListAsync();
        }
     
        /// <summary>
        /// Find with Params
        /// </summary>
        /// <param name="query">Sql Query, format: SELECT * FROM myTable WHERE name = @myName</param>
        /// <param name="key">Params to Query, pass in query using @param</param>
        /// <returns></returns>
        public async Task<List<T>> FindQueryAsync(string query, params object[] key)
        {
            return await _context.Set<T>().FromSqlRaw(query, key).ToListAsync();
        }     

        public async Task UpdateAsync(T obj)
        {
            _context.Set<T>().Update(obj);            
            await _context.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            return await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T obj)
        {
            _context.Set<T>().Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Func<T, bool> predicate)
        {
            _context.Set<T>()
                .Where(predicate).ToList()
                .ForEach(del => _context.Set<T>().Remove(del));

            await _context.SaveChangesAsync();
        }
     
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

    }
}
