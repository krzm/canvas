using System.Windows.Media;
using Shape.Model;
using Vector.Lib;
using MyShape = Shape.Model.Shape;

namespace Canvas.App;

public class LineCanvas
    : ICanvas
{
    private List<MyShape>? list;

    public List<MyShape> Shapes
    {
        get
        {
            if (list != null) return list;
            list = new List<MyShape>
                {
                    new Line
                    {
                        MassCenter = new Vector2(450.0, 150.0),
                        SecondPoint = new Vector2(350.0, 350.0),
                        Color = Colors.Red
                    }
                };
            return list;
        }
    }
}