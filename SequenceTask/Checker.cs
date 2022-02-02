using System;
using System.Collections.Generic;
using System.Linq;

namespace SequenceTask
{
    public class Checker
    {

        public static IEnumerable<ulong> GetMockMultiSet(int n, ulong min = 1, ulong max = 4294967295)
        {
            var r = new Random();
            var set = new List<ulong>();
            for (var i = 0; i < n; i++)
            {
                set.Add(r.GetRandomULong(min, max));
            }
        
            return set;
        }
        
        public static void IndexTest()
        {
            while (true)
            {
                var set = GetMockMultiSet(100, max: 200).OrderByDescending(x => x).ToList();

                // var set = new List<ulong>() { 3,4 }.OrderByDescending(x=>x).ToList();//test

                var x0 = SequenceProgram.GetX0(set);
                long res = 0;
                var sequence = new List<long>();
                sequence.Add((long)x0);
                for (var i = 0; i < set.Count; i++)
                {
                    if (i == 0)
                    {
                        res = GetNextXk((long)x0, set[i]);
                        sequence.Add(res);
                    }
                    else
                    {
                        res = GetNextXk(res, set[i]);
                        sequence.Add(res);
                    }
                }

                if (sequence.All(x=>x<0))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("x0 = " + x0);
                    
                    Console.WriteLine("set: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    foreach (var x in set)
                    {
                        Console.WriteLine(x);
                    }
                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("sequnce: ");                    
                    Console.ForegroundColor = ConsoleColor.Blue;

                    foreach (var l in sequence)
                    {
                        Console.WriteLine(l);
                    }
                }
            }
        }

        static long GetNextXk(long xk, ulong a)
        {
            checked
            {
                try
                {
                    return xk * xk - (long)a;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}