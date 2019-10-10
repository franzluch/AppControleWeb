using System.Collections.Generic;

namespace AppControle.Domain.Entities
{
    public class Produto : Entity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        //public int VendaId { get; set; }
        //public virtual Venda Venda { get; set; }
        public int EstoqueId { get; set; }
        public virtual ICollection<Estoque> Estoque { get; set; }
        public int Quantidade { get; set; }

        public override void Validate()
        {
            LimparMensagensValidacao();
            if (string.IsNullOrEmpty(Descricao))
                AdicionarCritica("Campo Descrição é obrigatório.");

        }

    }
}
