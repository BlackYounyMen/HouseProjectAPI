using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 扇表
    /// </summary>
    public class Fan
    {
        [Key]
        public int Id { get; set; }
        public int Building { get; set; }
        public int UnitNum { get; set; }
        public int FloorNum { get; set; }
        public int FanState { get; set; }
    }
}
