using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SequenceTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = GetN();
            var set = GetMultiSet(n);
            var x0 = (int)Math.Ceiling(Math.Sqrt(set.Max()));
            Console.WriteLine(x0); //fix good output
        }

        private static int GetN()
        {
            while (true)
            {
                Console.WriteLine("Input n:");
                var val = Console.ReadLine();
                if (int.TryParse(val, out int n)) //check floating point validation?
                {
                    if (n >= 1 && n <= Math.Pow(10, 5))
                    {
                        return n;
                    }
                }

                Console.WriteLine("wrong value, please input correct value");
            }
        }

        private static List<ulong> GetMockMultiSet(int n)
        {
            var r = new Random();
            var set = new List<ulong>();
            for (var i = 0; i < n; i++)
            {
                set.Add(r.GetRandomULong(1, (ulong)Math.Pow(10, 18)));
            }

            return set;
        }

        
        private static IEnumerable<ulong> GetMultiSet(int n)
        {
            Console.WriteLine($"now input {n} digits");
            var seq = new List<ulong>(n);

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

                    seq.Add((ulong)a);
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