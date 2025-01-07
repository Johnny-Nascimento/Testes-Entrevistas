
using System.Text.Json;
using System.Xml;

namespace Teste_DataInfo
{
    public class BaseTarefa
    {
        private const string nomeArquivo = "tarefa.json";

        public List<Tarefa> GetAll(bool trazerExcluidos = false)
        {
            List<Tarefa> listaTarefas = new List<Tarefa>();

            if (File.Exists(nomeArquivo))
            {
                string json = File.ReadAllText(nomeArquivo);
                listaTarefas = JsonSerializer.Deserialize<List<Tarefa>>(json);
            }

            if (trazerExcluidos)
                return listaTarefas;
            else
                return listaTarefas.Where(t => !t.Excluido).ToList();
        }

        public Tarefa GetById(int id)
        {
            List<Tarefa> listaTarefas = GetAll();

            return listaTarefas.FirstOrDefault(t => t.Id == id);
        }

        public void Post(Tarefa tarefa)
        {
            List<Tarefa> listaTarefas = GetAll(true);

            tarefa.Id = listaTarefas.Any() ? listaTarefas.Count + 1 : 1;
            tarefa.DataCriacao = DateTime.Now;

            listaTarefas.Add(tarefa);

            string jsonString = JsonSerializer.Serialize<List<Tarefa>>(listaTarefas);
            File.WriteAllText(nomeArquivo, jsonString);
        }

        public bool Update(int id, Tarefa tarefa)
        {
            Tarefa? tarefaBanco = GetById(id);
            if (tarefaBanco == null)
                return false;

            List<Tarefa> listaTarefas = GetAll(true);

            if (listaTarefas[id - 1].Excluido)
                return false;

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Status = tarefa.Status;

            listaTarefas[id - 1] = tarefaBanco;

            var json = JsonSerializer.Serialize(listaTarefas);
            File.WriteAllText(nomeArquivo, json);

            return true;
        }

        public bool Delete(int id)
        {
            Tarefa? tarefaBanco = GetById(id);
            if (tarefaBanco == null)
                return false;

            tarefaBanco.Excluido = true;

            List<Tarefa> listaTarefas = GetAll(true);

            listaTarefas[id - 1] = tarefaBanco;

            var json = JsonSerializer.Serialize(listaTarefas);
            File.WriteAllText(nomeArquivo, json);

            return true;
        }
    }
}
