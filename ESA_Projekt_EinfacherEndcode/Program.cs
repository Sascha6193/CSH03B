using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net.Http.Headers;

namespace ESA_Projekt_EinfacherEndcode
{
    public delegate void TransponderDel(string kennung, Position pos);
    interface ITransponder
    {
        void Transpond(string kennung, Position pos);
    }

    enum Düsenflugzeugtyp : short
    {
        A300, A310, A318, A319, A320, A321, A330, A340, A380, Boeing_717, Boeing_737, Boeing_747, Boeing_777, Boeing_BBJ
    }
    enum Airbus : short
    {
        _A300, _A310, _A318, _A319, _A320, _A321, _A330,
        _A340, _A350, _A380

        //A300, A310, A318, A319, A320, A321, A330,
        //A340, A350, A380

    }
    public struct Position
    {
        public int x, y, h;
        public Position(int x, int y, int h)
        {
            this.x = x;
            this.y = y;
            this.h = h;
        }
        public void PositionAendern(int deltaX, int deltaY, int deltaH)
        {
            x = x + deltaX;
            y = y + deltaY;
            h = h + deltaH;
        }
        public void HöheÄndern(int deltaH)//Aufruf in Sinken/Steigen
        {
            h = h + deltaH;
        }
    }

    class Flugschreiber
    {
        public string ProtokollDatei { get; private set; }
        private Starrflügelflugzeug sFlieger;
        private bool istProtokollactive;            //ist Protokoll aktiv?
        private bool iniFlugschreiber = true;      //Flugschreiber inizialisiert? false = nein. Wir deklarieren hier vorerst nur.

        public Flugschreiber(Starrflügelflugzeug sFl, bool iPrA)           //sFL = Starrflügel-Flugzeug, iaPr = ist Protokoll active
        {
            sFlieger = sFl;
            istProtokollactive = iPrA;


            string writepath = @"G:\Fernstudium C#\VS Projekte\BGF460\CSH03\Einsendeaufgabe_CSH03\bin\Release\";
            string zeitstempel = Convert.ToString(DateTime.Now.DayOfWeek) + "-" + Convert.ToString(DateTime.Now.Hour)
            + "-" + Convert.ToString(DateTime.Now.Minute) + "-" + Convert.ToString(DateTime.Now.Second);

            //Dateierstellung: Pfad, Kennung + DateTime (Aufgabe 3 ESA)
            ProtokollDatei = writepath + sFlieger.Kennung + "_" + zeitstempel + ".bin";

        }

        public string ZeitStempel()   //ESA Aufgabe 3
        {
            #region zeitstempel definition
            //Zur Übersichtlichkeit im Code (für Zeitstempel im Dateinamen mit Trennzeichen)
            //string day = Convert.ToString(DateTime.Now.DayOfWeek) + "-";    //DayOfWeek = z.B.: Monday (= String-Ausgabe)
            //string hour = Convert.ToString(DateTime.Now.Hour) + "-";
            //string minute = Convert.ToString(DateTime.Now.Minute) + "-";
            //string second = Convert.ToString(DateTime.Now.Second);
            //string zeitstempel = day + hour + minute + second;

            ////oder als Kurzfassung:
            ////string zeitstempel = Convert.ToString(DateTime.Now.DayOfWeek) + "-" + Convert.ToString(DateTime.Now.Hour)
            ////                      + "-" + Convert.ToString(DateTime.Now.Minute) + "-" + Convert.ToString(DateTime.Now.Second);
            #endregion
            string zeitstempel = Convert.ToString(DateTime.Now.DayOfWeek) + "-" + Convert.ToString(DateTime.Now.Hour)
                      + "-" + Convert.ToString(DateTime.Now.Minute) + "-" + Convert.ToString(DateTime.Now.Second);

            return zeitstempel;
        }

