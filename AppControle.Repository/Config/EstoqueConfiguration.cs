using AppControle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppControle.Repository.Config
{
    public class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.ProdutoId);
            builder.Property(u => u.Quantidade);

            builder.HasOne(u => u.Produto);
        }
    }
}
