using Gamble;
using Gamble.Interfaces;
using Gamble.Models;
using Gamble.Services;


class Program
{
    public static void Main(string[] args)
    {
        var instance = new GambleService();
        var game = new GambleGame(instance);
        game.RunGame();
    }
}