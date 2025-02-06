using AutoMapper;
using MyAbpApp.Products;
namespace MyAbpApp.ProductMapper
{
    public class ProductManagementApplicationAutoMapperProfile : Profile
    {
        public ProductManagementApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}