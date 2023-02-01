using House.Core;
using House.IRepository.ICustomerManagement;
using House.Model.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Repository.Customer
{
    public class CustomerItemRepository:BaseService<CustomerItem>,ICustomerItemRepository
    {
        public CustomerItemRepository(MyDbConText db):base(db)
        {

        }
    }
}
