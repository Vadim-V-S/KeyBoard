using KeyBoard.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoard
{
    internal class VowelSymbol : TranscriptSymbol
    {
        public VowelSymbol()
        {
            ListSymbols(105, 58);
            ListSymbols(618);
            ListSymbols(650);
            ListSymbols(117,58);
            ListSymbols(101);
            ListSymbols(601);
            ListSymbols(604,58);
            ListSymbols(596,58);
            ListSymbols(230);
            ListSymbols(652);
            ListSymbols(593,58);
            ListSymbols(594);
        }
    }
}
