using House.Dto;
using House.IRepository.SystemSettings;
using House.Model.SystemSettings;
using House.Repository.SystemSettings;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace House.API.Controllers.SystemSettings
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Log")]
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _IlogRepository;
        public LogController(ILogRepository ilogRepository)
        {
            _IlogRepository = ilogRepository;
        }

        /// <summary>
        /// 日志数据添加
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdd(Log log)
        {
            var state = await _IlogRepository.InsertAsync(log);
            return state;
        }


        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Log>> GetAll(string name, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Log>(true);
            if (!string.IsNullOrEmpty(name))
            {
                predicate.And(t => t.Title.Contains(name));
            }
            var data = await _IlogRepository.GetAllListAsync(predicate);
            PageModel<Log> Log = new PageModel<Log>();
            Log.PageCount = data.Count();
            Log.PageSize = Convert.ToInt32(Math.Ceiling(data.Count * 1.0 / pagesize));
            Log.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return Log;
        }

        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Log>> Recoil(int id)
        {
            var predicate = PredicateBuilder.New<Log>(true);
            predicate.And(t => t.Id == id);
            var data = await _IlogRepository.FirstOrDefaultAsync(predicate);
            PageModel<Log> log = new PageModel<Log>();

            log.Item = data;
            return log;
        }




        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DIcUpdate(Log log)
        {
            try
            {
                return await _IlogRepository.UpdateAsync(log);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> Delete(int id)
        {
            try
            {
                var predicate = PredicateBuilder.New<Log>(true);
                predicate.And(t => t.Id == id);
                return await _IlogRepository.DeleteAsync(predicate);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
