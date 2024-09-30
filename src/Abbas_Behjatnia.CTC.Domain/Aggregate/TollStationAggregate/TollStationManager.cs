

using System.ComponentModel.DataAnnotations;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class TollStationManager : DomainService<TollStation>
{
    IRepository<CountryDivision> _countryDivisionRepository => LazyServiceProvider.LazyGetService<IRepository<CountryDivision>>();

    public async Task<TollStation> NewAsync(string name, Guid provinceId, Guid cityId)
    {
        var id = new Guid();
        CheckTitle(name);
        await CheckProvinceAsync(provinceId);
        await CheckCityAsync(cityId);
        var tollStation = new TollStation(id, name, provinceId, cityId);
        return tollStation;
    }

    public void CheckTitle(string title)
    {
        if (title is null || string.IsNullOrWhiteSpace(title))
        {
            throw new ValidationException($"The Title could not be null or empty!!");
        }
    }

    public void SetTitle(TollStation tollStation, string title)
    {
        CheckTitle(title);
        tollStation.Title = title;
    }

    public async Task<CountryDivision> CheckProvinceAsync(Guid provinceId)
    {
        if (provinceId == default)
        {
            throw new ValidationException($"The Province could not be null or empty!!");
        }
        var province = await _countryDivisionRepository.FindAsync(it => it.Id == provinceId && it.Type == CountryDivisionType.Province);
        if (province == null)
        {
            throw new ValidationException($"The desired Province does not exist!!");
        }

        return province;
    }
    public async Task SetProvineAsync(TollStation tollStation, Guid provinceId)
    {
        var province = await CheckProvinceAsync(provinceId);

        tollStation.ProvinceId = provinceId;
        tollStation.Province = province;
    }

    public async Task<CountryDivision> CheckCityAsync(Guid cityId)
    {
        if (cityId == default)
        {
            throw new ValidationException($"The City could not be null or empty!!");
        }
        var city = await _countryDivisionRepository.FindAsync(it => it.Id == cityId && it.Type == CountryDivisionType.City);
        if (city == null)
        {
            throw new ValidationException($"The desired City does not exist!!");
        }

        return city;
    }
    public async Task SetCityAsync(TollStation tollStation, Guid cityId)
    {
        var city = await CheckCityAsync(cityId);

        tollStation.CityId = cityId;
        tollStation.City = city;
    }
}
