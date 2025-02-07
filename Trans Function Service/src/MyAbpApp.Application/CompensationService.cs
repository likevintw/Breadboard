// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Linq.Dynamic.Core;
// using System.Threading.Tasks;
// using Volo.Abp.Application.Dtos;
// using Volo.Abp.Domain.Repositories;
// using Volo.Abp.Domain.Entities;

// namespace MyAbpApp.Compensations
// {
//     public class CompensationService : MyAbpAppAppService, IProductAppService
//     {
//         private readonly IRepository<Product, Guid> _productRepository;

//         // 構造函數，注入 Product repository
//         public CompensationService(IRepository<Product, Guid> productRepository)
//         {
//             _productRepository = productRepository;
//         }


//         public async Task<ProductDto> GetAsync(Guid id)
//         {
//             var product = await _productRepository.GetAsync(id);
//             return ObjectMapper.Map<Product, ProductDto>(product);
//         }

//         public async Task<ProductDto> CreateAsync(CreateProductDto input)
//         {
//             var product = new Product
//             {
//                 Name = input.Name,
//                 Price = input.Price,
//                 IsFreeCargo = input.IsFreeCargo,
//                 ReleaseDate = input.ReleaseDate
//             };

//             await _productRepository.InsertAsync(product, autoSave: true);

//             return ObjectMapper.Map<Product, ProductDto>(product);
//         }

//         public async Task<ProductDto> UpdateAsync(UpdateProductDto input)
//         {
//             var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == input.Id);

//             if (product == null)
//             {
//                 throw new EntityNotFoundException(typeof(Product), input.Id);
//             }

//             product.Name = input.Name;
//             product.Price = input.Price;
//             product.IsFreeCargo = input.IsFreeCargo;
//             product.ReleaseDate = input.ReleaseDate;

//             await _productRepository.UpdateAsync(product);

//             // 將更新後的產品映射成 DTO 並返回
//             return ObjectMapper.Map<Product, ProductDto>(product);
//         }

//         public async Task<ProductDto> DeleteAsync(Guid Id)
//         {
//             var product = await _productRepository.GetAsync(Id);

//             // if (product == null)
//             // {
//             //     throw new EntityNotFoundException($"Product with Id {input.Id} not found.");
//             // }
//             await _productRepository.HardDeleteAsync(product);
//             // await _productRepository.DeleteAsync(product);
//             return ObjectMapper.Map<Product, ProductDto>(product);
//         }

//         public async Task RunIotDbDemo()
//         {
//             await Task.Delay(1);
//             Console.WriteLine("RunIotDbDemo");

//         }
//         public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
//         {

//             // 創建一個新的 ProductDto 物件
//             var product = new ProductDto()
//             {
//                 Name = "test GetListAsync product"
//             };
//             await Task.Delay(1);
//             // 將 ProductDto 放入列表中，並建立 PagedResultDto
//             var productList = new List<ProductDto> { product };

//             // 假設資料庫中只有 1 條產品，TotalCount 設為 1
//             var totalCount = 1;
//             Console.WriteLine("KEVIN using GetListAsync implement");
//             // 返回 PagedResultDto，包含產品列表和總數
//             return new PagedResultDto<ProductDto>
//             {
//                 TotalCount = totalCount,
//                 Items = productList
//             };
//         }
//     }
// }
