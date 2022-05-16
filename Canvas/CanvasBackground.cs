using System.Windows;
using Sim.Core;

namespace Canvas;

public class CanvasBackground
    : CanvasVisual
        , ICanvasBackground
{
    protected Size BackgroundSize;

    protected IRectangle Background { get; }

    public CanvasBackground(IRectangle background)
    {
        Background = background;
        BackgroundSize = new Size();
    }

    public void ViewLoaded(object sender,
        RoutedEventArgs routedEventArgs) => ViewLoaded();

    public virtual void ViewLoaded()
    {
        SetBackgroundSize();
        var visual = Background.GetVisual();
        if (!Visuals.Contains(visual))
            Visuals.Add(visual);
    }

    protected void SetBackgroundSize()
    {
        BackgroundSize.Width = Width;
        BackgroundSize.Height = Height;
        Background.Size = BackgroundSize;
    }

    public void ViewSizeChanged(object sender,
        SizeChangedEventArgs sizeChangedEventArgs) => ViewSizeChanged();

    public virtual void ViewSizeChanged()
    {
        SetBackgroundSize();
        Background.UpdateVisual();
    }
}