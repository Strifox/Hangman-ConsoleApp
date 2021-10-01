using Hangman.Handler;
using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            WordHandler guessHandler = new WordHandler();

            guessHandler.InitializeGame();
        }
    }
}
