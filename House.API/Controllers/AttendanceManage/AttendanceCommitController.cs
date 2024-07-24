using House.IRepository.SystemSettings;
using House.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using House.Dto;
using House.Model.SystemSettings;
using House.Model;
using House.Repository.SystemSettings;
using House.Repository;
using LinqKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using House.Model.TimeAndAttendanceManagement;
using System.Linq;

namespace House.API.Controllers.AttendanceManage
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "AttendanceCommit")]
    public class AttendanceCommitController : ControllerBase
    {
        private readonly ILeaveapplicationRepository _ILeaveapplicationRepository;
        private readonly IOutworkapplicationRepository _IOutworkapplicationRepository;
        private readonly ITravelapplicationRepository _ITravelapplicationRepository;

        public AttendanceCommitController(ILeaveapplicationRepository _IleaveapplicationRepository, IOutworkapplicationRepository _IoutworkapplicationRepository, ITravelapplicationRepository _ItravelapplicationRepository)
        {
            _ILeaveapplicationRepository = _IleaveapplicationRepository;
            _IOutworkapplicationRepository = _IoutworkapplicationRepository;
            _ITravelapplicationRepository = _ItravelapplicationRepository;

        }
      


        #region 出差申请

        /// <summary>
        /// 出差数据添加
        /// </summary>
        /// <param name="travelapplication"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdd(Travelapplication travelapplication)
        {
            var state = await _ITravelapplicationRepository.InsertAsync(travelapplication);
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
        public async Task<PageModel<Travelapplication>> GetAll(string name, int pageindex, int pagesize)
        {
            var predicate  = PredicateBuilder.New<Travelapplication>(true);
            if (!string.IsNullOrEmpty(name))
            {
                predicate.And(t => t.ProjectName.Contains(name));
            }
            var data = await _ITravelapplicationRepository.GetAllListAsync(predicate);
            PageModel<Travelapplication> Travelapplication = new PageModel<Travelapplication>();
            Travelapplication.PageCount = data.Count();
            Travelapplication.PageSize = Convert.ToInt32(Math.Ceiling(data.Count * 1.0 / pagesize));
            Travelapplication.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return Travelapplication;
        }

        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Travelapplication>> Recoil(int id)
        {
            var predicate = PredicateBuilder.New<Travelapplication>(true);
            predicate.And(t => t.Id == id);
            var data = await _ITravelapplicationRepository.FirstOrDefaultAsync(predicate);
            PageModel<Travelapplication> Travelapplication = new PageModel<Travelapplication>();

            Travelapplication.Item = data;
            return Travelapplication;
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="travelapplication"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DIcUpdate(Travelapplication travelapplication)
        {
            try
            {
                return await _ITravelapplicationRepository.UpdateAsync(travelapplication);
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
                var predicate = PredicateBuilder.New<Travelapplication>(true);
                predicate.And(t => t.Id == id);
                return await _ITravelapplicationRepository.DeleteAsync(predicate);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        #endregion

        #region 休假申请

        /// <summary>
        /// 休假数据添加
        /// </summary>
        /// <param name="leaveapplication"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> leaveCreateAdd(Leaveapplication leaveapplication)
        {
            var state = await _ILeaveapplicationRepository.InsertAsync(leaveapplication);
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
        public async Task<PageModel<Leaveapplication>> leaveGetAll(string name, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Leaveapplication>(true);
            if (!string.IsNullOrEmpty(name))
            {
                predicate.And(t => t.Applicant.Contains(name));
            }
            var data = await _ILeaveapplicationRepository.GetAllListAsync(predicate);
            PageModel<Leaveapplication> Leaveapplication = new PageModel<Leaveapplication>();
            Leaveapplication.PageCount = data.Count();
            Leaveapplication.PageSize = Convert.ToInt32(Math.Ceiling(data.Count * 1.0 / pagesize));
            Leaveapplication.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return Leaveapplication;
        }

        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Leaveapplication>> leaveRecoil(int id)
        {
            var predicate = PredicateBuilder.New<Leaveapplication>(true);
            predicate.And(t => t.Id == id);
            var data = await _ILeaveapplicationRepository.FirstOrDefaultAsync(predicate);
            PageModel<Leaveapplication> Leaveapplication = new PageModel<Leaveapplication>();

            Leaveapplication.Item = data;
            return Leaveapplication;
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="leaveapplication"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> leaveUpdate(Leaveapplication leaveapplication)
        {
            try
            {
                return await _ILeaveapplicationRepository.UpdateAsync(leaveapplication);
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
        public async Task<bool> leaveDelete(int id)
        {
            try
            {
                var predicate = PredicateBuilder.New<Leaveapplication>(true);
                predicate.And(t => t.Id == id);
                return await _ILeaveapplicationRepository.DeleteAsync(predicate);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        #endregion

        #region 外勤申请

        /// <summary>
        /// 外勤数据添加
        /// </summary>
        /// <param name="outworkapplication"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> OutWorkCreateAdd(Outworkapplication outworkapplication)
        {
            var state = await _IOutworkapplicationRepository.InsertAsync(outworkapplication);
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
        public async Task<PageModel<Outworkapplication>> OutWorkGetAll(string name, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Outworkapplication>(true);
            if (!string.IsNullOrEmpty(name))
            {
                predicate.And(t => t.ProjectName.Contains(name));
            }
            var data = await _IOutworkapplicationRepository.GetAllListAsync(predicate);
            PageModel<Outworkapplication> Outworkapplication = new PageModel<Outworkapplication>();
            Outworkapplication.PageCount = data.Count();
            Outworkapplication.PageSize = Convert.ToInt32(Math.Ceiling(data.Count * 1.0 / pagesize));
            Outworkapplication.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return Outworkapplication;
        }

        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Outworkapplication>> OutWorkRecoil(int id)
        {
            var predicate = PredicateBuilder.New<Outworkapplication>(true);
            predicate.And(t => t.Id == id);
            var data = await _IOutworkapplicationRepository.FirstOrDefaultAsync(predicate);
            PageModel<Outworkapplication> Outworkapplication = new PageModel<Outworkapplication>();

            Outworkapplication.Item = data;
            return Outworkapplication;
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="outworkapplication"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> OutWorkUpdate(Outworkapplication outworkapplication)
        {
            try
            {
                return await _IOutworkapplicationRepository.UpdateAsync(outworkapplication);
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
        public async Task<bool> OutWorkDelete(int id)
        {
            try
            {
                var predicate = PredicateBuilder.New<Outworkapplication>(true);
                predicate.And(t => t.Id == id);
                return await _IOutworkapplicationRepository.DeleteAsync(predicate);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        #endregion
    }
}
