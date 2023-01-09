using House.Dto;
using House.IRepository;
using House.IRepository.User;
using House.Model;
using House.Repository;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace House.API.Controllers.User
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _IRoleRepository;
        private readonly IRolePowerRepository _IRolePowerRepository;

        public RoleController(IRoleRepository iRoleRepository, IRolePowerRepository iRolePowerRepository)
        {
            _IRoleRepository = iRoleRepository;
            _IRolePowerRepository = iRolePowerRepository;
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="entityBase"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Role>> GetData(int pageindex, int pagesize)
        {
            var data = await _IRoleRepository.GetAllListAsync();

            PageModel<Role> datalist = new PageModel<Role>();
            datalist.PageCount = data.Count();
            datalist.PageSize = Convert.ToInt32(Math.Ceiling((data.Count * 1.0 / pagesize)));
            datalist.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return datalist;
        }

        /// <summary>
        /// 角色数据添加
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdd(Role role)
        {
            var state = await _IRoleRepository.InsertAsync(role);
            return state;
        }

        /// <summary>
        /// 角色数据修改
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Moditf(Role role)
        {
            var state = await _IRoleRepository.UpdateAsync(role);
            return state;
        }

        /// <summary>
        /// 角色数据删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Delete(List<int> id)
        {
            var predicate = PredicateBuilder.New<Role>(true);
            predicate.And(t => id.Contains(t.Id));
            var state = await _IRoleRepository.DeleteAsync(predicate);
            return state;
        }

        /// <summary>
        /// 获取角色一加载就返回的数据
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Branch>> RoleLoad(int rid)
        {
            var predicate = PredicateBuilder.New<RolePower>(true);
            predicate.And(t => t.RoleId == rid);
            var data = await _IRolePowerRepository.GetAllListAsync(predicate);
            var q = from n in data
                    select new Branch()
                    {
                        Id = n.RoleId,
                        Pid = n.PowerId,
                    };
            return q.ToList();
        }

        /// <summary>
        /// 可以根据他的数据进行重新绑定权限
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> RPDelete(List<RolePower> data)
        {
            var predicate = PredicateBuilder.New<RolePower>(true);
            predicate.And(t => t.RoleId == data[0].RoleId);
            await _IRolePowerRepository.DeleteAsync(predicate);
            foreach (var item in data)
            {
                await _IRolePowerRepository.InsertAsync(item);
            }
            return true;
        }

        /// <summary>
        /// 角色数据的显示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Menu>> GetAllRole()
        {
            //到时候只需要更改这样，就可以更改他的权限
            var data = await _IRoleRepository.GetAllListAsync();
            //0表示读取省级城市
            var q = from n in data
                    select new Menu()
                    {
                        Id = n.Id,
                        name = n.RoleName,
                    };
            return q.ToList();
        }
    }
}