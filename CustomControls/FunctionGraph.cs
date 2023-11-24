using System.ComponentModel;
using AE1.Helpers;
using NCalc;

namespace AE1.CustomControls;

internal partial class FunctionGraph : GraphBase
{
    private string _equationString = string.Empty;
    private Expression? _expression;

    [Description("Color of graph")]
    public Color GraphColor { get; set; } = Color.Red;
    [Category("Config")]
    [Description("Mathematical equation for graph to draw")]
    public string EquationString
    {
        get => _equationString;
        set
        {
            if (_equationString != value)
            {
                _equationString = value;
                _expression = new(NCalcHelpers.PreprocessEquation(value));
                Invalidate();
            }
        }
    }

    protected override void DrawGraph(Graphics g)
    {
        using Pen graphPen = new(GraphColor);
        float[] values = GetGraphPoints();

        float xPixelDelta = (float)Width / values.Length;
        float yPixelDelta = (float)(Height - YOffset) / (MaxValue!.Value - MinValue!.Value);

        float prevX = 0;
        float prevY = -((values[0] - MinValue!.Value) * yPixelDelta) + Height - YOffset;

        for (int i = 1; i < values.Length; i++)
        {
            float xImage = i * xPixelDelta;
            float yImage = -((values[i] - MinValue!.Value) * yPixelDelta) + Height - YOffset;

            g.DrawLine(graphPen, prevX, prevY, xImage, yImage);

            prevX = xImage;
            prevY = yImage;
        }
    }

    protected float[] GetGraphPoints()
    {
        int pointCount = (int)MathF.Ceiling((EndX - StartX) / DeltaX) + 1;
        float[] values = new float[pointCount];

        for (int i = 0; i < values.Length - 1; i++)
        {
            float x = i * DeltaX + StartX;
            _expression!.Parameters["x"] = x;
            values[i] = Convert.ToSingle(_expression.Evaluate());
        }

        // Calculate last value
        _expression!.Parameters["x"] = EndX;
        values[^1] = Convert.ToSingle(_expression.Evaluate());

        // Set min & max values of whole graph
        MinValue = values.Min();
        MaxValue = values.Max();

        return values;
    }
}