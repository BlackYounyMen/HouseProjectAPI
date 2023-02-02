using House.Core;
using House.IRepository;
using House.IRepository.SystemSettings;
using House.Model.SystemSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Repository.SystemSettings
{
    public class DictionariesRepository : BaseService<Dictionaries>, IDictionariesRepository
    {
        public DictionariesRepository(MyDbConText db) : base(db)
        {
        }
    }
}