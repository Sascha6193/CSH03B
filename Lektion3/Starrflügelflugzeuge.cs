using Lektion3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lektion1
{
    internal class Starrflügelflugzeuge : Flugzeuge, ITransponder 
    {
        public Starrflügelflugzeuge(string kennung, Positionen startPos) : base(kennung, startPos)
        {
            // Zentralen Transponderinstanz
            Lektion3.Program.transponder += new Lektion3.TransponderDel(Transpond);
        }

        public void Transpond(string kennung, Positionen pos)
        {
           
            if (kennung.Equals(this.kennung))
            {
                Console.WriteLine("{0} an Position x={1}, y={2}",kennung, pos.x,pos.y);
            }


            
            
        }

        public void Steuern()
        { 
            Lektion3.Program.transponder(kennung, pos);

            
            double a, b, alpha, a1, b1;

            bool gelandet = false;


           

            if (!gelandet)
            {
                a = zielPos.x - pos.x;
                b = zielPos.y - pos.y;
                alpha = Math.Atan2(b, a); // Winkel wird ausgerechnet 
                a1 = Math.Cos(alpha) * streckeProTakt; // Strecken länge wird ausgerechnet - Zwischenschritt (benötigt Winkel - alpha)
                b1 = Math.Sin(alpha) * streckeProTakt; // Strecken länge wird ausgerechnet - Zwischenschritt (benötigt Winkel - alpha)
                pos.PositionÄndern((int)a1, (int)b1, 0);
                if (Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)) < streckeProTakt)
                {
                    gelandet = true;
                    Console.WriteLine("\n{0} an Position x = {1}, y = {2} gelandet",kennung, pos.x, pos.y);
                    Program.fliegerRegister -= this.Steuern;
                    
                }
                
            }
                      
        }
    }
}
