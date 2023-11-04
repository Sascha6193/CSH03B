using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lektion1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Flugzeuge
            //Positionen pos2 = new Positionen(20, 0, 1500);
            //Flugzeuge flieger2 = new Flugzeuge("flieger2", pos2);
            //flieger2.Steigen(50);

            //// Düsenflugzeuge 
            //Positionen pos3 = new Positionen(0, 60, 800);
            //Düsenflugzeuge flieger3 = new Düsenflugzeuge("flieger3", pos3, Airbus.A300);
            //flieger3.Sinken(50);
            //flieger3.Buchen(250);


            //// Starrflügelflugzeuge 
            //Positionen pos3 = new Positionen(0, 0, 966);
            //Starrflügelflugzeuge flieger1 = new Starrflügelflugzeuge("flieger4", pos3);

            //flieger1.Transpond("AirbusTest", pos3);

            Program Test = new Program();
            //Test.Kennung();
            //Test.TestTransponder();

            Test.ProgrammTakten();
           



            Console.WriteLine();
            Console.ReadKey();

        }

        public void Kennung()
        {
            Flugzeuge flieger = new Flugzeuge("Lh 500", new Positionen (500,300,20));
            Console.WriteLine("Kennung = {0}", flieger.Kennung);
                        

        }

        public void TestTransponder() 
        {
            Starrflügelflugzeuge flieger1 = new Starrflügelflugzeuge("LH 3000", new Positionen (3000,2000,100));
            flieger1.Steuern();
            Console.WriteLine();

            Starrflügelflugzeuge flieger2 = new Starrflügelflugzeuge("LH 500", new Positionen(3500, 1500, 180));
            flieger1.Steuern();
            flieger2.Steuern();
            Console.WriteLine();


            Starrflügelflugzeuge flieger3 = new Starrflügelflugzeuge("LH 444", new Positionen(1730, 23400, 780));
            flieger1.Steuern();
            flieger2.Steuern();
            flieger3.Steuern();
            Console.WriteLine();

            // Flieger 2 objekt beim Delegate abmelden 
            transponder -= flieger2.Transpond;
            flieger1.Steuern();
            flieger3.Steuern();
            Console.WriteLine();





        }

        public void ProgrammTakten()
        {
            Console.WriteLine("\r {0}",DateTime.Now);
            Starrflügelflugzeuge flieger1 = new Starrflügelflugzeuge("LH 3000", new Positionen(3000,2000,100));
            Starrflügelflugzeuge flieger2 = new Starrflügelflugzeuge("LH 500", new Positionen(3500,1500,180));
            while (true)
            {
                flieger1.Steuern();
                flieger2.Steuern();
                Console.WriteLine();
                Thread.Sleep(1000);
            }
        }

        public static TransponderDel transponder;

        
    }


    delegate void TransponderDel(string kennung, Positionen pos); 
}
