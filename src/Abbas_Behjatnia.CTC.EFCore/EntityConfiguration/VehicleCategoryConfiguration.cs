
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abbas_Behjatnia.CTC.EFCore;

public class VehicleCategoryConfiguration : IEntityTypeConfiguration<VehicleCategory>
{
    public void Configure(EntityTypeBuilder<VehicleCategory> builder)
    {
        builder.Property(it => it.Name).HasColumnType("nvarchar(200)").IsRequired();

        builder
        .HasOne(it => it.Parent)
        .WithMany()
        .HasForeignKey(it => it.ParentId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
