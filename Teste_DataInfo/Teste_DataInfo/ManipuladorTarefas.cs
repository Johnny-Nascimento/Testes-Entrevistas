namespace Teste_DataInfo
{
    public class ManipuladorTarefas
    {        
        private List<Tarefa> Tarefas { get; set; }
        public ManipuladorTarefas()
        {
            Tarefas = new List<Tarefa>();
        }

        // Adicionar Tarefa: O usuário deve poder adicionar uma nova tarefa com título e descrição
        // opcional.
        public string AdicionaTarefa(Tarefa tarefa)
        {
            if (tarefa.Titulo == string.Empty)
                return "O campo titulo não foi informado.";

            tarefa.DataCriacao = DateTime.Now;
            Tarefas.Add(tarefa);

            return "Tarefa adicionada com Sucesso";
        }

        // Listar todas as tarefas cadastradas, mostrando título, descrição, status(pendente
        // ou concluída), e a data de criação.
        // Filtrar Tarefas: Permitir ao usuário visualizar apenas tarefas pendentes, concluídas ou todas. 
        public string ListaTarefas(Tarefa.StatusTarefa statusTarefa = Tarefa.StatusTarefa.Pendente | Tarefa.StatusTarefa.Concluido)
        {
            List<Tarefa> listaTarefasFiltrada = new List<Tarefa>();

            listaTarefasFiltrada = Tarefas.Where(t => t.Status.Equals(statusTarefa)).ToList();

            if (listaTarefasFiltrada.Count == 0)
                return "Não existem tarefas para serem mostradas.";

            string textoApresentacao = string.Empty;

            for (int i = 0; i < listaTarefasFiltrada.Count; ++i)
            {
                Tarefa tarefa = listaTarefasFiltrada[i];

                string status = tarefa.Status.Equals(Tarefa.StatusTarefa.Concluido) ? "Concluída" : "Pendente";
                string data = tarefa.DataCriacao.ToString("dd/MM/yyyy HH:mm");

                textoApresentacao += $" {i + 1}. [{status}] - {tarefa.Titulo} (Criado em: {data} Descrição: {tarefa.Descricao})\n";
            }

            return textoApresentacao;
        }

        // Editar Tarefa: Permitir que o usuário edite o título e a descrição de uma tarefa. 
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

        // Excluir Tarefa: Remover uma tarefa da lista. 
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

        // Marcar/Desmarcar como Concluída: Alternar o status da tarefa entre concluída e pendente
        public string AlteraStatusTarefa(int indexTarefa)
        {
            --indexTarefa;

            if (indexTarefa < Tarefas.Count)
            {
                Tarefa.StatusTarefa statusAtual = Tarefas[indexTarefa].Status;

                Tarefas[indexTarefa].Status = ~statusAtual;

                if (Tarefas[indexTarefa].Status.Equals(Tarefa.StatusTarefa.Concluido))
                    return "Tarefa alterada para concluida.";
                else
                    return "Tarefa alterada para pendente.";
            }
            else
                return "Tarefa inexistente.";
        }
    }
}
