using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 建筑编号表
    /// </summary>
    public class BuildingNum
    {
        [Key]
        public int Id { get; set; }
        public int Building { get; set; }
    }
}
