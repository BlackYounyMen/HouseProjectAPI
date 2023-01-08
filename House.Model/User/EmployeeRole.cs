namespace House.Model
{
    /// <summary>
    /// 角色用户关联表
    /// </summary>
    public class EmployeeRole : EntityBase
    {
        /// <summary>
        ///  用户Id
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        ///  角色Id
        /// </summary>
        public int RoleId { get; set; }
    }
}