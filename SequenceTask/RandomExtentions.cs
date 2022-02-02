using System;

namespace SequenceTask
{
    public static class RandomExtentions
    {
        public static ulong GetRandomULong(this Random rnd, ulong min, ulong max)
        {
            var r = new Random();
            var b = new byte[sizeof(ulong)];
            r.NextBytes(b);
            var longRand = BitConverter.ToUInt64(b, 0);


            return longRand % (max - min) + min;
        }
    }
}