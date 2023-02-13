//Хеширование цепочками — один из наиболее популярных методов реализации
//хеш-таблиц на практике. Ваша цель в данной задаче — реализовать такую схему, используя таблицу с m ячейками и полиномиальной хеш-функцией на строках

//про полиномиальное кэширование можно прочитать в алгоритме Робина-Карпа

using System;
using System.Text;

class Program
{
    static void Main()
    {
        int m = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());

        ChainStringHashTable hashTable = new ChainStringHashTable(m);
        StringBuilder output = new StringBuilder();

        for (int i = 0; i < n; i++)
        {
            string[] request = Console.ReadLine().Split();
            switch (request[0][0])
            {
                case 'a':
                    hashTable.AddString(request[1]);
                    break;

                case 'd':
                    hashTable.DeleteString(request[1]);
                    break;

                case 'f':
                    output.Append(hashTable.FindString(request[1]) 
                        ?"yes\n" 
                        :"no\n");
                    break;

                case 'c':
                    output.Append(hashTable.Check(int.Parse(request[1])) + '\n');
                    break;
            }
        }

        Console.WriteLine(output.ToString());
    }
}class ChainStringHashTable
{
    LinkedList[] chains;
    const long moduleConst = 1000000007;
    const long polynomialBase = 263;


    public ChainStringHashTable(long m)
    {
        chains = new LinkedList[m];
        for (long i = 0; i < m; i++)
        {
            chains[i] = new LinkedList();
        }
    }


    public void AddString(string str)
    {
        long chainIndex = GetHash(str);
        chains[chainIndex].Add(str);
    }


    public void DeleteString(string str)
    {
        long chainIndex = GetHash(str);
        chains[chainIndex].Remove(str);
    }


    public bool FindString(string str)
    {
        long chainIndex = GetHash(str);
        return chains[chainIndex].Find(str);
    }


    public string Check(long i) => chains[i].GetChain();


    long GetHash(string str)
    {
        long modulePolynomialSum = 0;
        long currentBase = 1;
        for (int i = 0; i < str.Length; i++)
        {
            modulePolynomialSum = (modulePolynomialSum + ((long)str[i] * currentBase)) % moduleConst;
            currentBase = (currentBase * polynomialBase) % moduleConst;
        }

        return modulePolynomialSum % chains.Length;
    }
}class LinkedList
{
    Node head;

    public LinkedList()
    {
        head = null;
    }

    public void Add(string data)
    {
        if (Find(data) == true) return;

        Node node = new Node(data);
        if (head != null)
        {
            node.Next = head;
        }

        head = node;
    }


    public void Remove(string data)
    {
        if (head == null) return;

        if (head.Data == data )
        {
            head = head.Next;
            return;
        }

        Node previous = head;
        while (previous.Next != null)
        {
            if (previous.Next.Data == data)
            {
                if (previous.Next.Next == null) 
                {
                    previous.Next = null;
                }
                else
                {
                    previous.Next = previous.Next.Next;
                }

                break;
            }
            previous = previous.Next;
        }
    }


    public bool Find(string data)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data == data)
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }


    public string GetChain()
    {
        StringBuilder builder = new StringBuilder();
        Node current = head;
        while (current != null)
        {
            builder.Append(current.Data + ' ');
            current = current.Next;
        }

        return builder.ToString();
    }
}public class Node
{
    public string Data { get; set; }
    public Node Next { get; set; }


    public Node(string data)
    {
        Data = data;
    }
}
