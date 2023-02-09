class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] tree = Array.ConvertAll<string, int>(Console.ReadLine().Split(), int.Parse);
        Node[] nodes = new Node[n];
        int maxHeight = 0;

        for (int i = 0; i < n; i++)
        {
            if (tree[i] == -1)
            {
                nodes[i] = new Node(1);
            }
            else
            {
                nodes[i] = new Node(0);
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (tree[i] == -1)
            {
                nodes[i].SetParent(null);
            }
            else
            {
                nodes[i].SetParent(nodes[tree[i]]);
            }
        }

        foreach (Node node in nodes)
        {
            maxHeight = Math.Max(maxHeight, node.GetFullHieght());
        }
        Console.WriteLine(maxHeight);
    }
}

class Node
{
    public Node Parent { get; private set; }
    int height;

    public Node(int height)
    {
        this.height = height;
    }

    public void SetParent(Node parent)
    {
        Parent = parent;
    }

    public int GetFullHieght()
    {
        if (height == 0)
        {
            height = 1 + Parent.GetFullHieght();
        }
        return height;
    }
}