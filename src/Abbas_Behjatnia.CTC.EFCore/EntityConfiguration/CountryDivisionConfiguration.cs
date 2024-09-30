
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abbas_Behjatnia.CTC.EFCore;

public class CountryDivisionConfiguration : IEntityTypeConfiguration<CountryDivision>
{
    public void Configure(EntityTypeBuilder<CountryDivision> builder)
    {
        builder.Property(it => it.Type).IsRequired();
        builder.Property(it => it.Name).HasColumnType("nvarchar(150)").IsRequired();

        builder.
            HasOne(it => it.Parent)
            .WithMany()
            .HasForeignKey(it => it.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
