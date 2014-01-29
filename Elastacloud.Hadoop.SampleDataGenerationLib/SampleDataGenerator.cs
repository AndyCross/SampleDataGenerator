using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Elastacloud.Hadoop.SampleDataGenerationLib.Rule;

namespace Elastacloud.Hadoop.SampleDataGenerationLib
{
    public class SampleDataGenerator
    {
        private readonly string _format;
        private readonly IEnumerable<DataGenerationRule> _dataGenerationRules;

        public SampleDataGenerator(string format, IEnumerable<DataGenerationRule> dataGenerationRules)
        {
            _format = format;
            _dataGenerationRules = dataGenerationRules;
        }

        public string[] GetData(int count)
        {
            var retValue = new List<string>();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1);
                retValue.Add(string.Format(_format, _dataGenerationRules.Select(t => RuleBasedDataFactory.GetData(t,i)).ToArray()));
            }
            return retValue.ToArray();
        }
    }

    public static class RuleBasedDataFactory
    {
        public static object GetData(DataGenerationRule dataGenerationRule, int i)
        {
            return dataGenerationRule.GenerateValue(i);
        }
    }


    public enum DataGenerationType
    {
        Int
    }
}
