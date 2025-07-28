using ECommerceStore.Data; // Adicionar este using para o AppDbContext
using ECommerceStore.Models;
using Microsoft.EntityFrameworkCore; // Necessário para métodos de extensão do EF Core, como ToListAsync, FirstOrDefaultAsync, Include
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceStore.Services
{
    public class ProductService
    {
        // Alterado de List<Product> para AppDbContext
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context) // Injetar AppDbContext no construtor
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllActiveAsync()
        {
            // Usar _context.Products para consultar o banco de dados
            return await _context.Products
                                 .Include(p => p.Category) // Incluir Category para carregar dados relacionados
                                 .Include(p => p.Brand)    // Incluir Brand para carregar dados relacionados
                                 .Where(p => p.IsActive)
                                 .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            // Usar _context.Products para consultar o banco de dados
            return await _context.Products
                                 .Include(p => p.Category) // Incluir Category
                                 .Include(p => p.Brand)    // Incluir Brand
                                 .Include(p => p.Images)     // Incluir Imagens, se necessário
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            // Adicionar ao DbSet e salvar mudanças
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            // Atualizar no DbSet e salvar mudanças
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            // Encontrar o produto e remover
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
