using Galaxy.DataContext;
using Galaxy.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Identity
{
    public abstract class GalaxyIdentityDbContext<TUser, TRole> : GalaxyIdentityDbContext<TUser, TRole, int>, IGalaxyContextAsync
          where TUser : IdentityUser<int>
          where TRole : IdentityRole<int>
    {

        public GalaxyIdentityDbContext(DbContextOptions options) : base(options)
        {
          
        }

        public GalaxyIdentityDbContext(DbContextOptions options, IAppSessionBase appSession) : base(options)
        {
            
        }
    }
}
