
using System.ComponentModel.DataAnnotations;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class TaxExemptSettingManager : DomainService<TaxExemptSetting>
{
    IRepository<TaxExemptSetting> _taxExemptSettingRepository => LazyServiceProvider.LazyGetService<IRepository<TaxExemptSetting>>();

    public TaxExemptSetting New(TaxExemptSettingType type, string value)
    {
        var id = new Guid();
        CheckTaxExemptSettingType(type);
        ChecValue(value);
        var taxExemptSetting = new TaxExemptSetting(id, type, value);
        return taxExemptSetting;
    }

    public void CheckTaxExemptSettingType(TaxExemptSettingType type)
    {
        if (!Enum.IsDefined(typeof(TaxExemptSettingType), type))
        {
            throw new ValidationException($"The Tax Exempt Setting Type is invalid!!");
        }
    }

    public void SetTaxExemptSettingType(TaxExemptSetting taxExemptSetting, TaxExemptSettingType type)
    {
        CheckTaxExemptSettingType(type);
        taxExemptSetting.Type = type;
    }

    public void ChecValue(string value)
    {
        if (value is null || string.IsNullOrWhiteSpace(value))
        {
            throw new ValidationException($"The Value of setting could not be null or empty!!");
        }
    }

    public void SetValue(TaxExemptSetting taxExemptSetting, string value)
    {
        ChecValue(value);
        taxExemptSetting.Value = value;
    }
}
