using AppControle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppControle.Repository.Config
{
    public class VendaConfiguration : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.ProdutoId);
            builder.Property(u => u.Quantidade);
            builder.Property(u => u.DataVenda);

            //builder.HasMany(u => u.Produtos).WithOne(p => p.Venda);
        }
    }
}
