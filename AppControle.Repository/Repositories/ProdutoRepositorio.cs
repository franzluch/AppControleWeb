using AppControle.Domain.Contracts;
using AppControle.Domain.Entities;
using AppControle.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppControle.Repository.Repositories
{
    public class ProdutoRepositorio : BaseRepositorio<Produto>, IProdutoRepositorio
    {
        private readonly ControleContext _db;
        public ProdutoRepositorio(ControleContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<Produto> ObterTodos2()
        {
            return _db.Produto
                .Include(c => c.Categoria)
                .Include(c => c.Estoque)
                .ToList();
        }
    }
}
