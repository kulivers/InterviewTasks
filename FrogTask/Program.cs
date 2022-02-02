using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace FrogTask
{
    class Frog
    {
        public List<int> PreviousSteps { get; set; } //?
        public int JumpsCount { get; set; } //set here best frog at this point
        public int CurrentPoint { get; set; }
        public int NextJumpValue { get; set; }

        public Frog(List<int> previousSteps, int jumpsCount, int currentPoint) 
        {
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
    }

    class Population
    {
        public List<Frog> Frogs { get; set; }
        public List<int> ReachedPoints { get; set; }
        public int StepCounter { get; set; }

        public void CreateNewFrogsForAvailableJumps()
        {
        }

        public void Kill_Bad_Frogs()
        {
            // if frog current point have been reached BEFORE - Kill frog
            //удалить фрогов у которых больше степов чем у лучшей
        }

        void MakeJump()
        {
            
        }
        void Simulate()
        {
            while (true)
            {
                if (Frogs.Count == 0)
                {
                    Console.WriteLine("All frogs dead, way cant be reached");
                    break;
                }
                else
                {
                    //удалить ненужных
                    
                    //create недостающих фрогс, мб не делать где есть ричед после сл прыжка
                    
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GetMockValues(out int h, out IEnumerable<int> avalibleJumps, out IEnumerable<int> fallsAfterJumps);
            var realJumpValues = TwoSetsDifference(avalibleJumps, fallsAfterJumps);
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
            }

            throw new Exception("wrong variant");
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