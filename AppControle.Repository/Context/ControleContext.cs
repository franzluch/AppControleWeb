using AppControle.Domain.Entities;
using AppControle.Repository.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppControle.Repository.Context
{
    public class ControleContext : DbContext
    {
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Venda> Venda { get; set; }

        public ControleContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new EstoqueConfiguration());

            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new VendaConfiguration());

            modelBuilder.Entity<Usuario>().HasData(new Usuario()
            {
                Id = 1,
                Email = "admin@controle.com",
                Cpf = "99999999999"
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
