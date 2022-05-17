using System.Windows;
using System.Windows.Controls;
using Sim.Core;

namespace Canvas;

public partial class CanvasEditorControl
    : UserControl
        , ICanvasEditorControl<IShape>
{
    private readonly CanvasConfigDialog canvas;

    public ICanvasConfigDialog<IShape> Canvas { get; }

    public CanvasEditorControl(
        ICanvasConfigDialog<IShape> canvasConfigDialog)
    {
        Canvas = canvasConfigDialog;
        canvas = (CanvasConfigDialog)Canvas;
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