using System;
using System.Collections.ObjectModel;
using DynamicData;

namespace AvaloniaApplication23.ViewModels;

public class Group
{
    private readonly ReadOnlyObservableCollection<ViewModel> items;

    public Group(string name, IObservable<IChangeSet<ViewModel, int>> changes)
    {
        Name = name;

        changes
            .Bind(out items)
            .Subscribe();
    }

    public string Name { get; }

    public ReadOnlyObservableCollection<ViewModel> Children => items;
}