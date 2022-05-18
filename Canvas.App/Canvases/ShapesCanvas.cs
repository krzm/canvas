using System.Windows;
using System.Windows.Media;
using Shape.Model;
using Vector.Lib;
using MyShape = Shape.Model.Shape;

namespace Canvas.App;

public class ShapesCanvas : ICanvas
{
    private List<MyShape>? list;

    public List<MyShape> Shapes
    {
        get
        {
            if (list != null) return list;
            list = new List<MyShape>
                {
                    new Rectangle
                    {
                        MassCenter = new Vector2(50.0, 50.0),
                        Size = new Size(50, 50),
                        Color = Colors.Black
                    },
                    new Circle
                    {
                        MassCenter = new Vector2(250.0, 250.0),
                        Radius = 50,
                        Color = Colors.Blue
                    },
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