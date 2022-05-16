using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Sim.Core;

namespace Canvas;

public class CanvasShape
    : CanvasBackground
        , ICanvasShape<IShape>
{
    public event EventHandler<MouseButtonEventArgs>? GameInputEvent;

    protected List<IShape>? ShapesField;

    public List<IShape>? Shapes
    {
        get
        {
            ArgumentNullException.ThrowIfNull(ShapesField);
            return ShapesField;
        }
    }

    public Context Context { get; set; }

    public ShapeTypes SelectedShape { get; set; }

    public Color Color { get; set; }

    public double Radius { get; set; }

    public double SizeWidth { get; set; }

    public double SizeHeight { get; set; }

    public bool Filled { get; set; }

    public string? Flag { get; set; }

    public string Image { get; set; } = string.Empty;

    public CanvasShape(IRectangle background)
        : base(background) => Initialize();

    private void Initialize()
    {
        ShapesField = new List<IShape>();
        Context = Context.Graphic;
        SelectedShape = ShapeTypes.Circle;
        Color = Colors.Green;
        Radius = 100.0;
        SizeWidth = 100.0;
        SizeHeight = 100.0;
        Filled = true;
        MouseDown += HandleMouseDown;
    }

    public void UpdateShapes()
    {
        foreach (var shape in Shapes!)
        {
            shape.UpdateVisual();
        }
    }

    public void RemoveShape(IShape shape)
    {
        Shapes!.Remove(shape);
        Dispatcher.BeginInvoke((Action)(
            () => Visuals.Remove(shape.GetVisual())));
    }

    public void AddShape(IShape shape)
    {
        Shapes!.Add(shape);
        Dispatcher.BeginInvoke((Action)(
            () => Visuals.Add(shape.GetVisual())));
    }

    public override void ViewLoaded()
    {
        base.ViewLoaded();
        AddShapes();
    }

    protected void AddShapes()
    {
        foreach (var shape in Shapes!)
        {
            var visual = shape.GetVisual();
            if (!Visuals.Contains(visual))
                Visuals.Add(visual);
        }
    }

    public override void ViewSizeChanged()
    {
        base.ViewSizeChanged();
        UpdateShapes();
    }

    public void Clear(object sender,
        RoutedEventArgs routedEventArgs) => Clear();

    public virtual void Clear()
    {
        Visuals.Clear();
        Shapes!.Clear();
        SetBackgroundSize();
        Visuals.Add(Background.GetVisual());
    }

    protected virtual void HandleMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        if (mouseButtonEventArgs.LeftButton == MouseButtonState.Pressed)
            GameInputEvent?.Invoke(sender, mouseButtonEventArgs);
    }
}