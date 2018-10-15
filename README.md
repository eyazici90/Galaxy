# Galaxy
Next generation framework for Domain Driven Design needs

# Usage 

 GalaxyMainBootsrapper.Create()
                 .RegisterContainerBuilder()
                     .UseGalaxyCore()
                     .UseGalaxyEntityFrameworkCore(
                                new DbContextOptionsBuilder<YourDbContext>()
                                     .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                     .UseGalaxyMapster()
                     .UseGalaxyFluentValidation(typeof(BrandValidation).Assembly)
                     .InitializeGalaxy();
