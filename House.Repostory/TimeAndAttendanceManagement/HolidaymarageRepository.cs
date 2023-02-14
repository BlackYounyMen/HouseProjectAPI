using House.Core;
using House.IRepository;
using House.IRepository.TimeAndAttendanceManagement;
using House.Model;
using House.Model.TimeAndAttendanceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Repository.TimeAndAttendanceManagement
{
    public class HolidaymarageRepository : BaseService<Holidaymarage>, IHolidaymarageRepository
    {
        public HolidaymarageRepository(MyDbConText db) : base(db)
        {
        }
    }
}