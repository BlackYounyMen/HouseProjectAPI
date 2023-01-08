using System;

namespace House.Model
{
    /// <summary>
    /// 人员
    /// </summary>
    public class Employee : EntityBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public int DepmentId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 工资
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// 人脸识别编号
        /// </summary>
        public string FaceCore { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime EntryTime { get; set; }

        /// <summary>
        /// 离职时间
        /// </summary>
        public DateTime EndTime { get; set; }

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