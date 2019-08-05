using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp4
{
    static class Program
    {
        static void Main()
        {
            YieldDemo();

            //var numbers = new[] { 1, 2, 3, 4, 5 };
            var numbers = new[] { "1", "2", "10" };
            foreach(var number in numbers.Filter(n => n.StartsWith("1")))
            //foreach(var number in numbers.Where(n => n.StartsWith("1")))
            {
                Console.WriteLine(number);
            }
        }

        #region Yield demo
        static void YieldDemo()
        {
            // Note how the values are generated using `yield`
            foreach(var x in GenerateValues())
            {
               Console.WriteLine(x);
            }
        }

        static IEnumerable<int> GenerateValues()
        {
           yield return 1;
           yield return 2;
           yield return 3;
        }
        #endregion

        // Note how `Filter` is generic so that it works with any type
        static IEnumerable<T> Filter<T>(this IEnumerable<T> input, Func<T, bool> filterFunction)
        {
            foreach(var item in input)
            {
                if (filterFunction(item))
                {
                    yield return item;
                }
            }
        }
    }
}
