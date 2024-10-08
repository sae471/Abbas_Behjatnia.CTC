

using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public interface IVehicleAppService : IBaseAppService<Vehicle, VehicleOutputDto, VehicleInputDto>
{
    Task<VehicleTrafficTaxListMOutputDto> VehicleTaxCalculation(Guid vehicleId, DateTime fromDate = default, DateTime toDate = default);

}