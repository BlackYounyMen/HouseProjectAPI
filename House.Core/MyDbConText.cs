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
        public virtual DbSet<Employee> Employee { get; set; }

        /// <summary>
        /// 角色用户关联表
        /// </summary>
        public virtual DbSet<EmployeeRole> EmployeeRole { get; set; }

        /// <summary>
        /// 角色表
        /// </summary>
        public virtual DbSet<Role> Role { get; set; }

        /// <summary>
        /// 权限角色关联
        /// </summary>
        public virtual DbSet<RolePermission> RolePermission { get; set; }

        /// <summary>
        /// 权限表
        /// </summary>
        public virtual DbSet<Permission> Permission { get; set; }
    }
}