using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        int[] lenghths = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int[] tablesData = Console.ReadLine().Split().Select(int.Parse).ToArray();

        Union_Find tablesUnions = new Union_Find(tablesData, lenghths[0]);
        int[] answer = new int[lenghths[1]];

        for (int i = 0; i < lenghths[1]; i++)
        {
            int[] unionPositions = Console.ReadLine().Split().Select(int.Parse).ToArray();
            tablesUnions.Union(unionPositions[0], unionPositions[1]);
            answer[i] = tablesUnions.maxSize;
        }

        foreach (int maxSize in answer)
        {
            Console.WriteLine(maxSize);
        }
    }
}


class Table
{
    public int link;
    public int records;

    public Table(int records,int selfIndex)
    {
        this.records = records;
        link = selfIndex;
    }
}

class Union_Find
{
    Table[] tables;
    public int maxSize = 0;

    public Union_Find(int[] tablesData, int size)
    {
        tables = new Table[size];
        MakeSets(tablesData, size);
    }


    public void Union(int destination, int source)
    {
        destination = Find(destination - 1);
        source = Find(source - 1);

        if (destination != source)
        {
            tables[destination].records += tables[source].records;
            tables[source].records = 0;
            tables[source].link = destination;
            maxSize = Math.Max(maxSize, tables[destination].records);
        }
    }


    int Find(int destination)
    {
        if (tables[destination].link != destination)
        {
            tables[destination].link = Find(tables[destination].link);
        }
        return tables[destination].link;
    }


    void MakeSets(int[] tablesData, int size)
    {
        for (int i = 0; i < size; i++)
        {
            tables[i] = new Table(tablesData[i],i);
            maxSize = Math.Max(maxSize, tablesData[i]);
        }
    }

}