


using System.ComponentModel.DataAnnotations;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class CurrencyUnitManager : DomainService<CurrencyUnit>
{
    IRepository<CurrencyUnit> _currencyUnitRepository => LazyServiceProvider.LazyGetService<IRepository<CurrencyUnit>>();

    public CurrencyUnit New(string name, int decimalٔNumber)
    {
        var id = new Guid();
        CheckName(name);
        CheckDecimalNumber(decimalٔNumber);
        var currencyUnit = new CurrencyUnit(id, name, decimalٔNumber);
        return currencyUnit;
    }

    public void CheckName(string name)
    {
        if (name is null || string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException($"The Currency Unit Name could not be null or empty!!");
        }
    }

    public void SetName(CurrencyUnit currencyUnit, string name)
    {
        CheckName(name);
        currencyUnit.Name = name;
    }

    public void CheckDecimalNumber(int decimalٔNumber)
    {
        if (decimalٔNumber < 0)
        {
            throw new ValidationException($"The Decimal Number is invalid!!");
        }
    }

    public void SetDecimalNumber(CurrencyUnit currencyUnit, int decimalNumber)
    {
        CheckDecimalNumber(decimalNumber);
        currencyUnit.DecimalNumber = decimalNumber;
    }
}
