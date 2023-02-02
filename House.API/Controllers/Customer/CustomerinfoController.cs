using House.Dto;
using House.IRepository.Contract;
using House.IRepository.ICustomerManagement;
using House.Model.CustomerManagement;
using House.Repository.Customer;
using LinqKit;
using MathNet.Numerics.RootFinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Threading.Tasks;

namespace House.API.Controllers.Customer
{
    /// <summary>
    /// 合同控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Customer")]
    public class CustomerinfoController : ControllerBase
    {
        private readonly ICustomerinfoRepository _ICustomerinfoRepository;

        private readonly IContractChargesRepository _IContractChargesRepository;

        private readonly IContractInfoRepository _IContractInfoRepository;

        private readonly ISubscriptioninfoRepository _ISubscriptioninfoRepository;

        public CustomerinfoController(ICustomerinfoRepository icustomerinforepository, IContractChargesRepository IContractChargesRepository, IContractInfoRepository IContractInfoRepository, ISubscriptioninfoRepository ISubscriptioninfoRepository)
        {
            _ICustomerinfoRepository = icustomerinforepository;
            _IContractChargesRepository = IContractChargesRepository;
            _IContractInfoRepository = IContractInfoRepository;
            _ISubscriptioninfoRepository = ISubscriptioninfoRepository;
        }

        /// <summary>
        /// 客户信息 (用于合同录入时选择)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Customerinfo>> Customers()
        {
            var data = await _ICustomerinfoRepository.GetAllListAsync();
            return new PageModel<Customerinfo> { Data = data };
        }

        /// <summary>
        /// 根据选中客户获取详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Customerinfo>> GetByNumber(string number)
        {
            var predicate = PredicateBuilder.New<Customerinfo>();
            predicate.And(t => t.Number == number);
            var data = await _ICustomerinfoRepository.FirstOrDefaultAsync(predicate);
            return new PageModel<Customerinfo> { Item = data };
        }

        /// <summary>
        /// 合同添加
        /// </summary>
        /// <param name="customedto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CustomeAdd(CustomeDto customedto)
        {
            try
            {
                await _IContractInfoRepository.InsertAsync(customedto.contractinfo);

                foreach (var item in customedto.subscriptioninfo)
                {
                    await _ISubscriptioninfoRepository.InsertAsync(item);
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }
    }
}