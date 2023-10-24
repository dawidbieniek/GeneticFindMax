using System.Security.Policy;

using NCalc;

namespace AE1;

// TODO: Change names of parameters
internal class GeneticAlgorithm
{
	private static readonly Random Rnd = new();

	private readonly float _crossingProbability;
	private readonly float _mutationProbability;
	private readonly int _chromosomeLength;
	private readonly Expression _valueFunction;
	private readonly int _minX;
	private readonly int _maxX;
	private readonly int _maxValue;
	private int[] _population;

	public GeneticAlgorithm(float crossingProbability, float mutationProbability, int populationSize, string valueFunctionString, int minX, int maxX)
	{
		_crossingProbability = crossingProbability / 100;
		_mutationProbability = mutationProbability / 100;

		_minX = minX;
		_maxX = maxX;

		// Chromosome length is nearest, greater or equal, power of 2
		_chromosomeLength = (int)Math.Ceiling(Math.Log(maxX - minX, 2));
		// Value function is math function offset by smallest value
		float minY = FindFunctionMin(valueFunctionString, 0.2f);    // TODO: To chyba nie najlepszy sposób
		_valueFunction = new(NCalcHelpers.PreprocessEquation(valueFunctionString + $"+{minY}"));
		// Population initialized with random values
		int maxValue = 1 << _chromosomeLength;
		_population = Enumerable.Repeat(0, populationSize).Select(i => Rnd.Next(maxValue)).ToArray();

		_maxValue = (int)Math.Pow(2, _chromosomeLength) - 1;
	}

	public StatisticalValues CurrentStatisticalValues
	{
		get
		{
			IEnumerable<float> values = _population.Select(
			v =>
			{
				_valueFunction.Parameters["x"] = Decode(v);
				return Convert.ToSingle(_valueFunction.Evaluate());
			}).ToArray();

			return new(values.Min(), values.Max(), values.Average());
		}
	}

	/// <summary>
	/// Go through 1 iteration of algorithm (selection, crossing, mutation)
	/// </summary>
	public void Step()
	{
		_population = DoMutation(DoCrossing(DoSelection(EvaluateFitness())));
	}

	private float FindFunctionMin(string function, float deltaX)
	{
		float min = float.MaxValue;
		Expression expr = new(NCalcHelpers.PreprocessEquation(function));
		for (float x = _minX; x <= _maxX; x += deltaX)
		{
			expr.Parameters["x"] = x;
			float value = Convert.ToSingle(expr.Evaluate());
			if (value < min)
				min = value;
		}

		return min;
	}

	/// <summary>
	/// Calculates fitness of each member of population
	/// </summary>
	/// <returns> <see cref="IEnumerable{T}"/> of fitness values </returns>
	private IEnumerable<float> EvaluateFitness() => _population.Select(i =>
	{
		_valueFunction.Parameters["x"] = Decode(i);
		return Convert.ToSingle(_valueFunction.Evaluate());
	});

	/// <summary>
	/// Performs selection of members from population based on roulette wheel
	/// </summary>
	private int[] DoSelection(IEnumerable<float> fitness)
	{
		float totalFitness = fitness.Sum();

		// Create 'the wheel'
		List<float> cumulativeProbabilities = [];
		float sum = 0;
		foreach (float fit in fitness)
		{
			sum += fit / totalFitness;
			cumulativeProbabilities.Add(sum);
		}

		int[] selected = new int[_population.Length];

		// Spin 'the wheel'
		for (int i = 0; i < _population.Length; i++)
		{
			float value = Rnd.NextSingle();
			int index = -1;

			// Find the first item whose cumulative probability is greater than spun value
			for (int j = 0; j < cumulativeProbabilities.Count; j++)
			{
				if (value <= cumulativeProbabilities[j])
				{
					index = j;
					break;
				}
			}

			if (index == -1)        // Should never happen
				throw new InvalidOperationException("Wheel generated value out of bounds");

			selected[i] = _population[index];
		}

		return selected;
	}

	/// <summary>
	/// Performs crossover of parent population by randomly choosing 2 members and crossing their
	/// genome. Point of crossover is selected randomly
	/// </summary>
	/// <param name="selected">
	/// Array of parents. Should be already shuffled. If contains odd number of elements, new
	/// population will be smaller by 1
	/// </param>
	/// <returns> </returns>
	private int[] DoCrossing(int[] selected)
	{
		int[] crossed = new int[selected.Length / 2 * 2];

		for (int i = 0; i < crossed.Length - 1; i += 2)
		{
			if (Rnd.NextSingle() < _crossingProbability)
			{
				int crossoverPoint = Rnd.Next(1, _chromosomeLength - 1);

				int rightMask = (1 << crossoverPoint) - 1;
				int leftMask = ~rightMask;

				// Create offspring by combining parent genes
				crossed[i] = (selected[i] & rightMask) | (selected[i + 1] & leftMask);
				crossed[i + 1] = (selected[i] & leftMask) | (selected[i + 1] & rightMask);
			}
			else
			{
				crossed[i] = selected[i];
				crossed[i + 1] = selected[i + 1];
			}
		}

		return crossed;
	}

	/// <summary>
	/// Performs mutation of population. Tries to mutate every bit of every member of population
	/// </summary>
	private int[] DoMutation(int[] selected)
	{
		int[] mutated = (int[])_population.Clone();

		for (int i = 0; i < selected.Length; i++)
		{
			for (int j = 0; j < _chromosomeLength; j++)
			{
				if (Rnd.NextSingle() < _mutationProbability)
				{
					// Flip bit at current position
					mutated[i] ^= 1 << j;
				}
			}
		}

		return mutated;
	}

	/// <summary>
	/// Converts value from range (_minX, _maxX) to range (0, Pow(2, _chromosomeLength))
	/// </summary>
	private int Code(int value) => (int)MathF.Round(((float)(value - _minX) * _maxValue / (_maxX - _minX)));

	/// <summary>
	/// Converts value from range (0, Pow(2, _chromosomeLength)) to range (_minX, _maxX)
	/// </summary>
	private int Decode(int code) => (int)MathF.Round(((float)code * (_maxX - _minX) / _maxValue) + _minX);

	internal record StatisticalValues(float Min, float Max, float Avg)
	{
	}
}