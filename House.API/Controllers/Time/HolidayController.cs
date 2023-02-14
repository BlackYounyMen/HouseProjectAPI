using House.Dto;
using House.IRepository.TimeAndAttendanceManagement;
using House.Model.TimeAndAttendanceManagement;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace House.API.Controllers.Time
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Holiday")]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidaymarageRepository _IHolidaymarageRepository;

        public HolidayController(IHolidaymarageRepository IHolidaymarageRepository)
        {
            _IHolidaymarageRepository = IHolidaymarageRepository;
        }

        /// <summary>
        /// 节假日数据添加
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdd(Holidaymarage Holidaymarage)
        {
            var state = await _IHolidaymarageRepository.InsertAsync(Holidaymarage);
            return state;
        }

        /// <summary>
        /// 节假日数据删除
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> CreateAdd(int id)
        {
            var predicate = PredicateBuilder.New<Holidaymarage>(true);
            predicate.And(t => t.Id == id);
            var state = await _IHolidaymarageRepository.DeleteAsync(predicate);
            return state;
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="entityBase"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Holidaymarage>> GetData()
        {
            var predicate = PredicateBuilder.New<Holidaymarage>(true);

            var data = await _IHolidaymarageRepository.GetAllListAsync(predicate);

            PageModel<Holidaymarage> datalist = new PageModel<Holidaymarage>();

            datalist.Data = data;
            return datalist;
        }
    }
}