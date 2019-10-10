using AppControle.Domain.Contracts;
using AppControle.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AppControle.Repository.Repositories
{
    public class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : class
    {
        protected readonly ControleContext _db;
        public BaseRepositorio(ControleContext db)
        {
            _db = db;
        }
        public void Adicionar(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
            _db.SaveChanges();
        }

        public void Atualizar(TEntity entity)
        {
            //_db.Set<TEntity>().Update(entity);
            
            _db.Set<TEntity>().Attach(entity);
            _db.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _db.SaveChanges();
        }

        public TEntity ObterPorId(int id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> ObterTodos()
        {
            return _db.Set<TEntity>().AsNoTracking().ToList();
        }

        public void Remover(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
