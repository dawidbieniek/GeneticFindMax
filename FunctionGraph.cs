using System.ComponentModel;
using System.Text.RegularExpressions;

using NCalc;

namespace AE1;

internal partial class FunctionGraph : Panel
{
	private Bitmap? _bitmap;
	private string _equationString = string.Empty;
	private Expression? _expression;
	private float _startX = -10;
	private float _endX = 10;

	public FunctionGraph()
	{
	}

	[Category("Behavior")]
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

	[Category("Appearance")]
	[Description("Color of axes")]
	public Color AxisColor { get; set; } = Color.Black;
	[Category("Appearance")]
	[Description("Color of graph")]
	public Color GraphColor { get; set; } = Color.Red;
	[Category("Appearance")]
	[Description("Color of background")]
	public Color BackgroundColor { get; set; } = Color.White;
	[Category("Appearance")]
	[Description("Font size of axis labels")]
	public int AxisFontSize { get; set; } = 5;
	[Category("Appearance")]
	[Description("Should axes be drawn")]
	public bool DrawAxes { get; set; } = true;
	[Category("Behavior")]
	[Description("How much difference between points of graph")]
	public float DeltaX { get; set; } = 0.2f;
	public float StartX
	{
		get => _startX;
		set
		{
			if (_startX != value)
			{
				_startX = value;
				Invalidate();
			}
		}
	}
	public float EndX
	{
		get => _endX;
		set
		{
			if (_endX != value)
			{
				_endX = value;
				Invalidate();
			}
		}
	}
	public int LabelDensity { get; set; } = 10;

	public float YOffset { get; set; } = 0;

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);

		_bitmap = GenerateGraph(string.IsNullOrEmpty(EquationString));

		if (_bitmap is not null)
			e.Graphics.DrawImage(_bitmap, Point.Empty);
		else
		{
			Bitmap bmp = new(Width, Height);
			using (Graphics graphics = Graphics.FromImage(bmp))
			{
				// Fill the entire bitmap with gray
				using Brush brush = new SolidBrush(Color.Gray);
				graphics.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
			}
			e.Graphics.DrawImage(bmp, Point.Empty);
		}
	}

	private Bitmap GenerateGraph(bool empty = false)
	{
		Bitmap bitmap = new(Width, Height);

		using (Graphics g = Graphics.FromImage(bitmap))
		{
			SolidBrush backgroundBrush = new(BackgroundColor);

			// Clear background
			g.FillRectangle(backgroundBrush, 0, 0, Width, Height);

			if (!empty)
			{
				int pointCount = (int)MathF.Ceiling((EndX - StartX) / DeltaX) + 1;

				// Calculate all graph values
				float[] values = new float[pointCount];

				for (int i = 0; i < values.Length - 1; i++)
				{
					float x = (i * DeltaX) + StartX;
					_expression!.Parameters["x"] = x;
					values[i] = Convert.ToSingle(_expression.Evaluate());
				}

				_expression!.Parameters["x"] = EndX;
				values[^1] = Convert.ToSingle(_expression.Evaluate());

				// Draw graph
				DrawGraph(g, values);

				// Draw axes
				if (DrawAxes)
				{
					DrawAxesOnGraph(g, values);
				}
			}
		}

		return bitmap;
	}

	private void DrawGraph(Graphics g, float[] values)
	{
		Pen graphPen = new(GraphColor);

		float minVal = (float)values.Min();
		float maxVal = (float)values.Max();

		float xPixelDelta = (float)Width / values.Length;
		float yPixelDelta = (float)(Height - YOffset) / (maxVal - minVal);

		float prevX = 0;
		float prevY = -((values[0] - minVal) * yPixelDelta) + Height - YOffset;

		for (int i = 1; i < values.Length; i++)
		{
			float xImage = i * xPixelDelta;
			float yImage = -((values[i] - minVal) * yPixelDelta) + Height - YOffset;

			g.DrawLine(graphPen, prevX, prevY, xImage, yImage);

			prevX = xImage;
			prevY = yImage;
		}
	}

	private void DrawAxesOnGraph(Graphics g, float[] values)
	{
		Pen axisPen = new(AxisColor);

		float minY = (float)values.Min();
		float maxY = (float)values.Max();

		float yAxisPos;
		float xAxisPos;

		float xPixelDelta = Width / (EndX - StartX);
		float yPixelDelta = (Height - YOffset) / (maxY - minY);

		// Horizontal line
		if (maxY < 0)       // Top line
			yAxisPos = 1;
		else if (minY > 0)  // Bottom line
			yAxisPos = Height - YOffset;
		else                // Middle line
			yAxisPos = maxY * yPixelDelta;

		g.DrawLine(axisPen, 0, yAxisPos, Width, yAxisPos);

		// Vertical line
		if (StartX > 0)     // Left line
			xAxisPos = 1;
		else if (EndX < 0)  // Right line
			xAxisPos = Width - 1;
		else                // Middle line
			xAxisPos = -StartX * xPixelDelta;

		g.DrawLine(axisPen, xAxisPos, 0, xAxisPos, Height);

		DrawLabels(g, axisPen, minY, maxY, yAxisPos, xAxisPos);
	}

	private void DrawLabels(Graphics g, Pen axisPen, float minY, float maxY, float yAxisPos, float xAxisPos)
	{
		SolidBrush axisBrush = new(AxisColor);
		Font axisFont = new(FontFamily.GenericSansSerif, AxisFontSize);

		float xLabelDelta = (EndX - StartX) / LabelDensity;
		float yLabelDelta = (maxY - minY) / LabelDensity;
		float xLabelPixelDelta = Width / LabelDensity;
		float yLabelPixelDelta = Height / LabelDensity;

		for (int i = 0; i < LabelDensity; i++)
		{
			g.DrawLine(axisPen, i * xLabelPixelDelta, yAxisPos - 2, i * xLabelPixelDelta, yAxisPos + 2);
			g.DrawString(MathF.Round((i * xLabelDelta) + StartX, 1).ToString(), axisFont, axisBrush, i * xLabelPixelDelta, yAxisPos - 7);

			g.DrawLine(axisPen, xAxisPos - 2, i * yLabelPixelDelta, xAxisPos + 2, i * yLabelPixelDelta);
			g.DrawString(MathF.Round(maxY - (i * yLabelDelta), 1).ToString(), axisFont, axisBrush, xAxisPos + 5, i * xLabelPixelDelta);
		}
	}
}