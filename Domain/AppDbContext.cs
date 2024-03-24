using ConcasPay.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcasPay.Domain;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta>()
                .Property(c => c.Agencia)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Conta>()
                .Property(c => c.Numero)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Conta>()
                .Property(c => c.Banco)
                .HasDefaultValue("ConcasBank");
        }
}