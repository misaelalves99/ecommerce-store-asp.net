using System.Collections.Generic;

namespace ECommerceStore.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; } // navigation property anulável

        public int BrandId { get; set; }
        public Brand? Brand { get; set; } // navigation property anulável

        public bool IsActive { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
