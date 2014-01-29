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
    public class DictionaryDataGenerationTest
    {
        [Test]
        public void DictionaryDataGeneration_SingleItem_Returned()
        {
            var rule = new DictionaryStringDataGenerationRule(new[] {"hello"});
            var sampleDataGenerator = new SampleDataGenerator("{0}", new[] {rule});

            var data = sampleDataGenerator.GetData(1);

            Assert.AreEqual("hello", data[0]);
        }

        [Test]
        public void DictionaryDataGeneration_TwoItems_OneReturned()
        {
            var rule = new DictionaryStringDataGenerationRule(new[] { "hello", "world" });
            var sampleDataGenerator = new SampleDataGenerator("{0}", new[] { rule });

            var data = sampleDataGenerator.GetData(1);

            Trace.TraceInformation("Data was {0}", data[0]);

            Assert.IsTrue(data[0] == "hello" || data[0] == "world");
        }


    }
}
