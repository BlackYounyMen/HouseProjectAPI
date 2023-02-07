using House.Dto;
using House.IRepository;
using House.IRepository.ICustomerManagement;
using House.Model;
using House.Model.CustomerManagement;
using House.Repository;
using House.Repository.Customer;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
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

        #region 客户信息
      
        /// <summary>
        /// 页面数据的显示切换
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<CustomerListDtoState> GetState()
        {
            CustomerListDtoState d = new CustomerListDtoState();
            d.Select = true;
            d.Id = true;
            d.Number = true;
            d.CustomerName = true;
            d.CompanyAddress = true;
            d.Contacts = true;
            d.Telephone = false;
            d.BankAccount = false;
            d.BankName = false;
            d.EnterpriseCode = false;
            d.CustomerType = false;
            d.Industry = false;
            d.CreditRating = false;
            d.Representative = false;
            d.TaxpayerNum = false;
            d.Cus_Id = false;
            d.DustomerId = false;
            d.Name = false;
            d.Post = false;
            d.Phone = false;
            d.Dep = false;
            d.Email = false;
            d.EntryTime = false;
            d.FileName = false;
            d.UploadTime = false;
            d.FileSize = false;
            d.FileType = false;
            d.Enteredby = false;
            d.Url = false;
            d.FIleCategroy = false;
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
                    item.DustomerId = cid.ToString();
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
        public async Task<PageModel<CustomerListDto>> GetData(string name, int pageindex, int pagesize)
        {
            var customerlistdto = await GetDataitem();
            PageModel<CustomerListDto> customerlistdtoitem = new PageModel<CustomerListDto>();
            customerlistdtoitem.PageCount = customerlistdto.Count();
            customerlistdtoitem.PageSize = Convert.ToInt32(Math.Ceiling((customerlistdto.Count * 1.0) / pagesize));
            customerlistdtoitem.Data = customerlistdto.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return customerlistdtoitem;
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

        /// <summary>
        /// 获取文件的大小
        /// </summary>
        /// <param name="jpg"></param>
        /// <returns></returns>
        [HttpPost]
        public FileBackItem FileLoad(IFormFile jpg)
        {
            var postfile = HttpContext.Request.Form.Files[0];
            var saveUrl = Directory.GetCurrentDirectory() + @"\wwwroot\File\Annex\" + postfile.FileName;
            using (FileStream fs = new FileStream(saveUrl, FileMode.Create))
            {
                postfile.CopyTo(fs);
                fs.Flush();
            }
            FileBackItem d = new FileBackItem();
            d.FileName = postfile.FileName.Substring(0, postfile.FileName.IndexOf('.'));
            d.UploadTime = DateTime.Now;
            d.FileSize = GetFileSize(postfile.Length);
            d.FileType = postfile.FileName.Substring(postfile.FileName.IndexOf('.') + 1);
            d.Url = "https://localhost:5001/File/Annex/" + postfile.FileName;

            return d;
        }

        /// <summary>
        /// 可以用于获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<CustomerListDto>> GetDataitem()
        {
            var data1 = await _IPersonchargeRepository.GetAllListAsync();
            var data2 = await _IFileinfoRepository.GetAllListAsync();
            var data3 = await _ICustomerItemRepository.GetAllListAsync();
            var data = await _ICustomerinfoRepository.GetAllListAsync();
            List<CustomerListDto> customerlistdto = new List<CustomerListDto>();


            var list = from a in data1
                       join b in data3 on a.Id equals b.jid
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
        /// 导出数据到Excel中
        /// </summary>
        [HttpGet]
        public async Task<FileResult> NpoiExportExcel()
        {
            //定义工作簿
            HSSFWorkbook workbook = new HSSFWorkbook();
            //创建Sheet表单
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("客户基本信息");
            //设置表单列的宽度
            sheet.DefaultColumnWidth = 20;

            ///先改这里 这里先给我把字段写上 ok？嗯
            //新建标题行
            HSSFRow dataRow = (HSSFRow)sheet.CreateRow(0);
            dataRow.CreateCell(0).SetCellValue("ID");
            dataRow.CreateCell(1).SetCellValue("编号");
            dataRow.CreateCell(2).SetCellValue("客户名称");
            dataRow.CreateCell(3).SetCellValue("公司地址");
            dataRow.CreateCell(4).SetCellValue("联系人");
            dataRow.CreateCell(5).SetCellValue("电话号码");
            dataRow.CreateCell(6).SetCellValue("开户银行账号");
            dataRow.CreateCell(7).SetCellValue("开户银行名称");
            dataRow.CreateCell(8).SetCellValue("企业代码");
            dataRow.CreateCell(9).SetCellValue("客户类型");
            dataRow.CreateCell(10).SetCellValue("所属行业");
            dataRow.CreateCell(11).SetCellValue("信用级别");
            dataRow.CreateCell(12).SetCellValue("法定代表");
            dataRow.CreateCell(13).SetCellValue("纳税人识别号");
            dataRow.CreateCell(14).SetCellValue("客户信_Id");
            dataRow.CreateCell(15).SetCellValue("客户Id");
            dataRow.CreateCell(16).SetCellValue("姓名");
            dataRow.CreateCell(17).SetCellValue("职务");
            dataRow.CreateCell(18).SetCellValue("电话");
            dataRow.CreateCell(19).SetCellValue("部门");
            dataRow.CreateCell(20).SetCellValue("邮箱");
            dataRow.CreateCell(21).SetCellValue("录入时间");
            dataRow.CreateCell(22).SetCellValue("文件名");
            dataRow.CreateCell(23).SetCellValue("上传时间");
            dataRow.CreateCell(24).SetCellValue("文件大小");
            dataRow.CreateCell(25).SetCellValue("文件类型");
            dataRow.CreateCell(26).SetCellValue("录入人");
            dataRow.CreateCell(27).SetCellValue("附件路径");
            dataRow.CreateCell(28).SetCellValue("附件类别");
            var row = 1;
            var data = await GetDataitem();
            data.ForEach(m =>
            {
                dataRow = (HSSFRow)sheet.CreateRow(row);//新建数据行

                dataRow.CreateCell(0).SetCellValue(m.Id);
                dataRow.CreateCell(1).SetCellValue(m.Number);
                dataRow.CreateCell(2).SetCellValue(m.CustomerName);
                dataRow.CreateCell(3).SetCellValue(m.CompanyAddress);
                dataRow.CreateCell(4).SetCellValue(m.Contacts);
                dataRow.CreateCell(5).SetCellValue(m.Telephone);
                dataRow.CreateCell(6).SetCellValue(m.BankAccount);
                dataRow.CreateCell(7).SetCellValue(m.BankName);
                dataRow.CreateCell(8).SetCellValue(m.EnterpriseCode);
                dataRow.CreateCell(9).SetCellValue(m.CustomerType);
                dataRow.CreateCell(10).SetCellValue(m.Industry);
                dataRow.CreateCell(11).SetCellValue(m.CreditRating);
                dataRow.CreateCell(12).SetCellValue(m.Representative);
                dataRow.CreateCell(13).SetCellValue(m.TaxpayerNum);
                dataRow.CreateCell(14).SetCellValue(m.Cus_Id);
                dataRow.CreateCell(15).SetCellValue(m.DustomerId);
                dataRow.CreateCell(16).SetCellValue(m.Name);
                dataRow.CreateCell(17).SetCellValue(m.Post);
                dataRow.CreateCell(18).SetCellValue(m.Phone);
                dataRow.CreateCell(19).SetCellValue(m.Dep);
                dataRow.CreateCell(20).SetCellValue(m.Email);
                dataRow.CreateCell(21).SetCellValue(m.EntryTime);
                dataRow.CreateCell(22).SetCellValue(m.FileName);
                dataRow.CreateCell(23).SetCellValue(m.UploadTime);
                dataRow.CreateCell(24).SetCellValue(m.FileSize);
                dataRow.CreateCell(25).SetCellValue(m.FileType);
                dataRow.CreateCell(26).SetCellValue(m.Enteredby);
                dataRow.CreateCell(27).SetCellValue(m.Url);
                dataRow.CreateCell(28).SetCellValue(m.FIleCategroy);

                row++;
            });

            var fs = new MemoryStream();

            workbook.Write(fs);

            byte[] b = fs.ToArray();
            return File(b, System.Net.Mime.MediaTypeNames.Application.Octet, "客户信息数据.xls"); //关键语句
        }

        /// <summary>
        /// 格式化文件大小
        /// </summary>
        /// <param name="filesize">文件传入大小</param>
        /// <returns></returns>
        private static string GetFileSize(long filesize)
        {
            try
            {
                if (filesize < 0)
                {
                    return "0";
                }
                else if (filesize >= 1024 * 1024 * 1024)  //文件大小大于或等于1024MB
                {
                    return string.Format("{0:0.00} GB", (double)filesize / (1024 * 1024 * 1024));
                }
                else if (filesize >= 1024 * 1024) //文件大小大于或等于1024KB
                {
                    return string.Format("{0:0.00} MB", (double)filesize / (1024 * 1024));
                }
                else if (filesize >= 1024) //文件大小大于等于1024bytes
                {
                    return string.Format("{0:0.00} KB", (double)filesize / 1024);
                }
                else
                {
                    return string.Format("{0:0.00} bytes", filesize);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 客户数据页面加载
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<CustomerDto> CustomerRecoil( int id ) 
        {
            var data1 = await _IPersonchargeRepository.GetAllListAsync();
            var data2 = await _IFileinfoRepository.GetAllListAsync();
            var data3 = await _ICustomerItemRepository.GetAllListAsync();
            var data = await _ICustomerinfoRepository.GetAllListAsync();
            var fileitem =(
                        from a in data2
                        join b in data3 on a.Id equals b.fid
                        join c in data1 on b.jid equals c.Id                     
                        where c.Id == id
                        select a).FirstOrDefault();

            var customeritem = (
                        from a in data
                        join b in data3 on a.Id equals b.cid
                        join c in data1 on b.jid equals c.Id
                        where c.Id == id
                        select a).FirstOrDefault();



            CustomerDto d = new CustomerDto();
            d.personcharge = data1.Where(t => t.Id == id).ToList();
            d.fileinfo = fileitem;
            d.customerinfo = customeritem;
            return d;


        }

        #endregion


        #region 联系人
        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="entityBase"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Personcharge>> GetPerson(string name, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Personcharge>(true);
            if (!string.IsNullOrWhiteSpace(name))
            {
                predicate.And(t => t.Name.Contains(name));
            }
            var data = await _IPersonchargeRepository.GetAllListAsync(predicate);

            PageModel<Personcharge> datalist = new PageModel<Personcharge>();
            datalist.PageCount = data.Count();
            datalist.PageSize = Convert.ToInt32(Math.Ceiling((data.Count * 1.0 / pagesize)));
            datalist.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return datalist;
        }

        /// <summary>
        /// 导出数据到Excel中
        /// </summary>
        [HttpGet]
        public async Task<FileResult> PersonNpoiExportExcel()
        {
            //定义工作簿
            HSSFWorkbook workbook = new HSSFWorkbook();
            //创建Sheet表单
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("联系人信息");
            //设置表单列的宽度
            sheet.DefaultColumnWidth = 20;

            ///先改这里 这里先给我把字段写上 ok？嗯
            //新建标题行
            HSSFRow dataRow = (HSSFRow)sheet.CreateRow(0);
            dataRow.CreateCell(0).SetCellValue("ID");
        
            dataRow.CreateCell(1).SetCellValue("客户信_Id");
            dataRow.CreateCell(2).SetCellValue("客户Id");
            dataRow.CreateCell(3).SetCellValue("姓名");
            dataRow.CreateCell(4).SetCellValue("职务");
            dataRow.CreateCell(5).SetCellValue("电话");
            dataRow.CreateCell(6).SetCellValue("部门");
            dataRow.CreateCell(7).SetCellValue("邮箱");
            dataRow.CreateCell(8).SetCellValue("录入时间");
         
            var row = 1;
            var data = await _IPersonchargeRepository.GetAllListAsync();
            data.ForEach(m =>
            {
                dataRow = (HSSFRow)sheet.CreateRow(row);//新建数据行

                dataRow.CreateCell(0).SetCellValue(m.Id);

                dataRow.CreateCell(1).SetCellValue(m.Cus_Id);
                dataRow.CreateCell(2).SetCellValue(m.DustomerId);
                dataRow.CreateCell(3).SetCellValue(m.Name);
                dataRow.CreateCell(4).SetCellValue(m.Post);
                dataRow.CreateCell(5).SetCellValue(m.Phone);
                dataRow.CreateCell(6).SetCellValue(m.Dep);
                dataRow.CreateCell(7).SetCellValue(m.Email);
                dataRow.CreateCell(8).SetCellValue(m.EntryTime.ToString());
              

                row++;
            });

            var fs = new MemoryStream();

            workbook.Write(fs);

            byte[] b = fs.ToArray();
            return File(b, System.Net.Mime.MediaTypeNames.Application.Octet, "联系人数据.xls"); //关键语句
        }
        /// <summary>
        /// 联系人查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Personcharge> PerSonRecoil( int id ) 
        {
            var predicate = PredicateBuilder.New<Personcharge>(true); // where 1=1
            predicate.And(t => t.Id == id);  // and
            var data = await _IPersonchargeRepository.FirstOrDefaultAsync(predicate);
            return data;
        }
        #endregion



    }
}