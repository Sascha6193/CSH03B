using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lektion1
{
    internal abstract class Luftfahrzeuge // Basis Klasse
    {
        protected Positionen pos;
        protected string kennung;

        public string Kennung
        {  
            get { return kennung; } 
            
        }

        public Luftfahrzeuge(string kennung, Positionen startPos)
        {
            this.kennung = kennung;
            this.pos = startPos;
        }


        public abstract void Steigen(int meter);
        public abstract void Sinken(int meter);

        protected Positionen zielPos;
        protected int streckeProTakt;
        public void Starte(Positionen zielPos, int streckenProTakt)
        {
            this.zielPos = zielPos;
            this.streckeProTakt = streckenProTakt;
        }

    }
}
