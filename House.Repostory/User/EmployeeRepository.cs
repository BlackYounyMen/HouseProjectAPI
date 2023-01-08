using House.Core;
using House.IRepository;
using House.Model;

namespace House.Repository
{
    public class EmployeeRepository : BaseService<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MyDbConText db) : base(db)
        {
        }
    }
}