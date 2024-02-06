using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaProdutos.Models;
using sistemaProdutos.Repository;

namespace sistemaProdutos.Controllers
{
  public class ProdutosController : Controller
  {
    private readonly SistemaProdutosContext _context;

    public ProdutosController(SistemaProdutosContext context)
    {
      _context = context;
    }

    public async Task<IActionResult> Index()
    {
      return View(await _context.Produtos.ToListAsync());
    }

    public IActionResult Create()
    {
      return View();
    }

    public async Task<IActionResult> Cadastrar([Bind("Id,Nome,Valor,Codigo,Estoque")] Produto produto)
    {
      if (ModelState.IsValid)
      {
        _context.Add(produto);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(produto);
    }
  }
}

