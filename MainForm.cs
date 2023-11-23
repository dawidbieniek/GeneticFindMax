using System.Globalization;

namespace AE1;

public partial class MainForm : Form
{
	private static readonly CultureInfo DotForDecimalSeparatorCulture = new("en-US");

	private readonly string _defaultFunction = "-0.4*x^2+2*x+10";
	private readonly int _defaultStartX = -1;
	private readonly int _defaultEndX = 26;
	private readonly float _defaultPk = 80;
	private readonly float _defaultPm = 1;
	private readonly int _defaultPopulation = 100;
	private bool _wrongFunction;
	private bool _wrongParameters;
	private bool _isThreadPaused;

	private Thread? _gaThread;
	private ManualResetEvent? _gaThreadPausedEvent;
	private CancellationTokenSource? _gaCancelationToken;

	public MainForm()
	{
		CultureInfo.CurrentCulture = DotForDecimalSeparatorCulture;
		InitializeComponent();

		SetDefaultValues();
	}

	private bool WrongFunction
	{
		get => _wrongFunction;
		set
		{
			_wrongFunction = value;
			functionOk_button.Enabled = !value;
			UpdateStartButtonEnabledState();
		}
	}

	private bool WrongParameters
	{
		get => _wrongParameters;
		set
		{
			_wrongParameters = value;
			UpdateStartButtonEnabledState();
		}
	}

	private bool IsThreadPaused
	{
		get => _isThreadPaused;
		set
		{
			_isThreadPaused = value;
			start_button.Text = value ? "Start" : "Pause";
		}
	}

	private Thread? GaThread
	{
		get => _gaThread;
		set
		{
			_gaThread = value;

			reset_button.Enabled = value is not null;
		}
	}

	private void SetDefaultValues()
	{
		function_entry.Text = _defaultFunction;
		functionGraph_graph.EquationString = _defaultFunction;
		xFrom_entry.Text = _defaultStartX.ToString();
		functionGraph_graph.StartX = _defaultStartX;
		xTo_entry.Text = _defaultEndX.ToString();
		functionGraph_graph.EndX = _defaultEndX;
		WrongFunction = false;

		crossProb_entry.Text = _defaultPk.ToString();
		mutProb_entry.Text = _defaultPm.ToString();
		population_entry.Text = _defaultPopulation.ToString();
		WrongParameters = false;

		GaThread = null;
	}

	private void UpdateStartButtonEnabledState()
	{
		start_button.Enabled = !(WrongFunction || WrongParameters);
	}

	private void function_entry_TextChanged(object sender, EventArgs e)
	{
		if (MyRegexes.MathFunctionRegex().IsMatch(function_entry.Text))
		{
			function_entry.ForeColor = SystemColors.WindowText;
			WrongFunction = false;
		}
		else
		{
			function_entry.ForeColor = Color.Red;
			WrongFunction = true;
		}
	}

	private void functionOk_button_Click(object sender, EventArgs e)
	{
		functionGraph_graph.EquationString = function_entry.Text;
		functionGraph_graph.StartX = Convert.ToInt32(xFrom_entry.Text);
		functionGraph_graph.EndX = Convert.ToInt32(xTo_entry.Text);
	}

	private void xFrom_entry_TextChanged(object sender, EventArgs e)
	{
		if (MyRegexes.IntNumberRegex().IsMatch(xFrom_entry.Text))
		{
			xFrom_entry.ForeColor = SystemColors.WindowText;
			WrongFunction = false;
		}
		else
		{
			xFrom_entry.ForeColor = Color.Red;
			WrongFunction = true;
		}
	}

	private void xTo_entry_TextChanged(object sender, EventArgs e)
	{
		if (MyRegexes.IntNumberRegex().IsMatch(xTo_entry.Text))
		{
			xTo_entry.ForeColor = SystemColors.WindowText;
			WrongFunction = false;
		}
		else
		{
			xTo_entry.ForeColor = Color.Red;
			WrongFunction = true;
		}
	}

	private void crossProb_entry_TextChanged(object sender, EventArgs e)
	{
		if (MyRegexes.PositiveFloatNumberRegex().IsMatch(crossProb_entry.Text) && Convert.ToSingle(crossProb_entry.Text) <= 100f)
		{
			crossProb_entry.ForeColor = SystemColors.WindowText;
			WrongParameters = true;
		}
		else
		{
			crossProb_entry.ForeColor = Color.Red;
			WrongParameters = true;
		}
	}

	private void mutProb_entry_TextChanged(object sender, EventArgs e)
	{
		if (MyRegexes.PositiveFloatNumberRegex().IsMatch(mutProb_entry.Text) && Convert.ToSingle(mutProb_entry.Text) <= 100f)
		{
			mutProb_entry.ForeColor = SystemColors.WindowText;
			WrongParameters = true;
		}
		else
		{
			mutProb_entry.ForeColor = Color.Red;
			WrongParameters = true;
		}
	}

