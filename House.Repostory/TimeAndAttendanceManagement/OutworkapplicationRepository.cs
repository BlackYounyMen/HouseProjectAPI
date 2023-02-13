using House.Core;
using House.IRepository;

using House.Model.TimeAndAttendanceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Repository
{
    /// <summary>
    /// 外勤申请
    /// </summary>
    public class OutworkapplicationRepository : BaseService<Outworkapplication>, IOutworkapplicationRepository
    {
        public OutworkapplicationRepository(MyDbConText db) : base(db)
        {
        }
    }
}