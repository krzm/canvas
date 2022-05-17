using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Shape.Model;
using Sim.Core;

namespace Canvas;

public class CanvasConfigDialog
    : CanvasCursor
        , ICanvasConfigDialog<IShape>
{
    private object? sender;

    public CanvasConfigDialog(
        IDictionary<SpecialShapes, IShape> specialShapes
        , ISerializer dataSerialization
        , IShapeFactory shapeFactory)
            : base(specialShapes, dataSerialization, shapeFactory) =>
                Initialize();

    private void Initialize()
    {
        Color = Colors.Red;
        SelectedShape = ShapeTypes.Circle;
    }

    public void SetingShapeHandler(object sender, EventArgs eventArgs)
    {
        if (sender is ComboBox comboBox)
            SelectedShape = (ShapeTypes)comboBox.SelectedValue;
        SetCursor();
    }

    public void OnSettingContext(object sender, EventArgs eventArgs)
    {
        if (sender is ComboBox comboBox)
            Context = (Context)comboBox.SelectedValue;
    }

    public void OnSettingShapeColor(object sender, EventArgs eventArgs)
    {
        //todo
    }

    public void OnSettingRadiusWidth(object sender, EventArgs eventArgs)
    {
        this.sender = sender;
        if (SelectedShape == ShapeTypes.Circle)
        {
            SetCircleRadius();
        }
        if (SelectedShape == ShapeTypes.Rectangle)
        {
            SetRectangleWidth();
        }
    }

    private void SetCircleRadius()
    {
        if (sender is Slider slider)
            Radius = slider.Value;
        if (sender is TextBox textBox)
            Radius = double.Parse(textBox.Text);
        if (ShapeCursor is Circle circle)
            circle.Radius = Radius;
    }

    private void SetRectangleWidth()
    {
        if (sender is Slider slider)
            SizeWidth = slider.Value;
        if (sender is TextBox textBox)
            SizeWidth = double.Parse(textBox.Text);
        if (ShapeCursor is Rectangle rectangle)
            rectangle.Size = new Size(SizeWidth, SizeHeight);
    }

    public void OnSettingHeight(object sender, EventArgs eventArgs)
    {
        this.sender = sender;
        if (SelectedShape == ShapeTypes.Rectangle)
        {
            SetRectangleHeight();
        }
    }

    private void SetRectangleHeight()
    {
        if (sender is Slider slider)
            SizeHeight = slider.Value;
        if (sender is TextBox textBox)
            SizeHeight = double.Parse(textBox.Text);
        if (ShapeCursor is Rectangle rectangle)
            rectangle.Size = new Size(SizeWidth, SizeHeight);
    }

    public void OnSettingFilledFlag(object sender, EventArgs eventArgs)
    {
        if (sender is CheckBox checkBox)
        {
            var isFilled = checkBox.IsChecked;
            Filled = isFilled.HasValue ? isFilled.Value : false;
        }
        ShapeCursor.IsColorFilled = Filled;
    }

    public void OnSettingTextFlag(object sender, EventArgs eventArgs)
    {
        if (sender is TextBox textBox)
            Flag = textBox.Text;
    }

    public void OnSettingImagePath(object sender, ImageEventArgs imageEventArgs)
    {
        Image = imageEventArgs?.ImagePath ?? string.Empty;
        ShapeCursor.RelativeImagePath = imageEventArgs?.ImagePath;
    }
}