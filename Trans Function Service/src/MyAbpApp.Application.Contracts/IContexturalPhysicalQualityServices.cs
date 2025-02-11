using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using MyAbpApp.ContexturalPhysicalQualityDtos;
namespace MyAbpApp.IContexturalPhysicalQualityServices;
public interface IContexturalPhysicalQualityService : IApplicationService
{
    Task<ContexturalPhysicalQualityDto> CreateAsync(CreateOrUpdateContexturalPhysicalQualityDto input);
    Task<ContexturalPhysicalQualityDto> GetAsync(Guid id);
    Task<ContexturalPhysicalQualityDto> UpdateAsync(Guid id, CreateOrUpdateContexturalPhysicalQualityDto input);
    Task DeleteAsync(Guid id);
}
