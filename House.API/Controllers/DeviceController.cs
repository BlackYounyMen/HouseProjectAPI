using House.Dto;
using House.IRepository.DeviceManagement;
using House.IRepository.User;
using House.Model;
using House.Repository.User;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace House.API.Controllers
{
    /// <summary>
    /// 设备控制器(管理所有设备)
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Device")]
    public class DeviceController : ControllerBase
    {
        private readonly IWaterMeterRepository _IWaterMeterRepository;

        public DeviceController(IWaterMeterRepository iwatermeterrepository)
        {
            _IWaterMeterRepository = iwatermeterrepository;
        }

        /// <summary>
        /// 水表数据的显示
        /// </summary>
        /// <param name="building"></param>
        /// <param name="unitnum"></param>
        /// <param name="state"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<WaterMeter>> WaterGetData(string building, string unitnum, string state, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<WaterMeter>(true);
            if (!string.IsNullOrWhiteSpace(building))
            {
                predicate.And(t => t.Building == Convert.ToInt32(building));
            }
            if (!string.IsNullOrWhiteSpace(unitnum))
            {
                predicate.And(t => t.UnitNum == Convert.ToInt32(unitnum));
            }
            if (!string.IsNullOrWhiteSpace(state))
            {
                if (state == "True")
                {
                    predicate.And(t => t.WaterState == Convert.ToBoolean(state));
                }
                else if (state == "False")
                {
                    predicate.And(t => t.WaterState == Convert.ToBoolean(state));
                }
                else
                {
                }
            }
            var data = await _IWaterMeterRepository.GetAllListAsync();

            PageModel<WaterMeter> datalist = new PageModel<WaterMeter>();
            datalist.PageCount = data.Count();
            datalist.PageSize = Convert.ToInt32(Math.Ceiling((data.Count * 1.0) / pagesize));
            datalist.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return datalist;
        }
    }
}