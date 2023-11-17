using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lektion1
{
    internal class Flugzeuge : Luftfahrzeuge
    {
        public Flugzeuge(string kennung, Positionen starPos) : base(kennung, starPos)
        {
                      
        }

       


        public override void Steigen(int meter)
        {
            pos.PositionÄndern(0, 0, meter);
            Console.WriteLine(kennung + " Steigt " + meter + " Meter, Höhe = " + pos.h);
        }


        public override void Sinken(int meter)
        {
            Console.WriteLine(kennung + " Sinkt " + -meter + " Meter, Höhe = " + pos.h);
        }
    }
}
