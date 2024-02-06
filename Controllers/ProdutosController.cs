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

    public async Task<IActionResult> Editar(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var product = await _context.Produtos.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(int id, [Bind("Id,Nome,Valor,Codigo,Estoque")] Produto produto)
    {
      if (id != produto.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(produto);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ProductExists(produto.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(produto);
    }

    public async Task<IActionResult> Excluir(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var product = await _context.Produtos
          .FirstOrDefaultAsync(m => m.Id == id);
      if (product == null)
      {
        return NotFound();
      }

      return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> ConfirmarExclusao(int id)
    {
      var product = await _context.Produtos.FindAsync(id);
      _context.Produtos.Remove(product);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
      return _context.Produtos.Any(e => e.Id == id);
    }
  }
}

