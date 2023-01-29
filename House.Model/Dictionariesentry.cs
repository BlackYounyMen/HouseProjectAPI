using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 字典条目表
    /// </summary>
    public class Dictionariesentry
    {
        [Key]
        public int Id { get; set; }
        public string Coding { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int OrderId { get; set; }
        public bool State { get; set; }
    }
}
