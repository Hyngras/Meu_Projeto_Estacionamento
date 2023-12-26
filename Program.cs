using System;
using System.Collections.Generic;

namespace Meu_Projeto
{
    class Veiculo
    {
        public string Placa { get; set; }
        public DateTime Entrada { get; set; }

        public Veiculo(string placa)
        {
            Placa = placa;
            Entrada = DateTime.Now;
        }
    }

    class Estacionamento
    {
        private List<Veiculo> veiculosEstacionados;
        private decimal precoInicial;
        private decimal precoPorHora;

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            veiculosEstacionados = new List<Veiculo>();
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo:");
            string placa = Console.ReadLine();

            Veiculo novoVeiculo = new Veiculo(placa);
            veiculosEstacionados.Add(novoVeiculo);

            Console.WriteLine($"Veículo {placa} adicionado ao estacionamento.");
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo a ser removido:");
            string placa = Console.ReadLine();

            Veiculo veiculoRemovido = veiculosEstacionados.Find(v => v.Placa == placa);

            if (veiculoRemovido != null)
            {
                TimeSpan tempoEstacionado = DateTime.Now - veiculoRemovido.Entrada;
                decimal valorCobrado = CalcularValorEstacionamento(tempoEstacionado);
                Console.WriteLine($"Veículo {placa} removido. Valor cobrado: R${valorCobrado}");
                veiculosEstacionados.Remove(veiculoRemovido);
            }
            else
            {
                Console.WriteLine($"Veículo {placa} não encontrado no estacionamento.");
            }
        }

        public void ListarVeiculos()
        {
            Console.WriteLine("Veículos estacionados:");

            foreach (var veiculo in veiculosEstacionados)
            {
                Console.WriteLine($"Placa: {veiculo.Placa}, Entrada: {veiculo.Entrada.ToString("dd/MM/yyyy HH:mm:ss")}");
            }
        }

        private decimal CalcularValorEstacionamento(TimeSpan tempoEstacionado)
        {
            int horasEstacionado = (int)tempoEstacionado.TotalHours;
            decimal valorCobrado = precoInicial + (precoPorHora * horasEstacionado);

            return valorCobrado;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Seja bem-vindo ao sistema de estacionamento!");
            decimal precoInicial = LerDecimal("Digite o preço inicial:");
            decimal precoPorHora = LerDecimal("Agora digite o preço por hora:");

            Estacionamento estacionamento = new Estacionamento(precoInicial, precoPorHora);

            bool exibirMenu = true;

            while (exibirMenu)
            {
                Console.Clear();
                Console.WriteLine("Digite a sua opção:");
                Console.WriteLine("1 - Cadastrar veículo");
                Console.WriteLine("2 - Remover veículo");
                Console.WriteLine("3 - Listar veículos");
                Console.WriteLine("4 - Encerrar");

                switch (Console.ReadLine())
                {
                    case "1":
                        estacionamento.AdicionarVeiculo();
                        break;

                    case "2":
                        estacionamento.RemoverVeiculo();
                        break;

                    case "3":
                        estacionamento.ListarVeiculos();
                        break;

                    case "4":
                        exibirMenu = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }

                Console.WriteLine("Pressione uma tecla para continuar");
                Console.ReadLine();
            }

            Console.WriteLine("O programa se encerrou");
        }

        private static decimal LerDecimal(string mensagem)
        {
            decimal valor;
            while (true)
            {
                Console.Write(mensagem);
                if (decimal.TryParse(Console.ReadLine(), out valor))
                {
                    return valor;
                }
                else
                {
                    Console.WriteLine("Valor inválido. Tente novamente.");
                }
            }
        }
    }
}