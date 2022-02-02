using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace SequenceTask.Tests
{
    public class SequenceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SequenceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void IndexTest()
        {
            var set = SequenceProgram.GetMockMultiSet(10, 1, 25).ToList();
            var x0 = SequenceProgram.GetX0(set);
            _testOutputHelper.WriteLine("x="+ x0.ToString());
            set.Add(x0);
            var orderedEnumerable = set.OrderBy(x => x);
            
            foreach (var x in orderedEnumerable)
            {
                _testOutputHelper.WriteLine(x.ToString());
            }        
        }

        // bool Foo(ulong x0, IEnumerable<ulong> set)
        // {
        //     var enumerable = set.ToList();
        //     enumerable.Prepend(x0);
        //     bool ok = true;
        //     
        //     for (int i = 0; i < enumerable.Count()-1; i++)
        //     {
        //         
        //     }
        //     foreach (var ak in enumerable)
        //     {
        //         var akn = ak ^ 2 - ak;
        //         // check akn > 0;
        //         ak = akn;    
        //     }
        //     
        //     
        // }

    }
}