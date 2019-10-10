using AppControle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppControle.Repository.Config
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome);

            //builder.Ignore(u => u.Produto);
        }
    }
}
