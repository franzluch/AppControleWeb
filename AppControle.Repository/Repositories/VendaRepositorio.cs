using AppControle.Domain.Contracts;
using AppControle.Domain.Entities;
using AppControle.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AppControle.Repository.Repositories
{
    public class VendaRepositorio : BaseRepositorio<Venda>, IVendaRepositorio
    {
        private readonly ControleContext _db;

        public VendaRepositorio(ControleContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<Venda> ObterTodos2()
        {
            return _db.Venda
                .Include(c => c.Produto)
                .ToList();
        }
    }
}
