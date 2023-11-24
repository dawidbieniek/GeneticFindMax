using System.ComponentModel;

namespace AE1.CustomControls;

internal abstract partial class GraphBase : Panel
{
	private Bitmap _placeholder;
	private float _startX = -1;
	private float _endX = 26;
	private bool _skipGraphDuringDesign;

	public GraphBase(bool skipDrawDuringDesign = false)
	{
		_skipGraphDuringDesign = skipDrawDuringDesign;
		_placeholder = GeneratePlaceholder();
	}

	[Category("Config")]
	[Description("Color of axes")]
	public Color AxisColor { get; set; } = Color.Black;
	[Category("Config")]
	[Description("Color of background")]
	public Color BackgroundColor { get; set; } = Color.White;
	[Category("Config")]
	[Description("Font of axis labels")]
	public Font LabelsFont { get; set; } = new(FontFamily.GenericSansSerif, 5);
	[Category("Config")]
	[Description("Should axes be drawn")]
	public bool DrawAxes { get; set; } = true;
	[Category("Config")]
	[Description("Should labels be drawn")]
	public bool DrawLabels { get; set; } = true;
	[Category("Config")]
	[Description("How much difference between points in graph")]
	public float DeltaX { get; set; } = 0.2f;
	[Category("Config")]
	[Description("How many labels should each axis have")]
	public int LabelDensity { get; set; } = 10;

	[Category("Config")]
	[Description("How much should graph be padded")]
	public float YOffset { get; set; } = 0;
	[Category("Config")]
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
	[Category("Config")]
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

	[Browsable(false)]
	public float? MinValue { get; protected set; } = null;
	[Browsable(false)]
	public float? MaxValue { get; protected set; } = null;

	protected virtual Bitmap GenerateGraph()
	{
		Bitmap bmp = new(Width, Height);

		using (Graphics g = Graphics.FromImage(bmp))
		{
			DrawBackground(g);

			if (_skipGraphDuringDesign && DesignMode)
				return bmp;

			DrawGraph(g);

			if (DrawAxes)
			{
				PointF axisPoint = DrawAxesOnGraph(g);
				if (DrawLabels)
				{
					DrawGraphLabels(g, axisPoint);
				}
			}

			DrawLegend(g);
		}

		return bmp;
	}

	protected virtual void DrawBackground(Graphics g)
	{
		using SolidBrush backgroundBrush = new(BackgroundColor);
		g.FillRectangle(backgroundBrush, 0, 0, Width, Height);
	}

	protected abstract void DrawGraph(Graphics g);

	protected virtual PointF DrawAxesOnGraph(Graphics g)
	{
		if (MinValue is null || MaxValue is null)
			throw new InvalidOperationException("MinValue & MaxValue must be set before calling this method");

		Pen axisPen = new(AxisColor);

		float xPixelDelta = Width / (EndX - StartX);
		float yPixelDelta = (Height - YOffset) / (MaxValue!.Value - MinValue!.Value);

		// Horizontal line
		float yAxisPos;

		if (MaxValue!.Value < 0)        // Top line
			yAxisPos = 1;
		else if (MinValue!.Value > 0)   // Bottom line
			yAxisPos = Height - YOffset;
		else                            // Middle line
			yAxisPos = MaxValue!.Value * yPixelDelta;

		g.DrawLine(axisPen, 0, yAxisPos, Width, yAxisPos);

		// Vertical line
		float xAxisPos;

		if (StartX > 0)                 // Left line
			xAxisPos = 1;
		else if (EndX < 0)              // Right line
			xAxisPos = Width - 1;
		else                            // Middle line
			xAxisPos = -StartX * xPixelDelta;

		g.DrawLine(axisPen, xAxisPos, 0, xAxisPos, Height);

		return new PointF(xAxisPos, yAxisPos);
	}

	protected virtual void DrawGraphLabels(Graphics g, PointF axisPoint)
	{
		if (MinValue is null || MaxValue is null)
			throw new InvalidOperationException("MinValue & MaxValue must be set before calling this method");

		SolidBrush axisBrush = new(AxisColor);
		Pen axisPen = new(AxisColor);

		float xLabelDelta = (EndX - StartX) / LabelDensity;
		float yLabelDelta = (MaxValue!.Value - MinValue!.Value) / LabelDensity;
		float xLabelPixelDelta = Width / LabelDensity;
		float yLabelPixelDelta = Height / LabelDensity;

		for (int i = 0; i < LabelDensity; i++)
		{
			g.DrawLine(axisPen, i * xLabelPixelDelta, axisPoint.Y - 2, i * xLabelPixelDelta, axisPoint.Y + 2);
			g.DrawString(MathF.Round(i * xLabelDelta + StartX, 1).ToString(), LabelsFont, axisBrush, i * xLabelPixelDelta, axisPoint.Y - 7);

			g.DrawLine(axisPen, axisPoint.X - 2, i * yLabelPixelDelta, axisPoint.X + 2, i * yLabelPixelDelta);
			g.DrawString(MathF.Round(MaxValue!.Value - i * yLabelDelta, 1).ToString(), LabelsFont, axisBrush, axisPoint.X + 5, i * xLabelPixelDelta);
		}
	}

	protected virtual void DrawLegend(Graphics g)
	{ }

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);

		_placeholder = GeneratePlaceholder();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);

		Bitmap bmp = GenerateGraph();

		e.Graphics.DrawImage(bmp ?? _placeholder, Point.Empty);
	}

	private Bitmap GeneratePlaceholder()
	{
		Bitmap bmp = new(Width, Height);
		using (Graphics graphics = Graphics.FromImage(bmp))
		{
			// Fill the entire bitmap with gray
			using Brush brush = new SolidBrush(BackgroundColor);
			graphics.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
		}

		return bmp;
	}
}