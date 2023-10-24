using System.Globalization;

namespace AE1;

//TODO: Better navigation (tab) in form

public partial class MainForm : Form
{
	private static readonly CultureInfo DotForDecimalSeparatorCulture = new("en-US");

	private bool _wrongFunction;
	private bool _wrongParameters;
	private bool _isThreadPaused;

	private Thread? _gaThread;
	private ManualResetEvent? _gaResetEvent;

	private readonly string _defaultFunction = "-0.4*x^2+2*x+10";
	private readonly int _defaultStartX = -1;
	private readonly int _defaultEndX = 26;
	private readonly float _defaultPk = 100;
	private readonly float _defaultPm = 1;
	private readonly int _defaultPopulation = 100;

	public MainForm()
	{
		CultureInfo.CurrentCulture = DotForDecimalSeparatorCulture;
		InitializeComponent();

		SetDefaultValues();
	}

	private void SetDefaultValues()
	{
		function_entry.Text = _defaultFunction;
		functionGraph1_graph.EquationString = _defaultFunction;
		xFrom_entry.Text = _defaultStartX.ToString();
		functionGraph1_graph.StartX = _defaultStartX;
		xTo_entry.Text = _defaultEndX.ToString();
		functionGraph1_graph.EndX = _defaultEndX;

		crossProb_entry.Text = _defaultPk.ToString();
		mutProb_entry.Text = _defaultPm.ToString();
		population_entry.Text = _defaultPopulation.ToString();
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
			if (!value)
			{
				start_button.Text = "Pause";
			}
		}
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
		functionGraph1_graph.EquationString = function_entry.Text;  // TODO: Rename graph
		functionGraph1_graph.StartX = Convert.ToInt32(xFrom_entry.Text);
		functionGraph1_graph.EndX = Convert.ToInt32(xTo_entry.Text);
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
		if (_gaThread is null)
		{
			GeneticAlgorithm ga = new(Convert.ToSingle(crossProb_entry.Text),
				Convert.ToSingle(mutProb_entry.Text),
				Convert.ToInt32(population_entry.Text),
				function_entry.Text,
				Convert.ToInt32(xFrom_entry.Text),
				Convert.ToInt32(xTo_entry.Text));

			_gaResetEvent = new(true);
			IsThreadPaused = false;

			_gaThread = new(new ThreadStart(delegate { PerformGeneticAlgorithm(ga); }));

			_gaThread.Start();
		}
		else
		{
			if (IsThreadPaused)
			{
				_gaResetEvent!.Set();
				IsThreadPaused = false;
			}
			else
			{
				_gaResetEvent!.Reset();
				IsThreadPaused = true;
			}
		}
	}

	/// <summary>
	/// Thread method. Should be only used inside another thread
	/// </summary>
	private void PerformGeneticAlgorithm(GeneticAlgorithm ga)
	{
		for (int i = 0; i < 100000; i++)
		{
			_gaResetEvent!.WaitOne();
			ga.Step();

			GeneticAlgorithm.StatisticalValues stats = ga.CurrentStatisticalValues;

			Invoke((MethodInvoker)delegate
		{
			iteration_displayLabel.Text = i.ToString();
			min_displayLabel.Text = stats.Min.ToString("n2");
			avg_displayLabel.Text = stats.Avg.ToString("n2");
			max_displayLabel.Text = stats.Max.ToString("n2");
		});

			//Thread.Sleep(500);
		}
	}

	private void reset_button_Click(object sender, EventArgs e)
	{
	}

	private void function_entry_Leave(object sender, EventArgs e)
	{
		if(string.IsNullOrEmpty(function_entry.Text))
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
}