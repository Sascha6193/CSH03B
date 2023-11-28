using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESA_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program myObj = new Program();   
            string dateinamen = @".\ESA2.txt";
            byte[] ar = {32, 32, 67, 67, 32, 32, 32, 35, 32, 35, 32,

                         32, 67, 32, 32, 67, 32, 32, 35, 32, 35, 32,

                         67, 32, 32, 32, 32, 32, 35, 35, 35, 35, 35,

                         67, 32, 32, 32, 32, 32, 32, 35, 32, 35, 32,

                         67, 32, 32, 32, 32, 32, 35, 35, 35, 35, 35,

                         32, 67, 32, 32, 67, 32, 32, 35, 32, 35, 32,

                         32, 32, 67, 67, 32, 32, 32, 35, 32, 35, 32 
            }; 
            myObj.ESA2In(dateinamen, ar);
            myObj.ESA2Out(dateinamen);
        }
           


        
        


        #region Richtige Einsendeaufgabe 
        public void ESA2In(string dateiname, byte[] arrays)
        {

            BinaryWriter stream = new BinaryWriter(File.Open(dateiname, FileMode.Create));
            for (int i = 0; i < arrays.Length; i++)
            {
                stream.Write(arrays[i]); // schreibe bis Ende des Arrays
            }
            stream.Close();
            Console.WriteLine("Datei wurde Angelegt, Bitte drücke Taste für nächsten Schritt");
            Console.ReadKey();
            Console.Clear();
        }
        

        public void ESA2Out(string dateiname)
        {
            BinaryReader stream = new BinaryReader(File.Open(dateiname,FileMode.Open));
            int toReadBytes = 0;
            byte[] temp = new byte[11];
            do
            {
                toReadBytes = stream.Read(temp, 0, 11);
                for (int i = 0;i < toReadBytes; i++)
                {
                    Console.Write("{0}", (char)temp[i]);
                }
                Console.WriteLine();
            } while (toReadBytes > 0);

            stream.Close();
            Console.WriteLine("Bitte Taste drücken");
            Console.ReadKey();
        }

        #endregion
    }
}
