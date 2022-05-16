using System.Windows;
using System.Windows.Media;

namespace Canvas;

public abstract class CanvasVisual
    : FrameworkElement
{
    protected VisualCollection Visuals { get; private set; }

    public CanvasVisual()
    {
        Visuals = new VisualCollection(this);
        ClipToBounds = true;
    }

    protected override int VisualChildrenCount
    {
        get
        {
            return Visuals.Count;
        }
    }

    protected override Visual GetVisualChild(int index)
    {
        if (IsIndexOutOfBounds(index))
            throw new ArgumentOutOfRangeException(
                $"{nameof(GetVisualChild)}:{index}");
        return Visuals[index];
    }

    private bool IsIndexOutOfBounds(int index)
    {
        return index < 0 || index >= Visuals.Count;
    }
}