using DAL.Models;

namespace BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        //this interface will have 4 CRUD Opertations[Create - Update - Remove -Get]

        #region Repository Pattern
        //IEnumerable<Department> GetAll();
        //Department GetById(int id);
        //int Update(Department department);
        //int Remove(Department department);
        //int AddDepartment(Department department); 
        #endregion

    }
}
