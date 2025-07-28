using System.Collections.Generic;

namespace ECommerceStore.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Propriedades para hierarquia de categorias:
        public int? ParentCategoryId { get; set; } // Chave estrangeira para a categoria pai, pode ser nula para categorias raiz.
        public Category? ParentCategory { get; set; } // Propriedade de navegação para a categoria pai.

        public ICollection<Category> Subcategories { get; set; } = new List<Category>(); // Coleção de subcategorias.
        public ICollection<Product> Products { get; set; } = new List<Product>(); // Coleção de produtos nesta categoria.

        // Propriedade para controlar status ativo/inativo
        public bool IsActive { get; set; } = true; // Indica se a categoria está ativa.
    }
}
