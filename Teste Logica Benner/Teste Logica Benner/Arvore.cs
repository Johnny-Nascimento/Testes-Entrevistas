namespace Teste_Logica
{
    // The class should also provide four public methods, Connect, Disconnect, Query and LevelConnection.
    public class Arvore
    {
        private readonly Dictionary<int, List<int>> folhas = new Dictionary<int, List<int>>();

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
            if (LevelConnection(folhaA, folhaB) > 0)
                return true;

            return false;
        }

        // The levelConnetion method will also take two integers and should also throw an exception as appropriate, returning an integer value.
        // It should return 0 if the elements are not connected, 1 if the elements are
        // directly connected and 2 or more when elements are indirectly connect, returning the number that
        // represents how many connections there are between the searched elements. 
#pragma region Documentacao
        /* 
        Arvore.LevelConnection(1, 5);
        Num primeiro momento é acessado a folhaA e procurado se existe uma conexão com a folhaB para retornar 1 como conexão direta.

        Em caso de não encontrar o próximo passo é criar uma conexão com folhaA, e procurar todas as folhas que conectam-se com folhaA
        Não encontrando retorna-se 0

        Encontrado, percorre-se o dicionario de folhas, e procura na lista de conexões a folhaB, encontrando é retornado o número de conexões atual.
        Não encontrado, adiciona todas as novas folhas a lista de conexoes filtro e repete-se a busca

        Variaveis
        folhaA = indica a folha inicial da busca
        folhaB = indica a folha objetivo da busca
        conexoesA = indica a lista de conexões da folhaA
        numeroConexoes = recebe iterações para cada rodada de busca por conexões, inicia com 1 pois a primeira procura ja foi feita na conexoesA
        conexoesAux = recebe a lista de conexões para serem filtradas

        a condição while (numeroConexoes < Math.Max(folhaA, folhaB)) substitui o while(true), prevenindo que as interações ultrapassem o último valor objetivo,
        pois imagina-se que o maior caminho de 1 para 100 seja 99
        */
#pragma endregion Documentacao
        public int LevelConnection(int folhaA, int folhaB)
        {
            ValidaParametros(folhaA, folhaB);

            List<int> conexoesA = new List<int>();
            folhas.TryGetValue(folhaA, out conexoesA);

            int conexao = conexoesA.FindIndex(c => c == folhaB);
            if (conexao != -1)
                return 1;

            int numeroConexoes = 1;
            int numeroMaxConexoes = Math.Max(folhaA, folhaB);
            List<int> conexoesAux = new List<int> { folhaA };

            HashSet<int> chavesProcessadas = new HashSet<int>();

            while (numeroConexoes < numeroMaxConexoes)
            {
                numeroConexoes++;

                var folhasAux = folhas.Where(
                    l => l.Value.FindIndex(
                            c => conexoesAux.FindIndex(
                                cc => cc == c
                            ) != -1
                         ) != -1
                    )
                    .ToDictionary();

                if (folhasAux.Count == 0)
                    return 0;

                conexoesAux.Clear();

                foreach (var folha in folhasAux)
                {
                    if (chavesProcessadas.Add(folha.Key))
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
