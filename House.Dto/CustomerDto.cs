using House.Model;
using House.Model.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Dto
{
    public class CustomerDto
    {
        /// <summary>
        ///客户信息
        /// </summary>
        public Customerinfo customerinfo { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public List<Personcharge> personcharge { get; set; }

        /// <summary>
        /// 附件表
        /// </summary>
        public Fileinfo fileinfo { get; set; }
    }
}