using Shape.Model;
using Sim.Core;
using MyShape = Shape.Model.Shape;

namespace Canvas.App;

public class GameCanvas
    : ICanvas
{
    private readonly List<MyShape> _list = new List<MyShape>();

    public List<MyShape> Shapes
    {
        get
        {
            if (_list.Count > 0) return _list;
            var gameData = new GameData(new SerializerXml(), @"C:\Tests\Game\GameShapes.xml");
            foreach (var shape in gameData.Shapes)
            {
                _list.Add((MyShape)shape);
            }
            return _list;
        }
    }
}