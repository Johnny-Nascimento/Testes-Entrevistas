namespace Teste_DataInfo
{
    public class Tarefa
    {
        [Flags]
        public enum StatusTarefa : byte
        {
            Concluido = 1,
            Pendente = 2
        };

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusTarefa Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
