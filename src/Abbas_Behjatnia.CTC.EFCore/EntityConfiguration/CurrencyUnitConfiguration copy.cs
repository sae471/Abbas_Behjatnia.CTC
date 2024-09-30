
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abbas_Behjatnia.CTC.EFCore;

public class CurrencyUnitConfiguration : IEntityTypeConfiguration<CurrencyUnit>
{
    public void Configure(EntityTypeBuilder<CurrencyUnit> builder)
    {
        builder.Property(it => it.Name).HasColumnType("nvarchar(150)").IsRequired();
        builder.Property(it => it.DecimalNumber).IsRequired();
    }
}
