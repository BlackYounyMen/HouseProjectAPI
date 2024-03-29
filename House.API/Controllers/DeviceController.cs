﻿using House.Dto;
using House.IRepository.DeviceManagement;
using House.IRepository.User;
using House.Model;
using House.Repository.User;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using House.Utils;
using House.Model.DeviceManagement;

namespace House.API.Controllers
{
    /// <summary>
    /// 设备控制器(管理所有设备)
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Device")]
    public class DeviceController : ControllerBase
    {
        private readonly IWaterMeterRepository _IWaterMeterRepository;
        private readonly IElectricmeterRepository _IElectricmeterRepository;
        private readonly IFiredoorRepository _IFiredoorRepository;
        private readonly IFanRepository _IFanRepository;

        public DeviceController(IWaterMeterRepository iwatermeterrepository, IElectricmeterRepository ielectricmeterRepository, IFiredoorRepository iFiredoorRepository, IFanRepository ifanRepository)
        {
            _IWaterMeterRepository = iwatermeterrepository;
            _IElectricmeterRepository = ielectricmeterRepository;
            _IFiredoorRepository = iFiredoorRepository;
            _IFanRepository = ifanRepository;
        }





        #region 水表
        /// <summary>
        /// 水表查询（号楼）条件所用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Building>> GetAll()
        {
            var data = await _IWaterMeterRepository.GetAllListAsync();

            var Buikcount = data.Distinct(testc => testc.Building);

            List<Building> Blist = new List<Building>();

            foreach (var item in Buikcount)
            {
                Building a = new Building();
                a.Num = item.Building;
                a.Name = item.Building + "号楼";
                Blist.Add(a);
            }
            return Blist;
        }

        /// <summary>
        /// 水表查询(单元)条件所用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UnitNum>> GetUnitAll(int id)
        {
            if (id != -1)
            {
                var predicate = PredicateBuilder.New<WaterMeter>(true);
                predicate = predicate.And(t => t.Building == id);
                var data = await _IWaterMeterRepository.GetAllListAsync(predicate);

                var Unitcount = data.Distinct(t => t.UnitNum);
                List<UnitNum> Clist = new List<UnitNum>();
                foreach (var item in Unitcount)
                {
                    UnitNum a = new UnitNum();
                    a.Num = item.UnitNum;
                    a.Name = item.UnitNum + "单员";
                    Clist.Add(a);
                }
                return Clist;
            }
            return null;
        }

        /// <summary>
        /// 水表数据的显示
        /// </summary>
        /// <param name="building"></param>
        /// <param name="unitnum"></param>
        /// <param name="state"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<WaterMeter>> WaterGetData(int building, int unitnum, string state, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<WaterMeter>(true);
            if (building != -1)
            {
                predicate.And(t => t.Building == building);
            }
            if (unitnum != -1)
            {
                predicate.And(t => t.UnitNum == unitnum);
            }
            if (!string.IsNullOrEmpty(state))
            {
                if (state == "true")
                {
                    predicate.And(t => t.WaterState == Convert.ToBoolean(state));
                }
                else if (state == "false")
                {
                    predicate.And(t => t.WaterState == Convert.ToBoolean(state));
                }
                else
                {
                }
            }

            var data = await _IWaterMeterRepository.GetAllListAsync(predicate);

            PageModel<WaterMeter> datalist = new PageModel<WaterMeter>();
            datalist.PageCount = data.Count();
            datalist.PageSize = Convert.ToInt32(Math.Ceiling((data.Count * 1.0) / pagesize));
            datalist.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return datalist;
        }

        /// <summary>
        /// 修改水表的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> EditState(int id)
        {
            var predicate = PredicateBuilder.New<WaterMeter>(true);
            predicate.And(t => t.Id == id);
            var data = await _IWaterMeterRepository.FirstOrDefaultAsync(predicate);
            if (data.WaterState == true)
            {
                data.WaterState = false;
                return await _IWaterMeterRepository.UpdateAsync(data);
            }
            else
            {
                data.WaterState = true;
                return await _IWaterMeterRepository.UpdateAsync(data);
            }
        }

        #endregion


