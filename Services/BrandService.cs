using ECommerceStore.Data;
using ECommerceStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceStore.Services
{
    public class BrandService
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            // FindAsync é excelente para buscar por chave primária
            return await _context.Brands.FindAsync(id);
        }

        public async Task AddAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Brand brand)
        {
            var existingBrand = await _context.Brands.FindAsync(brand.Id);
            if (existingBrand == null)
                return false; // Retorna false se a marca não for encontrada para atualização

            // Atualiza as propriedades da marca existente com os novos valores
            // Esta é uma boa maneira de garantir que apenas as propriedades fornecidas no 'brand' sejam atualizadas
            _context.Entry(existingBrand).CurrentValues.SetValues(brand);
            await _context.SaveChangesAsync();
            return true; // Retorna true em caso de sucesso
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
            }
        }
    }
}
