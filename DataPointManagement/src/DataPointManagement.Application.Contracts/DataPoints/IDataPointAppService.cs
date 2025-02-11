
// TODO: Application.Contracts/DataPoints/IDataPointAppService

using System;
using System.Threading.Tasks;
/// 
/// Remove Volo.Abp.Application.Dtos since they inherent FullAuditedAggregateRoot that might not be proper to extend
/// Use PagedResultDataPointListDto.cs for now
/// Archer 2025/2/4
/// 
//using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DataPointManagement.DataPoints;

public interface IDataPointAppService : IApplicationService
{
    Task<PagedResultDataPointListDto> GetPagedListAsync(PagedResultDataPointListRequestDto input);
    Task<DataPointDto> GetLastAsync(LastDataPointRequestDto input);
    Task CreateAsync(CreateUpdateDataPointDto input);
    Task UpdateAsync(CreateUpdateDataPointDto input);
    //Task DeleteAsync(DeleteDataPointDto input);
}
