namespace AE1;

internal partial class StatsGraph : GraphBase
{
	private readonly List<float> _min = [];
	private readonly List<float> _avg = [];
	private readonly List<float> _max = [];

	public StatsGraph() : base()
	{
		DrawDuringDesign = false;
	}

	public Color MinGraphColor { get; set; } = Color.Orange;
	public Color AvgGraphColor { get; set; } = Color.Green;
	public Color MaxGraphColor { get; set; } = Color.Blue;

	public void AddStats(float min, float avg, float max)
	{
		if (_min.Count == Width)
			_min.Clear();
		_min.Add(min);

		if (_avg.Count == Width)
			_avg.Clear();
		_avg.Add(avg);

		if (_max.Count == Width)
			_max.Clear();
		_max.Add(max);
	}

	protected override void DrawGraph(Graphics g)
	{
		if (_min.Count == 0 || _avg.Count == 0 || _max.Count == 0) return;

		using Pen minPen = new(MinGraphColor);
		using Pen avgPen = new(AvgGraphColor);
		using Pen maxPen = new(MaxGraphColor);

		for (int i = 1; i < _min.Count; i++)
			g.DrawLine(minPen, i - 1, _min[i - 1], i, _min[i]);

		for (int i = 1; i < _avg.Count; i++)
			g.DrawLine(avgPen, i - 1, _avg[i - 1], i, _avg[i]);

		for (int i = 1; i < _max.Count; i++)
			g.DrawLine(maxPen, i - 1, _max[i - 1], i, _max[i]);
		// TODO: Set min and max
		MinValue = -10;
		MaxValue = 300;
	}
}