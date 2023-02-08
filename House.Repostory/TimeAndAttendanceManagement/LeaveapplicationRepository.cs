using House.Core;
using House.IRepository;
using House.IRepository.TimeAndAttendanceManagement;
using House.Model.TimeAndAttendanceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Repository.TimeAndAttendanceManagement
{
    /// <summary>
    /// 休假申请
    /// </summary>
    public class LeaveapplicationRepository : BaseService<Leaveapplication>, ILeaveapplicationRepository
    {
        public LeaveapplicationRepository(MyDbConText db) : base(db)
        {
        }
    }
}