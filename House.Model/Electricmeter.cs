using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 电表
    /// </summary>
    public class Electricmeter
    {
        [Key]
        public int Id { get; set; }
        public int Building { get; set; }
        public int UnitNum { get; set; }
        public int FloorNum { get; set; }
        public int SurplusElectric { get; set; }
        public int SumElectric { get; set; }

        public bool ElectricState { get; set; }
    }
}
