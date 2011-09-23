using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CollectionTest
{
    class Program
    {
        static int iterations = 10000;

        static void Main(string[] args)
        {
            TestList();
            TestHashSet();
            Console.ReadLine();
        }

        static void TestList()
        {
            Stopwatch swatch = new Stopwatch();
            List<string> strings = new List<string>();
            Random rng = new Random(300);

            for (int i = 0; i < iterations; i++)
            {
                int nextValue = rng.Next(0, iterations);
                strings.Add(nextValue.ToString());
            }

            rng = new Random(300);

            swatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                int nextValue = rng.Next(0, iterations);
                bool isContained = strings.Contains(nextValue.ToString());
            }
            swatch.Stop();

            Console.WriteLine("Time for List<string> contains: {0}", swatch.Elapsed);
        }

        static void TestHashSet()
        {
            Stopwatch swatch = new Stopwatch();
            HashSet<string> strings = new HashSet<string>();
            Random rng = new Random(300);

            for (int i = 0; i < iterations; i++)
            {
                int nextValue = rng.Next(0, iterations);
                strings.Add(nextValue.ToString());
            }

            rng = new Random(300);

            swatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                int nextValue = rng.Next(0, iterations);
                bool isContained = strings.Contains(nextValue.ToString());
            }
            swatch.Stop();

            Console.WriteLine("Time for HashSet contains: {0}", swatch.Elapsed);
        }

    }
}
