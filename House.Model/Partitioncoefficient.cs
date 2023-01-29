using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 分区系数表
    /// </summary>
    public class Partitioncoefficient
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal Proportion { get; set; }
    }
}
