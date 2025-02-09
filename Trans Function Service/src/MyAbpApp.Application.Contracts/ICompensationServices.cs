using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using MyAbpApp.CompensationDtos;
namespace MyAbpApp.ICompensationServices;
public interface ICompensationService : IApplicationService
{
    // 創建補償資料
    Task<CompensationDto> CreateAsync(CreateCompensationDto input);

    // 根據 ID 取得補償資料
    Task<CompensationDto> GetAsync(Guid id);

    // 取得所有補償資料
    Task<GetCompensationDto> GetListAsync();

    // 根據 ID 更新補償資料
    Task<CompensationDto> UpdateAsync(Guid id, CreateCompensationDto input);

    // 根據 ID 刪除補償資料
    Task DeleteAsync(Guid id);
}
