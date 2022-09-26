namespace AvaloniaApplication23.ViewModels;

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