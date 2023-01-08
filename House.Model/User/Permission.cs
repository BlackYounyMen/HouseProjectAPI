namespace House.Model
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Permission : EntityBase
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 上级Id
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }
    }
}