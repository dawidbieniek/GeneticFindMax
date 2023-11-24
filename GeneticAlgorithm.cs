using NCalc;

namespace AE1;

internal class GeneticAlgorithm
{
	private static readonly Random Rnd = new();

	private readonly float _crossingProbability;
	private readonly float _mutationProbability;
	/// <summary>
	/// Number of bits used to encode population values (nearest, greater or equal, power of 2 of
	/// max value)
	/// </summary>
	private readonly int _chromosomeLength;
	/// <summary>
	/// Actual values of original function
	/// </summary>
	private readonly Expression _valueFunction;
	/// <summary>
	/// Fitness function values. Original function offset by min value
	/// </summary>
	private readonly Expression _fitFunction;
	private readonly int _minX;
	private readonly int _maxX;
	/// <summary>
	/// Max possible value of chromosome
	/// </summary>
	private readonly int _maxChromosomeValue;
	private int[] _population;

	public GeneticAlgorithm(float crossingProbability, float mutationProbability, int populationSize, string valueFunctionString, int minX, int maxX, float minY)
	{
		_crossingProbability = crossingProbability / 100;
		_mutationProbability = mutationProbability / 100;

		_minX = minX;
		_maxX = maxX;

		_chromosomeLength = (int)Math.Ceiling(Math.Log(maxX - minX, 2));

		_valueFunction = new(NCalcHelpers.PreprocessEquation(valueFunctionString));

		// Fitness function
		string offset = (minY < 0 ? "+" : "") + (-minY).ToString();
		_fitFunction = new(NCalcHelpers.PreprocessEquation(valueFunctionString + offset));

		// Population initialized with random values
		int maxValue = 1 << _chromosomeLength;  // Pow(2, _chromosomeLength)
		_population = Enumerable.Repeat(0, populationSize).Select(i => Rnd.Next(maxValue)).ToArray();

		_maxChromosomeValue = (int)Math.Pow(2, _chromosomeLength) - 1;
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

	/// <summary>
	/// Calculates fitness of each member of population
	/// </summary>
	/// <returns> <see cref="IEnumerable{T}"/> of fitness values </returns>
	private IEnumerable<float> EvaluateFitness() => _population.Select(i =>
	{
		_fitFunction.Parameters["x"] = Decode(i);
		float v = Convert.ToSingle(_fitFunction.Evaluate());
		return v * v;
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
	private int Code(int value) => (int)MathF.Round(((float)(value - _minX) * _maxChromosomeValue / (_maxX - _minX)));

	/// <summary>
	/// Converts value from range (0, Pow(2, _chromosomeLength)) to range (_minX, _maxX)
	/// </summary>
	private int Decode(int code) => (int)MathF.Round(((float)code * (_maxX - _minX) / _maxChromosomeValue) + _minX);

	internal record StatisticalValues(float Min, float Max, float Avg)
	{
	}
}