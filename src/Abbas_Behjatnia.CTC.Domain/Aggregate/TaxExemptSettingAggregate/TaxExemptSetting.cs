using System;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Domain.Entities;


namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class TaxExemptSetting : AggregateRoot<Guid>
{
    public virtual TaxExemptSettingType Type { get; internal set; }
    public virtual string Value { get; internal set; }

    protected TaxExemptSetting() { }
    public TaxExemptSetting(Guid id, TaxExemptSettingType type,string value) : base(id)
    {
        Id = id;
        Type = type;
        Value = value;
    }
}