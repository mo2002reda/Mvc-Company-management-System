using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int? id);
        void Update(T entity);
        void Remove(T entity);
        Task AddAsync(T entity);

        //Update & Remove => not have async Version because there only change object state {Removed OR Updated } so them don't any operations

    }
}
