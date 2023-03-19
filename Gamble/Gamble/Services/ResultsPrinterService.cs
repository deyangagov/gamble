using Gamble.Models;

namespace Gamble.Classes
{
    public static class ResultsPrinterService
    {
        private static string newLine = Environment.NewLine;
        public static string PrintResults(SpinResults results, int rows, int cols)
        {
            string result = string.Empty;
            if(results == null || results.SymbolNames == null)
            {
                throw new ArgumentNullException("SymbolNames must not be null!");
            }
            if(results.SymbolNames.Count != rows * cols)
            {
                throw new ArgumentOutOfRangeException("SymbolNames count must be equal to Rows * Columns!");
            }
            for(int i = 0; i < rows; i++)
            {
                result += GetLine(cols);
                result += GetRow(results.SymbolNames.Skip(i * cols).Take(cols));
            }
            result += GetLine(cols);
            result += newLine;
            result += $"You have won: {Math.Round(results.AmountWon, 1)}" + newLine;
			results.Balance = results.Balance < 0 ? 0 : results.Balance;
            result += $"Current balance: {Math.Round(results.Balance, 1)}";
            return result;
        }

        private static string GetRow(IEnumerable<string> symbolNames)
        {
            string result = "|";
            foreach(string symbolName in symbolNames)
            {
                result+=symbolName + "|";
            }
            return result + newLine;
        }

        private static string GetLine(int cols)
        {
            if (cols == 0) { 
                return string.Empty;
            }
            string result = "+";
            for (int i = 0; i < cols; i++)
            {
                result += "-+";
            }
            return result + newLine;
        }
    }
}
