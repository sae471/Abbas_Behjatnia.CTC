
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abbas_Behjatnia.CTC.EFCore;

public class TrafficConfiguration : IEntityTypeConfiguration<Traffic>
{
    public void Configure(EntityTypeBuilder<Traffic> builder)
    {
        builder.Property(it => it.DateTime).IsRequired();

        builder.
            HasOne(it => it.Vehicle)
            .WithMany()
            .HasForeignKey(it => it.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.
            HasOne(it => it.TollStation)
            .WithMany()
            .HasForeignKey(it => it.TollStationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
