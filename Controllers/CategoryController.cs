using ECommerceStore.Models;
using ECommerceStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        // Construtor: Injeta a dependência de CategoryService
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        // Ação para listar todas as categorias
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories); // Retorna a view Index com a lista de categorias
        }

        // GET: Category/Create
        // Ação para exibir o formulário de criação de uma nova categoria
        public IActionResult Create()
        {
            return View(); // Retorna a view Create
        }

        // POST: Category/Create
        // Ação para processar os dados do formulário e criar uma nova categoria
        [HttpPost]
        [ValidateAntiForgeryToken] // Proteção contra ataques CSRF
        public async Task<IActionResult> Create(Category category)
        {
            // Verifica se o modelo (Category) é válido com base nas validações definidas no modelo
            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(category); // Adiciona a categoria usando o serviço
                return RedirectToAction(nameof(Index)); // Redireciona para a página Index após o sucesso
            }
            // Se o modelo for inválido, retorna a mesma view com os dados preenchidos e erros de validação
            return View(category);
        }

        // GET: Category/Edit/5
        // Ação para exibir o formulário de edição de uma categoria existente
        public async Task<IActionResult> Edit(int? id) // 'id' é anulável para lidar com chamadas sem ID
        {
            if (id == null)
                return NotFound(); // Retorna 404 se nenhum ID for fornecido

            // Busca a categoria pelo ID usando o serviço
            var category = await _categoryService.GetByIdAsync(id.Value); // Usa .Value para acessar o int do nullable int
            if (category == null)
                return NotFound(); // Retorna 404 se a categoria não for encontrada

            return View(category); // Retorna a view Edit com os dados da categoria
        }

        // POST: Category/Edit/5
        // Ação para processar os dados do formulário e atualizar uma categoria existente
        [HttpPost]
        [ValidateAntiForgeryToken] // Proteção contra ataques CSRF
        public async Task<IActionResult> Edit(int id, Category category)
        {
            // Verifica se o ID da URL corresponde ao ID do objeto Category
            if (id != category.Id)
                return NotFound(); // Retorna 404 se houver inconsistência de IDs

            // Verifica se o modelo é válido
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateAsync(category); // Atualiza a categoria usando o serviço
                    return RedirectToAction(nameof(Index)); // Redireciona para a página Index após o sucesso
                }
                catch // Captura qualquer exceção durante a atualização (ex: erro de banco de dados)
                {
                    ModelState.AddModelError("", "Erro ao atualizar a categoria."); // Adiciona um erro ao ModelState
                }
            }
            // Se o modelo for inválido ou ocorrer um erro, retorna a mesma view com os dados preenchidos
            return View(category);
        }

        // GET: Category/Delete/5
        // Ação para exibir a página de confirmação de exclusão
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound(); // Retorna 404 se nenhum ID for fornecido

            // Busca a categoria pelo ID
            var category = await _categoryService.GetByIdAsync(id.Value);
            if (category == null)
                return NotFound(); // Retorna 404 se a categoria não for encontrada

            return View(category); // Retorna a view Delete com os dados da categoria para confirmação
        }

        // POST: Category/Delete/5
        // Ação para executar a exclusão da categoria após confirmação
        [HttpPost, ActionName("Delete")] // Mapeia para a URL "Delete" mas usa o nome de ação "DeleteConfirmed"
        [ValidateAntiForgeryToken] // Proteção contra ataques CSRF
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Opcional: buscar a categoria novamente para verificar sua existência antes de tentar excluir.
            // Isso evita um NotFound se ela foi removida por outra operação entre o GET e o POST.
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            await _categoryService.DeleteAsync(id); // Exclui a categoria usando o serviço
            return RedirectToAction(nameof(Index)); // Redireciona para a página Index após a exclusão
        }

        // GET: Category/Details/5
        // Ação para exibir os detalhes de uma categoria específica
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound(); // Retorna 404 se nenhum ID for fornecido

            // Busca a categoria pelo ID
            var category = await _categoryService.GetByIdAsync(id.Value);
            if (category == null)
                return NotFound(); // Retorna 404 se a categoria não for encontrada

            return View(category); // Retorna a view Details com os dados da categoria
        }
    }
}
