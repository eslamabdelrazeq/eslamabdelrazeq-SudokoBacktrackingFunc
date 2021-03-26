using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoko.Backtracking
{
    class Program
    {
        static bool RunAlgorithm(int[,] gamePad, int row, int col, List<Func<int[,], int, int, bool>> preSolvingConditions)
        {
            int N = (int)Math.Sqrt(gamePad.Length);
            var preConditionsNotMet = preSolvingConditions.Any(d => d.Invoke(gamePad, row, col) == false);
            if (!preConditionsNotMet)
                return true;

            if (col == N)
            {
                row++;
                col = 0;
            }

            if (gamePad[row, col] != 0)
                return RunAlgorithm(gamePad, row, col + 1, preSolvingConditions);

            for (int value = 1; value < 10; value++)
            {

                var notEligble = EligiblityBehavior.Any(d => d.Invoke(gamePad, row, col, value) == false);
                if (!notEligble)
                {
                    gamePad[row, col] = value;
                    if (RunAlgorithm(gamePad, row, col + 1, preSolvingConditions))
                        return true;
                }

                gamePad[row, col] = 0;
            }
            return false;
        }
        static void print(int[,] gamePad)
        {
            int size = (int)Math.Sqrt(gamePad.Length);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write(gamePad[i, j] + " ");
                Console.WriteLine();
            }
        }
        static bool IsGamePadEnded(int[,] gamePad, int row, int col)
        {
            int size = (int)Math.Sqrt(gamePad.Length);
            if (row == size - 1 && col == size)
                return true;
            return false;
        }
        static bool IsRowEligibleToHoldValue(int[,] gamePad, int row, int col, int value)
        {
            for (int x = 0; x <= 8; x++)
                if (gamePad[row, x] == value)
                    return false;
            return true;
        }
        static bool IsColumEligibleToHoldValue(int[,] gamePad, int row, int col, int value)
        {
            for (int x = 0; x <= 8; x++)
                if (gamePad[x, col] == value)
                    return false;

            return true;
        }
        static bool IsSquareEligibleToHoldValue(int[,] gamePad, int row, int col, int value)
        {
            int startRow = row - row % 3;
            int startCol = col - col % 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (gamePad[i + startRow, j + startCol] == value)
                        return false;
            return true;
        }
        private static List<Func<int[,], int, int, int, bool>> EligiblityBehavior = new List<Func<int[,], int, int, int, bool>>();
        private static List<Func<int[,], int, int, bool>> PreSolvingConditions = new List<Func<int[,], int, int, bool>>();
        static void Main(string[] args)
        {
            Func<int[,], int, int, int, bool> eb1 = IsRowEligibleToHoldValue;
            Func<int[,], int, int, int, bool> eb2 = IsColumEligibleToHoldValue;
            Func<int[,], int, int, int, bool> eb3 = IsSquareEligibleToHoldValue;

            EligiblityBehavior
                .AssignBehavior(eb1)
                .AssignBehavior(eb2)
                .AssignBehavior(eb3);

            int size = 9;
            int[,] gamePad = new int[size, size];
            PreSolvingConditions
                .Add(IsGamePadEnded);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    gamePad[x, y] = 0;
                }
            }
            if (RunAlgorithm(gamePad, 0, 0, PreSolvingConditions))
                print(gamePad);
            else
                Console.WriteLine("No Solution exists");
            Console.ReadLine();
        }
    }
}
