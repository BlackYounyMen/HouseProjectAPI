using House.Core;
using House.IRepository.DeviceManagement;
using House.Model.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Repository.DeviceManagement
{
    public class ElectricmeterRepository : BaseService<Electricmeter>, IElectricmeterRepository
    {
        private readonly MyDbConText _db;

        public ElectricmeterRepository(MyDbConText db) : base(db)
        {
            _db = db;
        }

        public void Add()
        {
            //var query = _db.Categories.FromSql("select * from Category");

            //var categoryID = 1;
            //var query = _db.Categories.FromSql($"GetCategoryById {categoryID}");

            //var result = query.ToList();

            //Assert.NotNull(result);
        }
    }
}