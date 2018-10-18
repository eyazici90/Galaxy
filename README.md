## Galaxy
Next generation framework for Domain Driven Design needs. .Net Core 2.x support !

## IoC
Autofac 

## Adopted principles
SOLID
Domain Driven Design

## Persistance
EntityFramework Core
Dapper
NHibernate

## Event Bus
RabbitMQ support

## Document Databases
MongoDB

## Object Mappers
Mapster
AutoMapper

## Cache
Redis
In-Memory

## Object Validation
FluentValidation

## Log
Serilog support

## Modules
Galaxy.Identity

## Advantages

 1. Conventinal Dependency Registering
 2. Async await first 
 3. Multi Tenancy
 4. GlobalQuery Filtering
 5. Domain Driven Design Concepts
 6. Repository and UnitofWork pattern implementations
 7. Object to object mapping with abstraction
 8. Auto Audit Trailing
 9. Domain Events Pattern implementation with MediatR
 10. Net Core 2.x support
 11. Auto object validation support
 12. Aspect Oriented Programming
 13. Simple Usage
 
   
## Basic Usage

     GalaxyCoreModule.Create()
                 .RegisterContainerBuilder()
                     .UseGalaxyCore()
                     .UseGalaxyEntityFrameworkCore(
                                new DbContextOptionsBuilder<YourDbContext>()
    				 .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                         .UseGalaxyMapster()
                         .UseGalaxyFluentValidation();
