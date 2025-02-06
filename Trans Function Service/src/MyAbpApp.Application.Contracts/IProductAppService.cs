using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
namespace MyAbpApp.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<PagedResultDto<ProductDto>>
        GetListAsync(PagedAndSortedResultRequestDto input);
        Task<ProductDto> GetAsync(Guid id);
        Task<ProductDto> CreateAsync(CreateProductDto input);
        Task<ProductDto> UpdateAsync(UpdateProductDto input);
        Task<ProductDto> DeleteAsync(Guid id);
        Task RunIotDbDemo();
    }
}