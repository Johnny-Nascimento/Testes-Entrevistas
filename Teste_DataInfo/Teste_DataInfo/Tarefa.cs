namespace Teste_DataInfo
{
    public class Tarefa
    {
        public enum StatusTarefa
        {
            Todos  = 1,
            Concluido = 2,
            Pendente = 3
        };

        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
        public bool Excluido { get; set; } = false;
    }
}
