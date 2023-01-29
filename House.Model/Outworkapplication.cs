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
    /// 操作日志输出工作应用程序表
    /// </summary>
    public class Outworkapplication
    {
        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Place { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Duration { get; set; }
        public decimal Kilometers { get; set; }
        public string Applicant { get; set; }
    }
}
