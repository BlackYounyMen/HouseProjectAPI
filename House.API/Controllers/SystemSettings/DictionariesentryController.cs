using House.Dto;
using House.IRepository.SystemSettings;
using House.Model;
using House.Model.SystemSettings;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace House.API.Controllers.SystemSettings
{
    /// <summary>
    /// 字典项管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Dice")]
    public class DictionariesentryController : ControllerBase
    {
        private readonly IDictionariesentryRepository _IDictionariesentryRepository;

        public DictionariesentryController(IDictionariesentryRepository idictionariesentryrepository)
        {
            _IDictionariesentryRepository = idictionariesentryrepository;
        }

        /// <summary>
        /// 字典项下拉数据显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Dictionariesentry>> GetSelect(int id)
        {
            var predicate = PredicateBuilder.New<Dictionariesentry>(true);
            predicate.And(t => t.Pid == id);
            var data = await _IDictionariesentryRepository.GetAllListAsync(predicate);
            PageModel<Dictionariesentry> dictionariesentry = new PageModel<Dictionariesentry>();
            dictionariesentry.Data = data;
            return dictionariesentry;
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Dictionariesentry>> GetAll(int id, string itemname, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Dictionariesentry>(true);
            predicate.And(t => t.Pid == id);
            if (!string.IsNullOrEmpty(itemname))
            {
                predicate.And(t => t.ItemName.Contains(itemname));
            }
            var data = await _IDictionariesentryRepository.GetAllListAsync(predicate);
            PageModel<Dictionariesentry> dictionariesentry = new PageModel<Dictionariesentry>();
            dictionariesentry.PageCount = data.Count();
            dictionariesentry.PageSize = Convert.ToInt32(Math.Ceiling((data.Count * 1.0) / pagesize));
            dictionariesentry.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return dictionariesentry;
        }

        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Dictionariesentry>> Recoil(int id)
        {
            var predicate = PredicateBuilder.New<Dictionariesentry>(true);
            predicate.And(t => t.Id == id);
            var data = await _IDictionariesentryRepository.FirstOrDefaultAsync(predicate);
            PageModel<Dictionariesentry> dictionariesentry = new PageModel<Dictionariesentry>();

            dictionariesentry.Item = data;
            return dictionariesentry;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dictionariesentry"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DIcAdd(Dictionariesentry dictionariesentry)
        {
            try
            {
                var coding = NPinyin.Pinyin.GetPinyin(dictionariesentry.ItemName);
                dictionariesentry.Coding = coding.Replace(" ", "");
                return await _IDictionariesentryRepository.InsertAsync(dictionariesentry);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dictionariesentry"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DIcUpdate(Dictionariesentry dictionariesentry)
        {
            try
            {
                var coding = NPinyin.Pinyin.GetPinyin(dictionariesentry.ItemName);
                dictionariesentry.Coding = coding.Replace(" ", "");
                return await _IDictionariesentryRepository.UpdateAsync(dictionariesentry);
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
                var predicate = PredicateBuilder.New<Dictionariesentry>(true);
                predicate.And(t => t.Id == id);
                return await _IDictionariesentryRepository.DeleteAsync(predicate);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> EditState(int id)
        {
            try
            {
                var predicate = PredicateBuilder.New<Dictionariesentry>(true);
                predicate.And(t => t.Id == id);
                var data = await _IDictionariesentryRepository.FirstOrDefaultAsync(predicate);
                if (data.State == true)
                {
                    data.State = false;
                    return await _IDictionariesentryRepository.UpdateAsync(data);
                }
                else
                {
                    data.State = true;
                    return await _IDictionariesentryRepository.UpdateAsync(data);
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}