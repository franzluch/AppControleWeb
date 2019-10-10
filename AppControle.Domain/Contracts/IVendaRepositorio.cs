using AppControle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppControle.Domain.Contracts
{
    public interface IVendaRepositorio : IBaseRepositorio<Venda>
    {
        IEnumerable<Venda> ObterTodos2();
    }
}
