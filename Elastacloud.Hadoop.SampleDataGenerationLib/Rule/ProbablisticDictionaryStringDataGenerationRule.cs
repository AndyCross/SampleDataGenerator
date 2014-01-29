using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Elastacloud.Hadoop.SampleDataGenerationLib.Rule
{
    public class ProbablisticDictionaryStringDataGenerationRule : DataGenerationRule
    {
        private readonly List<ProbablisticTuple> _tuples;

        public ProbablisticDictionaryStringDataGenerationRule(List<ProbablisticTuple> tuples)
        {
            _tuples = tuples;
        }

        public override object GenerateValue(int i1)
        {
            var random = new Random((int) DateTime.Now.Ticks);
            double seed = 0;

            // get a random number scoped to the number of tuples
            for (int i = 0; i < _tuples.Count; i++)
            {
                seed += random.NextDouble();
            }

            //find closest tuple
            var tuplesByProximity = _tuples.OrderBy(t => (t.Probability*_tuples.Count) - seed);
            var tuple = tuplesByProximity.FirstOrDefault(t => ((t.Probability*_tuples.Count) - seed) > 0);


            if (tuple == null && _tuples.Any()) return _tuples.First().Value;

            return tuple.Value;
        }
    }

    public class ProbablisticTuple
    {
        public double Probability { get; private set; }
        public string Value { get; private set; }

        public ProbablisticTuple(double probability, string value)
        {
            if (probability > 1D) Trace.TraceWarning("The correct usage of probability is the range 0d->1d");

            Probability = probability;
            Value = value;
        }
    }
}
