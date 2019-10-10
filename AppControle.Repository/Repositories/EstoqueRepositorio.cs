using AppControle.Domain.Contracts;
using AppControle.Domain.Entities;
using AppControle.Repository.Context;

namespace AppControle.Repository.Repositories
{
    public class EstoqueRepositorio : BaseRepositorio<Estoque>, IEstoqueRepositorio
    {
        public EstoqueRepositorio(ControleContext db) : base(db)
        {
        }
    }
}
