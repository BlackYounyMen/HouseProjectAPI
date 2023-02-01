using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model.CustomerManagement
{
    /// <summary>
    /// 负责人表
    /// </summary>
    public class Personcharge : EntityBase
    {
       
        public string Cus_Id { get; set; }
        public string DustomerId { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }
        public string Phone { get; set; }
        public string Dep { get; set; }
        public string Email { get; set; }
        public DateTime EntryTime { get; set; } = DateTime.Now;
    }
}
