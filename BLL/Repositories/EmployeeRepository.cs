using BLL.Interfaces;
using DAL.Context;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _dbContext;

        public EmployeeRepository(CompanyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #region Repository Pattern
        //private readonly CompanyDbContext _dbContext;

        //public EmployeeRepository(CompanyDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //public int Add(Employee employee)
        //{
        //    _dbContext.employees.Add(employee);
        //    return _dbContext.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAll()
        //=> _dbContext.employees.ToList();

        //public Employee GetById(int id)
        //=> _dbContext.employees.Find();

        //public int Remove(Employee employee)
        //{
        //    _dbContext.employees.Remove(employee);
        //    return _dbContext.SaveChanges();
        //}

        //public int Update(Employee employee)
        //{
        //    _dbContext.employees.Update(employee);
        //    return _dbContext.SaveChanges();
        //} 
        #endregion
        public IEnumerable<Employee> SearchByAddress(string address)
         => _dbContext.employees.Where(E => E.Address.Contains(address));

        public IEnumerable<Employee> SearchByName(string Name)
        => _dbContext.employees.Where(E => E.Name.ToLower().Contains(Name.ToLower()));
    }
}
