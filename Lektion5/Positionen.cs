using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lektion1
{
    public struct Positionen
    {
        public int x,y,h;
        public Positionen(int x, int y, int h)
        {
            this.x = x;
            this.y = y;
            this.h = h;
        }

        public void PositionÄndern(int deltaX, int deltaY, int deltaH)
        {
            x = x + deltaX;
            y = y + deltaY;
            h = h + deltaH;
        }

        public void HöheÄndern(int deltaH)  //Aufruf in Sinken/Steigen           
        {
            h = h + deltaH;
        }

    }
    public delegate void TransponderDel(string kennung, Positionen pos);

}
