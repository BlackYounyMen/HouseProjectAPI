using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model.CustomerManagement
{
    public class CustomerItem:EntityBase
    {
        /// <summary>
        /// 这个是客户自增值
        /// </summary>
        public int cid { get; set; }

        /// <summary>
        /// 这个是甲方负责人自增值
        /// </summary>
        public int jid { get; set; }

        /// <summary>
        /// 这个是附件表自增值
        /// </summary>

        public int fid { get; set; }
    }
}
