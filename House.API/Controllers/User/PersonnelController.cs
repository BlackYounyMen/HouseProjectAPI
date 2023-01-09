using House.IRepository.User;
using House.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using House.Repository.User;
using House.Repository;
using LinqKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using House.Model;
using House.Dto;
using System.Linq;
using System;

namespace House.API.Controllers.User
{
    /// <summary>
    /// 人员控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Personnel")]
    public class PersonnelController : ControllerBase
    {
        private readonly IPersonnelRepository _IPersonnelRepository;
        private readonly IPersonnelRoleRepository _IPersonnelRoleRepository;

        public PersonnelController(IPersonnelRepository iPersonnelRepository, IPersonnelRoleRepository iPersonnelRoleRepository)
        {
            _IPersonnelRepository = iPersonnelRepository;
            _IPersonnelRoleRepository = iPersonnelRoleRepository;
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="entityBase"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Personnel>> GetData(int pageindex, int pagesize)
        {
            var data = await _IPersonnelRepository.GetAllListAsync();

            PageModel<Personnel> datalist = new PageModel<Personnel>();
            datalist.PageCount = data.Count();
            datalist.PageSize = Convert.ToInt32(Math.Ceiling((data.Count * 1.0 / pagesize)));
            datalist.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return datalist;
        }

        /// <summary>
        /// 权限数据添加
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdd(Personnel power)
        {
            var i = await _IPersonnelRepository.InsertAsync(power);
            return i;
        }

        /// <summary>
        /// 数据页面第一次加载
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Token<Personnel>> Recoil(int id)
        {
            var predicate = PredicateBuilder.New<Personnel>(true);
            predicate.And(t => t.Id == id);

            Token<Personnel> d = new Token<Personnel>();
            d.Result = await _IPersonnelRepository.FirstOrDefaultAsync(predicate);
            return d;
        }

        /// <summary>
        /// 权限数据修改
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Moditf(Personnel power)
        {
            var i = await _IPersonnelRepository.UpdateAsync(power);
            return i;
        }

        /// <summary>
        /// 权限数据删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Delete(List<int> id)
        {
            var predicate = PredicateBuilder.New<Personnel>(true);
            predicate.And(t => id.Contains(t.Id));
            var i = await _IPersonnelRepository.DeleteAsync(predicate);
            return i;
        }

        /// <summary>
        /// 获取人员一加载就返回的角色
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Branch>> PersonnelLoad(int rid)
        {
            var predicate = PredicateBuilder.New<PersonnelRole>(true);
            predicate.And(t => t.PersonnelId == rid);
            var data = await _IPersonnelRoleRepository.GetAllListAsync(predicate);
            var q = from n in data
                    select new Branch()
                    {
                        Id = n.RoleId,
                        Pid = n.PersonnelId,
                    };
            return q.ToList();
        }

        /// <summary>
        /// 可以根据他的数据进行重新绑定权限
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> RPDelete(List<PersonnelRole> data)
        {
            var predicate = PredicateBuilder.New<PersonnelRole>(true);
            predicate.And(t => t.RoleId == data[0].RoleId);
            await _IPersonnelRoleRepository.DeleteAsync(predicate);
            foreach (var item in data)
            {
                await _IPersonnelRoleRepository.InsertAsync(item);
            }
            return true;
        }
    }
}