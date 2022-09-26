using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication23.ViewModels;

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