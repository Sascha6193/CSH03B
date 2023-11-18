using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lektion1
{
    internal class Düsenflugzeuge : Starrflügelflugzeuge, ITransponder
    {
        protected Airbus typ;
        private int sitzplätze;
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
                if (sitzplätze < (fluggäste + value))
                {
                    Console.WriteLine("Keine Buchung: Die Fluggastzahl würde mit der " +
                                      "Zubuchung von {0} Plätzen die verfügbaren Plätze " +
                                      "von {1} um {2} übersteigen ", value, sitzplätze, value + fluggäste - sitzplätze);
                }
                else
                {
                    fluggäste += value;
                }
            }
            get { return fluggäste; }
        }
        

        public Düsenflugzeuge(string kennung, Positionen startPos, Airbus typ ) : base(kennung, startPos)
        {
            this.typ = typ;
            sitzplätze = (int)typ;
            Console.WriteLine("Der Flieger vom Typ {0} hat {1} Plätze", this.GetType(), sitzplätze);

        }

        public void Buchen(int plätze)
        {
            Fluggäste = plätze;
        
        }
       
    }
}

