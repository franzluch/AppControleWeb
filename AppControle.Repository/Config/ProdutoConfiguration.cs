using AppControle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppControle.Repository.Config
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.CategoriaId);
            builder.Property(u => u.Descricao);

            builder.Ignore(u => u.Quantidade);
            //builder.HasMany(u => u.Categoria);
            builder.HasOne(u => u.Categoria);//.WithOne(p => p.Produto);
            //builder.HasMany(u => u.Estoque).WithOne(p => p.Produto);
            //builder.HasMany(u => u.Estoque);
        }
    }
}
