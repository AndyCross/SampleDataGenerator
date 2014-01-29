using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elastacloud.Hadoop.SampleDataGenerationLib.Rule
{
    public class HierarchicalDataGenerationRule<TParent, TChild> : BaseHierarchicalDataGenerationRule<TParent>
    {
        private readonly Dictionary<TParent, Dictionary<TChild, double>> _probabilityMatrix;

        public HierarchicalDataGenerationRule(Dictionary<TParent, Dictionary<TChild, double>> probabilityMatrix)
        {
            _probabilityMatrix = probabilityMatrix;
        }

        public override object GenerateValue(int seed, TParent parentValue)
        {
            var childProbabilities = _probabilityMatrix[parentValue];
            Dictionary<double, TChild> orderedProbabilities = new Dictionary<double, TChild>();
            double sum = 0;
            foreach (var childProbability in childProbabilities)
            {
                sum += childProbability.Value;
                orderedProbabilities.Add(sum, childProbability.Key);
            }

            double random = new Random((int)DateTime.Now.Ticks).NextDouble();

            var nearestValueForProbability = orderedProbabilities.First(p => p.Key > random).Value;

            return nearestValueForProbability;
        }
    }
}
