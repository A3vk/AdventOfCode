namespace AdventOfCode._2023.Day08;

public class Network
{
    public Node? Root { get; private set; }

    public bool Add(Node value)
    {
        if (Root == null)
        {
            Root = value;
            return true;
        }

        return Root.Add(value);
    }
}

public class Node
{
    public string Value { get; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }

    public Node(string value) => Value = value;

    public bool Add(Node node)
    {
        if (Value == node.Value)
        {
            Left = node.Left;
            Right = node.Right;

            return true;
        }

        if (Left?.Add(node) == true) return true;
        if (Right?.Add(node) == true) return true;

        return false;
    }
}