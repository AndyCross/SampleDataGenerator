using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Hadoop.MapReduce;

namespace Elastacloud.Hadoop.SampleDataMapReduceJob
{
    public class SampleReducer : ReducerCombinerBase
    {
        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            context.EmitKeyValue(key, values.Count().ToString(CultureInfo.InvariantCulture));
        }
    }
}