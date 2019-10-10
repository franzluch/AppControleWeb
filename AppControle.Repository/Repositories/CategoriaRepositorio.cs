using AppControle.Domain.Contracts;
using AppControle.Domain.Entities;
using AppControle.Repository.Context;

namespace AppControle.Repository.Repositories
{
    public class CategoriaRepositorio : BaseRepositorio<Categoria>, ICategoriaRepositorio
    {
        public CategoriaRepositorio(ControleContext db) : base(db)
        {
        }
    }
}
