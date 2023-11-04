using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lektion1
{
    internal class Düsenflugzeuge : Starrflügelflugzeuge
    {
        protected Airbus typ;
        private int sitzplätze;
        private int fluggäste;
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

        // public int maxPlätze; 

        public Düsenflugzeuge(string kennung, Positionen pos, Airbus typ ) : base(kennung, pos)
        {
            this.typ = typ;
            sitzplätze = (int)typ;
            Console.WriteLine("Der Flieger vom Typ {0} hat {1} Plätze",this.GetType(),sitzplätze);

        }

        public void Buchen(int plätze)
        {
            Fluggäste = plätze;
        }
        
       
    }
}

