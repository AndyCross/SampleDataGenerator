using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Elastacloud.Hadoop.SampleDataGenerationLib.Rule;
using NUnit.Framework;

namespace Elastacloud.Hadoop.SampleDataGenerationLib.Tests
{
    [TestFixture]
    public class RealisticTests
    {
        [Test]
        public void RealisticTest_Format_Int_String()
        {
            var format = "prefix\t{0}\tmidfix\t{1}\tpostfix";
            var dataRuleInt = new RandomIntDataGenerationRule(1, 1);
            var dataRuleDictionary = new DictionaryStringDataGenerationRule(new[] {"Alan Turing"});


            var generator = new SampleDataGenerator(format, new DataGenerationRule[] {dataRuleInt, dataRuleDictionary});
            var data = generator.GetData(1);

            Trace.TraceInformation("Data was {0}", data[0]);

            Assert.AreEqual(string.Format(format, 1, "Alan Turing"), data[0]);
        }
    }
}
