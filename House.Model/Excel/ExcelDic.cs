using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model.Excel
{
    public class ExcelDic:EntityBase
    {
        /// <summary>
        /// 食品名称
        /// </summary>
        public string name { get; set; }


        /// <summary>
        /// 食品规格
        /// </summary>
        public string specification { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public string coding { get; set; }

        

        /// <summary>
        /// 数量
        /// </summary>
        public string num { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public string usetime { get; set; }


        /// <summary>
        /// 总金额
        /// </summary>
        public string summoney { get; set; }


        public string title { get; set; }
    }
}
