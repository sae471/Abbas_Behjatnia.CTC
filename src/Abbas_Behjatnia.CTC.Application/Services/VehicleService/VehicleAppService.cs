
using System.ComponentModel.DataAnnotations;
using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Abbas_Behjatnia.CTC.Application.Services;
public class VehicleAppService : BaseAppService<Vehicle, VehicleOutputDto, VehicleInputDto>, IVehicleAppService
{
    private VehicleManager _vehicleManager => (VehicleManager)LazyServiceProvider.LazyGetService<IDomainService<Vehicle>>();
    IRepository<Traffic> _trafficRepository => LazyServiceProvider.LazyGetService<IRepository<Traffic>>();
    IRepository<TollStation> _tollStationRepository => LazyServiceProvider.LazyGetService<IRepository<TollStation>>();
    IRepository<TaxExempt> _taxExemptRepository => LazyServiceProvider.LazyGetService<IRepository<TaxExempt>>();
    ITaxExemptSettingAppService _taxExemptSettingService => LazyServiceProvider.LazyGetService<ITaxExemptSettingAppService>();
    public override async Task<VehicleOutputDto> UpsertAsync(VehicleInputDto input)
    {
        var isNew = false;
        var vehicle = await Repository.FindAsync(input.Id);
        if (vehicle == null)
        {
            isNew = true;
            vehicle = await _vehicleManager.NewAsync(input.Type, input.OwnerShipType, input.PlateNumber, input.ChassisNumber);
        }
        vehicle.ManufacturerCompany = input.ManufacturerCompany ?? string.Empty;
        vehicle.ManufacturerClass = input.ManufacturerClass ?? string.Empty;
        vehicle.YearofManufacture = input.YearofManufacture;
        vehicle.Color = input.Color ?? string.Empty;
        await _vehicleManager.SetVehicleCategoryAsync(vehicle, input.VehicleCategoryId ?? default);

        if (!isNew)
        {
            _vehicleManager.SetVehicleType(vehicle, input.Type);
            _vehicleManager.SetVehicleOwnerShip(vehicle, input.OwnerShipType);
            await _vehicleManager.SetPlateAndChassisNumberAsync(vehicle, input.ChassisNumber, input.PlateNumber);
            return Mapper.Map<Vehicle, VehicleOutputDto>(await Repository.UpdateAsync(vehicle));
        }
        return Mapper.Map<Vehicle, VehicleOutputDto>(await Repository.InsertAsync(vehicle));
    }

    public async Task<VehicleTrafficTaxListMOutputDto> VehicleTaxCalculation(Guid vehicleId, DateTime fromDate = default, DateTime toDate = default)
    {
        if (vehicleId == default)
        {
            throw new ValidationException($"The Vehicle could not be null or empty!!");
        }
        var vehicle = await Repository.FindAsync(vehicleId);
        if (vehicle == null)
        {
            throw new ValidationException($"The desired Vehicle does not exist!!");
        }
        var VehicleTrafficList = await
            (
                from traffic in _trafficRepository.Set()
                join tollStation in _tollStationRepository.Set() on traffic.TollStationId equals tollStation.Id
                where traffic.VehicleId == vehicleId
                 && (fromDate == default || traffic.DateTime >= fromDate)
                 && (toDate == default || traffic.DateTime <= toDate)
                orderby traffic.DateTime
                select new VehicleTrafficTaxMOutputDto
                {
                    Id = traffic.Id,
                    DateTime = traffic.DateTime,
                    TollStationId = traffic.TollStationId,
                    TollStationTitle = tollStation.Title,
                    ProvinceId = tollStation.ProvinceId ?? default,
                    CityId = tollStation.CityId ?? default,
                    TotalTax = 0,
                    Exempt = 0,
                    SurplusTax = 0,
                    AppliedTax = 0
                }
            ).ToListAsync();

        var tollStationIdList = VehicleTrafficList.Select(it => it.TollStationId).Distinct().ToList();
        var provinceIdList = VehicleTrafficList.Select(it => it.ProvinceId).Distinct().ToList();
        var cityIdList = VehicleTrafficList.Select(it => it.CityId).Distinct().ToList();

        var taxExempt =
            await _taxExemptRepository.GetQueryableAsync().Result
            .Where(
                it =>
                (it.VehicleCategoryId == default || it.VehicleCategoryId == vehicle.VehicleCategoryId)
                && (it.VehicleType == default || it.VehicleType == vehicle.Type)
                && (it.TollStationId == default || tollStationIdList.Contains(it.TollStationId ?? default))
                && (it.ProvinceId == default || provinceIdList.Contains(it.ProvinceId ?? default))
                && (it.CityId == default || cityIdList.Contains(it.CityId ?? default))
        ).ToListAsync();

        VehicleTrafficList.ForEach(traffic =>
        {
        var vehicleTrafficTaxExemptList = taxExempt.Where(
            it =>
            (it.FromTime == default || it.FromTime <= traffic.DateTime.TimeOfDay)
            && (it.ToTime == default || it.ToTime >= traffic.DateTime.TimeOfDay)
            && (it.FromDate.Date == default || it.FromDate.Date <= traffic.DateTime.Date)
            && (it.ToDate.Date == default || it.ToDate.Date >= traffic.DateTime.Date)
            && (it.DayofWeek == default || (int)it.DayofWeek == (int)traffic.DateTime.DayOfWeek + 1)
            && (it.Month == default || it.Month == traffic.DateTime.Month)
            && (it.Year == default || it.Year == traffic.DateTime.Year)
            && (it.TollStationId == default || it.TollStationId == traffic.TollStationId)
            && (it.ProvinceId == default || it.ProvinceId == traffic.ProvinceId)
            && (it.CityId == default || it.CityId == traffic.CityId)
            && (it.VehicleCategoryId == default || it.VehicleCategoryId == vehicle.VehicleCategoryId)
            && (it.VehicleType == default || it.VehicleType == vehicle.Type))
                .ToList();

        traffic.TotalTax = vehicleTrafficTaxExemptList.Where(it => !it.IsExempt).Sum(it => it.Amount);
        var percentageExemption = vehicleTrafficTaxExemptList.Where(it => it.IsExempt && it.AmountIsPercentage).Sum(it => it.Amount);
        traffic.Exempt =
            vehicleTrafficTaxExemptList.Where(it => it.IsExempt && !it.AmountIsPercentage).Sum(it => it.Amount)
            + (percentageExemption * traffic.TotalTax / 100);

        traffic.AppliedTax = traffic.TotalTax - traffic.Exempt;
        traffic.AppliedTax = traffic.AppliedTax < 0 ? 0 : traffic.AppliedTax;
        traffic.TaxExemptList = Mapper.Map<List<TaxExempt>, List<TaxExemptOutputDto>>(vehicleTrafficTaxExemptList);
    });

        await _taxExemptSettingService.NormalizationNumberOfAppliedTrafficInMaximumValuePerHoure(VehicleTrafficList);
    await _taxExemptSettingService.NormalizationByMaximumTaxAmountPerDay(VehicleTrafficList);

        return new VehicleTrafficTaxListMOutputDto
        {
            TotalTax = VehicleTrafficList.Sum(it => it.TotalTax),
            Exempt = VehicleTrafficList.Sum(it => it.Exempt),
            SurplusTax = VehicleTrafficList.Sum(it => it.SurplusTax),
            AppliedTax = VehicleTrafficList.Sum(it => it.AppliedTax),
            VehicleTrafficTaxMOutputDto = VehicleTrafficList
};
    }
}