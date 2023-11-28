using Lektion5;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lektion1
{
    internal class Duesenflugzeuge : Starrfluegelflugzeuge, ITransponder
    {

        public Airbus typ;
        private int sitzplaetze;
        private int fluggäste;
        public int maxPlätze;

        public int MaxPlätze                  //definierung zu maxPlätze, wir können nur die verfügbaren Plätze nutzen eines Fliegers!
        {
            set                               //maximalwert setzen (durch enum Airbus typ, vordefinierte Werte)
            {
                maxPlätze = value;
            }
            get                               
            {
                return maxPlätze;
            }
        }

        public int Fluggäste
        {
            set
            {
                if (sitzplaetze < (fluggäste + value))
                {
                    Console.WriteLine("Keine Buchung: Die Fluggastzahl würde mit der " +
                                      "Zubuchung von {0} Plätzen die verfügbaren Plätze " +
                                      "von {1} um {2} übersteigen ", value, sitzplaetze, value + fluggäste - sitzplaetze);
                }
                else
                {
                    fluggäste += value;
                }
            }
            get { return fluggäste; }
        }
        

        public Duesenflugzeuge(string kennung, Positionen startPos) : base(kennung, startPos)
        {
            bool initialized = this.Starte();
            if (initialized)
            {
                Program.transponder += this.Transpond;
                Program.fliegerRegister += this.Steuern;
            }
            Console.WriteLine("Der Flieger vom Typ {0} hat {1} Plätze", this.GetType(), sitzplaetze);

        }

        public void Buchen(int plätze)
        {
            Fluggäste = plätze;
        
        }

        #region CSH03B Lektion 5
        public bool Starte()
        {
            string pfad = @".\" + Kennung + ".init";
            StreamReader reader;
            try
            {
                reader = new StreamReader(File.Open(pfad,FileMode.Open));
            }
            catch (IOException e) 
            {
                Console.WriteLine("{0} Fehler beim Zugriff auf Datei" + pfad, e.GetType().Name);
                return false;
            }
            int[] data = new int[9];
            for ( int i = 0; i < 9 ; i++ ) 
            {
                string str = reader.ReadLine();
                str = str.Substring(str.IndexOf('=') + 1);
                // zur Komtrolle, später auskomenntiren:
                Console.WriteLine(str);
                data[i] = Int32.Parse(str);
                
            }
            reader.Close();

            this.zielPos.x = data[0];
            this.zielPos.y = data[1];  
            this.zielPos.h = data[2];
            streckeProTakt = data[3];
            flughoehe = data[4];
            steighoeheProTakt = data[5];
            sinkhoeheProTakt = data[6];
            Array typen = Enum.GetValues(typeof(Airbus));
            this.typ = (Airbus)typen.GetValue(data[7]);
            /* inizialiesierung */ sitzplaetze = data[8];
            Console.WriteLine("Flug {0} von Typ {1} mit {2} Pleatzen initialliesiert.",kennung,typ, sitzplaetze);
            steigt = true;
            return true;
            #endregion
        }

    }
}

