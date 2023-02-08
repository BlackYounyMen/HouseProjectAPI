using House.Dto;
using House.IRepository.ICustomerManagement;
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
    [ApiExplorerSettings(GroupName = "HumanResources")]
    public class HumanResourcesController : ControllerBase
    {
        private readonly IHumanResourcesRepository _IHumanResourcesRepository;
        
        public HumanResourcesController(IHumanResourcesRepository _IhumanResourcesRepository)
        {
            _IHumanResourcesRepository = _IhumanResourcesRepository;
           
        }


        /// <summary>
        /// 角色数据添加
        /// </summary>
        /// <param name="Humanresource"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdd(Humanresources Humanresource)
        {
            var state = await _IHumanResourcesRepository.InsertAsync(Humanresource);
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
        public async Task<PageModel<Humanresources>> GetAll(string name, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Humanresources>(true);
            if (!string.IsNullOrEmpty(name))
            {
                predicate.And(t => t.Name.Contains(name));
            }
            var data = await _IHumanResourcesRepository.GetAllListAsync(predicate);
            PageModel<Humanresources> Humanresources = new PageModel<Humanresources>();
            Humanresources.PageCount = data.Count();
            Humanresources.PageSize = Convert.ToInt32(Math.Ceiling(data.Count * 1.0 / pagesize));
            Humanresources.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return Humanresources;
        }

        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Humanresources>> Recoil(int id)
        {
            var predicate = PredicateBuilder.New<Humanresources>(true);
            predicate.And(t => t.Id == id);
            var data = await _IHumanResourcesRepository.FirstOrDefaultAsync(predicate);
            PageModel<Humanresources> Humanresources = new PageModel<Humanresources>();

            Humanresources.Item = data;
            return Humanresources;
        }




        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Humanresource"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DIcUpdate(Humanresources Humanresource)
        {
            try
            {


                return await _IHumanResourcesRepository.UpdateAsync(Humanresource);
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
                var predicate = PredicateBuilder.New<Humanresources>(true);
                predicate.And(t => t.Id == id);
                return await _IHumanResourcesRepository.DeleteAsync(predicate);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
