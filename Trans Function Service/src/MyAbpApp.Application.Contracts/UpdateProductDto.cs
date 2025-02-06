
using System;
using Volo.Abp.Application.Dtos;
namespace MyAbpApp.Products
{
    public class UpdateProductDto : AuditedEntityDto<Guid>
    {
        public required new Guid Id { get; set; }
        public required string Name { get; set; }
        public float Price { get; set; }
        public bool IsFreeCargo { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}