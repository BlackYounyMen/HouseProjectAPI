using House.Core;
using House.IRepository.Contract;
using House.IRepository.User;
using House.Model;
using House.Model.ContractManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Repository.Contract
{
    public class ContractInfoRepository : BaseService<ContractInfo>, IContractInfoRepository
    {
        public ContractInfoRepository(MyDbConText db) : base(db)
        {
        }
    }
}