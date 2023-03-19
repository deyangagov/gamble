using Gamble.Interfaces;
using Gamble.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamble
{
    public class GambleGame
    {
        private readonly IGambleService _gambleService;

        public GambleGame(IGambleService gambleService)
        {
            _gambleService = gambleService;
        }

        public void RunGame()
        {
            // initial config
            // TODO read from file
            int rows = 4;
            int cols = 3;
            List<Symbol> symbols = new List<Symbol>() {
                new Symbol("A", 0.4, 45),
                new Symbol("B", 0.6, 35),
                new Symbol("P", 0.8, 15),
                new Symbol("*", 0.0, 5, true)
            };

            double balance = 0;
            do
            {
                Console.WriteLine("Please deposit money you would like to play with:");
            }
            while (!double.TryParse(Console.ReadLine(), out balance));
            balance = Math.Round(balance, 1);

            _gambleService.Play(symbols, rows, cols, balance);

            Console.WriteLine("Out Of Money!");
            Console.WriteLine("Press ENTER");
            Console.ReadLine();
        }
    }
}
