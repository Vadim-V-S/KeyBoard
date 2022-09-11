using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoard.Model
{
    internal class DiphtongSymbol : TranscriptSymbol
    {
        public DiphtongSymbol()
        {
            ListSymbols(618, 601);
            ListSymbols(101, 618);
            ListSymbols(650, 601);
            ListSymbols(596, 618);
            ListSymbols(601, 650);
            ListSymbols(101, 601);
            ListSymbols(97, 618);
            ListSymbols(97, 650);
        }
    }
}