	private void population_entry_TextChanged(object sender, EventArgs e)
	{
		if (MyRegexes.PositiveIntNumberRegex().IsMatch(population_entry.Text))
		{
			population_entry.ForeColor = SystemColors.WindowText;
			WrongParameters = true;
		}
		else
		{
			population_entry.ForeColor = Color.Red;
			WrongParameters = true;
		}
	}

	private void start_button_Click(object sender, EventArgs e)
	{
		if (GaThread is null)
		{
			GeneticAlgorithm ga = new(Convert.ToSingle(crossProb_entry.Text), Convert.ToSingle(mutProb_entry.Text), Convert.ToInt32(population_entry.Text), function_entry.Text, Convert.ToInt32(xFrom_entry.Text), Convert.ToInt32(xTo_entry.Text), functionGraph_graph.MinValue ?? 0);

			_gaThreadPausedEvent = new(true);
			IsThreadPaused = false;

			_gaCancelationToken = new();
			GaThread = new(new ThreadStart(delegate { PerformGeneticAlgorithm(ga); }));

			GaThread.Start();
		}
		else
		{
			if (IsThreadPaused)
			{
				_gaThreadPausedEvent!.Set();
				IsThreadPaused = false;
			}
			else
			{
				_gaThreadPausedEvent!.Reset();
				IsThreadPaused = true;
			}
		}
	}

	/// <summary>
	/// Thread method. Should be only used inside another thread
	/// </summary>
	private void PerformGeneticAlgorithm(GeneticAlgorithm ga)
	{
		// Repeat stepping algorithm forever (max 100000 times)
		for (int i = 0; i < 100000; i++)
		{
			// Stop here if is paused or reset
			_gaThreadPausedEvent!.WaitOne();

			// Aborting thread
			if (_gaCancelationToken!.IsCancellationRequested)
			{
				if (!IsDisposed)
				{
					Invoke((MethodInvoker)delegate { SetLabels("0", "0", "0", "0"); });
					Invoke((MethodInvoker)delegate { DestoryThread(); });
				}
				break;
			}

			// Execute algorithm
			ga.Step();

			GeneticAlgorithm.StatisticalValues stats = ga.CurrentStatisticalValues;

			// Update labels
			if (!IsDisposed)
				Invoke((MethodInvoker)delegate { SetLabels(stats.Min.ToString("n2"), stats.Avg.ToString("n2"), stats.Max.ToString("n2"), i.ToString()); });
		}
	}

	private void SetLabels(string minText, string avgText, string maxText, string iterationText)
	{
		iteration_displayLabel.Text = iterationText;
		min_displayLabel.Text = minText;
		avg_displayLabel.Text = avgText;
		max_displayLabel.Text = maxText;
	}

	private void DestoryThread()
	{
		IsThreadPaused = true;
		GaThread = null;
	}

	private void reset_button_Click(object sender, EventArgs e)
	{
		_gaCancelationToken!.Cancel();
		if (IsThreadPaused)
			_gaThreadPausedEvent!.Set();
	}

	private void function_entry_Leave(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(function_entry.Text))
		{
			function_entry.Text = _defaultFunction;
			function_entry.ForeColor = SystemColors.WindowText;
			WrongFunction = false;
		}
	}

	private void xFrom_entry_Leave(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(xFrom_entry.Text))
		{
			xFrom_entry.Text = _defaultStartX.ToString();
			xFrom_entry.ForeColor = SystemColors.WindowText;
			WrongFunction = false;
		}
	}

	private void xTo_entry_Leave(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(xTo_entry.Text))
		{
			xTo_entry.Text = _defaultEndX.ToString();
			xTo_entry.ForeColor = SystemColors.WindowText;
			WrongFunction = false;
		}
	}

	private void crossProb_entry_Leave(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(crossProb_entry.Text))
		{
			crossProb_entry.Text = _defaultPk.ToString();
			crossProb_entry.ForeColor = SystemColors.WindowText;
			WrongParameters = false;
		}
	}

	private void mutProb_entry_Leave(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(mutProb_entry.Text))
		{
			mutProb_entry.Text = _defaultPm.ToString();
			mutProb_entry.ForeColor = SystemColors.WindowText;
			WrongParameters = false;
		}
	}

	private void population_entry_Leave(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(population_entry.Text))
		{
			population_entry.Text = _defaultPopulation.ToString();
			population_entry.ForeColor = SystemColors.WindowText;
			WrongParameters = false;
		}
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		_gaCancelationToken?.Cancel();
		// TODO: safely close thread
	}
}