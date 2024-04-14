using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QualiCadastro.Models;

namespace QualiCadastro.Services
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Email>().HasOne(x => x.Cadastro)
                .WithMany(x => x.Emails)
                .HasForeignKey(x => x.IdCadastro);
        }

        public DbSet<Cadastro> Cadastros { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}
