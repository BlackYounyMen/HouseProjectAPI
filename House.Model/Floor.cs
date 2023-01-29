using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 地板表
    /// </summary>
    public class Floor
    {
        [Key]
        public int Id { get; set; }
        public int UnitNum { get; set; }
        public int FloorNum { get; set; }
    }
}
