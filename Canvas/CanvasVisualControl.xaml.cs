using System.Windows;
using System.Windows.Controls;
using Sim.Core;

namespace Canvas;

public partial class CanvasVisualControl
    : UserControl
        , ICanvasVisualControl<IShape>
{
    private readonly CanvasSerializaton canvas;

    public ICanvasSerializaton<IShape> Canvas { get; }

    public CanvasVisualControl(
        ICanvasSerializaton<IShape> canvasSerializaton)
    {
        Canvas = canvasSerializaton;
        canvas = (CanvasSerializaton)Canvas;
        InitializeComponent();
        Initialize();
    }

    private void Initialize()
    {
        Layout.Children.Add(canvas);
        Layout.Loaded += canvas.ViewLoaded;
        Layout.SizeChanged += canvas.ViewSizeChanged;
    }

    private void LayoutSizeChanged(
        object sender
        , SizeChangedEventArgs sizeChangedEventArgs)
    {
        canvas.Width = Layout.ActualWidth;
        canvas.Height = Layout.ActualHeight;
    }

    private void LayoutLoaded(
        object sender
        , RoutedEventArgs routedEventArgs)
    {
        canvas.Width = Layout.ActualWidth;
        canvas.Height = Layout.ActualHeight;
    }
}