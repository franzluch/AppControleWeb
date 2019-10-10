namespace AppControle.Domain.Entities
{
    public class Categoria : Entity
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        //public int ProdutoId { get; set; }
        //public virtual Produto Produto { get; set; }

        public override void Validate()
        {
            LimparMensagensValidacao();
            if (string.IsNullOrEmpty(Nome))
                AdicionarCritica("Campo Nome é obrigatório.");
        }
    }
}
