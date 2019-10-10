namespace AppControle.Domain.Entities
{
    public class Estoque : Entity
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public override void Validate()
        {
            LimparMensagensValidacao();
            if (Quantidade < 0)
                AdicionarCritica("Campo de quantidade deve ser maior que zero.");

        }

    }
}
