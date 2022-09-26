using System;

namespace AvaloniaApplication23.ViewModels;

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