using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using StateSearch;

namespace EightPuzzleR.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Type Start and End States. (One per line)");

            string startState = Console.ReadLine();
            string closeState = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(startState) || startState.Trim().Length != 9)
            {
                startState = "012345678";
                Console.WriteLine("Assuming StartState as: {0}", startState);
            }

            if (string.IsNullOrWhiteSpace(closeState) || closeState.Trim().Length != 9)
            {
                closeState = "087654321";
                Console.WriteLine("Assuming StartState as: {0}", closeState);
            }

            IStateSearchable<EightPuzzle> stateSearch;

            try
            {
                EightPuzzle start = EightPuzzleFactory.Create(startState);
                EightPuzzle close = EightPuzzleFactory.Create(closeState);

                stateSearch = new AStar<EightPuzzle>(start, close);

                stateSearch.FindPath();

                if (stateSearch.HasSolution == false)
                {
                    Console.WriteLine("No solution Found");
                    Console.ReadKey();
                    return;
                }

            }
            catch
            {
                Console.WriteLine("Was not possible to solve the puzzle!");
                Console.ReadKey();
                return;
            }

            AnimateSolutions(stateSearch);
            Console.WriteLine("Cost: " + stateSearch.MinimumCost);
            Console.ReadKey();

        }

        private static void AnimateSolutions(IStateSearchable<EightPuzzle> search)
        {
            IEnumerable<EightPuzzle> solution = search.Path.Reverse();

            foreach (var item in solution)
            {
                PrintPuzzleString(item.ToString());
                Thread.Sleep(200);
            }
        }

        private static void PrintPuzzleString(string item)
        {
            Console.Clear();
            for (int i = 0; i < item.Length; ++i)
            {
                if (i > 0 && (i % 3) == 0)
                {
                    Console.WriteLine();
                }
                Console.Write("{0} ", item[i] == '0' ? "_" : item[i].ToString());
            }
        }
    }
}
