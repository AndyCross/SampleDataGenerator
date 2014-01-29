using System;

namespace Elastacloud.Hadoop.SampleDataGenerationLib.Rule
{
    public class RandomIntDataGenerationRule : DataGenerationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public RandomIntDataGenerationRule(int min, int max)
        {
            Max = max;
            Min = min;
        }

        public override object GenerateValue(int i)
        {
            return new Random((int) DateTime.Now.Ticks).Next(Min, Max);
        }
    }
}