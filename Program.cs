using Hangman.Handler;
using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            WordHandler guessHandler = new WordHandler();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine($"Press on " +
                    $"\n[1] to play the game" +
                    $"\n[Esc] to exit the game.");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        guessHandler.InitializeGame();
                        Console.Clear();
                        break;

                    case ConsoleKey.Escape:
                        isRunning = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("You must press the numerical button [1] or Escape button [Esc]");
                        break;
                }
            }

        }
    }
}
