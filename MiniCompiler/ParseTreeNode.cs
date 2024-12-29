public class ParseTreeNode
{
    public string Value { get; set; }
    public List<ParseTreeNode> Children { get; set; }

    public ParseTreeNode(string value)
    {
        Value = value;
        Children = new List<ParseTreeNode>();
    }

    // Method to add a child node
    public void AddChild(ParseTreeNode child)
    {
        Children.Add(child);
    }
}
