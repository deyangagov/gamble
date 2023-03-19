using Gamble.Classes;
using Gamble.Models;

namespace Gamble.Interfaces
{
    public interface IGambleService
    {
        SpinResults Bet(double amountBet, GameBoard board, double balance);
        Symbol GetSymbol(Random rand, GameBoard board);
        void Play(List<Symbol> symbols, int rows, int cols, double balance);
        List<Symbol> Spin(GameBoard board);
    }
}