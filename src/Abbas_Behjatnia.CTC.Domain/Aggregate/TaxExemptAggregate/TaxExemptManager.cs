using System;
using System.ComponentModel.DataAnnotations;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class TaxExemptManager : DomainService<TaxExempt>
{
    IRepository<CountryDivision> _countryDivisionRepository => LazyServiceProvider.LazyGetService<IRepository<CountryDivision>>();
    IRepository<TollStation> _tollStationRepository => LazyServiceProvider.LazyGetService<IRepository<TollStation>>();
    IRepository<VehicleCategory> _vehicleCategoryRepository => LazyServiceProvider.LazyGetService<IRepository<VehicleCategory>>();
    IRepository<CurrencyUnit> _currencyUnitRepository => LazyServiceProvider.LazyGetService<IRepository<CurrencyUnit>>();

    public TaxExempt New(string title, decimal amount, bool IsExempt, bool amountIsPercentage)
    {
        var id = new Guid();
        CheckTitle(title);
        CheckAmount(amount);
        var taxExempt = new TaxExempt(id, title, amount);
        taxExempt.IsExempt = IsExempt;
        taxExempt.AmountIsPercentage = amountIsPercentage;
        return taxExempt;
    }
    
    public void CheckTitle(string title)
    {
        if (title is null || string.IsNullOrWhiteSpace(title))
        {
            throw new ValidationException($"The Title could not be null or empty!!");
        }
    }

    public void SetTitle(TaxExempt taxExempt, string title)
    {
        CheckTitle(title);
        taxExempt.Title = title;
    }

    public void CheckAmount(decimal amount)
    {
        if (amount == default)
        {
            throw new ValidationException($"The Value could not be null or empty!!");
        }
    }

    public void SetAmount(TaxExempt taxExempt, decimal amount)
    {
        CheckAmount(amount);
        taxExempt.Amount = amount;
    }

    public void SetVehicleType(TaxExempt taxExempt, VehicleType vehicleType)
    {
        if (vehicleType == default)
            return;
        if (!Enum.IsDefined(typeof(VehicleType), vehicleType))
        {
            throw new ValidationException($"Vehicle Type is invalid!!");
        }
        taxExempt.VehicleType = vehicleType;
    }

    public void SetDayofWeek(TaxExempt taxExempt, Domain.Shared.DayOfWeek dayofWeek)
    {
        if (dayofWeek == default)
            return;
        if (!Enum.IsDefined(typeof(Domain.Shared.DayOfWeek), dayofWeek))
        {
            throw new ValidationException($"Day of Week is invalid!!");
        }
        taxExempt.DayofWeek = dayofWeek;
    }

    public async Task SetProvinceAsync(TaxExempt taxExempt, Guid provinceId)
    {
        if (provinceId == default)
        {
            return;
        }
        var province = await _countryDivisionRepository.FindAsync(it => it.Id == provinceId && it.Type == CountryDivisionType.Province);
        if (province == null)
        {
            throw new ValidationException($"The desired Province does not exist!!");
        }

        taxExempt.ProvinceId = provinceId;
        taxExempt.Province = province;
    }

    public async Task SetCityAsync(TaxExempt taxExempt, Guid cityId)
    {
        if (cityId == default)
        {
            return;
        }
        var city = await _countryDivisionRepository.FindAsync(it => it.Id == cityId && it.Type == CountryDivisionType.City);
        if (city == null)
        {
            throw new ValidationException($"The desired City does not exist!!");
        }
        taxExempt.CityId = cityId;
        taxExempt.City = city;
    }

    public async Task SetTollStationAsync(TaxExempt taxExempt, Guid tollStationId)
    {
        if (tollStationId == default)
        {
            return;
        }
        var tollStation = await _tollStationRepository.FindAsync(it => it.Id == tollStationId);
        if (tollStation == null)
        {
            throw new ValidationException($"The desired Toll Station does not exist!!");
        }

        taxExempt.TollStationId = tollStationId;
        taxExempt.TollStation = tollStation;
    }

    public async Task SetVehicleCategoryAsync(TaxExempt taxExempt, Guid vehicleCategoryId)
    {
        if (vehicleCategoryId == default)
        {
            return;
        }

        var vehicleCategory = await _vehicleCategoryRepository.FindAsync(vehicleCategoryId);
        if (vehicleCategory == null)
        {
            throw new ValidationException($"The desired Vehicle Category does not exist!!");
        }

        taxExempt.VehicleCategoryId = vehicleCategoryId;
        taxExempt.VehicleCategory = vehicleCategory;

    }
}
