namespace Teste_Logica
{
    // The class should also provide four public methods, Connect, Disconnect, Query and LevelConnection.
    public class Arvore
    {
        private Dictionary<int, List<int>> folhas = new Dictionary<int, List<int>>();

        // The constructor should take a positive integer value indicating the number of elements in the set. Passing 
        // an invalid value should throw an exception.
        public Arvore(int quantidadeFolhas)
        {
            if (quantidadeFolhas <= 0)
                throw new Exception("Esperado número maior que 0.");

            for (int i = 1; i <= quantidadeFolhas; i++)
            {
                folhas.Add(i, new List<int>());
            }
        }

        // The connect method will take two integers indicating the elements to connect.This method should throw exceptions as appropriate.
        public void Connect(int folhaA, int folhaB)
        {
            ValidaParametros(folhaA, folhaB);

            List<int> conexoesA = new List<int>();
            folhas.TryGetValue(folhaA, out conexoesA);

            List<int> conexoesB = new List<int>();
            folhas.TryGetValue(folhaB, out conexoesB);

            if (conexoesA.Count != 0 && conexoesB.Count != 0)
            {
                int conexao = conexoesA.FindIndex(c => c == folhaB);
                if (conexao != -1)
                    throw new Exception("(A) ja está conectado com (B).");
            }

            conexoesA.Add(folhaB);
            conexoesB.Add(folhaA);
        }

        // The disconnect method will take two integers indicating the elements to disconnect.This method should throw exceptions as appropriate.
        public void Disconnect(int folhaA, int folhaB)
        {
            ValidaParametros(folhaA, folhaB);

            List<int> conexoesA = new List<int>();
            folhas.TryGetValue(folhaA, out conexoesA);

            List<int> conexoesB = new List<int>();
            folhas.TryGetValue(folhaB, out conexoesB);

            int conexao = conexoesA.FindIndex(c => c == folhaB);
            if (conexao == -1)
                throw new Exception("Elementos não estão conectados entre si.");

            conexoesA.Remove(folhaB);
            conexoesB.Remove(folhaA);
        }

        // The query method will take two integers indicating the elements to query.This method should throw exceptions as appropriate.
        // It should return true if the elements are connected, directly or indirectly, and
        // false if the elements are not connected.
        public bool Query(int folhaA, int folhaB)
        {
            ValidaParametros(folhaA, folhaB);

            List<int> conexoesA = new List<int>();
            folhas.TryGetValue(folhaA, out conexoesA);

            List<int> conexoesB = new List<int>();
            folhas.TryGetValue(folhaB, out conexoesB);

            if (conexoesA.Count == 0 || conexoesB.Count == 0)
                return false;

            int conexao = conexoesA.FindIndex(c => c == folhaB);
            if (conexao == -1)
                return false;

            return true;
        }

        // The levelConnetion method will also take two integers and should also throw an exception as appropriate, returning an integer value.
        // It should return 0 if the elements are not connected, 1 if the elements are
        // directly connected and 2 or more when elements are indirectly connect, returning the number that
        // represents how many connections there are between the searched elements. 
        public int LevelConnection(int folhaA, int folhaB)
        {
            ValidaParametros(folhaA, folhaB);

            List<int> conexoesA = new List<int>();
            folhas.TryGetValue(folhaA, out conexoesA);

            List<int> conexoesB = new List<int>();
            folhas.TryGetValue(folhaB, out conexoesB);

            int conexao = conexoesA.FindIndex(c => c == folhaB);
            if (conexao != -1)
                return 1;

            var folhasAux = folhas;
            int numeroConexoes = 1;
            List<int> conexoesAux = new List<int> { folhaA };
            while (true)
            {
                numeroConexoes++;

                folhasAux = folhasAux.Where(
                    l => l.Value.FindIndex(c => conexoesAux.FindIndex(cc => cc == c) != -1) != -1
                    ).ToDictionary();

                if (folhasAux.Count == 0)
                    return 0;

                conexoesAux.Clear();

                foreach (var folha in folhasAux)
                {
                    conexoesAux.Add(folha.Key);

                    int index = folha.Value.FindIndex(c => c == folhaB);
                    if (index != -1)
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
            List<int> _ = new List<int>();

            if (!folhas.TryGetValue(folhaA, out _) || !folhas.TryGetValue(folhaB, out _))
                throw new Exception("Uma ou ambas as posições não existem.");
        }
    }
}
