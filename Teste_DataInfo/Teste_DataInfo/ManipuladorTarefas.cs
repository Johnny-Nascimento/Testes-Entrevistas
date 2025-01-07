using static Teste_DataInfo.Tarefa;

namespace Teste_DataInfo
{
    public class ManipuladorTarefas
    {        
        private BaseTarefa BaseTarefa { get; set; }
        public ManipuladorTarefas()
        {
            BaseTarefa = new BaseTarefa();
        }

        public string AdicionaTarefa(Tarefa tarefa)
        {
            if (tarefa.Titulo == string.Empty)
                return "O campo titulo não foi informado.";

            BaseTarefa.Post(tarefa);

            return "Tarefa adicionada com Sucesso";
        }

        // Ex saída
        // 1. [Pendente] - Estudar C# (Criado em: 10/11/2023) Descrição: Revisar conceitos avançados  
        // 2. [Concluído] - Preparar reunião(Criado em: 09/11/2023) Descrição: Criar slides para a apresentação
        public string ListaTarefas(StatusTarefa statusTarefa = StatusTarefa.Todos)
        {
            List<Tarefa> listaTarefas = BaseTarefa.GetAll();

            if (statusTarefa != StatusTarefa.Todos)
                listaTarefas = listaTarefas.Where(t => t.Status.Equals(statusTarefa)).ToList();

            if (listaTarefas.Count == 0)
                return "Não existem tarefas para serem mostradas.";

            string textoApresentacao = string.Empty;

            foreach (Tarefa tarefa in listaTarefas)
            {
                string status = tarefa.Status.Equals(StatusTarefa.Concluido) ? "Concluída" : "Pendente";
                string data = tarefa.DataCriacao.ToString("dd/MM/yyyy");

                textoApresentacao += $" {tarefa.Id}. [{status}] - {tarefa.Titulo} (Criado em: {data}) Descrição: {tarefa.Descricao}\n";
            }

            return textoApresentacao;
        }

        public string EditaTarefa(int Id, Tarefa tarefa)
        {
            if (tarefa.Titulo == string.Empty)
                return "O campo titulo não foi informado.";

            if (BaseTarefa.Update(Id, tarefa))
                return "Tarefa alterada com sucesso.";
            else
                return "Tarefa inexistente.";
        }

        public string ExcluiTarefa(int id)
        {
            if (BaseTarefa.Delete(id))
                return "Tarefa excluida com sucesso.";
            else
                return "Tarefa inexistente.";
        }

        public string AlteraStatusTarefa(int Id)
        {
            Tarefa? tarefa = BaseTarefa.GetById(Id);
            if (tarefa == null)
                return "Tarefa inexistente.";

            tarefa.Status = tarefa.Status.Equals(StatusTarefa.Concluido) ? StatusTarefa.Pendente : StatusTarefa.Concluido;

            if (BaseTarefa.Update(Id, tarefa))
                return "Tarefa alterada com sucesso.";
            else
                return "Tarefa inexistente.";
        }
    }
}
