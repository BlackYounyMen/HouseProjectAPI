using House.Dto;
using House.IRepository.Contract;
using House.Model.SystemSettings;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using House.Model.ContractManagement;

namespace House.API.Controllers.Customer
{
    /// <summary>
    /// 合同收费api
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Customer")]
    public class ContractChargesController : ControllerBase
    {
        private readonly IContractChargesRepository _IContractChargesRepository;
        private readonly IContractInfoRepository _contractRepository;

        public ContractChargesController(IContractChargesRepository icontractchargesrepository, IContractInfoRepository contractRepository)
        {
            _IContractChargesRepository = icontractchargesrepository;
            _contractRepository = contractRepository;
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<ContractCharges>> GetAll(int id)
        {
            var predicate = PredicateBuilder.New<ContractCharges>(true);
            predicate.And(t => t.ContractId == id);
            var data = await _IContractChargesRepository.GetAllListAsync(predicate);
            PageModel<ContractCharges> ContractCharges = new PageModel<ContractCharges>();
            ContractCharges.Data = data;
            return ContractCharges;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ContractCharges"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> ChargesAdd(ContractCharges ContractCharges)
        {
            try
            {
                await _IContractChargesRepository.InsertAsync(ContractCharges);
                var predicate = PredicateBuilder.New<ContractInfo>(true);
                predicate.And(t => t.Id == ContractCharges.ContractId);
                var data = await _contractRepository.FirstOrDefaultAsync(predicate);
                data.SumMoney += Convert.ToDouble(ContractCharges.AmountRecorded);
                await _contractRepository.UpdateAsync(data);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}