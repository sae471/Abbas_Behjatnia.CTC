

using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public interface ITollStationAppService : IBaseAppService<TollStation, TollStationOutputDto, TollStationInputDto>
{

}