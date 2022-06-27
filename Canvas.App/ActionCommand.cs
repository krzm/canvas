using System.Windows.Input;

namespace Canvas.App;

public class ActionCommand
    : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly Action action;

    public ActionCommand(Action action)
    {
        this.action = action;
    }

    public bool CanExecute(object? parameter)
        => true;

    public void Execute(object? parameter)
    {
        action();
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}