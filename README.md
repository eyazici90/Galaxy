# Galaxy
Next generation framework for Domain Driven Design needs

# Usage 

 GalaxyMainBootsrapper.Create()<br/>
                 &nbsp;.RegisterContainerBuilder()<br/>
                     &nbsp;.UseGalaxyCore()<br/>
                     &nbsp;.UseGalaxyEntityFrameworkCore(<br/>
                              &nbsp;&nbsp;&nbsp;  new DbContextOptionsBuilder<YourDbContext>()<br/>
                                   &nbsp;&nbsp;&nbsp;&nbsp;  .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))<br/>
                    &nbsp;.UseGalaxyMapster()<br/>
                    &nbsp;.UseGalaxyFluentValidation(typeof(BrandValidation).Assembly)<br/>
                    &nbsp;.InitializeGalaxy();<br/>
