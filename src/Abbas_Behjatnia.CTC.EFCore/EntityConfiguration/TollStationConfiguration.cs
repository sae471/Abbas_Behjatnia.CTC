
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abbas_Behjatnia.CTC.EFCore;

public class TollStationConfiguration : IEntityTypeConfiguration<TollStation>
{
    public void Configure(EntityTypeBuilder<TollStation> builder)
    {
        builder.Property(it => it.Title).HasColumnType("nvarchar(500)").IsRequired();

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
    }
}
