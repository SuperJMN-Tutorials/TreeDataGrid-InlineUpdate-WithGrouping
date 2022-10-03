using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApplication23.ViewModels;

public class TreeNode
{
    public TreeNode(object value) : this(value, Enumerable.Empty<TreeNode>())
    {
    }

    public TreeNode(object value, IEnumerable<TreeNode> children)
    {
        Value = value;
        Children = children;
    }

    public object Value { get; }
    public IEnumerable<TreeNode> Children { get; }

    public bool IsExpanded { get; set; } = true;

    public override string? ToString()
    {
        return Value.ToString();
    }
}