using Microsoft.EntityFrameworkCore;
using Teste_Playmove_API.Entities;

namespace Teste_Playmove_API.Persistence
{
    public class FornecedorDbContext : DbContext
    {
        public FornecedorDbContext(DbContextOptions<FornecedorDbContext> options) : base(options)
        {
            
        }

        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fornecedor>(e =>
            {
                e.HasKey(f => f.Id);
                e.Property(f => f.Nome)
                .IsRequired()
                .HasColumnType("varchar(300)");

                e.Property(f => f.Email)
                .HasColumnType("varchar(300)");
            });
        }
    }
}
