using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 旅游申请表
    /// </summary>
    public class Travelapplication
    {
        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string TravelPlace { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Duration { get; set; }
        public string Remarks { get; set; }
        
        public decimal Kilometers { get; set; }
        public string Applicant { get; set; }
    }
}
