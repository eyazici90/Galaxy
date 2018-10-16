using Galaxy.DataContext;
using Galaxy.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Identity
{
    public abstract class GalaxyDbContext<TUser, TRole> : GalaxyDbContext<TUser, TRole, int>, IGalaxyContextAsync
          where TUser : IdentityUser<int>
          where TRole : IdentityRole<int>
    {

        public  GalaxyDbContext(DbContextOptions options) : base(options)
        {
          
        }

        public GalaxyDbContext(DbContextOptions options, IAppSessionBase appSession) : base(options)
        {
            
        }
    }
}