        public void PosProtokollieren(string kennung, Position pos)
        {
            if (!iniFlugschreiber)
            {
                throw new Exception("Fligschreiber muss initialisiert werden, bevor er genutzt werden kann!");
            }

            if (kennung == sFlieger.Kennung)
            {
                string content2 = pos.x + "-" + pos.y + "-" + pos.h;
                FinishWrite(content2);
            }

        }
        public void InizProtokoll(Position startPos, Position zielPos)
        {
            #region Übersichtlichkeitskürzel

            //Zur Übersichtlichkeit im Code (Positionsangaben des Headers mit Trennzeichen)
            string zPosHead = zielPos.x + "-" + zielPos.y + "-" + zielPos.h;                 //ZielPositionHeader (zPosHead)
            string posHead = startPos.x + "-" + startPos.y + "-" + startPos.h;                              //PositionHeader (posHead)
            #endregion

            string content = @"Flug mit Kennung & Typ " + sFlieger.Kennung + sFlieger.typ + " startet an Position:" + posHead + " mit Zielposition: " + zPosHead;
            istProtokollactive = true;                                                       //Protokoll wird inizialisiert / aktiviert sobald diese Methode aufgerufen wird.
            FinishWrite(content);

        }

        private void FinishWrite(string content)
        {
            if (istProtokollactive)
            {
                BinaryWriter bWriter = new BinaryWriter(File.Open(ProtokollDatei, FileMode.Append));

                bWriter.Write(content);
                //bWriter.Flush();                 //Flush = Löscht die Puffer für diesen Datenstrom,
                                                 //veranlasst die Ausgabe aller gepufferten Daten in die Datei und löscht zudem alle Zwischendateipuffer.
                bWriter.Close();
            }
        }
    }


    abstract class Luftfahrzeug
    {
        protected string kennung;
        public string Kennung
        {
            protected set { kennung = value; }
            get { return kennung; }

        }

        private int sitzplaetze;
        private int fluggaeste;
        public int Fluggaeste
        {
            set
            {
                if (sitzplaetze < (fluggaeste + value))
                    Console.WriteLine("Keine Buchung: Die " + "Fluggastzahl würde mit der Zubuchung " + "von {0} Plätzen die verfügbaren Plätze "
                        + "von {1} um {2} übersteigen!", value, sitzplaetze, value + fluggaeste - sitzplaetze);
                else
                    fluggaeste += value;
            }
            get { return fluggaeste; }
        }

        protected Position pos;
        public Luftfahrzeug(string kennung, Position startPos)
        {
            Kennung = kennung;
            this.pos = startPos;
        }

        public abstract void Steigen(int meter);
        public abstract void Sinken(int meter);
    }
    class Flugzeug : Luftfahrzeug
    {
        protected Position zielPos;
        protected int streckeProTakt;
        protected int flughoehe;
        protected int steighoeheProTakt;
        protected int sinkhoeheProTakt;
        protected bool steigt = false;
        protected bool sinkt = false;

        public void Starte(Position zielPos, int streckeProTakt, int flughoehe, int steighoeheProTakt, int sinkhoeheProTakt)
        {
            this.zielPos = zielPos;
            this.streckeProTakt = streckeProTakt;
            this.flughoehe = flughoehe;
            this.steighoeheProTakt = steighoeheProTakt;
            this.sinkhoeheProTakt = sinkhoeheProTakt;
            this.steigt = true;
        }
        public Flugzeug(string kennung, Position startPos) : base(kennung, startPos)
        {
            this.kennung = kennung;
            this.pos = startPos;
        }
        public override void Steigen(int meter)
        {
            pos.HöheÄndern(meter);
            Console.WriteLine(kennung + "steigt " + meter + "Meter, Höhe =" + pos.h);
        }
        public override void Sinken(int meter)
        {
            pos.HöheÄndern(-meter);
            Console.WriteLine(kennung + " sinkt " + meter + " Meter, Höhe = " + pos.h);
        }
    }
    class Starrflügelflugzeug : Flugzeug, ITransponder
    {
        double a, b, alpha, a1, b1;
        bool gelandet = false;
        public Flugschreiber Flugschreiber { get; protected set; }

