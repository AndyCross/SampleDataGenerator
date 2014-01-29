using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elastacloud.Hadoop.SampleDataGenerationLib.Rule
{
    public abstract class DataGenerationRule : BaseDataGenerationRule
    {
        public abstract object GenerateValue(int seed);
    }
}
