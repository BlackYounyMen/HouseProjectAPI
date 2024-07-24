using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System;
using NPOI.SS.Formula.Functions;
using Newtonsoft.Json;
using SqlSugar;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using House.IRepository.DeviceManagement;
using House.Model.Excel;
using House.Model.SystemSettings;
using System.Text;
using System.Xml.Linq;
using House.IRepository.Excel;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics.Mcmc;
using System.Linq;
using House.Model.TimeAndAttendanceManagement;
using LinqKit;

namespace House.API.Controllers.Excel
{

    // 运用Rbac（用户 - 角色 -权限 ）机制来进行对用户的管理 如用户太少的话不会启用此方案 因为越简答的东西使用的人群就会越多  并且数据就是用来展示使用的
    //注 ： 自增不在显示 每个表中必会有这个字段  方便进行增删改操作
    // 每张表最多使用一个月  这样使用的目的是方便汇总数据 以防人更改上个月的信息  造成数据失误的产生  同时也是方便自己操作 多加尝试  否则以后忘记的数据会更加的多  因为已经好久没有设计过程序
    // 目前设计构想有3张表
    // 一：为数据的历史记录信息  方便展示一种商品每天到底会有多少 现在构想的字段有 （ 名称 规格 数量  单价  总价  日期）  根据日期进行数据的显示以及排列  
    // 二：为数据每天规格（斤 瓶  桶 等等）增删改查的表 方便更加直观的显示总共要了一个月要了多少斤的数据 ps：因为每天各种蔬菜瓜果的价格 都不太相同  这个需要（采购方）进行操作每天的价格 
    // 三：字典表  用来具体显示判断数据是否正确显示  显示一些分类字段
    // 采购分类的数据以后要做成活的数据 方便进行管理  要基于 for 循环来进行处理这个问题
    //  此页面所有人都可以进行查看展示  但是要修改数据的金额 要采购人 使用



    #region 导入已经完成
        //现在导入已经完成  但是 see 的时候要靠什么来 see  是要靠 它的id值 还是它的名称 如果是名称的话 可能会有重复的数据 根据id来查询 这个是要字典表来进行维护这个数据

    #endregion

    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Excel")]
    public class ExcelController : ControllerBase
    {

        private readonly IExcelRepository _iexcelrepository;


        private readonly IExcelDicRepository _excelDicRepository;

        public ExcelController(IExcelRepository excelRepository, IExcelDicRepository excelDicRepository)
        {
            _iexcelrepository = excelRepository;
            _excelDicRepository = excelDicRepository;
        }


        /// <summary>
        /// 测试数据库
        /// </summary>
        /// <param name="demo"></param> 
        /// <returns></returns>
        [HttpPost("ExcelDemoUsing")]

        public async Task<List<string> >  ExcelDemoUsing( List<ExcelDemo> demo) 
        {
            List<string> console = new List<string>();

            try
            {
                int i = 1;              

                foreach (ExcelDemo demoItem in demo)
                {
                 
                    string consoleAdd = await ExcelRunGpt(demoItem.path, demoItem.dateTime);
                    consoleAdd = i + "," + consoleAdd;
                    console.Add(consoleAdd);
                    i++;
                }
                return console;
            }
            catch (Exception ex)
            {
                console.Add(ex.ToString());
                return console;

            }


        }


        /// <summary>
        /// 获取Excel 数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>

