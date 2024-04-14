using System.ComponentModel.DataAnnotations;

namespace QualiCadastro.Models
{
    public class Email
    {
        public int Id { get; set; }

        public int IdCadastro { get; set; }

        public string EmailCadastro { get; set; } = string.Empty;

        public Cadastro Cadastro { get; set; } = new Cadastro();
    }
}
