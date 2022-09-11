using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoard.Model
{
    public abstract class TranscriptSymbol : ISymbolsSet
    {
        public List<string> Symbols { get; }

        protected TranscriptSymbol()
        {
            Symbols = new List<string>();
        }

        public List<string> ListSymbols(int symbol)
        {
            Symbols.Add((char)(symbol) + "");
            return Symbols;
        }

        public List<string> ListSymbols(int symbol1, int symbol2)
        {
            string item1 = ((char)(symbol1) + "");
            string item2 = ((char)(symbol2) + "");

            Symbols.Add(item1 + item2);

            return Symbols;
        }
    }
}
