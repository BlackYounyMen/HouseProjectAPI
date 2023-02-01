using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 附件表
    /// </summary>
    public class Fileinfo : EntityBase
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Cus_Id { get; set; }

        public string FileName { get; set; }
        public DateTime UploadTime { get; set; } = DateTime.Now;
        public string FileSize { get; set; }
        public string FileType { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public string Enteredby { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        public string FIleCategroy { get; set; }
    }
}