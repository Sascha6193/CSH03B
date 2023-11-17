﻿using Lektion1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;



namespace Lektion3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program Test = new Program();   
            Test.ProgrammTakten();

            



            Console.WriteLine();
            
            Console.ReadKey();

        }


        public void Kennung()
        {
            Flugzeuge flieger = new Flugzeuge("Lh 500", new Positionen(500, 300, 20));
            Console.WriteLine("Kennung = {0}", flieger.Kennung);


        }

        // Code stehen lassen und prüfen
        //public void TestTransponder()
        //{
        //    Starrflügelflugzeuge flieger1 = new Starrflügelflugzeuge("LH 3000", new Positionen(3000, 2000, 100));
        //    flieger1.Steuern();
        //    Console.WriteLine();

        //    Starrflügelflugzeuge flieger2 = new Starrflügelflugzeuge("LH 500", new Positionen(3500, 1500, 180));
        //    flieger1.Steuern();
        //    flieger2.Steuern();
        //    Console.WriteLine();


        //    Starrflügelflugzeuge flieger3 = new Starrflügelflugzeuge("LH 444", new Positionen(1730, 23400, 780));
        //    flieger1.Steuern();
        //    flieger2.Steuern();
        //    flieger3.Steuern();
        //    Console.WriteLine();

        //    Flieger 2 objekt beim Delegate abmelden 
        //    transponder -= flieger2.Transpond;
        //    flieger1.Steuern();
        //    flieger3.Steuern();
        //    Console.WriteLine();





        //}
        

        public void ProgrammTakten()
        {
            Console.WriteLine("\r{0}", DateTime.Now);
            //Starrflügelflugzeuge flieger1 = new Starrflügelflugzeuge("LH 3000", new Positionen(3000, 2000, 100));
            //flieger1.Starte(new Positionen(1000, 500, 200), 200);
            //Program.fliegerRegister += flieger1.Steuern;
            Starrflügelflugzeuge flieger2 = new Starrflügelflugzeuge("LH 500", new Positionen(3500, 1500, 180));
            flieger2.Starte(new Positionen(1000, 500, 200), 260);
            Program.fliegerRegister += flieger2.Steuern;
            
            while (true)
            {
                fliegerRegister();
                Console.WriteLine();
                Thread.Sleep(1000);
               
                
            }

            

        }

        public static TransponderDel transponder;
        public static FliergerRegisterDel fliegerRegister;

    }

    delegate void TransponderDel(string kennung, Positionen pos);

    delegate void FliergerRegisterDel();

}
