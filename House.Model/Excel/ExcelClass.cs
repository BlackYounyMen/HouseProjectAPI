using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model.Excel
{
    /// <summary>
    /// 每天记录的采购总数
    /// </summary>
    public class ExcelClass:EntityBase
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
        /// 食品价格
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public string num { get; set; }

        /// <summary>
        /// 商品总价
        /// </summary>
        public string sum { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public string dictionary { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }


        /// <summary>
        /// 采购时间
        /// </summary>
        public DateTime usetime { get; set; }
    }
}
