using House.Dto;
using House.IRepository;
using House.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace House.API.Controllers

{
    /// <summary>
    /// 用户登录控制   器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Login")]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeRepository _IEmployeeRepository;
        private readonly IPermissionRepository _IPermissionRepository;

        public LoginController(IEmployeeRepository employeerepository, IPermissionRepository iPermissionRepository)
        {
            _IEmployeeRepository = employeerepository;
            _IPermissionRepository = iPermissionRepository;
        }

        /// <summary>
        /// 登录功能
        /// </summary>
        /// <returns></returns>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UserLogin()
        {
            return Ok(true);
        }

        /// <summary>
        /// 人员查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Employee> GetAll()
        {
            List<Employee> data = new List<Employee>();
            data = _IEmployeeRepository.GetAllList();
            return data;
        }

        /// <summary>
        /// 菜单显示
        /// </summary>
        [HttpGet]
        public async Task<List<Menu>> GetMenu()
        {
            //到时候只需要更改这样，就可以更改他的权限
            var data = await _IPermissionRepository.GetAllListAsync();
            //0表示读取省级城市
            var nodes = data.Where(d => d.PId == 0).ToList();
            var q = from n in nodes
                    select new Menu()
                    {
                        name = n.Name,
                        icon = n.Icon,
                        Id = n.Id,
                        PId = n.PId,
                        path = n.Name,
                    };
            List<Menu> list = q.ToList();

            GetSon(list);
            return list;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="dtolist"></param>
        private void GetSon(List<Menu> dtolist)
        {
            foreach (var n in dtolist)
            {
                var data = _IPermissionRepository.GetAllList();
                var n_1 = data.Where(d => d.PId == n.Id).ToList();
                var q_1 = from node in n_1
                          select new Menu()
                          {
                              name = node.Name,
                              icon = node.Icon,
                              Id = node.Id,
                              PId = node.PId,
                              path = node.Name,
                          };

                List<Menu> list = q_1.ToList();
                if (list.Count() > 0)
                {
                    n.children = new List<Menu>();
                    n.children.AddRange(list);
                }

                GetSon(list);
            }
        }

        /// <summary>
        /// 导出数据到Excel中
        /// </summary>
        //public FileResult NpoiExportExcel()
        //{
        //    //定义工作簿
        //    HSSFWorkbook workbook = new HSSFWorkbook();
        //    //创建Sheet表单
        //    HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("车辆信息");
        //    //设置表单列的宽度
        //    sheet.DefaultColumnWidth = 20;

        //    //新建标题行
        //    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(0);
        //    dataRow.CreateCell(0).SetCellValue("厂牌型号");
        //    dataRow.CreateCell(1).SetCellValue("车牌号");
        //    dataRow.CreateCell(2).SetCellValue("司机姓名");
        //    dataRow.CreateCell(3).SetCellValue("所属公司");
        //    dataRow.CreateCell(4).SetCellValue("车型长");
        //    dataRow.CreateCell(5).SetCellValue("车型宽");
        //    dataRow.CreateCell(6).SetCellValue("车型高");
        //    dataRow.CreateCell(7).SetCellValue("车身颜色");
        //    dataRow.CreateCell(8).SetCellValue("购买日期");
        //    dataRow.CreateCell(9).SetCellValue("运营证号");
        //    dataRow.CreateCell(10).SetCellValue("保险到期时间");
        //    dataRow.CreateCell(11).SetCellValue("年检到期时间");
        //    dataRow.CreateCell(12).SetCellValue("保养公里设置");
        //    var row = 1;
        //    var persons = _app.GetAll().Result.data;
        //    persons.ForEach(m =>
        //    {
        //        dataRow = (HSSFRow)sheet.CreateRow(row);//新建数据行

        //        dataRow.CreateCell(0).SetCellValue(m.LableModel);
        //        dataRow.CreateCell(1).SetCellValue(m.CarNum);
        //        dataRow.CreateCell(2).SetCellValue(m.DriverName);
        //        dataRow.CreateCell(3).SetCellValue(m.FromCompany);
        //        dataRow.CreateCell(4).SetCellValue(m.Long.ToString());
        //        dataRow.CreateCell(5).SetCellValue(m.Width.ToString());
        //        dataRow.CreateCell(6).SetCellValue(m.Heigth.ToString());
        //        dataRow.CreateCell(7).SetCellValue(m.Color);
        //        dataRow.CreateCell(8).SetCellValue(m.BuyDate.ToString());
        //        dataRow.CreateCell(9).SetCellValue(m.OperationNum);
        //        dataRow.CreateCell(10).SetCellValue(m.InsuranceDate.ToString());
        //        dataRow.CreateCell(11).SetCellValue(m.YearDate.ToString());
        //        dataRow.CreateCell(12).SetCellValue(m.MaintenanceKmSetting);

        //        row++;
        //    });

        //    var fs = new MemoryStream();

        //    workbook.Write(fs);

        //    byte[] b = fs.ToArray();
        //    return File(b, System.Net.Mime.MediaTypeNames.Application.Octet, "车辆数据.xls"); //关键语句
        //}
    }
}