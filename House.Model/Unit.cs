using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 单元表
    /// </summary>
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public int Building { get; set; }
        public int UnitNum { get; set; }
    }
}