        public Düsenflugzeugtyp typ;
        public Starrflügelflugzeug(string kennung, Position startPos, bool flugschreiber) : base(kennung, startPos)
        {
        }
        public void Transpond(string kennung, Position pos)
        {
            DateTime timestamp = DateTime.Now;
            double abstand = Math.Sqrt(Math.Pow(this.pos.x - pos.x, 2) + Math.Pow(this.pos.y - pos.y, 2));
            if (kennung.Equals(this.kennung))
            {
                Console.Write("{0}:{1}", timestamp.Minute, timestamp.Second);
                Console.Write("\t{0}-Position: {1}-{2}-{3}", this.kennung, pos.x, pos.y, pos.h);
                Console.Write(" Zieldistanz: {0} m\n", Zieldistanz());
            }
            else
            {
                Console.Write("\t{0} ist {1} m von {2} entfernt.\n", this.kennung, (int)abstand, kennung);
                if (Math.Abs(this.pos.h - pos.h) < 100 && abstand < 500)
                {
                    Console.WriteLine("\tWARNUNG: {0} hat nur {1} m Höhenabstand!", kennung, Math.Abs(this.pos.h - pos.h));
                }
            }

            //Beim transpond muss der FLugschreibe mit rein:
            Flugschreiber.PosProtokollieren(kennung, pos);
        }

