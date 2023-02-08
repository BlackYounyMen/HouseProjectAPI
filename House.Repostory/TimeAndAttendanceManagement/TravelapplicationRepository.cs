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
    /// 出差申请
    /// </summary>
    public class TravelapplicationRepository : BaseService<Travelapplication>, ITravelapplicationRepository
    {
        public TravelapplicationRepository(MyDbConText db) : base(db)
        {
        }
    }
}