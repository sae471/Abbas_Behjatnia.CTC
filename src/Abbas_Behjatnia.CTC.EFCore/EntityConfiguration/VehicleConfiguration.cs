
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abbas_Behjatnia.CTC.EFCore;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.Property(it => it.Type).IsRequired();
        builder.Property(it => it.OwnerShipType).IsRequired();
        builder.Property(it => it.PlateNumber).HasColumnType("varchar(20)").IsRequired();
        builder.Property(it => it.ChassisNumber).HasColumnType("varchar(200)").IsRequired();

        builder.
            HasOne(it => it.VehicleCategory)
            .WithMany()
            .HasForeignKey(it => it.VehicleCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
