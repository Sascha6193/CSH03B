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
            //Lektion3.Program.transponder += new Lektion3.TransponderDel(Transpond);
            Program.transponder += Transpond;
        }

        double a, b, alpha, a1, b1;
        bool gelandet = false;
        public void Transpond(string kennung, Positionen pos)
        {
           
            if (kennung.Equals(this.kennung))
            {
                Console.WriteLine("{0} an Position x={1}, y={2}",kennung, pos.x,pos.y);
            }


            
            
        }

        public void Steuern()
        { 
            Program.transponder(kennung, pos);


            #region Ausgeklammerter  Code 
            // double a, b, alpha, a1, b1;



            //if (!gelandet)
            //{
            //    a = zielPos.x - pos.x;
            //    b = zielPos.y - pos.y;
            //    alpha = Math.Atan2(b, a); // Winkel wird ausgerechnet 
            //    a1 = Math.Cos(alpha) * streckeProTakt; // Strecken länge wird ausgerechnet - Zwischenschritt (benötigt Winkel - alpha)
            //    b1 = Math.Sin(alpha) * streckeProTakt; // Strecken länge wird ausgerechnet - Zwischenschritt (benötigt Winkel - alpha)
            //    pos.PositionÄndern((int)a1, (int)b1, 0);
            //    if (Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)) < streckeProTakt)
            //    {
            //        gelandet = true;
            //        Console.WriteLine("\n{0} an Position x = {1}, y = {2} gelandet", kennung, pos.x, pos.y);
            //        Program.fliegerRegister -= this.Steuern;
            //    }

            //}
            #endregion

            bool gelandet = false;
            bool steigt = true; 
            // Todo

            
            if (steigt )
            {
                if (this.SinkenEinleiten())
                {
                    steigt = false;
                    sinkt = true; 
                }
                else if (pos.h > flughöhe)
                {
                    steigt = false; 
                }
            }
            else if ( sinkt )
            {
                if ( pos.h <= zielPos.h + sinkhöheProTakt )
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
                    // Berenung flasch
                    // double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - this.PositionsBerechnen(strecke, steigenProTakt);
                    double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(steighöheProTakt, 2));
                    this.PositionsBerechnen(strecke, steighöheProTakt);
                }
                else if (sinkt)
                {
                    double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(sinkhöheProTakt, 2));
                    this.PositionsBerechnen(strecke, -sinkhöheProTakt);
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

                //Console.WriteLine("\n{0} gelandet ( Zieldistanz={1}, Hohendistanz={2} ", kennung, Zieldistanz(), posh.h - zielPos.h);
                Console.WriteLine($"\nFlugzeug {kennung} gelandet (Zieldistanz = {Zieldistanz()}, Höhendistanz = {pos.h - zielPos.h})");
            }
        }

        

        private bool SinkenEinleiten()
        {
            double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2)) - Math.Pow(sinkhöheProTakt,2);

            int sinkstrecke = (int)(strecke * (pos.h - zielPos.h ) / sinkhöheProTakt);

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

