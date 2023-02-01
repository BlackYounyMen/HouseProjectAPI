﻿using House.Core;
using House.IRepository;
using House.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Repository
{
    public class FileinfoRepository:BaseService<Fileinfo>,IFileinfoRepository
    {
        public FileinfoRepository(MyDbConText db):base(db)
        {

        }
    }
}
