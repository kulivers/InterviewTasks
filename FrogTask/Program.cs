using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace FrogTask
{
    class Frog
    {
        public int JumpsCount { get; set; } //set here best frog at this point
        public int CurrentPointIdx { get; set; }
        public int NextJumpValue { get; set; }

        public Frog(List<int>? previousSteps, int jumpsCount, int currentPoint)
        {
            JumpsCount = jumpsCount;
            CurrentPointIdx = currentPoint;
        }

        static void SetNewJumpRules(int nextJumpValue)
        {
            //say to frog what she will do now
        }

        static void MakeJump()
        {
            //check is it avalible на всякий

            //set new values
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(JumpsCount, CurrentPointIdx, NextJumpValue);
        }
    }

    partial class Population
    {
        public void KillExcessFrogsAtPoint(int i)
        {
            try
            {
                var bestFrogCountAtI = Frogs.Where(f => f.CurrentPointIdx == 0).Select(f => f.JumpsCount).First();
                Frogs.RemoveWhere(f => f.JumpsCount > bestFrogCountAtI);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AddFrogTo(int i, int nextJumpValue)
        {
            // Frogs.Add()
        }
    }

    partial class Population
    {
        public HashSet<Frog> Frogs { get; set; }
        public HashSet<int> ReachedPointsIdxs { get; set; }
        public int StepCounter { get; set; }
        public int[] Way { get; set; }

        public Population(int[] way)
        {
            Way = way;
            Frogs = new HashSet<Frog>();
            ReachedPointsIdxs = new HashSet<int>();
        }

        public void CreateNewFrogsForAvailableJumps() //start from 0 idx, cuz we dont know where to jump
        {
            //dont create frogs in reached points

            //1 add one frog to fst position
            //2 get frogs on way
            //3 create available jumps frogs
        }

        public void Kill_Bad_Frogs()
        {
            // if frog current point have been reached BEFORE - Kill frog
            //удалить фрогов у которых больше степов чем у лучшей

            //если нету мест куда прыгнуть и не конец - убивать
        }

        void MakeJump()
        {
        }

        int Simulate()
        {
            while (true)
            {
                if (Frogs.Count == 0)
                {
                    return -1;
                }
                else
                {
                    //create недостающих фрогс, мб не делать где есть ричед после сл прыжка

                    //удалить ненужных
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            GetMockValues(out int h, out IEnumerable<int> avalibleJumps, out IEnumerable<int> fallsAfterJumps);
            var realJumpValues = TwoSetsDifference(avalibleJumps, fallsAfterJumps).ToArray();
            var pop = new Population(realJumpValues);

            
        }


        static void CoutSetByNRaws(IEnumerable<int> arrr, int n)
        {
            var arr = arrr.ToArray();
            for (int i = 0, nr = 1; i < arr.Count(); i++, nr++)
            {
                Console.Write(arr[i] + " ");
                if (nr == n)
                {
                    Console.WriteLine();
                    nr = 0;
                }
            }
        }

        static IEnumerable<int> TwoSetsDifference(IEnumerable<int> fromEnumerable, IEnumerable<int> second)
        {
            var from = fromEnumerable as int[] ?? fromEnumerable.ToArray();
            var secEnumerable = second as int[] ?? second.ToArray();

            if (from.Length != secEnumerable.Length)
                throw new Exception("counts are not same");
            var res = new List<int>();
            for (var i = 0; i < from.Length; i++)
            {
                res.Add(from[i] - secEnumerable[i]);
            }

            return res;
        }

        static void GetMockValues(out int h, out IEnumerable<int> avalibleJumps, out IEnumerable<int> fallsAfterJumps,
            int variant = 1)
        {
            switch (variant)
            {
                case 1:
                {
                    h = 10;
                    avalibleJumps = new[]
                    {
                        2, 5, 4, 3, 1,
                        4, 1, 2, 1, 1
                    };
                    fallsAfterJumps = new[]
                    {
                        1, 2, 3, 4, 1,
                        4, 1, 2, 1, 1
                    };
                    break;
                }
                default:
                {
                    throw new Exception("wrong variant");
                }
            }
        }

        static void GetValues(out int h, out IEnumerable<int> avalibleJumps, out IEnumerable<int> fallsAfterJumps)
        {
            while (true)
            {
                Console.WriteLine("Input height:");
                var val = Console.ReadLine();
                if (int.TryParse(val, out h)) //check floating point validation?
                {
                    if (h >= 1 && h <= Math.Pow(10, 5))
                    {
                        break;
                    }
                }

                Console.WriteLine("wrong value, please input correct value");
            }

            Console.WriteLine("input Avalible jumps values");
            avalibleJumps = GetMultiSet(h);
            Console.WriteLine("input falls After Jumps");
            fallsAfterJumps = GetMultiSet(h);
        }

        private static IEnumerable<int> GetMultiSet(int n)
        {
            var seq = new List<int>(n);

            int countOfDigits;

            StartInput:
            countOfDigits = 0;
            var input = Console.ReadLine();
            if (input != null)
                for (var match = Regex.Match(input, @"\d+"); match.Success; match = match.NextMatch())
                {
                    countOfDigits++;
                    var a = int.Parse(match.Value, NumberFormatInfo.InvariantInfo);


                    if (a < 1 || a > Math.Pow(10, 18))
                    {
                        Console.WriteLine("wrong input, try again");
                        goto StartInput;
                    }

                    seq.Add(a);
                }
            else
            {
                Console.WriteLine("wrong input, try again");
                goto StartInput;
            }

            if (countOfDigits == n) return seq;

            Console.WriteLine("wrong input, try again");
            goto StartInput;
        }
    }
}