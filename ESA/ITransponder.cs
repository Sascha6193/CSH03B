using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lektion1
{
    internal interface ITransponder
    {
        void Transpond(string kennung, Positionen pos);
        
    }
}
