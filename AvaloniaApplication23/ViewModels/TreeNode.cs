using System;
using System.Collections.ObjectModel;

namespace AvaloniaApplication23.ViewModels;

public class TreeNode
{
    private readonly Func<object, ReadOnlyObservableCollection<TreeNode>> getChildren;
    public object Value { get; }
    public ReadOnlyObservableCollection<TreeNode> Children => getChildren(Value);
    public bool IsExpanded { get; set; } = true;
    public TreeNode(object value) : this(value, _ => new ReadOnlyObservableCollection<TreeNode>(new ObservableCollection<TreeNode>()))
    {
    }

    public TreeNode(object value, Func<object, ReadOnlyObservableCollection<TreeNode>> getChildren)
    {
        this.getChildren = getChildren;
        Value = value;
    }

    public override string? ToString()
    {
        return Value.ToString();
    }
}