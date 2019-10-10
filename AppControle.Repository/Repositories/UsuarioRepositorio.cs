using AppControle.Domain.Contracts;
using AppControle.Domain.Entities;
using AppControle.Repository.Context;
using System.Linq;

namespace AppControle.Repository.Repositories
{
    public class UsuarioRepositorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly ControleContext _db;

        public UsuarioRepositorio(ControleContext db) : base(db)
        {
            _db = db;

        }
        public bool Autenticar(Usuario usuario)
        {
            return _db.Usuario.Where(x => x.Email == usuario.Email && x.Cpf == usuario.Cpf).FirstOrDefault() != null;
        }
    }
}
