using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ESA_Projekt;
using System.Runtime.Remoting.Messaging;

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

    #region  Einsendeaufgabe 3 ESA_Projekt
    public class FlugSchreiber
    {
        // 3.a
        private BinaryWriter writer;
        private string kennung;
        private string typ;

        
        // Methode zum schreiben von Positionsinformationen
        public void WritePositionData(Positionen positionen) 
        {
            // 3.e
            if (ProgramESAProjket.protokollieren) // überprüfe denn schalter
            {
                // 3.c
                // Schreibe Positionsinformation
                writer.Write(positionen.x);
                writer.Write(positionen.y);
                writer.Write(positionen.h);
            }
        }

        
        
        public void StartFlugSchreiber()
        {
            if(ProgramESAProjket.protokollieren)
            {
                // 3.b
                string filePath = ConstructFilePath();// implementiere diese Methode 

                writer = new BinaryWriter(File.Open(filePath, FileMode.Create));

                string header = ConstructHeader(); // implementiere diese Methode 
                writer.Write(header);  
            }
        }

        private string GetTimetamp()
        {
            // Todo überprüffen ob Mehtode richtig geschrieben 
            string timestamp = DateTime.Now.ToString();

            return timestamp;
        }

        private string ConstructFilePath()
        {
             

            string timestamp = GetTimetamp(); // implementiere diese Methode

            string fileName = $"kennung_{timestamp}.bin"; 
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            
            return filePath;
        }

        private string ConstructHeader() 
        {
            string flugInfo = $"Flug\"{kennung}\" (Typ\"{typ}\") startet an Position" + 
            $"\"startPos\" mit Zielposition\"zielPos\"";

            return flugInfo;
        }

        // 3.d
        public void StopWriter() 
        {
            if (writer != null)
            { 
                writer.Close(); 
            }

        }
    }
    #endregion
}
