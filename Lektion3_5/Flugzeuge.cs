using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lektion1
{
    internal class Flugzeuge : Luftfahrzeuge
    {
        //protected Positionen zielPos;            //brauchst du HIER Beides NICHT, da du das in Luftfahrzeufe bereits deklariert hast. Vererbung!
        //protected int streckeProTakt;            
        protected int flughöhe;
        protected int steighöheProTakt;
        protected int sinkhöheProTakt;
        protected bool steigt = false;
        protected bool sinkt = false;
        public void Starte(Positionen zielPos, int streckeProTakt, int flughöhe, int steighöheProTakt, int sinkhöheProTakt)
        {
            this.zielPos = zielPos;
            this.streckeProTakt = streckeProTakt;

            this.flughöhe = flughöhe;
            this.steighöheProTakt = steighöheProTakt;
            this.sinkhöheProTakt = sinkhöheProTakt;
            this.steigt = true;

        }

        public Flugzeuge(string kennung, Positionen startPos) : base(kennung, startPos)
        {
            this.kennung = kennung;
            this.pos = startPos;
        }

        public override void Steigen(int meter)
        {
            pos.HöheÄndern(meter); //Nicht PositionÄndern verwenden. Da wir hier "steigen" müssen, ist es die Höhe! Methode habe ich ergänzt in "Positionen"
            Console.WriteLine(kennung + " Steigt " + meter + " Meter, Höhe = " + pos.h);
        }


        public override void Sinken(int meter)
        {
            pos.HöheÄndern(-meter); //Nicht PositionÄndern verwenden. Da wir Höhe in Meter abziehen müssen, wird Minus gerechnet!
            Console.WriteLine(kennung + " Sinkt " + meter + " Meter, Höhe = " + pos.h);               // Das Minus kann hier weg, da wir es oben einsetzen (übersichtlicher zumal)
        }
    }
}
