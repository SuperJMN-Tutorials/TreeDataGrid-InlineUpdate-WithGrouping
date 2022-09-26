using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Bogus;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication23.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        var f = new Faker("es");

        var changes = Observable
            .Interval(TimeSpan.FromSeconds(2), RxApp.MainThreadScheduler)
            .Select(_ => new Model(f.Random.Int(0, 1), f.PickRandom("Privado", "Público"), f.PickRandom("amarillo", "rojo", "verde", "negro", "azul")))
            .ToObservableChangeSet(x => x.Id)
            .TransformWithInlineUpdate(x => new TreeNode(new ViewModel(x)), (node, model) => {
                if (node.Value is ViewModel v)
                {
                    v.Model = model;
                } });

        var groupedChanges = changes
            .Group(x => ((ViewModel)x.Value).Model.Group)
            .Transform(x => new TreeNode(new Group(x.Key, x.Cache.Connect()), o => ((Group)o).Children));

        changes
            .Do(x =>
            {
                x.ToList().ForEach(y => Debug.WriteLine(y));
            })
            .Subscribe();

        groupedChanges
            .Bind(out var items)
            .Subscribe();

        Source = new HierarchicalTreeDataGridSource<TreeNode>(items)
        {
            Columns =
            {
                new HierarchicalExpanderColumn<TreeNode>(new TemplateColumn<TreeNode>("Group", new FuncDataTemplate<TreeNode>((node, ns) => Create(node))), x => x.Children, x => x.Children.Any(), x => x.IsExpanded),
                new TextColumn<TreeNode, string>("Id", model => model.Try<ViewModel, string>(x => x.Model.Id.ToString()) ?? ""),
                new TextColumn<TreeNode, string>("Color",  model => model.Try<ViewModel, string>(x => x.Model.Color.ToString()) ?? ""),
            },
        };

        Source.RowSelection!.SingleSelect = true;
    }

    private static Control Create(TreeNode node)
    {
        if (node is { Value: Group g})
        {
            return new TextBlock {Text = g.Name};
        }

        if (node is { Value: ViewModel vm})
        {
            return new CheckBox
            {
                [!ToggleButton.IsCheckedProperty] = new Binding("IsSelected"),
                DataContext = vm,
            };
        }

        return new TextBlock(){ Text = "Nope"};
    }

    public HierarchicalTreeDataGridSource<TreeNode> Source { get; }
}

public static class TreeNodeMixin
{
    public static TRet? Try<T, TRet>(this TreeNode node, Func<T, TRet> action)
    {
        if (node.Value is T value)
        {
            return action(value);
        }

        return default;
    }

    public static void With<T>(this TreeNode a, TreeNode another, Action<T, T> action)
    {
        if (a.Value is T va && another.Value is T vb)
        {
            action(va, vb);
        }
    }
}

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

public class ViewModel
{
    public ViewModel(Model model)
    {
        Model = model;
    }

    public Model Model { get; set; }

    [Reactive]
    public bool IsSelected { get; set; }

    public override string ToString()
    {
        return $"{nameof(Model)}: {Model}, {nameof(IsSelected)}: {IsSelected}";
    }
}

public class Model : ViewModelBase
{
    public int Id { get; }
    public string Color { get; }
    public string Group { get; }

    public Model(int id, string color, string group)
    {
        Id = id;
        Color = color;
        Group = group;
    }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Color)}: {Color}, {nameof(Group)}: {Group}";
    }
}