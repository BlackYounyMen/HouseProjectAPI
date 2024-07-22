using House.Core;
using House.IRepository;
using House.Model.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Repository
{
    public class ExcelDicRepository : BaseService<ExcelDic>, IExcelDicRepository
    {
        public ExcelDicRepository(MyDbConText db): base(db) 
        {
                    
        }
    }
}
