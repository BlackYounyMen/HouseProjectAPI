using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace House.Model
{
    /// <summary>
    /// 评估项目表
    /// </summary>
    public class AssessmentItem
    {
        [Key]
        public int Id { get; set; }
        public string AssessmenId { get; set; }
        public string Name { get; set; }
        public int Performance { get; set; }
        public string Remarks { get; set; }
        public decimal Completion { get; set; }
        public decimal Incomplete { get; set; }
        public decimal Historical { get; set; }
  
        
    }
}
