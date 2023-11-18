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
            protected set { kennung = value; } //Datenschutz! Damit die Kennung nicht einfach geändert werden kann musst du sie protecten
            get { return kennung; }            //gibt dir nur die Kennung an sich zurück, aber du lässt sie sonst überschreiben ohne dem protected!
            
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
