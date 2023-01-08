using House.Model;
using Microsoft.EntityFrameworkCore;

namespace House.Core
{
    public class MyDbConText : DbContext
    {
        public MyDbConText(DbContextOptions<MyDbConText> options) : base(options)
        {
        }

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
    }
}