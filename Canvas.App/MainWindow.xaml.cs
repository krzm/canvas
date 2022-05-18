using System.Windows;
using MyShape = Shape.Model.Shape;

namespace Canvas.App;

public partial class MainWindow
    : Window
{
    private readonly CanvasVisualControl canvasVisualControl;

    public MainWindow(
        CanvasVisualControl canvasVisualControl)
    {
        InitializeComponent();
        this.canvasVisualControl = canvasVisualControl;
        RootLayout.Children.Add(this.canvasVisualControl);
    }

    public void AddShapes(List<MyShape> shapes)
    {
        canvasVisualControl.Canvas.Clear();
        canvasVisualControl.Canvas.Shapes?.AddRange(shapes);
        canvasVisualControl.Canvas.ViewLoaded();
    }

    private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var vm = (RescaleViewModel)DataContext;
        if (WindowState == WindowState.Maximized)
        {
            vm.MaximaziedScale();
        }
        else
        {
            vm.DefaultScale();
        }
    }
}