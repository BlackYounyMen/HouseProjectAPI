using House.Dto;
using House.IRepository;
using House.IRepository.DeviceManagement;
using House.IRepository.SystemSettings;
using House.IRepository.User;
using House.Model;
using House.Model.SystemSettings;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace House.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Dictionaries")]
    public class DictionariesController : ControllerBase
    {
        private readonly IDictionariesRepository _IDictionariesRepository;
        public DictionariesController(IDictionariesRepository _DictionariesRepository)
        {
            _IDictionariesRepository = _DictionariesRepository;
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Dictionaries>> GetAll(int id, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Dictionaries>(true);
            predicate.And(t => t.Id == id);
            var data = await _IDictionariesRepository.GetAllListAsync(predicate);
            PageModel<Dictionaries> Dictionaries = new PageModel<Dictionaries>();
            Dictionaries.PageCount = data.Count();
            Dictionaries.PageSize = Convert.ToInt32(Math.Ceiling((data.Count * 1.0) / pagesize));
            Dictionaries.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return Dictionaries;
        }

        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Dictionaries>> GetAllSelect()
        {
        
            var data = await _IDictionariesRepository.GetAllListAsync();
            PageModel<Dictionaries> Dictionaries = new PageModel<Dictionaries>();

            Dictionaries.Data = data;
            return Dictionaries;
        }

        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Dictionaries>> Recoil(int id)
        {
            var predicate = PredicateBuilder.New<Dictionaries>(true);
            predicate.And(t => t.Id == id);
            var data = await _IDictionariesRepository.FirstOrDefaultAsync(predicate);
            PageModel<Dictionaries> Dictionaries = new PageModel<Dictionaries>();

            Dictionaries.Item = data;
            return Dictionaries;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Dictionaries"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DIcAdd(Dictionaries Dictionaries)
        {
            try
            {
                return await _IDictionariesRepository.InsertAsync(Dictionaries);
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
        /// <param name="Dictionaries"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DIcUpdate(Dictionaries Dictionaries)
        {
            try
            {
                return await _IDictionariesRepository.UpdateAsync(Dictionaries);
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
                var predicate = PredicateBuilder.New<Dictionaries>(true);
                return await _IDictionariesRepository.DeleteAsync(predicate);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }

}
