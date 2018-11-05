using Galaxy.Mapster.Bootstrapper;
using Galaxy.TestBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Mapster.Tests
{
   public class GalaxyMapsterTestApplication : ApplicationTestBase
    {
        public GalaxyMapsterTestApplication()
        {
            Build(builder => 
            {
                 builder
                    .UseGalaxyMapster();
            }).Initialize();
        }
    }
}
