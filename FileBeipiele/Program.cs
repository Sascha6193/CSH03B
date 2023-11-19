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
            string pfad = @"D:\Entwicklung\CSH-Lehrgang\CSH03\FileBeipiele\bin\Release";
            //byte[] array = { 68, 97, 116, 101, 105 };
            // test.DateiErstellen(pfad, array);
            string content = @"Die Text wird Inhalt der Datei.
Er enthählt Zeilenumbrüche und ein Pfadangabe:
D:\Entwicklung\CSH-Lehrgang\CSH03\FileBeipiele\bin\Release";
            test.WriterNutzen(pfad, content);
            test.ReaderNutzen(pfad);



        }

        public void DateiErstellen(string pfad, byte[] array) 
        {
            FileStream stream = File.Open(pfad,FileMode.Create);
            stream.Write(array, 0, array.Length);
            stream.Close();
        }

        public void DateiLesen(string pfad)
        {
            FileStream stream = File.Open(pfad,FileMode.Open);
            byte[] array = new byte[stream.Length];
            stream.Read(array, 0, (int)stream.Length); 
            for (int i = 0; i < array.Length; i++) 
            {
                Console.Write((char)array[i]);
            }
            Console.WriteLine();
            stream.Close();
        }

        public void WriterNutzen(string pfad,string content) 
        {
            StreamWriter writer = new StreamWriter(File.Open(pfad,FileMode.Create)); 
            writer.WriteLine(content);
            writer.Close();
        }

        public void ReaderNutzen(string pfad) 
        {
            StreamReader reader = new StreamReader(File.Open(pfad, FileMode.Open));
            Console.WriteLine(reader.ReadLine());
            reader.Close();
        }

    }
}
