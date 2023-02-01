
using House.Dto;
using House.IRepository;
using House.IRepository.ICustomerManagement;
using House.Model;
using House.Model.CustomerManagement;
using House.Repository.Customer;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NPOI.HPSF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace House.API.Controllers.CustomerManagement
{
    /// <summary>
    /// 客户信息控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Customerinfo")]
    public class CustomerinfoController : ControllerBase
    {
        private readonly ICustomerinfoRepository _ICustomerinfoRepository;
        private readonly IPersonchargeRepository _IPersonchargeRepository;
        private readonly IFileinfoRepository _IFileinfoRepository;
        private readonly ICustomerItemRepository _ICustomerItemRepository;
        public CustomerinfoController(ICustomerinfoRepository iCustomerinfoRepository, IPersonchargeRepository _ipersonchargeRepository, IFileinfoRepository _ifileinfoRepository, ICustomerItemRepository _icustomerItemRepository)
        {

            _ICustomerinfoRepository = iCustomerinfoRepository;
            _IPersonchargeRepository = _ipersonchargeRepository;
            _IFileinfoRepository = _ifileinfoRepository;
            _ICustomerItemRepository = _icustomerItemRepository;
        }

        /// <summary>
        /// 页面数据的显示切换
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<CustomerListDtoState> GetState() 
        {
            CustomerListDtoState d = new CustomerListDtoState();
            d.Id = true;
            d.Number = true;
            d.CustomerName = true;
            d.CompanyAddress = true;
            d.Contacts = true;
            d.Telephone = true;
            d.BankAccount = true;
            d.BankName = true;
            d.EnterpriseCode = true;
            d.CustomerType = true;
            d.Industry = true;
            d.CreditRating = true;
            d.Representative = true;
            d.TaxpayerNum = true;
            d.Cus_Id = true;
            d.DustomerId = true;
            d.Name = true;
            d.Post = true;
            d.Phone = true;
            d.Dep = true;
            d.Email = true;
            d.EntryTime = true;
            d.FileName = true;
            d.UploadTime = true;
            d.FileSize = true;
            d.FileType = true;
            d.Enteredby = true;
            d.Url = true;
            d.FIleCategroy = true;
            return d;

        }

        /// <summary>
        /// 客户信息添加
        /// </summary>
        /// <param name="customerinfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdd(CustomerDto customerdto)
        {

            try
            {
                //甲方联系人 =   redis.get()
                //附件列表 =   redis.get()
                await _ICustomerinfoRepository.InsertAsync(customerdto.customerinfo);
                var cid = customerdto.customerinfo.Id;
                List<int> jid = new List<int>();
                foreach (var item in customerdto.personcharge)
                {
                    await _IPersonchargeRepository.InsertAsync(item);
                    jid.Add(item.Id);

                }
                await _IFileinfoRepository.InsertAsync(customerdto.fileinfo);
                var fid = customerdto.fileinfo.Id;

                List<CustomerItem> customeritem = new List<CustomerItem>();
                foreach (var item in jid)
                {
                    CustomerItem list = new CustomerItem();
                    list.cid = cid;
                    list.jid = item;
                    list.fid = fid;
                    customeritem.Add(list);
                }

                foreach (var item in customeritem)
                {
                    await _ICustomerItemRepository.InsertAsync(item);
                }

                return true;
                //if(i>0)
                //{
                //    //var i = await _ICustomerinfoRepository.InsertAsync(甲方联系人);
                //    //var i = await _ICustomerinfoRepository.InsertAsync(附件列表);
                //}



                //表  4个字段  id 基本信息（扩展信息已经包含）自增值     甲方负责人自增只  附件表自增      （注意：4个全部是int类型）


            }
            catch (Exception)
            {
                return false;
                throw;
            }


        }
        /// <summary>
        /// 客户显示
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<CustomerListDto>> GetData(string name, int pageindex, int pagesize)
        {
            var data1 = await _IPersonchargeRepository.GetAllListAsync();
            var data2 = await _IFileinfoRepository.GetAllListAsync();
            var data3 = await _ICustomerItemRepository.GetAllListAsync();
            var data = await _ICustomerinfoRepository.GetAllListAsync();
            List<CustomerListDto> customerlistdto = new List<CustomerListDto>();


            var list = from a in data1
                       join b in data3 on a.Id equals b.fid
                       join c in data on b.cid equals c.Id
                       join d in data2 on b.fid equals d.Id
                       select new
                       {
                           Id = a.Id,
                           Number = c.Number,
                           CustomerName = c.CustomerName,
                           CompanyAddress = c.CompanyAddress,
                           Contacts = c.Contacts,
                           Telephone = c.Telephone,
                           BankAccount = c.BankAccount,
                           BankName = c.BankName,
                           EnterpriseCode = c.EnterpriseCode,
                           CustomerType = c.CustomerType,
                           Industry = c.Industry,
                           CreditRating = c.CreditRating,
                           Representative = c.Representative,
                           TaxpayerNum = c.TaxpayerNum,
                           Cus_Id = a.Cus_Id,
                           DustomerId = a.DustomerId,
                           Name = a.Name,
                           Post = a.Post,
                           Phone = a.Phone,
                           Dep = a.Dep,
                           Email = a.Email,
                           EntryTime = a.EntryTime,
                           FileName = d.FileName,
                           UploadTime = d.UploadTime,
                           FileSize = d.FileSize,
                           FileType = d.FileType,
                           Enteredby = d.Enteredby,
                           Url = d.Url,
                           FIleCategroy = d.FIleCategroy,

                       };
            foreach (var item in list)
            {
                CustomerListDto d = new CustomerListDto();
                d.Id = item.Id;
                d.Number = item.Number;
                d.CustomerName = item.CustomerName;
                d.CompanyAddress = item.CompanyAddress;
                d.Contacts = item.Contacts;
                d.Telephone = item.Telephone;
                d.BankAccount = item.BankAccount;
                d.BankName = item.BankName;
                d.EnterpriseCode = item.EnterpriseCode;
                d.CustomerType = item.CustomerType;
                d.Industry = item.Industry;
                d.CreditRating = item.CreditRating;
                d.Representative = item.Representative;
                d.TaxpayerNum = item.TaxpayerNum;
                d.Cus_Id = item.Cus_Id;
                d.DustomerId = item.DustomerId;
                d.Name = item.Name;
                d.Post = item.Post;
                d.Phone = item.Phone;
                d.Dep = item.Dep;
                d.Email = item.Email;
                d.EntryTime = item.EntryTime;
                d.FileName = item.FileName;
                d.UploadTime = item.UploadTime;
                d.FileSize = item.FileSize;
                d.FileType = item.FileType;
                d.Enteredby = item.Enteredby;
                d.Url = item.Url;
                d.FIleCategroy = item.FIleCategroy;
                customerlistdto.Add(d);
            }
            return customerlistdto;


        }

        /// <summary>
        /// 数据反冲，客户数据页面第一次加载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Token<Customerinfo>> Recoil(int id)
        {
            var predicate = PredicateBuilder.New<Customerinfo>(true);
            predicate.And(t => t.Id == id);

            Token<Customerinfo> d = new Token<Customerinfo>();
            d.Result = await _ICustomerinfoRepository.FirstOrDefaultAsync(predicate);
            return d;
        }



    }
}
