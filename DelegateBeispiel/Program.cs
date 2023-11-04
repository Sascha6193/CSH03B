using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DelegateBeispiel
{

    delegate void Nachricht(string sender);
    internal class Program
    {
        public void GutenTag(string sender)
        {
            Console.WriteLine(sender + " sagt Guten Tag");
        }
        public void AufWiedersehen(string sender) 
        {
            Console.WriteLine(sender + " sagt auf Wiedersehen");
        }
        public void DelegateAnwenden()
        {
            Nachricht info = new Nachricht(GutenTag);
            info += new Nachricht(AufWiedersehen);
            info("Ihr Administrator");
            
        }
        public static void Main(string[] args)
        {
            Program test = new Program();
            test.DelegateAnwenden();
        }
    }
}
