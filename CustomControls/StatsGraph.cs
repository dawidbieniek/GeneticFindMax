namespace AE1.CustomControls;

internal partial class StatsGraph : GraphBase
{
	private readonly List<float> _min = [];
	private readonly List<float> _avg = [];
	private readonly List<float> _max = [];

	public StatsGraph() : base(true)
	{
		StartX = 0;
		EndX = Width;
	}

	public Color MinGraphColor { get; set; } = Color.Orange;
	public Color AvgGraphColor { get; set; } = Color.Green;
	public Color MaxGraphColor { get; set; } = Color.Blue;
	private bool CantDraw => _min.Count == 0 || _avg.Count == 0 || _max.Count == 0;

	public void AddStats(float min, float avg, float max)
	{
		if (_min.Count == Width)
		{
			_min.Clear();
			_avg.Clear();
			_max.Clear();

			StartX = EndX;
			EndX += EndX + Width;
		}
		_min.Add(min);
		_avg.Add(avg);
		_max.Add(max);

		Invalidate();
	}

	public void Clear()
	{
		_min.Clear();
		_avg.Clear();
		_max.Clear();

		StartX = 0;
		EndX = Width;

		Invalidate();
	}

	protected override void DrawGraph(Graphics g)
	{
		if (CantDraw) return;

		using Pen minPen = new(MinGraphColor);
		using Pen avgPen = new(AvgGraphColor);
		using Pen maxPen = new(MaxGraphColor);

		MinValue = _min.Min() - 5;
		MinValue = -5;
		MaxValue = _max.Max() + 5;

		float xPixelDelta = 1;
		float yPixelDelta = (float)(Height - YOffset) / (MaxValue!.Value - MinValue!.Value);

		DrawLine(g, _min, xPixelDelta, yPixelDelta, minPen);
		DrawLine(g, _avg, xPixelDelta, yPixelDelta, avgPen);
		DrawLine(g, _max, xPixelDelta, yPixelDelta, maxPen);
	}

	protected override PointF DrawAxesOnGraph(Graphics g)
	{
		return CantDraw ? PointF.Empty : base.DrawAxesOnGraph(g);
	}

	protected override void DrawGraphLabels(Graphics g, PointF axisPoint)
	{
		if (CantDraw) return;
		base.DrawGraphLabels(g, axisPoint);
	}

	protected override void DrawLegend(Graphics g)
	{
		Font font = new(FontFamily.GenericSansSerif, 10);
		SolidBrush fontBrush = new(Color.Black);

		Dictionary<string, SolidBrush> legend = new()
	{
		{ "Min", new SolidBrush(MinGraphColor) },
		{ "Avg", new SolidBrush(AvgGraphColor) },
		{ "Max", new SolidBrush(MaxGraphColor) }
	};

		SizeF measure = legend.Select((kv) => g.MeasureString(kv.Key, font)).Max(Comparer<SizeF>.Create((a, b) => a.Width.CompareTo(b.Width)));

		float padding = 0.2f;
		float colorPos = Width - measure.Width * (1 + padding) * 2;
		float textPos = Width - measure.Width * (1 + padding);

		int i = 0;
		foreach (KeyValuePair<string, SolidBrush> line in legend)
		{
			g.FillRectangle(line.Value, colorPos, Height / 2 + (1 + padding) * i * measure.Height, measure.Width, measure.Height);
			g.DrawString(line.Key, font, fontBrush, textPos, Height / 2 + (1 + padding) * i * measure.Height);
			i++;
		}
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
	}

	private void DrawLine(Graphics g, List<float> values, float xPixelDelta, float yPixelDelta, Pen pen)
	{
		float prevX = 0;
		float prevY = -((values[0] - MinValue!.Value) * yPixelDelta) + Height - YOffset;

		for (int i = 1; i < values.Count; i++)
		{
			float xImage = i * xPixelDelta;
			float yImage = -((values[i] - MinValue!.Value) * yPixelDelta) + Height - YOffset;

			g.DrawLine(pen, prevX, prevY, xImage, yImage);

			prevX = xImage;
			prevY = yImage;
		}
	}
}