        #region 电表
        /// <summary>
        /// 电表查询（号楼）条件所用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Building>> GetElectricAll()
        {
            var data = await _IElectricmeterRepository.GetAllListAsync();

            var Buikcount = data.Distinct(testc => testc.Building);

            List<Building> Blist = new List<Building>();

            foreach (var item in Buikcount)
            {
                Building a = new Building();
                a.Num = item.Building;
                a.Name = item.Building + "号楼";
                Blist.Add(a);
            }
            return Blist;
        }

        /// <summary>
        /// 电表查询(单元)条件所用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UnitNum>> GetElectricUnitAll(int id)
        {
            if (id != -1)
            {
                var predicate = PredicateBuilder.New<Electricmeter>(true);
                predicate = predicate.And(t => t.Building == id);
                var data = await _IElectricmeterRepository.GetAllListAsync(predicate);

                var Unitcount = data.Distinct(t => t.UnitNum);
                List<UnitNum> Clist = new List<UnitNum>();
                foreach (var item in Unitcount)
                {
                    UnitNum a = new UnitNum();
                    a.Num = item.UnitNum;
                    a.Name = item.UnitNum + "单员";
                    Clist.Add(a);
                }
                return Clist;
            }
            return null;
        }

        /// <summary>
        /// 电表数据的显示
        /// </summary>
        /// <param name="building"></param>
        /// <param name="unitnum"></param>
        /// <param name="state"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Electricmeter>> ElectricGetData(int building, int unitnum, string state, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Electricmeter>(true);
            if (building != -1)
            {
                predicate.And(t => t.Building == building);
            }
            if (unitnum != -1)
            {
                predicate.And(t => t.UnitNum == unitnum);
            }
            if (!string.IsNullOrEmpty(state))
            {
                if (state == "true")
                {
                    predicate.And(t => t.ElectricState == Convert.ToBoolean(state));
                }
                else if (state == "false")
                {
                    predicate.And(t => t.ElectricState == Convert.ToBoolean(state));
                }
                else
                {
                }
            }

            var data = await _IElectricmeterRepository.GetAllListAsync(predicate);