        private bool SinkenEinleiten()
        {
            double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(sinkhoeheProTakt, 2));
            int sinkstrecke = (int)(strecke * (pos.h - zielPos.h) / sinkhoeheProTakt);
            int zieldistanz = Zieldistanz();
            if (sinkstrecke >= zieldistanz)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void PositionBerechnen(double strecke, int steighöheProTakt)
        {
            // modifizierte Übernahme der bisherigen Berechungen aus "Steuern"
            a = zielPos.x - pos.x;
            b = zielPos.y - pos.y;
            alpha = Math.Atan2(b, a);
            a1 = Math.Cos(alpha) * strecke;
            b1 = Math.Sin(alpha) * strecke;
            pos.PositionAendern((int)a1, (int)b1, steighöheProTakt);
        }
        private int Zieldistanz()
        {
            return (int)Math.Sqrt(Math.Pow(zielPos.x - pos.x, 2) + Math.Pow(zielPos.y - pos.y, 2));
        }
        public void Steuern()
        {
            if (steigt)
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
            else if (sinkt)
            {
                if (pos.h <= zielPos.h + sinkhoeheProTakt)
                    gelandet = true;
            }
            else
            {
                //Horizontalflug
                if (this.SinkenEinleiten())
                {
                    sinkt = true;
                }
            }
            if (!gelandet)
            {
                // Zunächst die aktuelle Position ausgeben:
                Program.transponder(kennung, pos);
                //"Strecke" (am Boden) berechnen:
                if (steigt)
                {
                    double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(steighoeheProTakt, 2));
                    this.PositionBerechnen(strecke, steighoeheProTakt);
                }
                else if (sinkt)
                {
                    double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(sinkhoeheProTakt, 2));
                    this.PositionBerechnen(strecke, -sinkhoeheProTakt);
                }
                else
                {
                    // im Horizontalflug ist "strecke" gleich "streckeProTakt"
                    this.PositionBerechnen(streckeProTakt, 0);
                }
            }
            else
            {
                // Flieger derigistrieren, Transponder abschalten, Abschlussmeldung
                Program.fliegerRegister -= this.Steuern;
                Program.transponder -= this.Transpond;
                Console.WriteLine("\n {0} gelandet Zieldistanz= {1}, Höhendistanz= {2}", kennung, Zieldistanz(), pos.h - zielPos.h);
            }

        }
    }
    class Düsenflugzeug : Starrflügelflugzeug, ITransponder
    {

        private int sitzplaetze;
        public Airbus typ;
        public Düsenflugzeug(string kennung, Position startPos, bool useFlugschreiber) : base(kennung, startPos, useFlugschreiber)
        {

            bool inizialized = this.Starte();
            if (inizialized)
            {
                Flugschreiber = new Flugschreiber(this, useFlugschreiber);
                Flugschreiber.InizProtokoll(startPos, zielPos);

                //transponder & fliegeregister aufrufen
                Program.transponder += this.Transpond;
                Program.fliegerRegister += this.Steuern;
            }

        }

        //public virtual void Buchen(int plätze)
        //{
        //    Fluggeaste = plätze;
        //}

        public bool Starte()
        {
            //Zur Übersichtlichkeit im Code (Pfade der .int's (Reader) + .bin's(Writer) um platz im Code zu sparen)
            string readpath = ".\\";

            //Reader: ließt die Daten ein
            string pfad = readpath + this.kennung + ".init";

            StreamReader reader;
            try
            {
                reader = new StreamReader(File.Open(pfad, FileMode.Open));
            }
            catch (IOException e)
            {
                Console.WriteLine("{0} Fehler beim Zugriff auf Datei" + pfad, e.GetType().Name);
                return false;
            }

            int[] data = new int[9];
            for (int i = 0; i < 9; i++)
            {
                string str = reader.ReadLine();
                str = str.Substring(str.IndexOf("=") + 1);
                ////zur Komtrolle, später auskomenntieren:
                //Console.WriteLine(str);
                data[i] = Int32.Parse(str);
            }
            reader.Close();

            this.zielPos.x = data[0];
            this.zielPos.y = data[1];
            this.zielPos.h = data[2];
            streckeProTakt = data[3];
            flughoehe = data[4];
            steighoeheProTakt = data[5];
            sinkhoeheProTakt = data[6];

            Array typen = Enum.GetValues(typeof(Airbus));
            this.typ = (Airbus)typen.GetValue(data[7]);
            sitzplaetze = data[8];

            Console.WriteLine("Flug {0} vom Typ {1} mit {2} Plätzen initialisiert.", kennung, typ, sitzplaetze);
            Console.WriteLine("Flug mit Kennung & Typ " + kennung + typ + " startet an Position: {0}-{1}-{2} " +
                              "mit Zielposition: {3}-{4}-{5}", this.pos.x, this.pos.y, this.pos.h, this.zielPos.x, this.zielPos.y, this.zielPos.h);
            Console.WriteLine(); //Leerzeile
            //reader.Close();      //schließen des StreamReaders

            steigt = true;
            return true;
        }
    }

    class Program
    {
        public static TransponderDel transponder;
        public delegate void FliegerRegisterDel();
        public static FliegerRegisterDel fliegerRegister;
        public static bool protokollieren = true;

        public void ESA4Out(string protokollpfad)
        {
            BinaryReader bReader = new BinaryReader(File.Open(protokollpfad, FileMode.Open));
            Console.WriteLine("\n" + bReader.ReadString() + "\n");
            var datalänge = bReader.BaseStream.Length;
            while (bReader.BaseStream.Position != datalänge)
            {
                foreach (var zahl in bReader.ReadString().Split('-'))
                {
                    Console.Write("\t" + zahl);
                }
            }
            Console.WriteLine();

        }

        public void ProgrammTaken()
        {
            //Console.WriteLine("\r{0}", DateTime.Now);

            Düsenflugzeug flieger1 = new Düsenflugzeug("LH 500", new Position(3500, 1500, 180), protokollieren);
            Düsenflugzeug flieger2 = new Düsenflugzeug("LH 3000", new Position(3000, 2000, 100), protokollieren);

            while (fliegerRegister != null)
            {
                fliegerRegister();
                Console.WriteLine();
                Thread.Sleep(1000);
            }
            Console.WriteLine();

            ESA4Out(flieger1.Flugschreiber.ProtokollDatei);
            ESA4Out(flieger2.Flugschreiber.ProtokollDatei);

        }

        static void Main(string[] args)
        {
            Program test = new Program();
            test.ProgrammTaken();
            Console.ReadLine();

            //string s = "String";
            //Console.WriteLine(s.IndexOf('i'));
            //Console.WriteLine("Substring".Substring(3));
            //Console.WriteLine("Parameterwerte".Substring(4, 5));


        }
    }
}
