using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUniteOfWork
    {//have all Segenture Properity For All interfaces Repository
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public Task<int> CompleteAsync();
    }
}
