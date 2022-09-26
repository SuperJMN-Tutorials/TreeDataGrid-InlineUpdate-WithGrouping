# TreeDataGrid-InlineUpdate-WithGrouping
Sample of a [`TreeDataGrid`](https://github.com/AvaloniaUI/Avalonia.Controls.TreeDataGrid) with inline updates and grouping.

It uses an observable to update a TreeDataGrid.
Shows how to transform an `Observable` to a changeset that can be grouped. Also, it showcases the `TransformWithInlineUpdate` method here: https://github.com/reactivemarbles/DynamicData/pull/636

Uses 
- [DynamicData](https://github.com/reactivemarbles/DynamicData)
- [Avalonia](https://github.com/AvaloniaUI/Avalonia)
- [ReactiveUI](https://www.reactiveui.net)

https://user-images.githubusercontent.com/3109851/192312747-03aec620-bbe4-4c7d-a886-a11dc6584a00.mp

Please, notice how the list updates and groups people by location. Each person has a unique Id. Also, People has an action that updates over time.
The interesting thing is that selection is kept while this happens. This means that each instance of `ViewModel` is reused amongst model updates.

Credits to [Tomáš Filip](https://github.com/tomasfil) for his wonderful work.
