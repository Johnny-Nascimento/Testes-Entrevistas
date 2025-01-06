using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Teste_DataInfo.ManipuladorTarefas;

namespace Teste_DataInfo
{
    public class Menu
    {
        public enum Opcoes : int
        {
            AdicionarTarefa = 1,
            ListarTarefas = 2,
            EditarTarefa = 3,
            ExcluirTarefa = 4,
            MarcarDesmarcarConcluida = 5,
            FiltrarTarefas = 6,
            Sair = 0,
        }

        private int MostraMenu()
        {
            Console.WriteLine("1 - Adicionar Tarefa");
            Console.WriteLine("2 - Listar Tarefas");
            Console.WriteLine("3 - Editar Tarefa");
            Console.WriteLine("4 - Excluir Tarefa");
            Console.WriteLine("5 - Marcar / Desmarcar como concluída");
            Console.WriteLine("6 - Filtrar Tarefas");
            Console.WriteLine("0 - Sair");

            return Convert.ToInt32(Console.ReadLine());
        }

        public void Comecar()
        {
            Opcoes opcao = (Opcoes)MostraMenu();

            ManipuladorTarefas manipuladorTarefas = new ManipuladorTarefas();

            while (opcao != 0)
            {
                switch (opcao)
                {
                    case Opcoes.AdicionarTarefa:
                    {
                        Console.Clear();

                        Console.WriteLine("Campos sinalizados com '*' são obrigatórios.\n");

                        Tarefa tarefa = new Tarefa();

                        Console.WriteLine("Escreva o titulo da tarefa. *");
                        tarefa.Titulo = Console.ReadLine();

                        Console.WriteLine("Escreva a descrição da tarefa.");
                        tarefa.Descricao = Console.ReadLine();

                        Console.WriteLine(manipuladorTarefas.AdicionaTarefa(tarefa));
                    }
                    break;

                    case Opcoes.ListarTarefas:
                    {
                        Console.Clear();

                        Console.WriteLine(manipuladorTarefas.ListaTarefas());
                    }
                    break;

                    case Opcoes.EditarTarefa:
                    {
                        Console.Clear();

                        Console.WriteLine(manipuladorTarefas.ListaTarefas());

                        Console.WriteLine("Que tarefa deseja editar?");
                        int indexTarefa = Convert.ToInt32(Console.ReadLine());

                        Tarefa tarefa = new Tarefa();

                        Console.WriteLine("Escreva o titulo da tarefa. *");
                        tarefa.Titulo = Console.ReadLine();

                        Console.WriteLine("Escreva a descrição da tarefa.");
                        tarefa.Descricao = Console.ReadLine();

                        Console.WriteLine(manipuladorTarefas.EditaTarefa(indexTarefa, tarefa));
                    }
                    break;

                    case Opcoes.ExcluirTarefa:
                    {
                        Console.Clear();

                        Console.WriteLine(manipuladorTarefas.ListaTarefas());

                        Console.WriteLine("Que tarefa deseja excluir?");
                        int indexTarefa = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine(manipuladorTarefas.ExcluiTarefa(indexTarefa));
                    }
                    break;

                    case Opcoes.MarcarDesmarcarConcluida:
                    {
                        Console.Clear();

                        Console.WriteLine(manipuladorTarefas.ListaTarefas());

                        Console.WriteLine("Que tarefa deseja alterar o status?");
                        int indexTarefa = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine(manipuladorTarefas.AlteraStatusTarefa(indexTarefa));
                    }
                    break;

                    case Opcoes.FiltrarTarefas:
                    {
                        Console.Clear();

                        Console.WriteLine("1 - Ver todas as tarefas");
                        Console.WriteLine("2 - Ver tarefas concluidas");
                        Console.WriteLine("3 - Ver tarefas pendentes");

                        FiltroLeitura filtroTarefa = (FiltroLeitura)Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine(manipuladorTarefas.ListaTarefas(filtroTarefa));
                    }
                    break;

                    case Opcoes.Sair:
                        Console.WriteLine("0");
                    break;

                    default:
                        Console.WriteLine("Opção inválida");
                    break;
                }

                opcao = (Opcoes)MostraMenu();
            }

            Console.WriteLine("Fim");
        }
    }
}
