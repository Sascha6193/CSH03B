using Lektion1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Lektion5
{
    internal class Program
    {
        public static TransponderDel transponder;
        public delegate void FliergerRegisterDel();
        public static FliergerRegisterDel fliegerRegister;
        public static bool protokollieren = true;


        static void Main(string[] args)
        {

            Program Test = new Program();
            Test.ProgrammTakten();
            Console.WriteLine();

            //string s = "String";
            //Console.WriteLine(s.IndexOf('i'));
            //Console.WriteLine("Substring".Substring(4));
            //Console.WriteLine("Parameterwerte".Substring(4,10));

            #region Unnötig, lösch das raus
            // Todo kann wieder auskommentiert werden
            //Duesenflugzeuge flug = new Duesenflugzeuge("LH3000", new Positionen(500, 580, 200));
            //flug.Starte();
            #endregion

            #region Müll, erklärung im Kommentar daneben
            //Flugschreiber flugSchreiber = new Flugschreiber();                       //Warum deklarierst du das hier drin?
            //Console.WriteLine(flugSchreiber);                                        //Du musst ProgrammTakten nur im Main aufrufen, da sich dort alles abspielt.
            //Console.ReadKey();
            #endregion
        }



        class Flugschreiber
        {
            public string ProtokollDatei { get; private set; }
            private Starrfluegelflugzeuge sFlieger;
            private bool istProtokollactive;            //ist Protokoll aktiv?
            private bool iniFlugschreiber = true;      //Flugschreiber inizialisiert? false = nein. Wir deklarieren hier vorerst nur.

            public Flugschreiber(Starrfluegelflugzeuge sFl, bool iPrA)      //sFL = Starrflügel-Flugzeug, iaPr = ist Protokoll active
            {
                sFlieger = sFl;
                istProtokollactive = iPrA;


                string writepath = @"D:\Entwicklung\CSH-Lehrgang\CSH03\ESA\bin\Release\";
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

            public void PosProtokollieren(string kennung, Positionen pos)
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
            public void InizProtokoll(Positionen startPos, Positionen zielPos)
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

        public void ProgrammTakten()
        {
            #region Ausgeklammerter code 
            //Console.WriteLine("\r{0}", DateTime.Now);


            //Starrfluegelflugzeuge flieger1 = new Starrfluegelflugzeuge("LH 500", new Positionen(3500, 1500, 180));
            //Program.fliegerRegister += flieger1.Steuern;
            //flieger1.Starte(new Positionen(1000, 500, 200), 200, 300, 20, 10);

            //Starrfluegelflugzeuge flieger2 = new Starrfluegelflugzeuge("LH 3000", new Positionen(3000, 2000, 100));
            //Program.fliegerRegister += flieger2.Steuern;
            //flieger2.Starte(new Positionen(1000, 500, 200), 260, 350, 25, 15);
            #endregion

            #region  CSH03B Lektion 5

            Duesenflugzeuge flieger1 = new Duesenflugzeuge("LH 500", new Positionen(3500, 1500, 180), protokollieren);        //protokollieren hat gefehlt. & siehe Düsenflugzeugklasse! (Kommentar)
            Duesenflugzeuge flieger2 = new Duesenflugzeuge("LH 3000", new Positionen(3000, 2000, 100), protokollieren);       //Augen auf beim Würstchen-Kauf.

            while (fliegerRegister != null)
            {
                fliegerRegister();
                Console.WriteLine();
                Thread.Sleep(1000);


            }
            #endregion
            //hier noch 2 ESAOut Mehtoden aufrufen! Abschreiben ist nicht schwer, lass dich nur nicht erwischen, lol.
        }


    }
}
