using Lektion1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using ESA_Projekt;

namespace Lektion5
{
    internal class Program
    {
        public static TransponderDel transponder;
        public delegate void FliergerRegisterDel();
        public static FliergerRegisterDel fliegerRegister;


        static void Main(string[] args)
        {

            //Program Test = new Program();
            //Test.ProgrammTakten();
            //Console.WriteLine();

            //string s = "String";
            //Console.WriteLine(s.IndexOf('i'));
            //Console.WriteLine("Substring".Substring(4));
            //Console.WriteLine("Parameterwerte".Substring(4,10));

            // Todo kann wieder auskommentiert werden
            //Duesenflugzeuge flug = new Duesenflugzeuge("LH3000", new Positionen(500, 580, 200));
            //flug.Starte();

            FlugSchreiber flugSchreiber = new FlugSchreiber();
            Console.WriteLine(flugSchreiber.ToString());
            Console.ReadKey();
        }

        #region Auskommentierter Code Prüfen ob gebraucht wird 
        //public void Kennung()
        //{
        //    Flugzeuge flieger = new Flugzeuge("Lh 500 ", new Positionen(500, 300, 20));
        //    Console.WriteLine("Kennung = {0}", flieger.Kennung);
        //}

        // Code stehen lassen und prüfen
        //public void TestTransponder()
        //{
        //    Starrfluegelflugzeuge flieger1 = new Starrfluegelflugzeuge("LH 3000", new Positionen(3000, 2000, 100));
        //    flieger1.Steuern();
        //    Console.WriteLine();

        //    Starrfluegelflugzeuge flieger2 = new Starrfluegelflugzeuge("LH 500", new Positionen(3500, 1500, 180));
        //    flieger1.Steuern();
        //    flieger2.Steuern();
        //    Console.WriteLine();


        //    Starrfluegelflugzeuge flieger3 = new Starrfluegelflugzeuge("LH 444", new Positionen(1730, 23400, 780));
        //    flieger1.Steuern();
        //    flieger2.Steuern();
        //    flieger3.Steuern();
        //    Console.WriteLine();

        //    // Flieger 2 objekt beim Delegate abmelden 
        //    transponder -= flieger2.Transpond;
        //    flieger1.Steuern();
        //    flieger3.Steuern();
        //    Console.WriteLine();





        //}
        #endregion

        public void ProgrammTakten()
        {
            #region Ausgeklammerter code 
            //Console.WriteLine("\r{0}", DateTime.Now);


            //Starrfluegelflugzeuge flieger1 = new Starrfluegelflugzeuge("LH 500", new Positionen(3500, 1500, 180));
            //Program.fliegerRegister += flieger1.Steuern;
            //flieger1.Starte(new Positionen(1000, 500, 200), 200, 300, 20, 10);

            //Starrfluegelflugzeuge flieger2 = new Starrfluegelflugzeuge("LH 3000", new Positionen(3000, 2000, 100));
            //Program.fliegerRegister += flieger2.Steuern;
            //flieger2.Starte(new Positionen(1000, 500, 200), 260, 350, 25, 15);
            #endregion

            #region  CSH03B Lektion 5

            Duesenflugzeuge flieger1 = new Duesenflugzeuge("LH 500", new Positionen(3500, 1500, 180));
            Duesenflugzeuge flieger2 = new Duesenflugzeuge("LH 3000", new Positionen(3000, 2000, 100));

            while (fliegerRegister != null)
            {
                fliegerRegister();
                Console.WriteLine();
                Thread.Sleep(1000);


            }
            #endregion

        }


    }
}
