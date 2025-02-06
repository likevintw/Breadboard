
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyAbpApp.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public required string Name { get; set; }
        public float Price { get; set; }
        public bool IsFreeCargo { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
