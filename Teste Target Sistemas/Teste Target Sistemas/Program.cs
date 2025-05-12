using DocumentFormat.OpenXml.Office.CustomUI;
using Elmah.ContentSyndication;
using Newtonsoft.Json;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;

namespace Teste_Logica
{
    // Classes aux
    public class Faturamento
    {
        public int Dia { get; set; }
        public double Valor { get; set; }
    }

    // Resolução
    internal class Program
    {
        // 1) Observe o trecho de código abaixo:
        // int INDICE = 13, SOMA = 0, K = 0;
        // Enquanto K < INDICE faça { K = K + 1; SOMA = SOMA + K; }
        // Imprimir(SOMA);
        // Ao final do processamento, qual será o valor da variável SOMA?

        // R= 91

        static void ExercicoUm()
        {
            int soma = 0;
            for (int indice = 13, k = 0; k < indice;)
            {
                k += 1;
                soma += k;
            }

            Console.WriteLine(soma);
        }

        // 2) Dado a sequência de Fibonacci, onde se inicia por 0 e 1 e o próximo valor sempre será a soma dos 2 valores anteriores(exemplo: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34...), escreva um programa na linguagem que desejar onde, informado um número, ele calcule a sequência de Fibonacci e retorne uma mensagem avisando se o número informado pertence ou não a sequência.
        // IMPORTANTE: Esse número pode ser informado através de qualquer entrada de sua preferência ou pode ser previamente definido no código;
        static void Fibonacci(int numeroAlvo)
        {

            if (numeroAlvo == 0)
            {
                Console.WriteLine("É fibonacci");
                return;
            }

            int a = 1;
            int b = 1;

            bool ehFibonacci = false;

            while (b <= numeroAlvo)
            {
                if (numeroAlvo == b)
                {
                    Console.WriteLine("É fibonacci");
                    return;
                }

                int aux = b;
                b += a;
                a = aux;
            }

            Console.WriteLine("NÃO é fibonacci");
        }

        // 3) Dado um vetor que guarda o valor de faturamento diário de uma distribuidora, faça um programa, na linguagem que desejar, que calcule e retorne:
        // • O menor valor de faturamento ocorrido em um dia do mês;
        // • O maior valor de faturamento ocorrido em um dia do mês;
        // • Número de dias no mês em que o valor de faturamento diário foi superior à média mensal.
        // IMPORTANTE:
        // a) Usar o json ou xml disponível como fonte dos dados do faturamento mensal;
        // b) Podem existir dias sem faturamento, como nos finais de semana e feriados. Estes dias devem ser ignorados no cálculo da média;

        static void CalculaFturamentoJSON()
        {
            List<Faturamento> faturamentos = new List<Faturamento>();

            using (StreamReader r = new StreamReader("../../../../dados.json"))
            {
                string json = r.ReadToEnd();
                faturamentos = JsonConvert.DeserializeObject<List<Faturamento>>(json);
            }



            var menorFaturamento = faturamentos.Where(f => f.Valor != 0).Min(f => f.Valor);
            var maiorFaturamento = faturamentos.Max(f => f.Valor);

            double mediaMensal = 0;
            double somaMensal = 0;
            foreach (var faturamento in faturamentos)
            {
                somaMensal += faturamento.Valor;
            }

            var quantidadeFaturamentosComValor = faturamentos.Count(f => f.Valor != 0);

            mediaMensal = somaMensal / quantidadeFaturamentosComValor;

            var maiorQueMediaMensal = faturamentos.Where(f => f.Valor > mediaMensal);

            Console.WriteLine($"Menor faturamento: {menorFaturamento}");
            Console.WriteLine($"Maior faturamento: {maiorFaturamento}");
            Console.WriteLine($"Media mensal: {mediaMensal}");
            Console.WriteLine($"Dias que superaram a media mensal: {maiorQueMediaMensal.Count()}");
            Console.WriteLine($"Dias: ");
            foreach (var faturamento in maiorQueMediaMensal)
                Console.WriteLine(faturamento.Dia + " " + "Valor: " + faturamento.Valor);
        }

        //4) Dado o valor de faturamento mensal de uma distribuidora, detalhado por estado:
        //• SP – R$67.836,43
        //• RJ – R$36.678,66
        //• MG – R$29.229,88
        //• ES – R$27.165,48
        //• Outros – R$19.849,53
        //Escreva um programa na linguagem que desejar onde calcule o percentual de representação que cada estado teve dentro do valor total mensal da distribuidora.

        static void PercentualMensalRegiao()
        {
            Dictionary<string, double> regiaoValor = new Dictionary<string, double>();
            regiaoValor.Add("SP", 67836.43);
            regiaoValor.Add("RJ", 36678.66);
            regiaoValor.Add("MG", 29229.88);
            regiaoValor.Add("ES", 27165.48);
            regiaoValor.Add("OUTROS", 19849.53);

            var total = regiaoValor.Sum(rv => rv.Value);

            foreach (var rv in regiaoValor)
            {
                var aux = (rv.Value * 100) / total;

                Console.WriteLine($"Região {rv.Key} percentual {Math.Round(aux, 2)}");
            }
        }

        //5) Escreva um programa que inverta os caracteres de um string.
        //IMPORTANTE:
        //a) Essa string pode ser informada através de qualquer entrada de sua preferência ou pode ser previamente definida no código;
        //b) Evite usar funções prontas, como, por exemplo, reverse;

        static void InverteString(string texto)
        {
            string textoSaida = "";
            for(int i = texto.Length - 1; i >= 0; --i)
            {
                textoSaida += texto[i];
            }

            Console.WriteLine(textoSaida);
        }

        static void Main(string[] args)
        {
            InverteString("123456789");
        }
    }
}
