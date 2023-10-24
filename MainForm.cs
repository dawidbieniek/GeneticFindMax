using System.Globalization;
using System.Runtime.CompilerServices;

namespace AE1;

//TODO: Better navigation (tab) in form

public partial class MainForm : Form
{
	private static readonly CultureInfo DotForDecimalSeparatorCulture = new("en-US");

	private bool _wrongFunction;
	private bool _wrongParameters;

	private float _minY;

	private Thread? _gaThread;
	private ManualResetEvent? _gaResetEvent;
	private bool _isThreadPaused;

	public MainForm()
	{
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
		functionGraph1_graph.EquationString = function_entry.Text;	// TODO: Rename graph
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
		if (!MyRegexes.PositiveFloatNumberRegex().IsMatch(crossProb_entry.Text) || Convert.ToSingle(crossProb_entry.Text, DotForDecimalSeparatorCulture) > 100f)
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
		if (!MyRegexes.PositiveFloatNumberRegex().IsMatch(mutProb_entry.Text) || Convert.ToSingle(mutProb_entry.Text, DotForDecimalSeparatorCulture) > 100f)
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
			GeneticAlgorithm ga = new(
				Convert.ToSingle(crossProb_entry.Text, DotForDecimalSeparatorCulture),
				Convert.ToSingle(mutProb_entry.Text, DotForDecimalSeparatorCulture),
				Convert.ToInt32(population_entry.Text),
				function_entry.Text,
				Convert.ToInt32(xFrom_entry.Text),
				Convert.ToInt32(xTo_entry.Text));

			_gaResetEvent = new(false);
			_gaThread = new(() =>
			{
				for (int i = 0; i < 100000; i++)
				{
					_gaResetEvent.WaitOne();
					ga.Step();

					var stats = ga.CurrentStatisticalValues;

					iteration_displayLabel.Text = i.ToString();
					min_displayLabel.Text = stats.Min.ToString();
					avg_displayLabel.Text = stats.Avg.ToString();
					max_displayLabel.Text = stats.Max.ToString();
					Thread.Sleep(1000);

				}
			});

			_isThreadPaused = false;
			_gaThread.Start();
		}
		else
		{
			if (_isThreadPaused)
			{
				_gaResetEvent!.Reset();
				_isThreadPaused = false;
			}
			else
			{
				_gaResetEvent!.Set();
				_isThreadPaused = true;
			}
		}
	}
}