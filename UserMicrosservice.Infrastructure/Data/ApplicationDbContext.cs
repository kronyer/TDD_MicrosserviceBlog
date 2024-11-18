
using Microsoft.EntityFrameworkCore;
using UserMicrosservice.Domain;

namespace UserMicrosservice.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configurações adicionais de mapeamento podem ser feitas aqui
    }
}
