using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 假期日历表
    /// </summary>
    public class Holidaymarage
    {
        [Key]
        public int Id { get; set; }
        public DateTime HolidayTime { get; set; }
        public string HolidayType { get; set; }

    }
}
