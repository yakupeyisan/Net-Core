﻿using Core.DataAccess.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class UserRepository : EfBaseRepository<User, InstagramDbContext>, IUserRepository
    {
    }
}
