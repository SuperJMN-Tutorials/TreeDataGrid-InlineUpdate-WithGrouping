# TreeDataGrid with grouping and inline tranform.

## From Observable to `TreeDataGrid`

This covers a very common scenario in which you update data selectable data.

It's a very basic example that show shows how to transform an `Observable` to a changeset that can be grouped. 
Also, it showcases the `TransformWithInlineUpdate` method here: https://github.com/reactivemarbles/DynamicData/pull/636

# See it working

https://user-images.githubusercontent.com/3109851/192312747-03aec620-bbe4-4c7d-a886-a11dc6584a00.mp

Please, notice how the list updates and groups people by location. Each person has a unique Id. Also, People has an action that updates over time.
The interesting thing is that selection is kept while this happens. This means that each instance of `ViewModel` is reused amongst model updates.

Techologies & Frameworks used:
- [DynamicData](https://github.com/reactivemarbles/DynamicData)
- [Avalonia](https://github.com/AvaloniaUI/Avalonia)
- [ReactiveUI](https://www.reactiveui.net)

Credits to [Tomáš Filip](https://github.com/tomasfil) for his wonderful work.
