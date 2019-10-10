using System.Collections.Generic;
using System.Linq;

namespace AppControle.Domain.Entities
{
    public abstract class Entity
    {
        private List<string> _mensagensValidacao { get; set; }
        public List<string> MensagemValidacao
        {
            get
            {
                return _mensagensValidacao ?? (_mensagensValidacao = new List<string>());
            }
        }

        public abstract void Validate();
        public bool IsValid
        {
            get { return !MensagemValidacao.Any(); }
        }

        protected void LimparMensagensValidacao()
        {
            MensagemValidacao.Clear();
        }
        protected void AdicionarCritica(string msg)
        {
            MensagemValidacao.Add(msg);
        }
    }
}
