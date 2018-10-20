using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Galaxy.EFCore.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurationsFromCurrentAssembly(this ModelBuilder builder, Assembly assemb)
        {
            var applyGenericMethods = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            var applyGenericApplyConfigurationMethods = applyGenericMethods.Where(m => m.IsGenericMethod && m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));
            var applyGenericMethod = applyGenericApplyConfigurationMethods.Where(m => m.GetParameters().FirstOrDefault().ParameterType.Name == "IEntityTypeConfiguration`1").FirstOrDefault();

        
                var configurations = assemb.DefinedTypes.Where(t =>
                  t.ImplementedInterfaces.Any(i =>
                     i.IsGenericType &&
                     i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name,
                            StringComparison.InvariantCultureIgnoreCase)
                   ) &&
                   t.IsClass &&
                   !t.IsAbstract &&
                   !t.IsNested)
                   .ToList();

                foreach (var configuration in configurations)
                {
                    try
                    {
                        var entityType = configuration.BaseType.GenericTypeArguments.SingleOrDefault(t => t.IsClass);


                        var applyConfigGenericMethod = applyGenericMethod.MakeGenericMethod(entityType);

                        applyConfigGenericMethod.Invoke(builder,
                                new object[] { Activator.CreateInstance(configuration) });
                    }
                    catch (Exception ex) { }
                }
           
        }
        public static void ApplyAllConfigurationsFromCurrentAssembly(this ModelBuilder builder)
        {
            var assemblyList = GetAllAssembliesFromBase();

            var applyGenericMethods = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            var applyGenericApplyConfigurationMethods = applyGenericMethods.Where(m => m.IsGenericMethod && m.Name.Equals(nameof(ModelBuilder.ApplyConfiguration), StringComparison.OrdinalIgnoreCase));
            var applyGenericMethod = applyGenericApplyConfigurationMethods.Where(m => m.GetParameters().FirstOrDefault().ParameterType.Name == "IEntityTypeConfiguration`1").FirstOrDefault();

            assemblyList.ToList().ForEach(assemb => {
                var configurations = assemb.DefinedTypes.Where(t =>
                  t.ImplementedInterfaces.Any(i =>
                     i.IsGenericType &&
                     i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name,
                            StringComparison.InvariantCultureIgnoreCase)
                   ) &&
                   t.IsClass &&
                   !t.IsAbstract &&
                   !t.IsNested)
                   .ToList();

                foreach (var configuration in configurations)
                {
                    Type entityType = null;
                    try
                    {
                        if (!configuration.BaseType.GenericTypeArguments.Any())
                        {
                            entityType = configuration.ImplementedInterfaces.SingleOrDefault()
                                                .GenericTypeArguments.SingleOrDefault();
                        }
                        else
                        {
                            entityType = configuration.BaseType.GenericTypeArguments.SingleOrDefault(t => t.IsClass);
                        } 

                        var applyConfigGenericMethod = applyGenericMethod.MakeGenericMethod(entityType);

                        applyConfigGenericMethod.Invoke(builder,
                                new object[] { Activator.CreateInstance(configuration) });
                    }
                    catch {
                    }
                }
            });
        }

        private static Assembly[] GetAllAssembliesFromBase()
        {
            List<Assembly> allAssemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (string dll in Directory.GetFiles(path, "*.dll"))
                try
                {
                    allAssemblies.Add(Assembly.LoadFile(dll));
                }
                catch (Exception ex)
                {
                }

            return allAssemblies.ToArray();
        }
    }
}
