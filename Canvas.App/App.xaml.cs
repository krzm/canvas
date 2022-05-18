using System.Windows;
using System.Windows.Media;
using Shape.Model;
using Sim.Core;

namespace Canvas.App;

public partial class App
    : Application
{
    private void HandlingApplicationStartup(object sender, StartupEventArgs e)
    {
        IShapeFactory shapeFactory = new ShapeFactory();
        var background = (IRectangle)shapeFactory.GetShape(ShapeTypes.Rectangle, Colors.Aquamarine);

        ICanvasSerializaton<IShape> subControl = 
            new CanvasSerializaton(
                background
                , new SerializerXml());
        var control = new CanvasVisualControl(subControl);

        var data = new Dictionary<string, ICanvas>
            {
                { nameof(GameCanvas), new GameCanvas() }
                , { nameof(ShapesCanvas), new ShapesCanvas() }
                , { nameof(LineCanvas), new LineCanvas() }
            };

        var window = new MainWindow(control)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen
            ,
            WindowState = WindowState.Normal
        };
        var vm = new MainViewModel(data, new ConfigProvider());
        vm.ChangeDataEvent += window.AddShapes;
        vm.Initialize();
        window.DataContext = vm;

        MainWindow = window;
        MainWindow.Show();
    }
}