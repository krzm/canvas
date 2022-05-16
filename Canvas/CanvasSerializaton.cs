using System.Collections.Concurrent;
using System.Windows;
using Shape.Model;
using Sim.Core;
using ShapeModel = Shape.Model.Shape;

namespace Canvas;

public class CanvasSerializaton
    : CanvasShape
        , ICanvasSerializaton<IShape>
{
    private readonly ISerializer dataSerialization;
    private const string FilePath = @"C:\Tests\Shapes.xml";

    public CanvasSerializaton(IRectangle background,
        ISerializer dataSerialization) : base(background) =>
        this.dataSerialization = dataSerialization;

    public void Load(object sender,
        RoutedEventArgs routedEventArgs) => Load();

    public void Load()
    {
        var shapesSerializable = dataSerialization.Deserialize<ShapeContext>(FilePath);
        ConvertFromShapeType(shapesSerializable.Shapes);
    }

    private void ConvertFromShapeType(List<ShapeModel> shapes)
    {
        foreach (var shape in shapes)
        {
            Shapes!.Add(shape);
        }
        AddShapes();
    }

    public void Render(List<IShape> shapes)
    {
        foreach (var shape in shapes)
        {
            shape.UpdateVisual();
        }
    }

    public void Render(BlockingCollection<IShape> shapes)
    {
        foreach (var shape in shapes)
        {
            shape.UpdateVisual();
        }
    }

    public void RunDispatcher(Action action) => Dispatcher.BeginInvoke(action);

    public void Save(object sender,
        RoutedEventArgs routedEventArgs) => Save();

    public void Save()
    {
        var shapes = ConvertToShapeType();
        dataSerialization.Serialize(
            shapes,
            FilePath);
    }

    private List<ShapeModel> ConvertToShapeType()
    {
        var list = new List<ShapeModel>();
        foreach (var shape in Shapes!)
        {
            var shapeModel = shape as ShapeModel;
            ArgumentNullException.ThrowIfNull(shapeModel);
            list.Add(shapeModel);
        }
        return list;
    }
}