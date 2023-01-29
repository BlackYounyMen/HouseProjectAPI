using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 评估关系表
    /// </summary>
    public class Appraisalrelation
    {
        [Key]
        public int Id { get; set; }
        public int AssessId { get; set; }
        public int ProjectID { get; set; }
    }
}
