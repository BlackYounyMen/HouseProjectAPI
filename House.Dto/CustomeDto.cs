using House.Model.ContractManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Dto
{
    /// <summary>
    /// 合同DTO
    /// </summary>
    public class CustomeDto
    {
        public ContractInfo contractinfo { get; set; }
        public List<Subscriptioninfo> subscriptioninfo { get; set; }
    }
}