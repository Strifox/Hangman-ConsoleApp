﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hangman.Handler
{
    public class WordHandler
    {
        //C:\Users\johan\source\repos\Hangman\Textfiles\
        #region Private Fields
        private static readonly string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Textfiles\\secretwords.txt");
        private readonly string[] secretWordsArray = File.ReadAllText(path).Split(",", StringSplitOptions.RemoveEmptyEntries);
        private char[] correctGuesses = new char[] { };
        private int amountOfGuesses = 0;
        private string secretWord;

        #endregion

        public void InitializeGame()
        {
            StringBuilder strB = new StringBuilder();
            StringBuilder strBuilderWords = new StringBuilder();

            amountOfGuesses = 10;

            //Random generated word from an array of words
            secretWord = GetRandomWord().ToUpper();

            correctGuesses = new char[secretWord.Length];

            //Array with only underscores
            char[] hiddenWord = HiddenWord(secretWord);

            while (amountOfGuesses != 0)
            {
                Console.Clear();


                Console.WriteLine($"Type a word or a letter \tGuesses left: {amountOfGuesses}" +
                    $"\nWrong letters: {strB}" +
                    $"\nWrong words: {strBuilderWords}");


                //Writes each index in the array with the hidden letters to console
                foreach (var hiddenLetter in hiddenWord)
                {
                    Console.Write($"{hiddenLetter} ");
                }

                //Player input
                var input = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input) || int.TryParse(input, out _))
                {
                }
                else
                {
                    //Only true if input is one letter
                    if (input.Length == 1)
                    {
                        //Converts the string input to char
                        var letter = char.Parse(input);

                        //True if input matches a letter in the secret word
                        if (IsLetterCorrect(letter, secretWord))
                        {
                            for (int i = 0; i < hiddenWord.Length; i++)
                            {

                                //Checks if input matches a letter at specific index
                                if (secretWord[i] == letter)
                                {
                                    //Changes an underscore at specific index to the letter
                                    ChangeLetterAt(i, letter, hiddenWord);

                                    //Stores the letter in a char array
                                    correctGuesses[i] = letter;
                                }

                                //Checks if all the player inputs matches the secret word
                                if (hiddenWord.SequenceEqual(secretWord))
                                {
                                    //Player has won
                                    PlayerWins();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            // Checks if player input exists
                            if (!strB.ToString().Contains(letter))
                            {
                                // Adds the letter to StringBuilder.
                                strB.Append(letter).Append(" ");

                                // Removes one guess!
                                amountOfGuesses--;
                            }
                        }
                    }
                    else
                    {
                        // True if player input is a word and matches the secret word
                        if (IsWordCorrect(input, secretWord))
                        {
                            //Player has won
                            PlayerWins();
                            return;
                        }
                        else
                        {
                            // Checks if player input exists
                            if (!strBuilderWords.ToString().Contains(input))
                            {
                                // adds the word to StringBuilder.
                                strBuilderWords.Append(input).Append(" ");

                                //Removes one guess!
                                amountOfGuesses--;
                            }
                        }
                    }
                }
            }

            if (amountOfGuesses == 0)
            {
                PlayerLooses();
                return;
            }
        }

        /// <summary>
        /// Writes amount of guesses and the secret word.
        /// </summary>
        public void PlayerWins()
        {
            Console.Clear();
            Console.WriteLine($"Congratulations, you won!" +
                $"\nYou have {amountOfGuesses} left" +
                $"\nThe secret word is: {secretWord}" +
                $"\n\nPress ENTER to go back to the menu");

            Console.Read();
        }

        /// <summary>
        /// Writes amount of guesses and the secret word.
        /// </summary>
        public void PlayerLooses()
        {
            Console.Clear();
            Console.WriteLine($"Sorry, you have lost!" +
                $"\nThe secret word is: {secretWord}" +
                $"\n\nPress ENTER to go back to the menu");

            Console.Read();
        }

        /// <summary>
        /// Adds underscore to a char array
        /// </summary>
        /// <param name="secretWord">Word to be replaced with underscore</param>
        /// <returns>An array based on param length with only underscores</returns>
        public char[] HiddenWord(string secretWord)
        {
            //Creating an array for blanks
            char[] hiddenWord = new char[secretWord.Length];

            for (int i = 0; i < secretWord.Length; i++)
            {
                hiddenWord[i] = char.Parse("_");
            }

            return hiddenWord;
        }

        /// <summary>
        /// Chooses a random word from an array of strings
        /// </summary>
        /// <returns>A random word</returns>
        public string GetRandomWord()
        {
            Random rnd = new Random();

            int randomIndex = rnd.Next(secretWordsArray.Length);

            return secretWordsArray[randomIndex];
        }

        /// <summary>
        /// Replaces a letter at specific index
        /// </summary>
        /// <param name="position"></param>
        /// <param name="letter"></param>
        /// <param name="charArray"></param>
        public void ChangeLetterAt(int position, char letter, char[] charArray)
        {
            charArray[position] = letter;
        }

        /// <summary>
        /// Checks if letter matches a letter in the secret word
        /// </summary>
        /// <param name="guess"></param>
        /// <param name="secretWord"></param>
        /// <returns>True if a match</returns>
        public bool IsLetterCorrect(char guess, string secretWord)
        {
            foreach (var letter in secretWord)
            {
                if (guess == letter)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a word matches the secret word
        /// </summary>
        /// <param name="guess"></param>
        /// <param name="secretWord"></param>
        /// <returns>True if matched</returns>
        public bool IsWordCorrect(string guess, string secretWord)
        {
            if (guess.ToUpperInvariant() == secretWord.ToUpperInvariant())
                return true;

            return false;
        }
    }
}
