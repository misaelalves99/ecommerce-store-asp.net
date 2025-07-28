using System.Collections.Generic;

namespace ECommerceStore.Models
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        // Propriedades adicionadas/confirmadas:
        public string? Description { get; set; }  // Descrição da marca, pode ser nula.

        public bool IsActive { get; set; } = true; // Indica se a marca está ativa para exibição.

        // Coleção de produtos associados a esta marca.
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
