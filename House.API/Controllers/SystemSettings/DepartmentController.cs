using House.Dto;
using House.IRepository.SystemSettings;
using House.Model.SystemSettings;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using House.Model;

namespace House.API.Controllers.SystemSettings
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Department")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _IDepartmentRepository;

        public DepartmentController(IDepartmentRepository _IdepartmentRepository)
        {
            _IDepartmentRepository = _IdepartmentRepository;
        }


        /// <summary>
        /// 角色数据添加
        /// </summary>
        /// <param name="deptmarage"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdd(Deptmarage deptmarage)
        {
            var state = await _IDepartmentRepository.InsertAsync(deptmarage);
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
        public async Task<PageModel<Deptmarage>> GetAll(string name, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Deptmarage>(true);
            if (!string.IsNullOrEmpty(name))
            {
                predicate.And(t => t.Name.Contains(name));
            }
            var data = await _IDepartmentRepository.GetAllListAsync(predicate);
            PageModel<Deptmarage> Dictionaries = new PageModel<Deptmarage>();
            Dictionaries.PageCount = data.Count();
            Dictionaries.PageSize = Convert.ToInt32(Math.Ceiling(data.Count * 1.0 / pagesize));
            Dictionaries.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return Dictionaries;
        }

        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Deptmarage>> Recoil(int id)
        {
            var predicate = PredicateBuilder.New<Deptmarage>(true);
            predicate.And(t => t.Id == id);
            var data = await _IDepartmentRepository.FirstOrDefaultAsync(predicate);
            PageModel<Deptmarage> Dictionaries = new PageModel<Deptmarage>();

            Dictionaries.Item = data;
            return Dictionaries;
        }

        /// <summary>
       

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Dictionaries"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DIcUpdate(Deptmarage deptmarage)
        {
            try
            {

                
                return await _IDepartmentRepository.UpdateAsync(deptmarage);
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
                var predicate = PredicateBuilder.New<Deptmarage>(true);
                predicate.And(t => t.Id == id);
                return await _IDepartmentRepository.DeleteAsync(predicate);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

      
    }
}
