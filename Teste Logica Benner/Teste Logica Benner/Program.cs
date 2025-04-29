
namespace Teste_Logica
{
    public class Arvore
    {
        private Dictionary<int, List<int>> folhas = new Dictionary<int, List<int>>();

        // The constructor should take a positive integer value indicating the number of elements in the set. Passing 
        // an invalid value should throw an exception.
        public Arvore(uint quantidadeFolhas)
        {
            DiferenteDeZero(quantidadeFolhas, 1);

            for (int i = 1; i <= quantidadeFolhas; i++)
            {
                folhas.Add(i, null);
            }
        }

        // The class should also provide four public methods, Connect, Disconnect, Query and LevelConnection.

        // The connect method will take two integers indicating the elements to connect.This method should throw exceptions as appropriate.
        public void Connect(uint folhaA, uint folhaB)
        {
            DiferenteDeZero(folhaA, folhaB);

            // Se A ja tiver 2 conexões
            throw new Exception("(A) ja possui duas conexões.");

            // Se B ja tiver 2 conexões
            throw new Exception("(B) ja possui duas conexões.");

            // Se A ja estiver conectado com B
            throw new Exception("(A) ja está conectado com (B).");

            // Conecta
        }

        // The disconnect method will take two integers indicating the elements to disconnect.This method should throw exceptions as appropriate.
        public void Disconnect(uint folhaA, uint folhaB)
        {
            DiferenteDeZero(folhaA, folhaB);

            // Se não possuirem conexões
            throw new Exception("Elementos não estão conectados.");

            // Desconecta
        }

        // The query method will take two integers indicating the elements to query.This method should throw exceptions as appropriate.
        // It should return true if the elements are connected, directly or indirectly, and
        // false if the elements are not connected.
        public bool Query(uint folhaA, uint folhaB)
        {
            DiferenteDeZero(folhaA, folhaB);

            // Consulta

            bool temConexao = false;

            return temConexao;
        }

        // The levelConnetion method will also take two integers and should also throw an exception as appropriate, returning an integer value.
        // It should return 0 if the elements are not connected, 1 if the elements are
        // directly connected and 2 or more when elements are indirectly connect, returning the number that
        // represents how many connections there are between the searched elements. 
        public int LevelConnection(uint folhaA, uint folhaB)
        {
            DiferenteDeZero(folhaA, folhaB);

            // Nivel de conexão

            int nivelConexao = 0;

            return nivelConexao;
        }

        // Aux
        private void DiferenteDeZero(uint folhaA, uint folhaB)
        {
            if (folhaA == 0 || folhaB == 0)
                throw new Exception("Esperado número maior que 0.");
        }
    }

    internal class Program
    {
        private static void Testa(Arvore arvore)
        {
            Console.WriteLine("Faz conexão");
            arvore.Connect(1, 2);
            arvore.Connect(1, 6);
            arvore.Connect(6, 2);
            arvore.Connect(2, 4);
            arvore.Connect(5, 8);

            Console.WriteLine("Conectados");
            Console.WriteLine(arvore.Query(1, 2));
            Console.WriteLine(arvore.Query(1, 6));
            Console.WriteLine(arvore.Query(6, 2));
            Console.WriteLine(arvore.Query(2, 4));
            Console.WriteLine(arvore.Query(5, 8));

            Console.WriteLine("Não conectados.");
            Console.WriteLine(arvore.Query(7, 4));
            Console.WriteLine(arvore.Query(5, 6));

            Console.WriteLine("Nivel conexão (conectados)");
            Console.WriteLine(arvore.LevelConnection(1, 2));
            Console.WriteLine(arvore.LevelConnection(1, 6));
            Console.WriteLine(arvore.LevelConnection(6, 2));
            Console.WriteLine(arvore.LevelConnection(2, 4));
            Console.WriteLine(arvore.LevelConnection(5, 8));

            Console.WriteLine("Nivel conexão (não conectados)");
            Console.WriteLine(arvore.LevelConnection(7, 4));
            Console.WriteLine(arvore.LevelConnection(5, 6));
        }

        static void Main(string[] args)
        {
            Arvore arvore = new Arvore(8);

            Testa(arvore);

            arvore.Disconnect(1, 6);
            arvore.Disconnect(5, 8);

            Testa(arvore);
        }
    }
}
