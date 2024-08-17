namespace Teste_Playmove_API.Entities
{
    public class Fornecedor
    {
        public Fornecedor()
        {
            Id = 0;
            Excluido = false;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Excluido { get; set; }

        public void Update(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public void Delete()
        {
            Excluido = true;
        }
    }
}
