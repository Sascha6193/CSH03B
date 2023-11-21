using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Wiederholungaufgaben
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program test = new Program();
            string pfad = @"D:\Entwicklung\CSH-Lehrgang\CSH03\Wiederholungaufgaben\WiederholungsDatei42.txt";
            byte[] array = { 68, 97, 116, 101, 105 };
            //test.DateiErstellen(pfad, array);
            //test.ReaderNutzen(pfad);
            test.BinaryRead(pfad);
        }
        // Gibt array nicht aus (im explorer) 
        #region Lektion 4.2 Wiederholungsaufgabe 
        public void DateiErstellen(string pfad, byte[] array)
        {
            FileStream stream = File.Open(pfad, FileMode.Create);
            for ( int i = 0; i < array.Length; i++ ) 
            {
                stream.WriteByte(array[i]);
            }
            stream.Close();
        }
        #endregion 

        // Zeigt auf eine Exception (Methode ist 1:1 mit Lösung B 4.3)
       //#region Lektion 4.3 Wiederholungsaufgabe 
       // public void ReaderNutzen(string pfad)
       // {
       //     //StreamReader reader = new StreamReader(File.Open(pfad, FileMode.Open));
       //     int zeichen;
       //     while ((zeichen = reader.Read()) != -1)
       //     {
       //         Console.Write((char)zeichen);
       //         Console.WriteLine();
       //         reader.Close();
       //     }


       // }
       // #endregion

        #region Lektion 4.4 Wiederholungsaufgabe
        public void BinaryRead(string pfad)
        {
            BinaryReader reader = null;
            try
            {
                reader = new BinaryReader(File.Open(pfad, FileMode.Open));
            }
            catch 
            {
                Console.WriteLine("Die Datei \" {0} \" wurde nicht gefunden.", pfad );
                return;
            }

            bool goOn = true;
            while (goOn)
            {
                try
                {
                    Console.Write("{0} ", reader.ReadInt32());
                }
                catch (EndOfStreamException)
                {
                    goOn = false;
                }
            }
            reader.Close();
            Console.WriteLine();
        }
        #endregion

    }
}
