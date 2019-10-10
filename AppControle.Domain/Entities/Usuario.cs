using System.Text.RegularExpressions;

namespace AppControle.Domain.Entities
{
    public class Usuario : Entity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        public override void Validate()
        {
            LimparMensagensValidacao();

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (!match.Success)
                AdicionarCritica("Email informado é inválido.");

            /*regex = new Regex(@"^((\d{3}).(\d{3}).(\d{3})-(\d{2}))*$");
            match = regex.Match(Cpf);
            if (!match.Success)
                AdicionarCritica("Formato do Cpf informado é inválido.");
                */
            if(!IsCpf(Cpf))
                AdicionarCritica("Cpf informado é inválido.");            
        }

        private bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
