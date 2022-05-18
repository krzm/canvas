using MyShape = Shape.Model.Shape;

namespace Canvas.App;

public interface ICanvas
{
    List<MyShape> Shapes { get; }
}