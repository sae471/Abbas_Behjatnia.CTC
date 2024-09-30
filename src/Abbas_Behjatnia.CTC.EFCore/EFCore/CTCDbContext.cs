
using System.Reflection;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Abbas_Behjatnia.CTC.EFCore;

public class CTCDbContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleCategory> VehicleCategories { get; set; }
    public DbSet<CountryDivision> CountryDivisions { get; set; }
    public DbSet<TaxExempt> TaxExempts { get; set; }
    public DbSet<TollStation> TollStations { get; set; }
    public DbSet<Traffic> Traffic { get; set; }
    public DbSet<CurrencyUnit> CurrencyUnits { get; set; }

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