            PageModel<Electricmeter> datalist = new PageModel<Electricmeter>();
            datalist.PageCount = data.Count();
            datalist.PageSize = Convert.ToInt32(Math.Ceiling((data.Count * 1.0) / pagesize));
            datalist.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return datalist;
        }

        /// <summary>
        /// 电表的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> EditElectricState(int id)
        {
            var predicate = PredicateBuilder.New<Electricmeter>(true);
            predicate.And(t => t.Id == id);
            var data = await _IElectricmeterRepository.FirstOrDefaultAsync(predicate);
            if (data.ElectricState == true)
            {
                data.ElectricState = false;
                return await _IElectricmeterRepository.UpdateAsync(data);
            }
            else
            {
                data.ElectricState = true;
                return await _IElectricmeterRepository.UpdateAsync(data);
            }
        }
        #endregion


        #region 消防门表

        /// <summary>
        /// 消防门表查询（号楼）条件所用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Building>> GetFireAll()
        {
            var data = await _IFiredoorRepository.GetAllListAsync();

            var Buikcount = data.Distinct(testc => testc.Building);

            List<Building> Blist = new List<Building>();

            foreach (var item in Buikcount)
            {
                Building a = new Building();
                a.Num = item.Building;
                a.Name = item.Building + "号楼";
                Blist.Add(a);
            }
            return Blist;
        }

        /// <summary>
        /// 消防门表查询(单元)条件所用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UnitNum>> GetFireUnitAll(int id)
        {
            if (id != -1)
            {
                var predicate = PredicateBuilder.New<Firedoor>(true);
                predicate = predicate.And(t => t.Building == id);
                var data = await _IFiredoorRepository.GetAllListAsync(predicate);

                var Unitcount = data.Distinct(t => t.UnitNum);
                List<UnitNum> Clist = new List<UnitNum>();
                foreach (var item in Unitcount)
                {
                    UnitNum a = new UnitNum();
                    a.Num = item.UnitNum;
                    a.Name = item.UnitNum + "单员";
                    Clist.Add(a);
                }
                return Clist;
            }
            return null;
        }

        /// <summary>
        /// 消防门表数据的显示
        /// </summary>
        /// <param name="building"></param>
        /// <param name="unitnum"></param>
        /// <param name="state"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Firedoor>> FireGetData(int building, int unitnum, string state, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Firedoor>(true);
            if (building != -1)
            {
                predicate.And(t => t.Building == building);
            }
            if (unitnum != -1)
            {
                predicate.And(t => t.UnitNum == unitnum);
            }
            if (!string.IsNullOrEmpty(state))
            {
                if (state == "true")
                {
                    predicate.And(t => t.DoorStatus == Convert.ToBoolean(state));
                }
                else if (state == "false")
                {
                    predicate.And(t => t.DoorStatus == Convert.ToBoolean(state));
                }
                else
                {
                }
            }

            var data = await _IFiredoorRepository.GetAllListAsync(predicate);

            PageModel<Firedoor> datalist = new PageModel<Firedoor>();
            datalist.PageCount = data.Count();
            datalist.PageSize = Convert.ToInt32(Math.Ceiling((data.Count * 1.0) / pagesize));
            datalist.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return datalist;
        }

        /// <summary>
        /// 消防门表的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> EditFireState(int id)
        {
            var predicate = PredicateBuilder.New<Firedoor>(true);
            predicate.And(t => t.Id == id);
            var data = await _IFiredoorRepository.FirstOrDefaultAsync(predicate);
            if (data.DoorStatus == true)
            {
                data.DoorStatus = false;
                return await _IFiredoorRepository.UpdateAsync(data);
            }
            else
            {
                data.DoorStatus = true;
                return await _IFiredoorRepository.UpdateAsync(data);
            }
        }
        #endregion


        #region 消防风机表
        /// <summary>
        /// 消防风机表查询（号楼）条件所用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Building>> GetFanAll()
        {
            var data = await _IFanRepository.GetAllListAsync();

            var Buikcount = data.Distinct(testc => testc.Building);

            List<Building> Blist = new List<Building>();

            foreach (var item in Buikcount)
            {
                Building a = new Building();
                a.Num = item.Building;
                a.Name = item.Building + "号楼";
                Blist.Add(a);
            }
            return Blist;
        }

        /// <summary>
        /// 消防风机表查询(单元)条件所用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UnitNum>> GetFanUnitAll(int id)
        {
            if (id != -1)
            {
                var predicate = PredicateBuilder.New<Fan>(true);
                predicate = predicate.And(t => t.Building == id);
                var data = await _IFanRepository.GetAllListAsync(predicate);

                var Unitcount = data.Distinct(t => t.UnitNum);
                List<UnitNum> Clist = new List<UnitNum>();
                foreach (var item in Unitcount)
                {
                    UnitNum a = new UnitNum();
                    a.Num = item.UnitNum;
                    a.Name = item.UnitNum + "单员";
                    Clist.Add(a);
                }
                return Clist;
            }
            return null;
        }

        /// <summary>
        /// 消防风机表数据的显示
        /// </summary>
        /// <param name="building"></param>
        /// <param name="unitnum"></param>
        /// <param name="state"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Fan>> FanGetData(int building, int unitnum, string state, int pageindex, int pagesize)
        {
            var predicate = PredicateBuilder.New<Fan>(true);
            if (building != -1)
            {
                predicate.And(t => t.Building == building);
            }
            if (unitnum != -1)
            {
                predicate.And(t => t.UnitNum == unitnum);
            }
            if (!string.IsNullOrEmpty(state))
            {
                if (state == "true")
                {
                    predicate.And(t => t.FanState == Convert.ToBoolean(state));
                }
                else if (state == "false")
                {
                    predicate.And(t => t.FanState == Convert.ToBoolean(state));
                }
                else
                {
                }
            }

            var data = await _IFanRepository.GetAllListAsync(predicate);

            PageModel<Fan> datalist = new PageModel<Fan>();
            datalist.PageCount = data.Count();
            datalist.PageSize = Convert.ToInt32(Math.Ceiling((data.Count * 1.0) / pagesize));
            datalist.Data = data.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return datalist;
        }

        /// <summary>
        /// 消防风机表的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> EditFanState(int id)
        {
            var predicate = PredicateBuilder.New<Fan>(true);
            predicate.And(t => t.Id == id);
            var data = await _IFanRepository.FirstOrDefaultAsync(predicate);
            if (data.FanState == true)
            {
                data.FanState = false;
                return await _IFanRepository.UpdateAsync(data);
            }
            else
            {
                data.FanState = true;
                return await _IFanRepository.UpdateAsync(data);
            }
        }
        #endregion
    }
}