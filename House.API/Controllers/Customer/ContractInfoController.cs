using House.Dto;
using House.IRepository;
using House.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using System.IO;
using System;
using System.Threading.Tasks;
using LinqKit;
using House.IRepository.User;
using House.Utils;
using System.Collections.Generic;
using Newtonsoft.Json;
using SqlSugar;
using System.Linq;
using House.IRepository.Contract;
using House.Model.ContractManagement;

namespace House.API.Controllers.ContractManage
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ContractMan")]
    public class ContractInfoController : ControllerBase
    {
        private readonly IContractInfoRepository _contractRepository;
      

        public ContractInfoController(IContractInfoRepository contractrepository)
        {
            _contractRepository = contractrepository;
          
        }
 

        /// <summary>
        /// 合同列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<ContractInfo>> GetContract(string name,string starting,string termination,int pageIndex,int pageSize)
        {
            //拼接查询条件
            var predicate = PredicateBuilder.New<ContractInfo>(true);
            if (!string.IsNullOrWhiteSpace(name))
            {
                predicate.And(t => t.ContractNum.Contains(name));
            }
            if (!string.IsNullOrWhiteSpace(starting))
            {
                predicate.And(t => t.OriginalAmount >= Convert.ToDecimal(starting));
            }
            if (!string.IsNullOrWhiteSpace(termination))
            {
                predicate.And(t => t.OriginalAmount <= Convert.ToDecimal(termination));
            }

            var tmpContract = await _contractRepository.GetAllListAsync(predicate);

            PageModel<ContractInfo> datalist = new PageModel<ContractInfo>();
            datalist.PageCount = tmpContract.Count;
            datalist.PageSize = Convert.ToInt32(Math.Ceiling((tmpContract.Count * 1.0) / pageSize));

            datalist.Data = tmpContract.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList() ;
            return datalist;
        }

        /// <summary>
        /// 根据Id获取1条数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<ContractInfo>> GetById(int id)
        {
            var predicate = PredicateBuilder.New<ContractInfo>(true);
            predicate.And(t => t.Id == id);
            var contract = await _contractRepository.FirstOrDefaultAsync(predicate);

            return new PageModel<ContractInfo> { Item = contract };
        }

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<PageModel<ContractInfo>> DeleteContract(int id)
        {
            var predicate = PredicateBuilder.New<ContractInfo>(true);
            predicate.And(t => t.Id == id);
            bool res = await _contractRepository.DeleteAsync(predicate);

            return new PageModel<ContractInfo> { Code = res ? "1" : "0" };
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
            dataRow.CreateCell(0).SetCellValue("合同编号");
            dataRow.CreateCell(1).SetCellValue("合同名称");
            dataRow.CreateCell(2).SetCellValue("建设单位");
            dataRow.CreateCell(3).SetCellValue("合同额");
            dataRow.CreateCell(4).SetCellValue("实际合同额");
            dataRow.CreateCell(5).SetCellValue("签约日期");
            dataRow.CreateCell(6).SetCellValue("工程负责人");
            dataRow.CreateCell(7).SetCellValue("时间");

            var row = 1;
            var data = await _contractRepository.GetAllListAsync();
            data.ForEach(m =>
            {
                dataRow = (HSSFRow)sheet.CreateRow(row);//新建数据行

                dataRow.CreateCell(0).SetCellValue(m.Id);

                dataRow.CreateCell(1).SetCellValue(m.ContractId);
                dataRow.CreateCell(2).SetCellValue(m.ContractNum);
                dataRow.CreateCell(3).SetCellValue(m.ConstructionUnit);
                dataRow.CreateCell(4).SetCellValue(m.OriginalAmount.ToString());
                dataRow.CreateCell(5).SetCellValue(m.ActualAmount.ToString());
                dataRow.CreateCell(6).SetCellValue(m.ProjectLeader);
                dataRow.CreateCell(7).SetCellValue(m.SigningDate.ToString());


                row++;
            });

            var fs = new MemoryStream();

            workbook.Write(fs);

            byte[] b = fs.ToArray();
            return File(b, System.Net.Mime.MediaTypeNames.Application.Octet, "合同信息数据.xls"); //关键语句
        }
    }
}
