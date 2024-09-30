
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abbas_Behjatnia.CTC.EFCore;

public class TaxExemptConfiguration : IEntityTypeConfiguration<TaxExempt>
{
    public void Configure(EntityTypeBuilder<TaxExempt> builder)
    {
        builder.Property(it => it.Title).HasColumnType("varchar(200)").IsRequired();
        builder.Property(it => it.Value).IsRequired();
        
        builder.Property(it => it.From).IsRequired(false);
        builder.Property(it => it.To).IsRequired(false);
        builder.Property(it => it.DayofWeek).IsRequired(false);

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
