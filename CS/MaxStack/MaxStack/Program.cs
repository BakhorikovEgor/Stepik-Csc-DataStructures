using System;
using System.Linq;
using System.Collections.Generic;

namespace MaxStack
{
    class Program
    {
        static void Main()
        {
            Stack stack = new Stack(true);
            int n = int.Parse(Console.ReadLine());
            List<string> output = new List<string>();

            for (int i = 0; i < n; i++)
            {
                string[] s = Console.ReadLine().Split();

                if (s.Length > 1)
                {
                    stack.Push(int.Parse(s[1]));
                }
                else if (s[0] == "max")
                {

                    output.Add(stack.Max().ToString());
                }
                else
                {
                    stack.Pop();
                }
            }
            foreach (string s in output)
            {
                Console.WriteLine(s);
            }
        }
    }

    class Stack
    {
        List<int> stack = new List<int>();
        Stack maxValues;
        public int Lenght { get; set; } = 0;

        public Stack(bool isMaxStack)
        {
            if (isMaxStack)
            {
                maxValues = new Stack(false);
            }
            else
            {
                maxValues = null;
            }
        }

        public int Peek()
        {
            if (Lenght != 0)
            {
                return stack[Lenght - 1];
            }
            throw new ArgumentOutOfRangeException();
        }



        public void Push(int element)
        {
            if (maxValues != null) UpdateMax(element);
            stack.Add(element);
            Lenght++;
        }

        void UpdateMax(int element)
        {
            if (Lenght == 0)
            {
                maxValues.Push(element);
            }
            else
            {
                maxValues.Push(Math.Max(element, maxValues.Peek()));
            }
        }



        public int Pop()
        {
            if (Lenght != 0)
            {
                if (maxValues != null) maxValues.Pop();
                Lenght--;
                int temp = stack[Lenght];
                stack.RemoveAt(Lenght);
                return temp;
            }
            throw new ArgumentOutOfRangeException();
        }

        public int Max() => maxValues.Peek();

    }
}