using BLL.Interfaces;
using DAL.Context;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {


        #region Repository Pattern
        //private readonly CompanyDbContext _dbContext;

        //public DepartmentRepository(CompanyDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //public int Add(Department department)
        //{
        //    _dbContext.Add(department);
        //    return _dbContext.SaveChanges();
        //}

        //public IEnumerable<Department> GetAll()
        //{
        //    var department = _dbContext.departments.ToList();
        //    return department;
        //}


        //public Department GetById(int id)
        //{
        //    #region Search by Local Operator 
        //    //var dept =_dbContext.departments.Local.Where(x=>x.ID==id).FirstOrDefault();
        //    //if (dept == null)
        //    //    return _dbContext.departments.Where(x => x.ID == id).FirstOrDefault();
        //    //return dept; 
        //    #endregion
        //    var dept = _dbContext.departments.Find(id);
        //    return dept;

        //}

        //public int Remove(Department department)
        //{
        //    var dept = _dbContext.departments.Remove(department);
        //    return _dbContext.SaveChanges();
        //}

        //public int Update(Department department)
        //{
        //    var dept = _dbContext.departments.Update(department);
        //    return _dbContext.SaveChanges();
        //} 
        #endregion
        public DepartmentRepository(CompanyDbContext dbContext) : base(dbContext)
        {
            #region Explaination of parameter Constructor
            //DepartmentRepository inherit from GenericRepository so it inherit constructor chaining from GenericRepository 
            //GenericRepository Have parameter Constructor so when DepartmentRepository inherit this construrctor it should pass aparameter to this constructor 
            #endregion
        }
    }
}
//.SaveChanges() :return number of Rows affected in Database after operation (IQueriable)
//.Find() : Search firstly in Local Memory then Search In database
