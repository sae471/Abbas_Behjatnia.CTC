
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abbas_Behjatnia.CTC.EFCore;

public class TaxExemptSettingConfiguration : IEntityTypeConfiguration<TaxExemptSetting>
{
    public void Configure(EntityTypeBuilder<TaxExemptSetting> builder)
    {
        builder.Property(it => it.Type).IsRequired();
        builder.Property(it => it.Value).HasColumnType("varchar(200)").IsRequired();
    }
}
