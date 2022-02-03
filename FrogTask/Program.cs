using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace FrogTask
{
    public class Frog
    {
        public int JumpsCount { get; set; } //set here best frog at this point
        public int CurrentPointIdx { get; set; }
        public int NextJumpValue { get; set; }

        public Frog(int jumpsCount, int currentPoint, int nextJumpValue)
        {
            JumpsCount = jumpsCount;
            CurrentPointIdx = currentPoint;
            NextJumpValue = nextJumpValue;
        }

        public void MakeJump()
        {
            CurrentPointIdx += NextJumpValue;
        }

        public void Fall(int fallValue)
        {
            CurrentPointIdx -= fallValue;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(JumpsCount, CurrentPointIdx, NextJumpValue);
        }
    }

    public partial class Population
    {
        public Frog GetBestFrogAt(int i)
        {
            try
            {
                return Frogs.FirstOrDefault(f => f.CurrentPointIdx == i);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public void KillExcessFrogsAtPoint(int i)
        {
            try
            {
                var bestFrogCountAtI = Frogs.Where(f => f.CurrentPointIdx == i).Select(f => f.JumpsCount).First();
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
            var bestFrog = GetBestFrogAt(i);

            //del if works good
            if (bestFrog == null)
            {
                throw new Exception("Place where we were add frog is empty");
            }

            // KillExcessFrogsAtPoint(i); // we just add
            Frogs.Add(new Frog(bestFrog.JumpsCount, bestFrog.CurrentPointIdx, nextJumpValue));
        }

        public void CreateAndSetNewFrogsAt(int j)
        {
            ValidateFrogsCountAt(j);
            SetJumpValuesToFrogsAt(j);

            void ValidateFrogsCountAt(int i)
            {
                //смотрим сколько есть
                var frogsAtI = Frogs.Where(f => f.CurrentPointIdx == i);

                if (!frogsAtI.Any())
                    throw new Exception("We cant create frogs where them doesnt exsts");
                var maxAvailableJump = Jumps[i];

                //если больше - удаляем слабых 
                if (frogsAtI.Count() > maxAvailableJump)
                {
                    KillExcessFrogsAtPoint(i);
                }

                //если все еще больше(у них будет одинаковое колво степов) - просто срезаем до количества нужного
                if (frogsAtI.Count() > maxAvailableJump) //they have same steps cuz we killed sillies
                {
                    var topNBestFrogs = Frogs.Take(maxAvailableJump).ToArray();
                    Frogs.RemoveWhere(f => f.CurrentPointIdx == i);
                    foreach (var frog in topNBestFrogs)
                    {
                        Frogs.Add(frog);
                    }
                }

                //если меньше чем maxAvailableJump - добавляем нехватающих
                if (frogsAtI.Count() < maxAvailableJump)
                {
                    var bestFrog = GetBestFrogAt(i);
                    while (frogsAtI.Count() < maxAvailableJump)
                    {
                        Frogs.Add(new Frog(bestFrog.JumpsCount, bestFrog.CurrentPointIdx, bestFrog.NextJumpValue));
                    }
                }
            }

            //ставим им нужжные значения для прыжка !!!
            void SetJumpValuesToFrogsAt(int i)
            {
                var frogsAtI = Frogs.Where(f => f.CurrentPointIdx == i);
                var maxAvailableJump = Jumps[i];
                if (frogsAtI.Count() != maxAvailableJump)
                    throw new Exception("Frogs count != maxAvailableJump value");

                var jumpValForFrog = maxAvailableJump;
                foreach (var frog in frogsAtI)
                {
                    frog.NextJumpValue = jumpValForFrog;
                    jumpValForFrog--;
                }
            }
        }
    }

    public partial class Population
    {
        public HashSet<Frog> Frogs { get; set; }
        public HashSet<int> ReachedPointsIdxs { get; set; }
        public int StepCounter { get; set; }
        public int[] Jumps { get; set; }
        public int[] Falls { get; set; }

        public Population(int[] jumps, int[] falls)
        {
            Jumps = jumps;
            Falls = falls;
            Frogs = new HashSet<Frog>();
            ReachedPointsIdxs = new HashSet<int>();
        }

        void CreateAdam() // first frog
        {
            Frogs.Add(new Frog(0, 0, Jumps[0]));
        }


        public void CreateAllFrogsForAvailableJumps() //start from 0 idx, cuz we dont know where to jump
        {
            //get фрогс позишонс
            //на этих позициях креэйт фрогс
            var frogsPositions = Frogs.Select(f => f.CurrentPointIdx).ToHashSet();
            foreach (var position in frogsPositions)
            {
                CreateAndSetNewFrogsAt(position);
            }
        }

        public void KillFrogsOnReachedPoss() //after jump - kill frogs on Reached positions
        {
            // if frog current point have been reached BEFORE - Kill frog
            //удалить фрогов у которых больше степов чем у лучшей

            //если нету мест куда прыгнуть и не конец - убивать
        }

        public void MakeJumpsAll()
        {
            foreach (var frog in Frogs)
            {
                frog.MakeJump();
            }
        }

        public void MakeFallsAll()
        {
            for (var i = 0; i < Falls.Length; i++)
            {
                
            }
        }

        public int Simulate()
        {
            CreateAdam();
            return 1;
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
            IOValuesHelper.GetMockValues(out int h, out IEnumerable<int> avalibleJumps,
                out IEnumerable<int> fallsAfterJumps);
            var jumps = avalibleJumps as int[] ?? avalibleJumps.ToArray();
            var falls = fallsAfterJumps as int[] ?? fallsAfterJumps.ToArray();
            var pop = new Population(jumps, falls);
            pop.Simulate();
        }
    }

    public class IOValuesHelper
    {
        public static void GetMockValues(out int h, out IEnumerable<int> avalibleJumps,
            out IEnumerable<int> fallsAfterJumps,
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
                        0, 2, 3, 4, 1,
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

        public static void CoutSetByNRaws(IEnumerable<int> arrr, int n)
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

        public static IEnumerable<int> TwoSetsDifference(IEnumerable<int> fromEnumerable, IEnumerable<int> second)
        {
            var from = fromEnumerable as int[] ?? fromEnumerable.ToArray();
            var secEnumerable = second as int[] ?? second.ToArray();

            if (@from.Length != secEnumerable.Length)
                throw new Exception("counts are not same");
            var res = new List<int>();
            for (var i = 0; i < @from.Length; i++)
            {
                res.Add(@from[i] - secEnumerable[i]);
            }

            return res;
        }

        public static void GetValues(out int h, out IEnumerable<int> avalibleJumps,
            out IEnumerable<int> fallsAfterJumps)
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

        public static IEnumerable<int> GetMultiSet(int n)
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