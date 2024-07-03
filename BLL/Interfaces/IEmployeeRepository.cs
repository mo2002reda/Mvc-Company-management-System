using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        #region Repository Pattern
        //IEnumerable<Employee> GetAll();
        //Employee GetById(int id);
        //int Update(Employee employee);
        //int Remove(Employee employee);
        //int Add(Employee employee); 
        #endregion
        IEnumerable<Employee> SearchByAddress(string address);//filter the requirement in Database then get the result
        IEnumerable<Employee> SearchByName(string city);//get all addresses in memory then filter the result to get requirements
    }
}
