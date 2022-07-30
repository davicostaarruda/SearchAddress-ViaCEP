using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ViaCEP
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Deseja informar um CEP ou um Endereço?");
            Console.WriteLine("Digite 1 para CEP.");
            Console.WriteLine("Digite 2 para Endereço.");
            var option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Informe o CEP: ");
                    var cep = Console.ReadLine();
                    Executar(cep).Wait();
                break;
                case 2:
                    Console.WriteLine("Informe o Endereço (estado/cidade/rua ou avenida com as `/` respectivamente): ");
                    var address = Console.ReadLine();
                    Executar(address).Wait();
                break;
                default:
                    Console.WriteLine("Opção inválida, programa encerrado");
                break;
            }
        }

        private static async Task<string> GetJson(string path)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://viacep.com.br/ws/{path}/json/");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private static async Task Executar(string path)
        {
            var json = await GetJson(path);
            Console.WriteLine(json);
        }
    }
}