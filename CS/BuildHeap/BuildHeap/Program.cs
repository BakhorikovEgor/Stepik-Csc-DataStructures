using System;
using System.Linq;
using System.Collections.Generic;


class Program
{
    static void Main()
    {
        int size = int.Parse(Console.ReadLine());
        int[] nodes = Console.ReadLine().Split().Select(int.Parse).ToArray();
        List<Tuple<int, int>> result = BuildMinHeap(ref nodes, size);

        Console.WriteLine(result.Count);
        foreach (var pair in result)
        {
            Console.WriteLine($"{pair.Item1} {pair.Item2}");
        }
    }


    static List<Tuple<int, int>> BuildMinHeap(ref int[] nodes, int size)
    {
        List<Tuple<int, int>> swaps = new List<Tuple<int, int>>();
        for (int i = (size / 2); i >= 0; i--)
        {
            ShiftDown(ref nodes, swaps, i, size);
        }
        return swaps;
    }


    static void ShiftDown(ref int[] nodes, List<Tuple<int, int>> swaps, int index, int size)
    {
        while (index <= size)
        {
            int minIndex = index;
            try
            {
                if (nodes[index * 2 + 1] < nodes[minIndex])
                {
                    minIndex = index * 2 + 1;
                }
            }
            catch (IndexOutOfRangeException) { break; }

            try
            {
                if (nodes[index * 2 + 2] < nodes[minIndex])
                {
                    minIndex = index * 2 + 2;
                }
            }
            catch (IndexOutOfRangeException) { }

            if (minIndex == index) break;

            Swap(ref nodes, index, minIndex);
            swaps.Add(new Tuple<int, int>(index, minIndex));
            index = minIndex;
        }
    }


    static void Swap(ref int[] nodes, int index1, int index2)
    {
        int temp = nodes[index1];
        nodes[index1] = nodes[index2];
        nodes[index2] = temp;
    }
}