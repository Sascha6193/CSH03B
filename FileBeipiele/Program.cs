using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBeipiele
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program test = new Program();
            string path = @"D:\Entwicklung\CSH-Lehrgang\CSH03\FileBeipiele\Musterdatei2.bin";
            int[] pos = { 1400, 3250, 280 };
            test.BinaryWrite(path, pos);

            test.BinaryRead(path);


            #region Ausgeklammerter code 
            //string pfad = @"D:\Entwicklung\CSH-Lehrgang\CSH03\FileBeipiele\Musterdatei.txt";
            ////byte[] array = { 68, 97, 116, 101, 105 };
            //// test.DateiErstellen(pfad, array);
            //string content = @"Die Text wird Inhalt der Datei.
            //Er enthählt Zeilenumbrüche und ein Pfadangabe:
            //D:\Entwicklung\CSH-Lehrgang\CSH03\FileBeipiele\Musterdatei";
            //test.WriterNutzen(pfad, content);
            //test.ReaderNutzen(pfad);
            #endregion

            Console.ReadKey();

        }


        #region Methoden zum Verständniss  
        //public void DateiErstellen(string pfad, byte[] array) 
        //{
        //    FileStream stream = File.Open(pfad,FileMode.Create);
        //    stream.Write(array, 0, array.Length);
        //    stream.Close();
        //}

        //public void DateiLesen(string pfad)
        //{
        //    FileStream stream = File.Open(pfad,FileMode.Open);
        //    byte[] array = new byte[stream.Length];
        //    stream.Read(array, 0, (int)stream.Length); 
        //    for (int i = 0; i < array.Length; i++) 
        //    {
        //        Console.Write((char)array[i]);
        //    }
        //    Console.WriteLine();
        //    stream.Close();
        //}
        #endregion

        #region Methoden fürs einfaches lesen und schreiben von der Stream Klassen 
        public void WriterNutzen(string pfad,string content) 
        {
            StreamWriter writer = new StreamWriter(File.Open(pfad,FileMode.Create)); 
            writer.WriteLine(content);
            writer.Close();
        }

        public void ReaderNutzen(string pfad) 
        {
            StreamReader reader = new StreamReader(File.Open(pfad, FileMode.Open));
            string line; 
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
            //Console.WriteLine(reader.ReadLine()); Kann raus wird oben ersetzt in schleife 
            reader.Close();
        }
        #endregion

        #region Binary Methoden 
        public void BinaryWrite(string path, int[] content)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(path,FileMode.Create));
            for (int i = 0; i < content.Length; i++)
            {
                writer.Write(content[i]);
            }
            writer.Close();
        }

        public void BinaryRead( string path) 
        {
            BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));
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
