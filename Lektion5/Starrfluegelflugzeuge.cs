using Lektion5;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Lektion1
{
    internal class Starrfluegelflugzeuge : Flugzeuge
    {
        public Starrfluegelflugzeuge(string kennung, Positionen startPos) : base(kennung, startPos)
        {
            // Zentralen Transponderinstanz
            
            // Program.transponder += Transpond; sonst Doppelte flieger 
        }

        double a, b, alpha, a1, b1;
        bool gelandet = false;
        public void Transpond(string kennung, Positionen pos)
        {
            

            double abstand = Math.Sqrt(Math.Pow(this.pos.x - pos.x, 2) + Math.Pow(this.pos.y - pos.y,2));

            if (kennung.Equals(this.kennung))
            {
                DateTime timetamp = DateTime.Now;
                Console.Write("{0}:{1}" ,timetamp.Minute, timetamp.Second);
                Console.Write("{0} an Position x={1}, y={2}, h={3}",kennung, pos.x,pos.y, pos.h);            
               
                //Wiederholungsaufgabenteil
                Console.Write(" Zieldistanz: {0}m \n", Zieldistanz());
            }
            else
            {
                Console.Write("\t{0} ist {1} m von {2} enfernt. \n", this.kennung, (int)abstand, kennung);
                if (Math.Abs(this.pos.h - pos.h) < 100 && abstand < 500)
                {
                    Console.WriteLine("\tWARNUNG: {0} hat nur {1} m Höhenabstand!", kennung,Math.Abs(this.pos.h - pos.h));
                }
            }


            Console.Write("\t{0}-Position : {1}-{2}-{3}",this.kennung, pos.x, pos.y, pos.h);
            Console.Write("Zieldistanz: m\n", Zieldistanz());




        }

        public void Steuern()
        {
            //Program.transponder(kennung, startPos);

            #region Ausgeklammerter  Code 
            // double a, b, alpha, a1, b1;
            //if (!gelandet)
            //{
            //a = zielPos.x - pos.x;
            //b = zielPos.y - pos.y;
            //alpha = Math.Atan2(b, a); // Winkel wird ausgerechnet 
            //a1 = Math.Cos(alpha) * streckeProTakt; // Strecken länge wird ausgerechnet - Zwischenschritt (benötigt Winkel - alpha)
            //b1 = Math.Sin(alpha) * streckeProTakt; // Strecken länge wird ausgerechnet - Zwischenschritt (benötigt Winkel - alpha)
            //pos.PositionÄndern((int)a1, (int)b1, 0);
            //    if (Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)) < streckeProTakt)
            //    {
            //        gelandet = true;
            //        Console.WriteLine("\n{0} an Position x = {1}, y = {2} gelandet", kennung, pos.x, pos.y);
            //        Program.fliegerRegister -= this.Steuern;
            //    }

            //}
            #endregion

            //bool gelandet = false;
            //bool steigt = true; 
            

            
            if (steigt )
            {
                if (this.SinkenEinleiten())
                {
                    steigt = false;
                    sinkt = true; 
                }
                else if (pos.h > flughoehe)
                {
                    steigt = false; 
                }
            }
            else if ( sinkt )
            {
                if ( pos.h <= zielPos.h + sinkhoeheProTakt )
                {
                    gelandet = true;
                }
                else
                { // Horizontalflug 
                    sinkt = true; 
                }
            }

            if (!gelandet)
            {
                // Zunächst soll aktuelle Position ausgeben: 
                Program.transponder(kennung, pos);

                //  stecke am Boden Berechen:

                if (steigt)
                {                         
                    double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(steighoeheProTakt, 2));
                    this.PositionsBerechnen(strecke, steighoeheProTakt);
                }
                else if (sinkt)
                {
                    double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(sinkhoeheProTakt, 2));
                    this.PositionsBerechnen(strecke, -sinkhoeheProTakt);
                }
                else
                {
                    // im Horizontalflug ist strecke gleich streckeProTakt
                    this.PositionsBerechnen(streckeProTakt, 0);
                }
            }
            else
            {
                // Flieger deregistrieren, Tansponder abschalten, Abschlussmeldung 

                Program.fliegerRegister -= this.Steuern;
                Program.transponder -= this.Transpond;

                Console.WriteLine("\n{0} gelandet ( Zieldistanz={1}, Hohendistanz={2} ", kennung, Zieldistanz(), pos.h - zielPos.h);
                
            }
        }

        private bool SinkenEinleiten()
        {
            double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(sinkhoeheProTakt,2));           
            int sinkstrecke = (int)(strecke * (pos.h - zielPos.h ) / sinkhoeheProTakt);
            int zieldistanz = Zieldistanz();

            if (sinkstrecke >= zieldistanz) 
            {
                Console.WriteLine("{0} Sinkstrecke {1} >= Zieldistanz {2}", kennung, sinkstrecke, zieldistanz );
                return true; 
            }
            else
            {
                return false;
            }
            
            
                       
        }
        // Bei denn sinkflug ist ein Negativer Wert für denn zweiten Parameter anzugeben 
        private void PositionsBerechnen(double strecke, int steighöheProTakt)
        {

            a = zielPos.x - pos.x;
            b = zielPos.y - pos.y;

            alpha = Math.Atan2(a, b);

            a1 = Math.Cos(alpha) * strecke;
            b1 = Math.Sin(alpha) * strecke;

            pos.PositionÄndern((int)a1, (int)b1, steighöheProTakt);
        }

        private int Zieldistanz()
        {
            return (int)Math.Sqrt(Math.Pow(zielPos.x - pos.x, 2) + Math.Pow(zielPos.y - pos.y, 2));
        }
    }
}

