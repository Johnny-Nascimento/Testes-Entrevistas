using static Teste_DataInfo.Tarefa;

namespace Teste_DataInfo
{
    public class ManipuladorTarefas
    {        
        private List<Tarefa> Tarefas { get; set; }
        public ManipuladorTarefas()
        {
            Tarefas = new List<Tarefa>();
        }

        public string AdicionaTarefa(Tarefa tarefa)
        {
            if (tarefa.Titulo == string.Empty)
                return "O campo titulo não foi informado.";

            tarefa.DataCriacao = DateTime.Now;
            tarefa.Status = StatusTarefa.Pendente;

            Tarefas.Add(tarefa);

            return "Tarefa adicionada com Sucesso";
        }

        // Ex saída
        // 1. [Pendente] - Estudar C# (Criado em: 10/11/2023) Descrição: Revisar conceitos avançados  
        // 2. [Concluído] - Preparar reunião(Criado em: 09/11/2023) Descrição: Criar slides para a apresentação
        public string ListaTarefas(StatusTarefa statusTarefa = StatusTarefa.Todos)
        {
            List<Tarefa> listaTarefasFiltrada = new List<Tarefa>();

            if (statusTarefa == StatusTarefa.Todos)
                listaTarefasFiltrada = Tarefas;
            else
                listaTarefasFiltrada = Tarefas.Where(t => t.Status.Equals(statusTarefa)).ToList();

            if (listaTarefasFiltrada.Count == 0)
                return "Não existem tarefas para serem mostradas.";

            string textoApresentacao = string.Empty;

            for (int i = 0; i < listaTarefasFiltrada.Count; ++i)
            {
                Tarefa tarefa = listaTarefasFiltrada[i];

                string status = tarefa.Status.Equals(StatusTarefa.Concluido) ? "Concluída" : "Pendente";
                string data = tarefa.DataCriacao.ToString("dd/MM/yyyy");

                textoApresentacao += $" {i + 1}. [{status}] - {tarefa.Titulo} (Criado em: {data}) Descrição: {tarefa.Descricao}\n";
            }

            return textoApresentacao;
        }

        public string EditaTarefa(int indexTarefa, Tarefa tarefa)
        {
            if (tarefa.Titulo == string.Empty)
                return "O campo titulo não foi informado.";

            --indexTarefa;

            if (indexTarefa < Tarefas.Count)
            {
                Tarefas[indexTarefa].Titulo = tarefa.Titulo;
                Tarefas[indexTarefa].Descricao = tarefa.Descricao;

                return "Tarefa alterada com sucesso.";
            }
            else
                return "Tarefa inexistente.";
        }

        public string ExcluiTarefa(int indexTarefa)
        {
            --indexTarefa;

            if (indexTarefa < Tarefas.Count)
            {
                Tarefas.RemoveAt(indexTarefa);
                return "Tarefa excluida com sucesso.";
            }
            else
                return "Tarefa inexistente.";
        }

        public string AlteraStatusTarefa(int indexTarefa)
        {
            --indexTarefa;

            if (indexTarefa < Tarefas.Count)
            {
                Tarefa.StatusTarefa statusAtual = Tarefas[indexTarefa].Status;

                Tarefas[indexTarefa].Status = statusAtual.Equals(StatusTarefa.Concluido) ? StatusTarefa.Pendente : StatusTarefa.Concluido;

                if (Tarefas[indexTarefa].Status.Equals(StatusTarefa.Concluido))
                    return "Tarefa alterada para concluida.";
                else
                    return "Tarefa alterada para pendente.";
            }
            else
                return "Tarefa inexistente.";
        }
    }
}
