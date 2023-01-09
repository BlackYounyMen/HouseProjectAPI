using House.Dto;
using House.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using House.Model;
using LinqKit;

namespace House.API.Controllers.User
{
    /// <summary>
    /// 权限控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Power")]
    public class PowerController : ControllerBase
    {
        private readonly IPowerRepository _IPowerRepository;

        public PowerController(IPowerRepository iPowerRepository)
        {
            _IPowerRepository = iPowerRepository;
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="entityBase"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageModel<Power>> GetData(EntityBase entityBase)
        {
            var predicate = PredicateBuilder.New<Power>(true);
            var data = await _IPowerRepository.GetAllListAsync(predicate, entityBase);
            PageModel<Power> List = new PageModel<Power>();
            List.Data = data;
            return List;
        }

        /// <summary>
        /// 权限显示
        /// </summary>
        [HttpGet]
        public async Task<List<Menu>> GetMenu()
        {
            //到时候只需要更改这样，就可以更改他的权限
            var data = await _IPowerRepository.GetAllListAsync();
            //0表示读取省级城市
            var nodes = data.Where(d => d.SuperiorId == 0).ToList();
            var q = from n in nodes
                    select new Menu()
                    {
                        name = n.Name,
                        icon = n.Icon,
                        Id = n.Id,
                        PId = n.SuperiorId,
                        path = n.Url,
                    };
            List<Menu> list = q.ToList();

            GetSon(list);
            return list;
        }

        /// <summary>
        /// 权限数据添加
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdd(Power power)
        {
            var state = await _IPowerRepository.InsertAsync(power);
            return state;
        }

        /// <summary>
        /// 权限数据修改
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Moditf(Power power)
        {
            var state = await _IPowerRepository.UpdateAsync(power);
            return state;
        }

        /// <summary>
        /// 权限数据删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Delete(List<int> id)
        {
            var predicate = PredicateBuilder.New<Power>(true);
            predicate.And(t => id.Contains(t.Id));
            var state = await _IPowerRepository.DeleteAsync(predicate);
            return state;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="dtolist"></param>

        private void GetSon(List<Menu> dtolist)
        {
            foreach (var n in dtolist)
            {
                var data = _IPowerRepository.GetAllList();
                var n_1 = data.Where(d => d.SuperiorId == n.Id).ToList();
                var q_1 = from node in n_1
                          select new Menu()
                          {
                              name = node.Name,
                              icon = node.Icon,
                              Id = node.Id,
                              PId = node.SuperiorId,
                              path = node.Url,
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
    }
}