using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
class SlideWindows
{
    static void Main()
    {
        Stack<int> stack1 = new Stack<int>();
        Stack<int> stack1Max = new Stack<int>();
        Stack<int> stack2 = new Stack<int>();
        Stack<int> stack2Max = new Stack<int>();

        int n = int.Parse(Console.ReadLine());
        int[] data = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int windowSize = int.Parse(Console.ReadLine());

        StringBuilder output = new StringBuilder();

        foreach (int i in data)
        {
            stack1.Push(i);
            if (stack1Max.Count == 0) stack1Max.Push(i);
            else stack1Max.Push(Math.Max(stack1Max.Peek(), i));

            if (stack1.Count != windowSize)
            {
                if (stack2Max.Count != 0)
                {
                    output.Append(Math.Max(stack2Max.Peek(), stack1Max.Peek()) + " ");
                    stack2.Pop();
                    stack2Max.Pop();
                }
            }

            else
            {
                while (stack1.Count != 0)
                {
                    int firstInStack1 = stack1.Peek();
                    stack2.Push(firstInStack1);
                    if (stack2Max.Count == 0) stack2Max.Push(firstInStack1);
                    else stack2Max.Push(Math.Max(stack2Max.Peek(), firstInStack1));

                    stack1.Pop();
                    stack1Max.Pop();
                }
                output.Append(stack2Max.Peek() + " ");
                stack2.Pop();
                stack2Max.Pop();
            }
        }
        Console.WriteLine(output);
    }
}
