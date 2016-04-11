using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressingSubnets
{
    class Subnet
    {
        public int numero { get; set; }
        public int hosts { get; set; }
        public int mascara { get; set; }
        public string identificador { get; set; }
        public int primeiroIp { get; set; }
        public int ultimoIp { get; set; }
        public int broadcast { get; set; }
    }
}
