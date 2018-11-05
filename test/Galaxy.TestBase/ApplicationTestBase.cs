using Galaxy.Bootstrapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.TestBase
{
    public abstract class ApplicationTestBase : LocalTestBaseResolver
    {
        protected ApplicationTestBase()
        {
            Build(builder =>
            {
                builder
                     .UseGalaxyCore();
            });
        }
    }
}
