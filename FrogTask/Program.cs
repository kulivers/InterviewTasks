using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace FrogTask
{
    public class Frog
    {
        // public List<int> History { get; set; }
        public int JumpsCount { get; set; } //set here best frog at this point
        public int CurrentPointIdx { get; set; }
        public int NextJumpValue { get; set; }

        public Frog(int jumpsCount, int currentPoint, int nextJumpValue)
        {
            JumpsCount = jumpsCount;
            CurrentPointIdx = currentPoint;
            NextJumpValue = nextJumpValue;
            // History = new List<int>();
        }

        public void MakeJump()
        {
            CurrentPointIdx += NextJumpValue;
            JumpsCount++;
            // History.Add(CurrentPointIdx);
        }

        public void Fall(int fallValue)
        {
            CurrentPointIdx -= fallValue;
            // History.Add(CurrentPointIdx);
        }

        // public override int GetHashCode()
        // {
        //     return HashCode.Combine(JumpsCount, CurrentPointIdx, NextJumpValue);
        // }
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
                int maxAvailableJump;
                try
                {
                    maxAvailableJump = Jumps[i];
                }
                catch (Exception e)
                {
                    maxAvailableJump = 1;
                    GameOver = true;
                    StepCounter = frogsAtI.Select(f => f.JumpsCount).First();

                    // var winner = frogsAtI.Single();
                    // foreach (var i1 in winner.History)
                    // {
                    //     Console.Write(i1 + " - ");
                    // }
                }

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
                int maxAvailableJump;
                try
                {
                    maxAvailableJump = Jumps[i];
                }
                catch (Exception e)
                {
                    maxAvailableJump = 1;
                    GameOver = true;
                    StepCounter = frogsAtI.Select(f => f.JumpsCount).First();
                }

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
        public Dictionary<int, int> ReachedPointsIdxs { get; set; }
        public int StepCounter { get; set; }
        public int[] Jumps { get; set; }
        public int[] Falls { get; set; }

        public bool GameOver { get; set; }

        public Population(int[] jumps, int[] falls)
        {
            Jumps = jumps;
            Falls = falls;
            Frogs = new HashSet<Frog>();
            ReachedPointsIdxs = new Dictionary<int, int> //idx - reached
            {
                { 0, 1 }
            };
            StepCounter = 0;
            GameOver = false;
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

        public void MakeJumpsAll() //test
        {
            foreach (var frog in Frogs)
            {
                frog.MakeJump();
            }
        }

        public void MakeFallsAll() //test
        {
            IEnumerable<Frog> frogsOnI;
            for (var i = 0; i < Falls.Length; i++)
            {
                var i1 = i;
                frogsOnI = Frogs.Where(f => f.CurrentPointIdx == i1);
                foreach (var frog in frogsOnI)
                {
                    frog.Fall(Falls[i]);
                }
            }
        }


        public void ValidateFrogsPositionsAfterJump()
        {
            foreach (var frog in Frogs)
            {
                if (frog.CurrentPointIdx < 0)
                {
                    frog.CurrentPointIdx = 0;
                }

                if (frog.CurrentPointIdx > Jumps.Length - 1)
                {
                    GameOver = true;
                    StepCounter = frog.JumpsCount;
                    
                    // foreach (var i1 in frog.History)
                    // {
                    //     Console.Write(i1 + " - ");
                    // }
                    // return;
                }
            }
        }

        public void SaveFrogsInReached()
        {
            foreach (var frogPos in Frogs.Select(f => f.CurrentPointIdx))
            {
                var isValInReached = ReachedPointsIdxs.ContainsKey(frogPos);
                if (isValInReached)
                    ReachedPointsIdxs[frogPos]++;
                else
                    ReachedPointsIdxs.Add(frogPos, 1);
            }
        }

        public void KillFrogsOnReachedPoss() //after jump - kill frogs on Reached positions
        {
            foreach (var frog in Frogs)
            {
                if (ReachedPointsIdxs[frog.CurrentPointIdx] > 1)
                {
                    Frogs.Remove(frog);
                }
            }
        }

        public int Simulate()
        {
            CreateAdam();
            while (true)
            {
                if (Frogs.Count == 0)
                    return -1;
                else
                {
                    CreateAllFrogsForAvailableJumps();
                    MakeJumpsAll();
                    MakeFallsAll();
                    ValidateFrogsPositionsAfterJump();
                    SaveFrogsInReached();
                    KillFrogsOnReachedPoss();
                    if (GameOver)
                        return StepCounter;
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // IOValuesHelper.GetMockValues(out int h, out IEnumerable<int> avalibleJumps,
            //     out IEnumerable<int> fallsAfterJumps, variant: 3);
            IOValuesHelper.GetValues(out int h, out IEnumerable<int> avalibleJumps, out IEnumerable<int> fallsAfterJumps);
            var jumps = avalibleJumps as int[] ?? avalibleJumps.ToArray();
            var falls = fallsAfterJumps as int[] ?? fallsAfterJumps.ToArray();
            var pop = new Population(jumps, falls);
            var res = pop.Simulate();
            Console.WriteLine(res);
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
                        0, 2, 1, 4, 1,
                        4, 1, 2, 1, 1
                    };
                    break;
                }
                case 2:
                {
                    h = 10;
                    avalibleJumps = new[]
                    {
                        2, 5, 4, 3, 1,
                        4, 1, 2, 1, 1
                    };
                    fallsAfterJumps = new[]
                    {
                        0, 2, 1, 4, 1,
                        4, 1, 2, 1, 0
                    };
                    break;
                }
                case 3:
                {
                    h = 10;
                    avalibleJumps = new[]
                    {
                        2, 2, 3, 0, 0, 
                        4, 0, 1, 7, 6, 
                        9, 8, 5, 12
                    };
                    fallsAfterJumps = new[]
                    {
                        1, 0, 0, 5, 6,
                        0, 2, 3, 1, 1, 
                        0, 2, 1, 0
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

        public static void GetValues(out int h, out IEnumerable<int> avalibleJumps,
            out IEnumerable<int> fallsAfterJumps)
        {
            while (true)
            {
                // Console.WriteLine("Input height:");
                var val = Console.ReadLine();
                if (int.TryParse(val, out h)) //check floating point validation?
                {
                    if (h >= 1 && h <= Math.Pow(10, 5))
                    {
                        break;
                    }
                }

                // Console.WriteLine("wrong value, please input correct value");
            }

            // Console.WriteLine("input Avalible jumps values");
            avalibleJumps = GetMultiSet(h);
            // Console.WriteLine("input falls After Jumps");
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
                        // Console.WriteLine("wrong input, try again");
                        goto StartInput;
                    }

                    seq.Add(a);
                }
            else
            {
                // Console.WriteLine("wrong input, try again");
                goto StartInput;
            }

            if (countOfDigits == n) return seq;

            // Console.WriteLine("wrong input, try again");
            goto StartInput;
        }
    }
}