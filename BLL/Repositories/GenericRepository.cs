using BLL.Interfaces;
using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CompanyDbContext _dbContext;

        public GenericRepository(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))//to solve lazy loading which solve by specification properity
            {
                return (IEnumerable<T>)_dbContext.employees.Include(x => x.Departments);

            }
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        => await _dbContext.Set<T>().FindAsync(id);

        public void Remove(T entity)
        {
            _dbContext.Remove(entity);

        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }
    }
}
