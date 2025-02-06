using System;
using System.Threading.Tasks;

using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace MyAbpApp.Products;

public class DataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Product, Guid> _productRepository;

    public DataSeederContributor(IRepository<Product, Guid> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _productRepository.GetCountAsync() <= 0)
        {
            await _productRepository.InsertAsync(
                new Product
                {
                    Name = "first text product",
                    Price = 12.3F,
                    IsFreeCargo = false,
                    ReleaseDate = new DateTime(2024, 9, 27)
                },
                autoSave: true
            );
            await _productRepository.InsertAsync(
                new Product
                {
                    Name = "second text product",
                    Price = 25.3F,
                    IsFreeCargo = false,
                    ReleaseDate = new DateTime(2024, 10, 27)
                },
                autoSave: true
            );
        }
    }
}
