using System.Windows.Input;
using Sim.Core;
using MyShape = Shape.Model.Shape;

namespace Canvas.App;

public class MainViewModel
    : RescaleViewModel
{
    public event Action<List<MyShape>>? ChangeDataEvent;

    private readonly List<string> _shapesKey;
    private readonly Dictionary<string, ICanvas> _shapesProviders;
    private string? _selectedCanvas;

    public string? SelectedCanvas
    {
        get => _selectedCanvas;

        set
        {
            _selectedCanvas = value;
            ChangeData();
            OnPropertyChanged(nameof(SelectedCanvas));
        }
    }

    public List<string> CanvasSource => _shapesKey;

    public ICommand ChangeDataCommand { get; set; }

    public MainViewModel(
        Dictionary<string, ICanvas> shapesProviders
        , IConfigProvider configProvider) : base(configProvider)
    {
        _shapesProviders = shapesProviders;
        _shapesKey = _shapesProviders.Keys.ToList();
        ChangeDataCommand = new ActionCommand(ChangeData);
        SelectedCanvas = _shapesKey[0];
    }

    public void Initialize() =>
        ChangeDataEvent(_shapesProviders[SelectedCanvas].Shapes);

    private void ChangeData() =>
        ChangeDataEvent?.Invoke(_shapesProviders[SelectedCanvas].Shapes);
}