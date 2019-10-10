using System;
using System.Collections.Generic;

namespace AppControle.Domain.Entities
{
    public class Venda : Entity
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataVenda { get; set; }

        public override void Validate()
        {
            //throw new System.NotImplementedException();
        }

    }
}
