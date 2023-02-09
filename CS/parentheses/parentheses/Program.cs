using System;
using System.Collections.Generic;
using System.Linq;

public class MainClass
{
    public static void Main()
    {

        Stack stack = new Stack();
        String s = Console.ReadLine();
        String output = "Success";
        String[] close = { "]", "}", ")" };
        String[] open = { "[", "{", "(" };
        String[] correct = { "{}", "[]", "()" };

        for (int i = 0; i < s.Length; i++)
        {
            string ch = s[i].ToString();
            if (close.Contains(ch))
            {
                if (stack.IsEmpty())
                {
                    output = (i + 1).ToString();
                    break;
                }

                string start = stack.Pop().Item1;
                if (correct.Contains(start + ch)) continue;


                output = (i + 1).ToString();
                stack.Clear();
                break;
            }
            else if (open.Contains(ch))
            {
                stack.Push(ch, i + 1);
            }
        }

        if (!stack.IsEmpty())
        {
            output = stack.Pop().Item2.ToString();
        }
        Console.WriteLine(output);
    }
}


public class Stack
{
    List<(string, int)> stack = new List<(string, int)>();
    int lenght = 0;

    public void Push(string value, int number)
    {
        stack.Add((value, number));
        lenght += 1;
    }

    public (string, int) Pop()
    {
        if (lenght != 0)
        {
            var output = stack[lenght - 1];
            stack.RemoveAt(lenght - 1);
            lenght -= 1;
            return output;
        }
        throw new OverflowException();
    }

    public bool IsEmpty() => lenght == 0;

    public void Clear()
    {
        stack.Clear();
        lenght = 0;
    }
}
