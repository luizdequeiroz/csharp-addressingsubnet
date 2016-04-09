using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdressingSubnets
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Subnet> subnets = new List<Subnet>();
            int nSubnets = 0, nHosts = 0;

            Console.WriteLine("--------- Bem Vindo ao AddressingSubnets ---------");
            Console.WriteLine("---------- Por Luiz de Queiroz-11200878 ----------\n");
            Console.Write("Informe o número de subredes: ");
            nSubnets = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < nSubnets; i++)
            {
                Console.Write("Informe o número de hosts para a " + i+1 + "ª subrede: ");
                nHosts = Convert.ToInt32(Console.ReadLine());
                subnets.Add(new Subnet
                {
                    numero = i+1,
                    hosts = nHosts                
                });
            }

            foreach (var s in subnets)
            {
                Console.WriteLine("| Número | " + s.numero + " |\n"
                                + "| Hosts  | " + s.hosts + " |\n"
                                + "| Mascara| " + "\n"
                                + "| Ident  | " + "\n"
                                + "| 1º IP  | " + "\n"
                                + "| Uº IP  | " + "\n"
                                + "| Broad  | " + "\n");
            }

            Console.ReadKey();
        }
    }
}
