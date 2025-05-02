using System.Diagnostics;
using System.Linq;

namespace Teste_Logica_Benner.Testes
{
    public class ArvoreEx
    {
        private readonly Dictionary<int, LinkedList<int>> folhas = new Dictionary<int, LinkedList<int>>();

        public ArvoreEx(int quantidadeFolhas)
        {
            if (quantidadeFolhas <= 0)
                throw new Exception("Esperado número maior que 0.");

            for (int i = 1; i <= quantidadeFolhas; i++)
            {
                folhas.Add(i, new LinkedList<int>());
            }
        }

        public void Connect(int folhaA, int folhaB)
        {
            ValidaParametros(folhaA, folhaB);

            LinkedList<int> conexoesA = new LinkedList<int>();
            folhas.TryGetValue(folhaA, out conexoesA);

            LinkedList<int> conexoesB = new LinkedList<int>();
            folhas.TryGetValue(folhaB, out conexoesB);

            if (conexoesA.Count != 0 && conexoesB.Count != 0)
            {
                if (conexoesA.Contains(folhaB))
                    throw new Exception("(A) ja está conectado com (B).");
            }

            conexoesA.AddLast(folhaB);
            conexoesB.AddLast(folhaA);
        }

        public void Disconnect(int folhaA, int folhaB)
        {
            ValidaParametros(folhaA, folhaB);

            LinkedList<int> conexoesA = new LinkedList<int>();
            folhas.TryGetValue(folhaA, out conexoesA);

            LinkedList<int> conexoesB = new LinkedList<int>();
            folhas.TryGetValue(folhaB, out conexoesB);

            if (!conexoesA.Contains(folhaB))
                throw new Exception("Elementos não estão conectados entre si.");

            conexoesA.Remove(folhaB);
            conexoesB.Remove(folhaA);
        }

        public bool Query(int folhaA, int folhaB)
        {
            if (LevelConnection(folhaA, folhaB) > 0)
                return true;

            return false;
        }

        public int LevelConnection(int folhaA, int folhaB)
        {
            ValidaParametros(folhaA, folhaB);

            LinkedList<int> conexoesA = new LinkedList<int>();
            folhas.TryGetValue(folhaA, out conexoesA);

            if (conexoesA.Contains(folhaB))
                return 1;

            int numeroConexoes = 1;
            int numeroMaxConexoes = Math.Max(folhaA, folhaB);
            LinkedList<int> conexoesAux = new LinkedList<int>();
            conexoesAux.AddLast(folhaA);

            Dictionary<int, LinkedList<int>> folhasAux = new Dictionary<int, LinkedList<int>>();

            HashSet<int> chavesProcessadas = new HashSet<int>();

            while (numeroConexoes < numeroMaxConexoes)
            {
                numeroConexoes++;

                folhasAux.Clear();

                foreach (var folha in folhas)
                {
                    Parallel.ForEach(folha.Value, conexao =>
                        {
                            if (conexoesAux.Contains(conexao))
                            {
                                folhasAux.TryAdd(folha.Key, folha.Value);
                            }
                        });
                }

                if (folhasAux.Count == 0)
                    return 0;

                conexoesAux.Clear();

                foreach (var folha in folhasAux)
                {
                    if (chavesProcessadas.Add(folha.Key))
                        conexoesAux.AddLast(folha.Key);

                    if (folha.Value.Contains(folhaB))
                        return numeroConexoes;
                }
            }

            return 0;
        }

        private void ValidaParametros(int folhaA, int folhaB)
        {
            if (folhaA == folhaB)
                throw new Exception("Esperado números diferentes.");

            ValidaDiferenteDeZero(folhaA, folhaB);
            ValidaExiste(folhaA, folhaB);
        }

        private void ValidaDiferenteDeZero(int folhaA, int folhaB)
        {
            if (folhaA <= 0 || folhaB <= 0)
                throw new Exception("Esperado número maior que 0.");
        }

        private void ValidaExiste(int folhaA, int folhaB)
        {
            LinkedList<int> _ = new LinkedList<int>();

            if (!folhas.TryGetValue(folhaA, out _) || !folhas.TryGetValue(folhaB, out _))
                throw new Exception("Uma ou ambas as posições não existem.");
        }
    }
}
