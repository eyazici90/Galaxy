using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EntityFrameworkCore.EntityConfigurations
{
    public abstract class GalaxyBaseEntityTypeConfigration<T> : GalaxyBaseEntityTypeConfigration<T, int> where T : Entity<int>
    {

    }
}
