using Microsoft.EntityFrameworkCore;
using TestCRUD.Entities;

namespace TestCRUD;

public class LibraryContext : DbContext
{
    private readonly IConfiguration _configuration;

    public LibraryContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("MerapiDatabase"));
    }

    public DbSet<Book> Books { get; set; }
}