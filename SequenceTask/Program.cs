using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SequenceTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = GetN();
            // var set = GetMultiSet(n);
        }

        private static long GetNextSeqValue(ulong x, ulong a)
        {
            checked
            {
                return (long)(x * x - a); //mb out of range in x^2
            }
        }

        private ulong LongRandom(ulong min, ulong max, Random rand) //1 - make random generator 
        {
            var buf = new byte[8];
            rand.NextBytes(buf);
            var longRand = BitConverter.ToUInt64(buf, 0);
            return (ulong)(Math.Abs((ulong)(longRand % (ulong)(max - min))) + min);
        }

        private static IEnumerable<ulong> GenerateMockMultiSet(int n) //2 - make GenerateMockMultiSet 
        {
            yield return LongRandom((ulong)1, (ulong)Math.Pow(10,18));
        }

        private static IEnumerable<ulong> GetMultiSet(int n)
        {
            Console.WriteLine($"now input {n} digits");

            var seq = new List<ulong>(n);
            var input = Console.ReadLine();

            //should i validate for null input and not n values and do try parse?
            for (var match = Regex.Match(input, @"\d+"); match.Success; match = match.NextMatch())
            {
                var a = int.Parse(match.Value, NumberFormatInfo.InvariantInfo);
                seq.Add((ulong)a);
            }

            return seq;
        }

        private static int GetN()
        {
            while (true)
            {
                Console.WriteLine("Input n:");
                var val = Console.ReadLine();
                if (int.TryParse(val, out int n)) //check floating point validation?
                {
                    if (n > 1 && n <= Math.Pow(10, 5))
                    {
                        return n;
                    }
                }

                Console.WriteLine("wrong value, please input correct value");
            }
        }
    }
}