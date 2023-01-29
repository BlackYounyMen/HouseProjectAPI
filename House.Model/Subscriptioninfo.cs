using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 订阅信息表
    /// </summary>
    public class Subscriptioninfo
    {
        [Key]
        public int Id { get; set; }
        public string ContractId { get; set; }
        public string AgreementName { get; set; }
        public decimal BuiltupArea { get; set; }
        public decimal ActualAmount { get; set; }
        public bool ChargingStatus { get; set; }
        public string Remarks { get; set; }
        public DateTime SigningDate { get; set; }
    }
}
