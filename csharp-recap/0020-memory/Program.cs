using System;
using System.Collections;
using System.Linq;

namespace memory
{
    class PointClass { public int x; public int y; }
    struct PointStruct { public int x; public int y; }
    struct BoolStruct { public bool a; public bool b; }

    class Program
    {
        static void Main()
        {
            ByValByRef();
            MemoryUsage();
        }

        private static void MemoryUsage()
        {
            PointStruct[] resultStruct;
            var avgMemory = GetAverageMemory(() => resultStruct = Enumerable.Range(0, 1000)
                .Select(i => new PointStruct { x = i, y = i }).ToArray(), 1000);
            Console.WriteLine($"Avg. memory per struct = {avgMemory}");

            PointClass[] resultClass;
            avgMemory = GetAverageMemory(() => resultClass = Enumerable.Range(0, 1000)
                .Select(i => new PointClass { x = i, y = i }).ToArray(), 1000);
            Console.WriteLine($"Avg. memory per class = {avgMemory}");

            BoolStruct[] resultBoolStruct;
            avgMemory = GetAverageMemory(() => resultBoolStruct = Enumerable.Range(0, 1000)
                .Select(i => new BoolStruct { a = i % 2 == 0, b = i % 2 != 0 }).ToArray(), 1000);
            Console.WriteLine($"Avg. memory per struct with bools = {avgMemory}");

            BitArray bits;
            avgMemory = GetAverageMemory(() => bits = new BitArray(2000), 1000);
            Console.WriteLine($"Avg. memory per two bools = {avgMemory}");
        }

        static void ByValByRef()
        {
            var pc1 = new PointClass { x = 1, y = 2 };
            var pc2 = pc1;
            pc1.x++;
            Console.WriteLine($"pc2.x = {pc2.x}");
            // QUIZ: What is the output?

            var ps1 = new PointStruct { x = 1, y = 2 };
            var ps2 = ps1;
            ps1.x++;
            Console.WriteLine($"ps2.x = {ps2.x}");
            // QUIZ: What is the output?
        }

        static long GetTotalMemory()
        {
            GC.Collect();
            return GC.GetTotalMemory(true);
        }

        static float GetAverageMemory(Action body, int numberOfItems)
        {
            var memBefore = GetTotalMemory();
            body();
            var memAfter = GetTotalMemory();
            return ((float)(memAfter - memBefore)) / numberOfItems;
        }
    }
}
