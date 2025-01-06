using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Teste_DataInfo
{
    public class ManipuladorTarefas
    {
        public enum FiltroLeitura
        {
            Todos = 1,
            Concluido = 2,
            Pendente = 3
        };

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
        public string ListaTarefas(FiltroLeitura filtro = FiltroLeitura.Todos)
        {
            // Melhorar isso vvvvv

            List<Tarefa> listaTarefasFiltrada = new List<Tarefa>();

            if (filtro.Equals(FiltroLeitura.Todos))
                listaTarefasFiltrada = Tarefas.OrderByDescending(x => x.DataCriacao).ToList();
            else if (filtro.Equals(FiltroLeitura.Concluido))
                listaTarefasFiltrada = Tarefas.OrderByDescending(x => x.DataCriacao).Where(t => t.Status == true).ToList();
            else
                listaTarefasFiltrada = Tarefas.OrderByDescending(x => x.DataCriacao).Where(t => t.Status == false).ToList();

            // Melhorar isso ^^^^^

            if (listaTarefasFiltrada.Count == 0)
                return "Não existem tarefas para serem mostradas.";

            string textoApresentacao = string.Empty;

            for (int i = 0; i < listaTarefasFiltrada.Count; ++i)
            {
                Tarefa tarefa = listaTarefasFiltrada[i];

                string status = tarefa.Status ? "Concluída" : "Pendente";

                textoApresentacao += $" {i + 1} - Titulo: {tarefa.Titulo}\n Descricao: {tarefa.Descricao}\n Status: {status}\n Data de criação: {tarefa.DataCriacao.ToString("dd/MM/yyyy HH:mm")}\n";
                textoApresentacao += "________________________________________\n";
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
                bool statusAtual = Tarefas[indexTarefa].Status;

                Tarefas[indexTarefa].Status = !statusAtual;

                if (Tarefas[indexTarefa].Status)
                    return "Tarefa alterada para concluida.";
                else
                    return "Tarefa alterada para pendente.";
            }
            else
                return "Tarefa inexistente.";
        }
    }
}
