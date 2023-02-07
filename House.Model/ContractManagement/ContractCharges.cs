using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model.ContractManagement
{
    /// <summary>
    /// 合同收费表
    /// </summary>
    public class ContractCharges : EntityBase
    {
        
        public int ContractId { get; set; }
        public decimal AmountRecorded { get; set; }
        public string Contractinfo { get; set; }
        public DateTime RecordedTime { get; set; }
        public string Remarks { get; set; }
    }
}
