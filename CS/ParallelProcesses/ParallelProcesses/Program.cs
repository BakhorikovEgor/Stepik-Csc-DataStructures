using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        long[] lengths = Console.ReadLine().Split().Select(long.Parse).ToArray();
        long[] times = Console.ReadLine().Split().Select(long.Parse).ToArray();
        long[][] processesInfo = new long[lengths[1]][];
        PriorityQueue timesQueue = new PriorityQueue(lengths[0]);

        for (long i = 0; i < lengths[1]; i++)
        {
            long time = times[i];
            if (!timesQueue.TryInsert())
            {
                long[] processInfo = timesQueue.ExtractMin();
                processesInfo[processInfo[2]] = processInfo;
            }
            timesQueue.Insert(time, i);

            if (time == 0 && timesQueue.TryExtractMin())
            {
                long[] processInfo = timesQueue.ExtractMin();
                processesInfo[processInfo[2]] = processInfo;
            }
        }

        while (timesQueue.TryExtractMin())
        {
            long[] processInfo = timesQueue.ExtractMin();
            processesInfo[processInfo[2]] = processInfo;
        }

        for (long i = 0; i < lengths[1]; i++)
        {
            Console.WriteLine($"{processesInfo[i][0]} {processesInfo[i][1] - times[i]}");
        }
    }
}


class PriorityQueue
{
    public long CurrentIndex { get; private set; } = -1;

    readonly long maxSize;

    public long[][] priorityHeap;

    long [] processorsFullTime;

    Stack<long> ProcessorsName = new Stack<long>();


    public PriorityQueue(long size)
    {
        priorityHeap = new long[size][];
        BuildProcessorsQueue(size);
        processorsFullTime = new long[size];
        maxSize = size;
    }


    public void Insert(long value, long taskID)
    {
        CurrentIndex++;
        long processorName = ProcessorsName.Pop();
        processorsFullTime[processorName] += value;
        priorityHeap[CurrentIndex] = new long[] { processorName, processorsFullTime[processorName], taskID };
        ShiftUp(CurrentIndex);
    }


    public long[] ExtractMin()
    {
        long[] returnValue = priorityHeap[0];

        priorityHeap[0] = priorityHeap[CurrentIndex];
        CurrentIndex--;
        ProcessorsName.Push(returnValue[0]);
        ShiftDown(0);

        return returnValue;
    }


    void ShiftUp(long index)
    {
        while (index > 0 && priorityHeap[index][1] <= priorityHeap[GetParentIndex(index)][1])
        {
            if (priorityHeap[index][1] == priorityHeap[GetParentIndex(index)][1]
             && priorityHeap[index][0] > priorityHeap[GetParentIndex(index)][0]) break;

            Swap(index, GetParentIndex(index));
            index = GetParentIndex(index);
        }
    }


    void ShiftDown(long index)
    {
        while (index < CurrentIndex)
        {
            long leftChildInd = GetLeftChildIndex(index);
            long rightChildInd = GetRightChildIndex(index);
            long minChildInd = leftChildInd;

            if (rightChildInd <= CurrentIndex && priorityHeap[leftChildInd][1] > priorityHeap[rightChildInd][1])
            {
                minChildInd = rightChildInd;
            }

            else if (rightChildInd <= CurrentIndex && priorityHeap[leftChildInd][1] == priorityHeap[rightChildInd][1])
            {
                if (priorityHeap[rightChildInd][0] < priorityHeap[leftChildInd][0])
                {
                    minChildInd = rightChildInd;
                }
            }

            if (minChildInd > CurrentIndex || priorityHeap[minChildInd][1] > priorityHeap[index][1]) break;
            else if (priorityHeap[minChildInd][1] == priorityHeap[index][1] && priorityHeap[minChildInd][0] > priorityHeap[index][0]) break;
            Swap(index, minChildInd);
            index = minChildInd;
        }
    }


    void Swap(long index1, long index2)
    {
        long[] temp = priorityHeap[index1];
        priorityHeap[index1] = priorityHeap[index2];
        priorityHeap[index2] = temp;
    }


    void BuildProcessorsQueue(long size)
    {
        for (long i = size - 1; i != -1; i--)
        {
            ProcessorsName.Push(i);
        }
    }

    public bool TryInsert() => CurrentIndex < maxSize - 1;


    public bool TryExtractMin() => CurrentIndex > -1;


    long GetParentIndex(long index) => (index - 1) / 2;


    long GetLeftChildIndex(long index) => index * 2 + 1;


    long GetRightChildIndex(long index) => index * 2 + 2;

}