using System;
using System.Collections.ObjectModel;
using DynamicData;

namespace AvaloniaApplication23.ViewModels;

public class Group
{
    private readonly ReadOnlyObservableCollection<TreeNode> items;
    public string Name { get; }

    public ReadOnlyObservableCollection<TreeNode> Children => items;

    public Group(string name, IObservable<IChangeSet<TreeNode, int>> changes)
    {
        Name = name;

        changes
            .Bind(out items)
            .Subscribe();
    }
}