﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 奖金表
    /// </summary>
    public class Bonus
    {
        [Key]
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public decimal BonusRatio { get; set; }
        
    }
}
