using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Dto
{
    public class FileBackItem
    {
        /// <summary>
        ///文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// /上传时间
        /// </summary>
        public DateTime UploadTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件预览
        /// </summary>
        public string Url { get; set; }
    }
}