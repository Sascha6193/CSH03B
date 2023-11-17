using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Wiederholungaufgaben
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                Console.Write("\a \r {0}:{1}:{2}", now.Hour, now.Minute, now.Second);
            }
        }

        
    }
}
