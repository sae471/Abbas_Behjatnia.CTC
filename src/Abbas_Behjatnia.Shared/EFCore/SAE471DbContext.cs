
using System.Reflection;
using Abbas_Behjatnia.Shared.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Abbas_Behjatnia.Shared.EFCore
{
    public class BaseDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(AppConfiguration.Configuration.GetConnectionString("Default"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var implementedConfigTypes =
                        assembly.GetTypes()
                        .Where(t => !t.IsAbstract
                                    && !t.IsGenericTypeDefinition
                                    && t.GetTypeInfo().ImplementedInterfaces.Any(i =>
                                        i.GetTypeInfo().IsGenericType &&
                                        i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
                    foreach (var configType in implementedConfigTypes)
                    {
                        dynamic? config = Activator.CreateInstance(configType);
                        builder.ApplyConfiguration(config);
                    }
                }
                catch (ReflectionTypeLoadException)
                {
                }
            }
        }
    }
}