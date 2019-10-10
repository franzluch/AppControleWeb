using AppControle.Domain.Entities;

namespace AppControle.Domain.Contracts
{
    public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
    {
        bool Autenticar(Usuario usuario);
    }
}
