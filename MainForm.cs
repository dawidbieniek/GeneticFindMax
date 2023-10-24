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

	public MainForm()
	{
		CultureInfo.CurrentCulture = DotForDecimalSeparatorCulture;
		InitializeComponent();
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
		if (!MyRegexes.MathFunctionRegex().IsMatch(function_entry.Text))
		{
			function_entry.ForeColor = Color.Red;
			WrongFunction = true;
		}
		else
		{
			function_entry.ForeColor = SystemColors.WindowText;
			WrongFunction = false;
		}
	}

	private void functionOk_button_Click(object sender, EventArgs e)
	{
		functionGraph1_graph.EquationString = function_entry.Text;  // TODO: Rename graph
	}

	private void xFrom_entry_TextChanged(object sender, EventArgs e)
	{
		if (!MyRegexes.IntNumberRegex().IsMatch(xFrom_entry.Text))
		{
			xFrom_entry.ForeColor = Color.Red;
			WrongFunction = true;
		}
		else
		{
			xFrom_entry.ForeColor = SystemColors.WindowText;
			WrongFunction = false;
			functionGraph1_graph.StartX = Convert.ToInt32(xFrom_entry.Text);
		}
	}

	private void xTo_entry_TextChanged(object sender, EventArgs e)
	{
		if (!MyRegexes.IntNumberRegex().IsMatch(xTo_entry.Text))
		{
			xTo_entry.ForeColor = Color.Red;
			WrongFunction = true;
		}
		else
		{
			xTo_entry.ForeColor = SystemColors.WindowText;
			WrongFunction = false;
			functionGraph1_graph.EndX = Convert.ToInt32(xTo_entry.Text);
		}
	}

	private void crossProb_entry_TextChanged(object sender, EventArgs e)
	{
		if (!MyRegexes.PositiveFloatNumberRegex().IsMatch(crossProb_entry.Text) || Convert.ToSingle(crossProb_entry.Text) > 100f)
		{
			crossProb_entry.ForeColor = Color.Red;
			WrongParameters = true;
		}
		else
		{
			crossProb_entry.ForeColor = SystemColors.WindowText;
			WrongParameters = false;
		}
	}

	private void mutProb_entry_TextChanged(object sender, EventArgs e)
	{
		if (!MyRegexes.PositiveFloatNumberRegex().IsMatch(mutProb_entry.Text) || Convert.ToSingle(mutProb_entry.Text) > 100f)
		{
			mutProb_entry.ForeColor = Color.Red;
			WrongParameters = true;
		}
		else
		{
			mutProb_entry.ForeColor = SystemColors.WindowText;
			WrongParameters = false;
		}
	}

	private void population_entry_TextChanged(object sender, EventArgs e)
	{
		if (!MyRegexes.PositiveIntNumberRegex().IsMatch(population_entry.Text))
		{
			population_entry.ForeColor = Color.Red;
			WrongParameters = true;
		}
		else
		{
			population_entry.ForeColor = SystemColors.WindowText;
			WrongParameters = false;
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
}