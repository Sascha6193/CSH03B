using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lektion1
{
    internal class Starrflügelflugzeuge : Flugzeuge, ITransponder 
    {
        public Starrflügelflugzeuge(string kennung, Positionen pos) : base(kennung, pos)
        {
            // Zentralen Transponderinstanz
            Program.transponder += new TransponderDel(Transpond);
        }

        public void Transpond(string kennung, Positionen pos)
        {
           

            if (kennung.Equals(this.kennung))
            {
                Console.WriteLine(this.kennung + " erkennt Eingang eigenes Signal");
            }
            else if (this.pos.h - pos.h < 100 && this.pos.h - pos.h > 0 )
            {
                Console.WriteLine("Warnung: {0} fliegt nur {1} Meter höher als {2}", this.kennung, this.pos.h - pos.h ,kennung);
            }
            else if  (pos.h - this.pos.h < 100 && pos.h - this.pos.h > 0)
            {
                Console.WriteLine("Warnung: {0} fliegt nur {1} Meter tiefer als {2}", this.kennung, pos.h - this.pos.h ,kennung);
            }
            
        }

        public void Steuern()
        { 
            Program.transponder(kennung, pos);
        }
    }
}
