
namespace Teste_Logica
{
    public class Arvore
    {
        private Dictionary<int, List<int>> folhas = new Dictionary<int, List<int>>();

        // The constructor should take a positive integer value indicating the number of elements in the set. Passing 
        // an invalid value should throw an exception.
        public Arvore(int quantidadeFolhas)
        {
            ValidaDiferenteDeZero(quantidadeFolhas, 1);

            for (int i = 1; i <= quantidadeFolhas; i++)
            {
                folhas.Add(i, new List<int>());
            }
        }

        // The class should also provide four public methods, Connect, Disconnect, Query and LevelConnection.

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
                if (conexao != - 1)
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

            // Conexões indiretas
            // 
            //

            return 0;
        }

        // Aux
        private void ValidaParametros(int folhaA, int folhaB)
        {
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

    internal class Program
    {
        private static void Loga(Arvore arvore)
        {
            Console.WriteLine("Query");
            Console.WriteLine($"1 e 2 {arvore.Query(1, 2)}");
            Console.WriteLine($"1 e 6 {arvore.Query(1, 6)}");
            Console.WriteLine($"6 e 2 {arvore.Query(6, 2)}");
            Console.WriteLine($"2 e 4 {arvore.Query(2, 4)}");
            Console.WriteLine($"5 e 8 {arvore.Query(5, 8)}");
            Console.WriteLine($"7 e 4 {arvore.Query(7, 4)}");
            Console.WriteLine($"5 e 6 {arvore.Query(5, 6)}");

            Console.WriteLine("Level connection");
            Console.WriteLine($"1 e 2 {arvore.LevelConnection(1, 2)}");
            Console.WriteLine($"1 e 6 {arvore.LevelConnection(1, 6)}");
            Console.WriteLine($"6 e 2 {arvore.LevelConnection(6, 2)}");
            Console.WriteLine($"2 e 4 {arvore.LevelConnection(2, 4)}");
            Console.WriteLine($"5 e 8 {arvore.LevelConnection(5, 8)}");
            Console.WriteLine($"7 e 4 {arvore.LevelConnection(7, 4)}");
            Console.WriteLine($"5 e 6 {arvore.LevelConnection(5, 6)}");
        }

        static void Main(string[] args)
        {
            Arvore arvore = new Arvore(8);

            Console.WriteLine("Faz conexão");
            arvore.Connect(1, 2);
            arvore.Connect(1, 6);
            arvore.Connect(6, 2);
            arvore.Connect(2, 4);
            arvore.Connect(5, 8);

            Loga(arvore);

            Console.WriteLine("Remove 1 e 6");
            arvore.Disconnect(1, 6);
            Console.WriteLine("Remove 5 e 8");
            arvore.Disconnect(5, 8);

            Loga(arvore);
        }
    }
}
