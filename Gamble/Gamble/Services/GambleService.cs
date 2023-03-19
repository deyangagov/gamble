using Gamble.Classes;
using Gamble.Interfaces;
using Gamble.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamble.Services
{
    public class GambleService : IGambleService
    {
        public void Play(List<Symbol> symbols, int rows, int cols, double balance)
        {
            var board = new GameBoard(symbols, rows, cols);
            if (board == null)
            {
                throw new ArgumentNullException("Game board is not initialized! Please use InitializeGame() first!");
            }
            var betAmount = 0d;
            var converted = false;
            while (!(balance < 0.1))
            {
                do
                {
                    Console.WriteLine("Enter stake amount:");
                    converted = double.TryParse(Console.ReadLine(), out betAmount);
                    if (converted && balance < betAmount)
                    {
                        Console.WriteLine($"Please bet {Math.Round(balance, 1)} or less");
                    }
                }
                while (!converted || Math.Round(betAmount, 1) > Math.Round(balance, 1));
                var betResult = Bet(betAmount, board, balance);
                balance = betResult.Balance;
                Console.WriteLine(ResultsPrinterService.PrintResults(betResult, board.Rows, board.Columns));
            }
        }

        public List<Symbol> Spin(GameBoard board)
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            var resultBoard = new List<Symbol>();
            for (int i = 0; i < board.BoardSize; i++)
            {
                resultBoard.Add(GetSymbol(rand, board));
            }
            return resultBoard;
        }

        public Symbol GetSymbol(Random rand, GameBoard board)
        {
            var luck = rand.Next(99);
            var chance = 0d;
            foreach (var symbol in board.PossibleSymbols)
            {
                chance += symbol.Chance;
                if (luck < chance)
                {
                    return symbol;
                }
            }
            return board.PossibleSymbols.Last();
        }
        public SpinResults Bet(double amountBet, GameBoard board, double balance)
        {
            balance -= amountBet;
            var result = new SpinResults();

            var boardResult = Spin(board);
            var boardCoef = 0d;
            for (int i = 0; i < board.Rows; i++)
            {
                var coef = 0d;
                Symbol? lastSymbol = null;
                string rowSymbolName = string.Empty;
                for (int j = 0; j < board.Columns; j++)
                {
                    var currentSymbol = boardResult[i * board.Columns + j];
                    {
                        if (lastSymbol == null ||
                            rowSymbolName == string.Empty ||
                            rowSymbolName == currentSymbol.Name ||
                            currentSymbol.IsWildcard)
                        {
                            lastSymbol = currentSymbol;
                            if (!currentSymbol.IsWildcard)
                            {
                                rowSymbolName = currentSymbol.Name;
                            }
                            coef += lastSymbol.Value;
                        }
                        else
                        {
                            coef = 0;
                            break;
                        }
                    }
                }
                boardCoef += coef;
            }
            result.AmountWon = Math.Round(amountBet * boardCoef, 1);
            balance += result.AmountWon;
            result.Balance = balance;
            result.SymbolNames = boardResult.Select(x => x.Name).ToList();
            return result;
        }
    }
}
