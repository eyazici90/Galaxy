# Galaxy
Next generation framework for Domain Driven Design needs

# Usage 

 GalaxyMainBootsrapper.Create()<br/>
                 &nbsp;&nbsp;.RegisterContainerBuilder()<br/>
                     &nbsp;&nbsp;.UseGalaxyCore()<br/>
                     &nbsp;&nbsp;.UseGalaxyEntityFrameworkCore(<br/>
                              &nbsp;&nbsp;&nbsp;&nbsp;  new DbContextOptionsBuilder<YourDbContext>()<br/>
                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))<br/>
                    &nbsp;&nbsp;.UseGalaxyMapster()<br/>
                    &nbsp;&nbsp;.UseGalaxyFluentValidation(typeof(BrandValidation).Assembly)<br/>
                    &nbsp;&nbsp;.InitializeGalaxy();<br/>
