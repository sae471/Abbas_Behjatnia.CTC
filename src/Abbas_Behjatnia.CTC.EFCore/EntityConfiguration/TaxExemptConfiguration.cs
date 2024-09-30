
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abbas_Behjatnia.CTC.EFCore;

public class TaxExemptConfiguration : IEntityTypeConfiguration<TaxExempt>
{
    public void Configure(EntityTypeBuilder<TaxExempt> builder)
    {
        builder.Property(it => it.Title).HasColumnType("varchar(200)").IsRequired();
        builder.Property(it => it.Amount).IsRequired();
        builder.Property(it => it.Day).HasColumnType("smallint");
        builder.Property(it => it.Week).HasColumnType("tinyint");
        builder.Property(it => it.Month).HasColumnType("tinyint");
        builder.Property(it => it.Year).HasColumnType("smallint");

        builder.
            HasOne(it => it.CurrencyUnit)
            .WithMany()
            .HasForeignKey(it => it.CurrencyUnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.
            HasOne(it => it.Province)
            .WithMany()
            .HasForeignKey(it => it.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.
            HasOne(it => it.City)
            .WithMany()
            .HasForeignKey(it => it.CityId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.
            HasOne(it => it.TollStation)
            .WithMany()
            .HasForeignKey(it => it.TollStationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.
            HasOne(it => it.VehicleCategory)
            .WithMany()
            .HasForeignKey(it => it.VehicleCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
