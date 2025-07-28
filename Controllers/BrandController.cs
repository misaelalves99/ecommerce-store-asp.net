using ECommerceStore.Models;
using ECommerceStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceStore.Controllers
{
    public class BrandController : Controller
    {
        private readonly BrandService _brandService;

        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }

        // ======================
        // LISTAR TODAS AS MARCAS
        // ======================
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            return View(brands);
        }

        // ======================
        // FORMULÁRIO DE CRIAÇÃO
        // ======================
        public IActionResult Create()
        {
            return View();
        }

        // ======================
        // SUBMISSÃO DO FORMULÁRIO DE CRIAÇÃO
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                await _brandService.AddAsync(brand);
                return RedirectToAction(nameof(Index));
            }

            return View(brand);
        }

        // ======================
        // FORMULÁRIO DE EDIÇÃO
        // ======================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var brand = await _brandService.GetByIdAsync(id.Value);
            if (brand == null)
                return NotFound();

            return View(brand);
        }

        // ======================
        // SUBMISSÃO DA EDIÇÃO
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _brandService.UpdateAsync(brand);
                if (!success) // Verifica se a atualização foi bem-sucedida (se a marca foi encontrada)
                    return NotFound(); // Retorna NotFound se a marca não foi encontrada para atualização

                return RedirectToAction(nameof(Index));
            }

            return View(brand);
        }

        // ======================
        // CONFIRMAR EXCLUSÃO (GET)
        // ======================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var brand = await _brandService.GetByIdAsync(id.Value);
            if (brand == null)
                return NotFound();

            return View(brand);
        }

        // ======================
        // EXCLUSÃO DEFINITIVA (POST)
        // ======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Opcional: verificar se a marca existe antes de tentar deletar
            // var brand = await _brandService.GetByIdAsync(id);
            // if (brand == null) return NotFound();

            await _brandService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ======================
        // DETALHES (GET)
        // ======================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var brand = await _brandService.GetByIdAsync(id.Value);
            if (brand == null)
                return NotFound();

            return View(brand);
        }
    }
}
