using AppControle.Domain.Entities;
using System.Collections.Generic;

namespace AppControle.Domain.Contracts
{
    public interface IProdutoRepositorio : IBaseRepositorio<Produto>
    {
        IEnumerable<Produto> ObterTodos2();
    }
}