        private async Task<string> ExcelRunGpt(string path, DateTime dateTime)
        {
            // 处理的总记录数
            int totalProcessedCount = 0;

            // 在ExcelDic表中添加的新记录数
            int excelDicNewRecordsCount = 0;

            // 在ExcelDic表中更新的记录数
            int excelDicUpdatedRecordsCount = 0;

            // 在ExcelClass表中添加的新记录数
            int excelClassNewRecordsCount = 0;

            try
            {
                var olddata = await _excelDicRepository.GetAllListAsync();

                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(stream);
                    ISheet sheet = workbook.GetSheetAt(0);

                    string dictionary = "";
                    string title = "";

                    for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        var row = sheet.GetRow(rowIndex);
                        if (row == null) continue;

                        ExcelClass data = new ExcelClass { usetime = dateTime };

                        for (int columnIndex = 0; columnIndex < row.LastCellNum; columnIndex++)
                        {
                            ICell cell = row.GetCell(columnIndex);
                            string cellValue = cell?.ToString();

                            if (string.IsNullOrEmpty(cellValue)) continue;

                            switch (cellValue)
                            {
                                #region Excel舍去字段
                                case "名称": break;
                                case "规格": break;
                                case "单价": break;
                                case "数量": break;
                                case "总价": break;
                                case "合计": break;
                                #endregion

                                #region 根据需求来定制采购的分类
                                default:
                                    // 分类处理
                                    switch (cellValue)
                                    {
                                        case "肉类采购": dictionary = "1"; title = "肉类采购"; break;
                                        case "蔬果采购": dictionary = "2"; title = "蔬果采购"; break;
                                        case "调料采购": dictionary = "3"; title = "调料采购"; break;
                                        case "其他采购": dictionary = "4"; title = "其他采购"; break;
                                    }

                                    // 数据填充
                                    switch (columnIndex)
                                    {
                                        case 0: data.name = cellValue; break;
                                        case 1: data.specification = cellValue; break;
                                        case 2: data.price = cellValue; break;
                                        case 3: data.num = cellValue; break;
                                        case 4: data.sum = cellValue; break;
                                    }
                                    break;
                                #endregion

                            }
 
                        }

                        if (string.IsNullOrWhiteSpace(data.name) ||
                            data.name == "肉类采购" ||
                            data.name == "蔬果采购" ||
                            data.name == "调料采购" ||
                            data.name == "其他采购") continue;

                        data.dictionary = dictionary;
                        data.title = title;


                        var predicate = PredicateBuilder.New<ExcelDic>(true);
                        predicate.And(t => t.name == data.name);
                        predicate.And(t => t.specification == data.specification);


                        var existingRecord = olddata.FirstOrDefault(predicate);
                        if (existingRecord != null)
                        {
                            if (decimal.TryParse(data.num, out var newNum) && decimal.TryParse(existingRecord.num, out var existingNum))
                            {
                                existingRecord.num = (newNum + existingNum).ToString();
                                await _excelDicRepository.AUpdateAsync(existingRecord);
                                excelDicUpdatedRecordsCount++;
                            }
                            else
                            {
                                Console.WriteLine($"Invalid number format: {data.num} or {existingRecord.num}");
                            }
                        }
                        else
                        {
                            var dic = new ExcelDic
                            {
                                name = data.name,
                                coding = NPinyin.Pinyin.GetPinyin(data.name).Replace(" ", ""),
                                usetime = dateTime.Month.ToString(),
                                specification = data.specification,
                                title = title,
                                num = data.num
                            };
                            await _excelDicRepository.InsertAsync(dic);
                            excelDicNewRecordsCount++;
                        }

                        await _iexcelrepository.InsertAsync(data);
                        excelClassNewRecordsCount++;
                        totalProcessedCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常，可以记录日志或者返回错误信息
                return $"An error occurred: {ex.Message}";
            }

            return $"总添加数量: {totalProcessedCount} 个, 向统计表中添加数据: {excelDicNewRecordsCount} 个, 修改统计表中的数据: {excelDicUpdatedRecordsCount} 个,向记录表中添加的数据: {excelClassNewRecordsCount} 个";


           //  return $"Total records processed: {totalProcessedCount}, New records added to ExcelDic: {excelDicNewRecordsCount}, Records updated in ExcelDic: {excelDicUpdatedRecordsCount}, New records added to ExcelClass: {excelClassNewRecordsCount}";
        
        
        }















    }
}
