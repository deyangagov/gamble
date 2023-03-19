using Gamble.Models;

namespace Gamble.Classes
{
    public class GameBoard
    {
        /// <summary>
        /// List of all available symbols, that can be rolled
        /// Sum of the chance of all symbols must be 100
        /// </summary>
        public List<Symbol> PossibleSymbols { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public int BoardSize
        {
            get
            {
                return Rows * Columns;
            }
        }

        public GameBoard(List<Symbol> symbols, int rows, int cols)
        {
            var sum = symbols.Sum(s => s.Chance);
            if (sum != 100)
            {
                throw new ArgumentOutOfRangeException($"Wrong configuration! Sum of all symbols chances is {sum}, expected 100");
            }
            PossibleSymbols = symbols;
            Rows = rows;
            Columns = cols;
        }
    }
}
