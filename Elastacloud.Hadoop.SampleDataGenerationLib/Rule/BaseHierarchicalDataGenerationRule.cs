﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elastacloud.Hadoop.SampleDataGenerationLib.Rule
{
    public abstract class BaseHierarchicalDataGenerationRule<T> : BaseDataGenerationRule
    {
        public abstract object GenerateValue(int seed, T parentValue);
    }
}
