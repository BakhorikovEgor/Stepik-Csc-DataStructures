using System;
using System.Linq;
using System.Collections.Generic;


namespace BufferProgram
{
    class Program
    {
        static void Main()
        {
            int[] inputParams = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Buffer buffer = new Buffer(inputParams[0]);

            for(int i = 0; i < inputParams[1]; i++)
            {
                int[] packageData = Console.ReadLine().Split().Select(int.Parse).ToArray();
                buffer.AddProcess(packageData[0], packageData[1]);
            }
            buffer.ShowHandlingTime();
        }
    }

    class Buffer
    {
        List<int> handlingTimes = new List<int>();
        List<int> buffer = new List<int>();
        readonly int maxSize;
        int currentSize = 0;
        
        public Buffer(int bufferSize) {maxSize = bufferSize;}


        public void AddProcess(int arrival, int duration)
        {
            ClearHandled(arrival);
            if (currentSize < maxSize)
            {
                if(currentSize == 0)
                {
                    buffer.Add(arrival + duration);
                    handlingTimes.Add(arrival);
                }
                else
                {
                    int previousEnding = buffer[currentSize-1];
                    int distance = 0;
                    if (arrival > previousEnding)
                    {
                        distance = arrival - previousEnding;
                    }
                    buffer.Add(previousEnding + distance + duration);
                    handlingTimes.Add(previousEnding + distance);
                }
                currentSize++;
            }
            else handlingTimes.Add(-1);
        }

        void ClearHandled(int time)
        {
            while (currentSize != 0)
            {
                int endingTime = buffer.First();
                if (endingTime <= time)
                {
                    buffer.RemoveAt(0);
                    currentSize--;
                }
                else break;
            }
        }

        public void ShowHandlingTime()
        {
            foreach(int element in handlingTimes)
            {
                Console.WriteLine(element);
            }
        }
    }
}