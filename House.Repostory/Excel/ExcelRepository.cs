using House.Core;
using House.IRepository.Excel;
using House.Model.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Repository.Excel
{
    public class ExcelRepository : BaseService<ExcelClass>, IExcelRepository
    {
        public ExcelRepository(MyDbConText db) : base(db)
        {

        }
    }
}
