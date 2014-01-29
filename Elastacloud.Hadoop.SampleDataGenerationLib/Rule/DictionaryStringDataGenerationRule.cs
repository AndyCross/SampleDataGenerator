using System;

namespace Elastacloud.Hadoop.SampleDataGenerationLib.Rule
{
    public class DictionaryStringDataGenerationRule : DataGenerationRule
    {
        private readonly string[] _dictionary;

        public DictionaryStringDataGenerationRule(string[] dictionary)
        {
            _dictionary = dictionary;
        }

        public override object GenerateValue(int seed)
        {
            return _dictionary[new Random((int) DateTime.Now.Ticks).Next(0, _dictionary.Length)];
        }
    }
}