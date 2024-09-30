

using System.ComponentModel.DataAnnotations;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class CountryDivisionManager : DomainService<CountryDivision>
{
    IRepository<CountryDivision> _countryDivisionRepository => LazyServiceProvider.LazyGetService<IRepository<CountryDivision>>();

    public CountryDivision New(CountryDivisionType type, string name)
    {
        var id = new Guid();
        CheckCountryDivisionType(type);
        CheckName(name);
        var countryDivision = new CountryDivision(id, type, name);
        return countryDivision;
    }

    public void CheckCountryDivisionType(CountryDivisionType type)
    {
        if (!Enum.IsDefined(typeof(CountryDivisionType), type))
        {
            throw new ValidationException($"Country Division Type is invalid!!");
        }
    }

    public void setCountryDivisionType(CountryDivision countryDivision, CountryDivisionType type)
    {
        CheckCountryDivisionType(type);
        countryDivision.Type = type;
    }

    public void CheckName(string name)
    {
        if (name is null || string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException($"The Country Division Name could not be null or empty!!");
        }
    }

    public void SetName(CountryDivision countryDivision, string name)
    {
        CheckName(name);
        countryDivision.Name = name;
    }

    public async Task SetParentAsync(CountryDivision countryDivision, Guid parentId)
    {
        if (parentId == default)
            return;

        var parent = await _countryDivisionRepository.FindAsync(parentId);
        if (parent == null)
        {
            throw new ValidationException($"The desired Country Division does not exist and could not be set as a Parent!!");
        }

        countryDivision.ParentId = parentId;
        countryDivision.Parent = parent;

    }
}
