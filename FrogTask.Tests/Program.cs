using System;
using System.Collections.Generic;
using System.Linq;

namespace FrogTask.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            IOValuesHelper.GetMockValues(out int h, out IEnumerable<int> avalibleJumps,
                out IEnumerable<int> fallsAfterJumps);
            var jumps = avalibleJumps as int[] ?? avalibleJumps.ToArray();
            var falls = fallsAfterJumps as int[] ?? fallsAfterJumps.ToArray();
            var pop = new Population(jumps, falls);


            // IOValuesHelper.CoutSetByNRaws(avalibleJumps, 5);

            // TestCreatingAndKilling(pop);
            // TestCreatingFrogsOnAllPositions(pop);

            pop.Frogs = GetMockFrogs(pop);

            foreach (var frog in pop.Frogs)
            {
                if (frog.CurrentPointIdx == 1)
                {
                    pop.Frogs.Remove(frog);
                }
            }

            ShowFrogs(pop.Frogs.Where(f => f.CurrentPointIdx == 1));
        }


        private static void ShowFrogs(IEnumerable<Frog> frogs)
        {
            foreach (var frog in frogs)
            {
                Console.WriteLine("i = " + frog.CurrentPointIdx + " | jump to: " + frog.NextJumpValue);
            }
        }

        private static void TestCreatingFrogsOnAllPositions(Population pop)
        {
            pop.Frogs = GetMockFrogs(pop);
            pop.CreateAllFrogsForAvailableJumps();
        }

        private static void TestCreatingAndKilling(Population pop)
        {
            pop.Frogs = GetMockFrogs(pop);
            pop.CreateAndSetNewFrogsAt(1);
            pop.CreateAndSetNewFrogsAt(2);
            pop.KillExcessFrogsAtPoint(2);
        }

        static HashSet<Frog> GetMockFrogs(Population pop)
        {
            var frogs = pop.Frogs.ToList();
            frogs.Add(new Frog(1, 1, 5));
            frogs.Add(new Frog(1, 1, 4));
            frogs.Add(new Frog(1, 2, 4));
            frogs.Add(new Frog(1, 2, 4));
            frogs.Add(new Frog(1, 2, 4));
            frogs.Add(new Frog(1, 2, 4));
            frogs.Add(new Frog(1, 2, 4));
            frogs.Add(new Frog(1, 2, 4));
            frogs.Add(new Frog(1, 2, 4));
            frogs.Add(new Frog(1, 2, 4));
            frogs.Add(new Frog(1, 2, 4));

            frogs.Add(new Frog(2, 2, 4));
            frogs.Add(new Frog(2, 2, 4));
            frogs.Add(new Frog(2, 2, 4));
            frogs.Add(new Frog(2, 2, 4));
            frogs.Add(new Frog(2, 2, 4));

            return frogs.ToHashSet();
        }
    }
}