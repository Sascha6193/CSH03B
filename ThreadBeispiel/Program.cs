using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Data;

namespace ThreadBeispiel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program test = new Program();
            test.ProgrammTakten();

            while (true)
            {
                Console.Write("\r {0}" , DateTime.Now);
                Thread.Sleep(1000);
                
            }
        }

        
        public void ProgrammTakten() 
        {
        }
    }
}
