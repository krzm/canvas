using System.Windows.Input;
using Sim.Core;
using MyShape = Shape.Model.Shape;

namespace Canvas.App;

public class MainViewModel
    : RescaleViewModel
{
    public event Action<List<MyShape>>? ChangeDataEvent;

    private readonly List<string> shapesKey;
    private readonly Dictionary<string, ICanvas> shapesProviders;
    private string? selectedCanvas;

    public string? SelectedCanvas
    {
        get => selectedCanvas;

        set
        {
            selectedCanvas = value;
            ChangeData();
            OnPropertyChanged(nameof(SelectedCanvas));
        }
    }

    public List<string> CanvasSource => shapesKey;

    public ICommand ChangeDataCommand { get; set; }

    public MainViewModel(
        Dictionary<string, ICanvas> shapesProviders
        , IConfigProvider configProvider) : base(configProvider)
    {
        this.shapesProviders = shapesProviders;
        shapesKey = this.shapesProviders.Keys.ToList();
        ChangeDataCommand = new ActionCommand(ChangeData);
        SelectedCanvas = shapesKey[0];
    }

    public void Initialize()
    {
        ArgumentNullException.ThrowIfNull(SelectedCanvas);
        ChangeDataEvent?.Invoke(shapesProviders[SelectedCanvas].Shapes);
    }

    private void ChangeData()
    {
        ArgumentNullException.ThrowIfNull(SelectedCanvas);
        ChangeDataEvent?.Invoke(shapesProviders[SelectedCanvas].Shapes);
    }
}