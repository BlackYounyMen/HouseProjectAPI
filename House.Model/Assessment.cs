using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 评估表
    /// </summary>
    public class Assessment
    {
        [Key]
        public int Id { get; set; }
        public string TaskNo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Remarks { get; set; }
        public string ProjectAssessment { get; set; }
        public bool ArchiveorNot { get; set; }

    
    }
}
