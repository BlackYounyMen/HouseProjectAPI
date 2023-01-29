using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 采访表
    /// </summary>
    public class Interviewmarage
    {
        [Key]
        public int Id { get; set; }
        public int DeptId { get; set; }
        public string Name { get; set; }
        public DateTime InterviewTime { get; set; }
        public bool Result { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
    }
}
