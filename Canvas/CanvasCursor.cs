using System.Windows;
using System.Windows.Input;
using Shape.Model;
using Sim.Core;
using Vector.Lib;

namespace Canvas;

public class CanvasCursor
    : CanvasSerializaton
        , ICanvasCursor<IShape>
{
    private readonly IShapeFactory shapeFactory;
    protected IShape ShapeCursor;
    protected IShape? ActiveShape;
    private Point mousePoint;
    private Vector2 mousePoint3D;

    public CanvasCursor(
        IDictionary<SpecialShapes, IShape> specialShapes
        , ISerializer dataSerialization
        , IShapeFactory shapeFactory)
            : base(
                (IRectangle)specialShapes[SpecialShapes.Background]
                , dataSerialization)
    {
        ShapeCursor = specialShapes[SpecialShapes.Currsor];
        this.shapeFactory = shapeFactory;
        Initialize();
    }

    private void Initialize()
    {
        mousePoint3D = new Vector2();
        MouseMove += HandleMouseMove;
        MouseDown += HandleMouseDown;
    }

    public override void ViewLoaded()
    {
        base.ViewLoaded();
        Visuals.Add(ShapeCursor.GetVisual());
    }

    public void SetCursor()
    {
        ShapeCursor = shapeFactory.GetShape(
            SelectedShape, Color, Filled,
            Radius, SizeWidth, SizeHeight);
        Visuals.Add(ShapeCursor.GetVisual());
    }

    public override void Clear()
    {
        base.Clear();
        Visuals.Add(ShapeCursor.GetVisual());
    }

    protected void HandleMouseMove(
        object sender
        , MouseEventArgs mouseEventArgs)
    {
        SetCursorPosition(sender, mouseEventArgs);
        ShapeCursor.UpdateVisual();
    }

    private void SetCursorPosition(
        object sender
        , MouseEventArgs mouseEventArgs)
    {
        mousePoint = mouseEventArgs.GetPosition((UIElement)sender);
        ShapeCursor.MassCenter = new Vector2(mousePoint.X, mousePoint.Y);
    }

    protected override void HandleMouseDown(
        object sender
        , MouseButtonEventArgs mouseButtonEventArgs)
    {
        mousePoint = mouseButtonEventArgs.GetPosition((UIElement)sender);
        if (SelectedShape == ShapeTypes.Line)
        {
            HandleLineMouseDown();
        }
        else
        {
            SetActiveShape();
        }
        AddActiveShapeToShapes();
    }

    private void HandleLineMouseDown()
    {
        if (ActiveShape is ILine line)
        {
            HandleLineSecondPoint(line);
        }
        else SetActiveShapeAsLine();
    }

    private void HandleLineSecondPoint(ILine line)
    {
        mousePoint3D = new Vector2(mousePoint.X, mousePoint.Y);
        line.SecondPoint = mousePoint3D;
        //todo: add empty object
        ActiveShape = null;
    }

    private void SetActiveShapeAsLine()
    {
        ArgumentNullException.ThrowIfNull(Flag);
        ActiveShape = shapeFactory.GetShape(
            SelectedShape, Context, mousePoint,
            Color, Flag, Filled,
            Radius, SizeWidth, SizeHeight);
    }

    private void SetActiveShape()
    {
        ArgumentNullException.ThrowIfNull(Flag);
        ActiveShape = shapeFactory.GetShape(
            SelectedShape, Context, mousePoint,
            Color, Flag, Filled,
            Radius, SizeWidth, SizeHeight,
            Image);
    }

    private void AddActiveShapeToShapes()
    {
        if (ActiveShape != null)
        {
            Shapes!.Add(ActiveShape);
            Visuals.Add(ActiveShape.GetVisual());
        }
    }
}