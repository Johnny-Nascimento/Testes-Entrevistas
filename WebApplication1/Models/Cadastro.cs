using System.ComponentModel.DataAnnotations;

namespace QualiCadastro.Models
{
	public class Cadastro
	{
		public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "Quantidade máxima de caracteres permitidos é 200")]
        [Required(ErrorMessage = "Campo Nome deve ser prenchido")]
        [Display(Name = nameof(Nome))]
        public string Nome { get; set; } = "";

        [MaxLength(200, ErrorMessage = "Quantidade máxima de caracteres permitidos é 200")]
        [Display(Name = nameof(Empresa))]
        public string Empresa { get; set; } = "";

        [Display(Name = "E-mail")]
        public List<Email> Emails { get; set; } = new List<Email>();

        [MaxLength(20, ErrorMessage = "Quantidade máxima de caracteres permitidos é 20")]
        [Display(Name = "Telefone Pessoal")]
        public string TelefonePessoal { get; set; } = "";

        [MaxLength(20, ErrorMessage = "Quantidade máxima de caracteres permitidos é 20")]
        [Display(Name = "Telefone Comercial")]
        public string TelefoneComercial { get; set; } = "";
    }
}
