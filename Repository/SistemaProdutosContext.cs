using Microsoft.EntityFrameworkCore;
using sistemaProdutos.Models;

namespace TrybeHotel.Repository;
public class SistemaProdutosContext : DbContext
{
  public DbSet<Produto> Cities { get; set; }

  public SistemaProdutosContext(DbContextOptions<SistemaProdutosContext> options) : base(options) { }
  public SistemaProdutosContext() { }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      var connectionString = "Server=127.0.0.1;Database=produtos_db;User=SA;Password=Password12!;TrustServerCertificate=True";
      optionsBuilder.UseSqlServer(connectionString);
    }
  }
}