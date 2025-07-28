using ECommerceStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ECommerceStore.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (context.Brands.Any() || context.Categories.Any() || context.Products.Any())
                return;

            // Marcas
            var brands = new Brand[]
            {
                new Brand { Name = "Apple" },
                new Brand { Name = "Samsung" },
                new Brand { Name = "Sony" }
            };
            context.Brands.AddRange(brands);
            context.SaveChanges();

            // Categorias
            var categories = new Category[]
            {
                new Category { Name = "Smartphones" },
                new Category { Name = "Notebooks" },
                new Category { Name = "TVs" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Produtos
            var products = new Product[]
            {
                new Product { Name = "iPhone 15", Price = 7899.99m, BrandId = brands[0].Id, CategoryId = categories[0].Id },
                new Product { Name = "Samsung Galaxy S23", Price = 4599.50m, BrandId = brands[1].Id, CategoryId = categories[0].Id },
                new Product { Name = "Sony Bravia 4K", Price = 5299.00m, BrandId = brands[2].Id, CategoryId = categories[2].Id }
            };
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
