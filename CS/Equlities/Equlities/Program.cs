//Проверить, можно ли присвоить переменным целые значения, чтобы
//выполнить заданные равенства вида xi = xj и неравенства вида xp != xq.
//4.2 Автоматический анализ программ

using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] lenghts = Console.ReadLine().Split().Select(int.Parse).ToArray();
        char answer = '1';

        Union_Find equalityTrees = new Union_Find(lenghts[0]);

        for (int i = 0; i < lenghts[0]; i++)
        {
            equalityTrees.MakeSet(i);
        }

        for (int i = 0; i < lenghts[1]; i++)
        {
            int[] equalityData = Console.ReadLine().Split().Select(int.Parse).ToArray();
            equalityTrees.Union(equalityData[0] - 1, equalityData[1] - 1);
        }

        for (int i = 0; i < lenghts[2]; i++)
        {
            int[] inEqualityData = Console.ReadLine().Split().Select(int.Parse).ToArray();
            
            if (equalityTrees.Find(inEqualityData[0] - 1) == equalityTrees.Find(inEqualityData[1] - 1))
            {
                answer = '0';
                break;
            }
        }

        Console.WriteLine(answer);
    }
}


class Union_Find
{
    int[] variables;
    int[] ranks;
    

    public Union_Find(int size)
    {
        variables = new int[size];
        ranks = new int[size];
    }


    public void MakeSet(int position)
    {
        variables[position] = position;
        ranks[position] = 0;
    }


    public void Union(int varIndex1, int varIndex2)
    {
        varIndex1 = Find(varIndex1);
        varIndex2 = Find(varIndex2);

        if (varIndex1 == varIndex2) return;
        
        if (ranks[varIndex1] == ranks[varIndex2])
        {
            variables[varIndex2] = varIndex1;
            ranks[varIndex1] +=  1;
        }

        else if (ranks[varIndex1] > ranks[varIndex2])
        {
            variables[varIndex2] = varIndex1;
        }

        else variables[varIndex1] = varIndex2;

    }


    public int Find(int index)
    {
        if (index != variables[index])
        {
            variables[index] = Find(variables[index]);
        }
        return variables[index];
    }

}