﻿using House.Model;
using House.Model.ContractManagement;
using House.Model.CustomerManagement;
using House.Model.SystemSettings;
using Microsoft.EntityFrameworkCore;

namespace House.Core
{
    public class MyDbConText : DbContext
    {
        public MyDbConText(DbContextOptions<MyDbConText> options) : base(options)
        {
        }

        #region RBAC一套

        /// <summary>
        /// 人员
        /// </summary>
        public virtual DbSet<Personnel> Personnel { get; set; }

        /// <summary>
        /// 角色用户关联表
        /// </summary>
        public virtual DbSet<PersonnelRole> PersonnelRole { get; set; }

        /// <summary>
        /// 角色表
        /// </summary>
        public virtual DbSet<Role> Role { get; set; }

        /// <summary>
        /// 权限角色关联
        /// </summary>
        public virtual DbSet<RolePower> RolePower { get; set; }

        /// <summary>
        /// 权限表
        /// </summary>
        public virtual DbSet<Power> Power { get; set; }

        #endregion

        #region 设备数据

        /// <summary>
        /// 水表
        /// </summary>
        public virtual DbSet<WaterMeter> WaterMeter { get; set; }

        #endregion

        #region 客户管理

        /// <summary>
        /// 客户信息表
        /// </summary>

        public virtual DbSet<Customerinfo> Customerinfo { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public virtual DbSet<Personcharge> Personcharge { get; set; }

        /// <summary>
        /// 连接表
        /// </summary>
        public virtual DbSet<CustomerItem> CustomerItem { get; set; }

        #endregion

        #region 合同管理

        /// <summary>
        /// 合同费用表
        /// </summary>

        public virtual DbSet<ContractCharges> ContractCharges { get; set; }
        /// <summary>
        /// 合同信息表
        /// </summary>

        public virtual DbSet<ContractInfo> ContractInfo { get; set; }
        /// <summary>
        /// 合同签约信息表
        /// </summary>

        public virtual DbSet<Subscriptioninfo> Subscriptioninfo { get; set; }

     

        #endregion

        #region 系统数据

        /// <summary>
        /// 字典项表
        /// </summary>
        public virtual DbSet<Dictionariesentry> Dictionariesentry { get; set; }
        public virtual DbSet<Dictionaries> Dictionaries { get; set; }

        public virtual DbSet<Deptmarage> Deptmarage { get; set; }

        #endregion

        /// <summary>
        /// 附件表
        /// </summary>
        public virtual DbSet<Fileinfo> Fileinfo { get; set; }
    }
}