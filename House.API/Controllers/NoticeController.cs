using House.Dto;
using House.IRepository;
using House.IRepository.SystemSettings;
using House.Repository.SystemSettings;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using House.Model;
using System.Linq;
using House.Model.SystemSettings;
using System.Collections.Generic;

namespace House.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Notice")]
    public class NoticeController : ControllerBase
    {
        private readonly INoticeRepository _INoticeRepository;
        private readonly IHumanResourcesRepository _IHumanResourcesRepository;

        public NoticeController(INoticeRepository _InoticeRepository, IHumanResourcesRepository _IhumanResourcesRepository)
        {
            _INoticeRepository = _InoticeRepository;
            _IHumanResourcesRepository = _IhumanResourcesRepository;
        }

        /// <summary>
        /// 获取人员所有信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PerSonLIst>> GetHumen()
        {
            var predicate = PredicateBuilder.New<Humanresources>(true);

            var data = await _IHumanResourcesRepository.GetAllListAsync(predicate);
            List<PerSonLIst> list = new List<PerSonLIst>();
            foreach (var item in data)
            {
                PerSonLIst peritem = new PerSonLIst();
                peritem.key = item.Id;
                peritem.label = "     " + item.Name;
                list.Add(peritem);
            }
            return list;
        }

        /// <summary>
        /// 公告数据添加
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdd(Notice notice)
        {
            var state = await _INoticeRepository.InsertAsync(notice);
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
        public async Task<PageModel<Notice>> GetAll(string name, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Notice>(true);
            if (!string.IsNullOrEmpty(name))
            {
                predicate.And(t => t.Title.Contains(name));
            }
            var data = await _INoticeRepository.GetAllListAsync(predicate);
            PageModel<Notice> Notice = new PageModel<Notice>();
            Notice.PageCount = data.Count();
            Notice.PageSize = Convert.ToInt32(Math.Ceiling(data.Count * 1.0 / pagesize));
            Notice.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return Notice;
        }

        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<NoticeDto>> Recoil(int id)
        {
            var predicate = PredicateBuilder.New<Notice>(true);
            predicate.And(t => t.Id == id);
            var data = await _INoticeRepository.FirstOrDefaultAsync(predicate);

            NoticeDto Dtp = new NoticeDto();
            Dtp.Title = data.Title;
            Dtp.Content = data.Content;
            Dtp.ReleaseTime = data.ReleaseTime;
            Dtp.PublishUser = data.PublishUser;
            Dtp.State = data.State;

            List<int> i = new List<int>();
            var item = data.AcceptRole.Split(',');
            foreach (var a in item)
            {
                int d = Convert.ToInt32(a);
                i.Add(d);
            }
            var ids = PredicateBuilder.New<Humanresources>(true);
            ids.And(t => i.Contains(t.Id));

            var list = await _IHumanResourcesRepository.GetAllListAsync(ids);

            var name = "";
            foreach (var nameshow in list)
            {
                name += nameshow.Name + "    ";
            }
            name = name.Substring(0, name.Length - 1);
            Dtp.NameShow = name;
            PageModel<NoticeDto> Notice = new PageModel<NoticeDto>();
            Notice.Item = Dtp;
            return Notice;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DIcUpdate(Notice notice)
        {
            try
            {
                return await _INoticeRepository.UpdateAsync(notice);
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
                var predicate = PredicateBuilder.New<Notice>(true);
                predicate.And(t => t.Id == id);
                return await _INoticeRepository.DeleteAsync(predicate);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}