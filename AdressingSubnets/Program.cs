using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AddressingSubnets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "AddressingSubnet";
            Console.SetWindowSize(160, 40);
            Console.ForegroundColor = ConsoleColor.Green;

            List<Subnet> subnets = new List<Subnet>();
            int nSubnets = 0, nHosts = 0, ponto = 0;
            string ipPadrao;
            bool valido = false;

            Console.WriteLine("--------- Bem Vindo ao AddressingSubnets ---------");
            Console.WriteLine("---------------- Máscaras Classe C ---------------");
            Console.WriteLine("---------- Por Luiz de Queiroz-11200878 ----------\n");

            Console.WriteLine("INFORME OS VALORES SOLICITADOS ABAIXO!");

            do
            {
                Console.Write("Três primeiros octetos do IP da rede (exemplo: 192.168.0): ");
                ipPadrao = Console.ReadLine();

                Regex regex = new Regex(@"^([0-9]{1,3})\.([0-9]{1,3})\.([0-9]{1,3})$");
                Match mat = regex.Match(ipPadrao);
                if (!mat.Success)
                    Console.WriteLine("IP inválido! Informe de acordo com o solicitado!");
                else valido = true;
            } while (!valido);

            do
            {
                Console.Write("\nInforme o número de sub-redes: ");
                try
                {
                    nSubnets = Convert.ToInt32(Console.ReadLine());
                    valido = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Informe um número para a quantidade de sub-redes!");
                    valido = false;
                }
            } while (!valido);

            for (int i = 0; i < nSubnets; i++)
            {
                do
                {
                    Console.Write("Informe o número de hosts para a " + (i + 1) + "ª sub-rede: ");
                    try
                    {
                        nHosts = Convert.ToInt32(Console.ReadLine());
                        subnets.Add(new Subnet
                        {
                            numero = i + 1,
                            hosts = nHosts,
                            identificador = ipPadrao
                        });
                        valido = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Informe um número para a quantidade de hosts para esta sub-rede!");
                        valido = false;
                    }
                } while (!valido);
            }

            foreach (var s in subnets)
            {
                if (s.hosts <= 2)
                    s.mascara = 252;
                if (s.hosts <= 6 && s.hosts > 2)
                    s.mascara = 248;
                if (s.hosts <= 14 && s.hosts > 6)
                    s.mascara = 240;
                if (s.hosts <= 30 && s.hosts > 14)
                    s.mascara = 224;
                if (s.hosts <= 62 && s.hosts > 30)
                    s.mascara = 192;
                if (s.hosts <= 126 && s.hosts > 62)
                    s.mascara = 128;
            }

            subnets = subnets.OrderByDescending(s => s.hosts).ToList();

            Console.WriteLine("\nTABELA COM ENDEREÇAMENTOS PARA AS SUB-REDES.\n");

            int a = 1;
            for (int s = 0; s < subnets.Count; s++)
            {
                subnets[s].identificador += "." + ((s != 0) ? a : (a - 1));
                subnets[s].primeiroIp = (s == 0) ? a : (a + 1);
                subnets[s].ultimoIp = (s == 0) ? (254 - subnets[s].mascara) : ((254 - subnets[s].mascara) + a);
                subnets[s].broadcast = subnets[s].ultimoIp + 1;
                a = subnets[s].broadcast + 1;
            }

            subnets = subnets.OrderBy(s => s.numero).ToList();

            Console.WriteLine("____________________________________________________________________________________________________________");
            Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,16}|{3,16}|{4,16}|{5,16}|{6,16}|", 
                                "Número", "Hosts", "Máscara", "Identificador", "1º IP", "Uº IP", "Broadcast"));
            foreach (var s in subnets)
                Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,16}|{3,16}|{4,16}|{5,16}|{6,16}|",
                            s.numero, s.hosts, "255.255.255." + s.mascara, s.identificador, ipPadrao + "." + s.primeiroIp, ipPadrao + "." + s.ultimoIp, ipPadrao + "." + s.broadcast));
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("________________________________________________________________________________________ por Luiz de Queiroz");

            Console.WriteLine("- Pressione L para realizar outro cálculo de endereçamento de sub-redes da Classe C! -");
            Console.WriteLine("- Pressione qualquer outra tecla para sair!");
            if (Console.ReadKey().Key == ConsoleKey.L)
            {
                Console.WriteLine();
                Main(null);
            }
                
        }
    }
}