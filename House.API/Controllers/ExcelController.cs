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
using House.IRepository;
using House.Model.Excel;

namespace House.API.Controllers
{

    // 运用Rbac（用户 - 角色 -权限 ）机制来进行对用户的管理 如用户太少的话不会启用此方案 因为越简答的东西使用的人群就会越多  并且数据就是用来展示使用的
    //注 ： 自增不在显示 每个表中必会有这个字段  方便进行增删改操作
    // 每张表最多使用一个月  这样使用的目的是方便汇总数据 以防人更改上个月的信息  造成数据失误的产生  同时也是方便自己操作 多加尝试  否则以后忘记的数据会更加的多  因为已经好久没有设计过程序
    // 目前设计构想有3张表
    // 一：为数据的历史记录信息  方便展示一种商品每天到底会有多少 现在构想的字段有 （ 名称 规格 数量  单价  总价  日期）  根据日期进行数据的显示以及排列  
    // 二：为数据每天规格（斤 瓶  桶 等等）增删改查的表 方便更加直观的显示总共要了一个月要了多少斤的数据 ps：因为每天各种蔬菜瓜果的价格 都不太相同  这个需要（采购方）进行操作每天的价格 
    // 三：字典表  用来具体显示判断数据是否正确显示  显示一些分类字段

    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Excel")]
    public class ExcelController : ControllerBase
    {

        private readonly IExcelRepository _iexcelrepository;

        public ExcelController(IExcelRepository excelRepository)
        {
            _iexcelrepository = excelRepository;
        }



       


        /// <summary>
        /// 获取Excel 数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpPost("ExcelRun")]
        public string ExcelRun(string path)
        {
            int totalcount = 0;

          

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var titleid = "";  //给具体的标题id 用于显示展示数据
                var title = "";     //给具体的标题  一般是不会进行更改程序里面的设定
                IWorkbook workbook = new XSSFWorkbook(stream); // 创建工作簿对象  
                ISheet sheet = workbook.GetSheetAt(0); // 获取第一个工作表  
                
                for (int rowIndex = 0; rowIndex < sheet.LastRowNum; rowIndex++) // 遍历行  
                {
                    if (sheet.GetRow(rowIndex) != null) // 检查行是否存在  
                    {
                        var name = "";
                        var specification = "";
                        var price = "";
                        var num = "";
                        var sum = "";
                        for (int columnIndex = 0; columnIndex < sheet.GetRow(rowIndex).LastCellNum; columnIndex++) // 遍历列  
                        {
                            ICell cell = sheet.GetRow(rowIndex).GetCell(columnIndex); // 获取单元格
                            var DelStr = cell.ToString();
                            switch (DelStr)
                            {                               
                                #region 舍去字段
                                case "名称": break;
                                case "规格": break;
                                case "单价": break;
                                case "数量": break;
                                case "总价": break;
                                case "合计": break;
                                #endregion
                                default:
                                    if (!string.IsNullOrEmpty(DelStr)) // 检查单元格是否存在  
                                    {
                                        // 如果已经获取分类手段 ，在遇见新的分类，重新进行划分
                                        switch (DelStr)
                                        {
                                            case "": break;
                                            case "肉类采购": titleid = "1"; title = "肉类采购"; break;
                                            case "蔬果采购": titleid = "2"; title = "蔬果采购"; break;
                                            case "调料采购": titleid = "3"; title = "调料采购"; break;
                                            case "其他采购": titleid = "4"; title = "其他采购"; break;

                                        }
                                        switch (columnIndex)
                                        {
                                            case 0: name = DelStr; break;
                                            case 1: specification = DelStr; break;
                                            case 2: price = DelStr; break;
                                            case 3: num = DelStr; break;
                                            case 4: sum = DelStr; break;
                                            default: break;
                                        }
                                        break;
                                    }
                                    break;
                            }

                        }
                        #region  数据已经获取
                      
                        if (!string.IsNullOrWhiteSpace(name))
                        {
                            if (name != "肉类采购" && name != "蔬果采购" && name != "调料采购" && name != "其他采购")
                            {
                                totalcount++;
                                ExcelClass data = new ExcelClass();
                                data.name = name;
                                data.specification = specification;
                                data.price = price;
                                data.num = num;
                                data.sum = sum;
                                data.dictionary = titleid;
                                data.title = title;



                                //  var json = JsonConvert.SerializeObject(data);  序列化成json文件

                                //此为增加历史表格记录 每天承新增开始 先目标需求 在有一张表  ,查询 此表当中 有没有这个字段（例如 中华鲟 香菜 或者 香油 等等 有的话就修改他的数量  没有的话就给他进行新增操作）
                                Console.WriteLine(JsonConvert.SerializeObject(data) );

                                _iexcelrepository.Insert(data);
                            

                             
                            }
                        }


                        #endregion

                    }
                }
            }

           


            return null;

        }


    }
}
