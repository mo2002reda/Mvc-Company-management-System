using BLL.Interfaces;
using DAL.Context;
using System;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class UniteOfWork : IUniteOfWork, IDisposable
    {
        private readonly CompanyDbContext _dbContext;

        public UniteOfWork(CompanyDbContext dbContext)//Ask CLR For Object FRom DbContext
        {
            EmployeeRepository = new EmployeeRepository(dbContext);//will set the property
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();

        public void Dispose()//this function Close Connection with database after executing the request(implemented from idisposable Interface)
        {
            _dbContext.Dispose();
        }
    }
}
