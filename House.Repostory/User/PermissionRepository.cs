using House.Core;
using House.IRepository;
using House.Model;

namespace House.Repository
{
    public class PermissionRepository : BaseService<Permission>, IPermissionRepository
    {
        public PermissionRepository(MyDbConText db) : base(db)
        {
        }
    }
}