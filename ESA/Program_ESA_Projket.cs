using Lektion1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ESA_Projekt
{
    internal class ProgramESAProjket
    {
        static void Main2(string[] args)
        {
            FlugSchreiber flugzeug1 = new FlugSchreiber();
            Console.WriteLine(flugzeug1);
        }

        public static bool protokollieren = true;
    }
}
