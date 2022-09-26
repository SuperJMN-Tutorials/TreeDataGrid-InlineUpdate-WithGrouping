using System;
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

namespace AvaloniaApplication23.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        var f = new Faker("es");

        var changes = Observable
            .Interval(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
            .Select(_ => new Model(f.Random.Int(0, 10), f.PickRandom("Chilling", "Doing nothing", "Sleeping", "Eating", "Living la vida loca"), f.PickRandom("Living room", "Lounge", "Swimming pool", "Room", "Disco")))
            .ToObservableChangeSet(x => x.Id)
            .TransformWithInlineUpdate(x => new TreeNode(new ViewModel(x)), (node, model) =>
            {
                if (node.Value is ViewModel v)
                {
                    v.Model = model;
                }
            });

        var groupedChanges = changes
            .Group(x => ((ViewModel) x.Value).Model.Group)
            .Transform(x => new TreeNode(new Group(x.Key, x.Cache.Connect()), o => ((Group) o).Children));

        changes
            .Do(x => { x.ToList().ForEach(y => Debug.WriteLine(y)); })
            .Subscribe();

        groupedChanges
            .Bind(out var items)
            .Subscribe();

        Source = new HierarchicalTreeDataGridSource<TreeNode>(items)
        {
            Columns =
            {
                new HierarchicalExpanderColumn<TreeNode>(new TemplateColumn<TreeNode>("Location", new FuncDataTemplate<TreeNode>((node, ns) => Create(node))), x => x.Children, x => x.Children.Any(), x => x.IsExpanded),
                new TextColumn<TreeNode, string>("Person", model => model.Try<ViewModel, string>(x => x.Model.Id.ToString()) ?? ""),
                new TextColumn<TreeNode, string>("Action", model => model.Try<ViewModel, string>(x => x.Model.Color.ToString()) ?? "")
            }
        };
    }

    public HierarchicalTreeDataGridSource<TreeNode> Source { get; }

    private static Control Create(TreeNode node)
    {
        if (node is {Value: Group g})
        {
            return new TextBlock {Text = g.Name};
        }

        if (node is {Value: ViewModel vm})
        {
            return new CheckBox
            {
                [!ToggleButton.IsCheckedProperty] = new Binding("IsSelected"),
                DataContext = vm
            };
        }

        return new TextBlock();
    }
}