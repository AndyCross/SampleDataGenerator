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
    public class RandomIntDataGenerationTests
    {
        [Test]
        public void SampleDataGenerator_RandomInt_ExpectedResultRange()
        {
            var sampleDataGenerator = new SampleDataGenerator("{0}", new[] { new RandomIntDataGenerationRule(1, 1) });
            var data = sampleDataGenerator.GetData(count: 1);

            Assert.AreEqual("1", data[0]);
        }

        [Test]
        public void SampleDataGenerator_RandomInt_ResultRespectsMin()
        {

            var sampleDataGenerator = new SampleDataGenerator("{0}", new[] { new RandomIntDataGenerationRule(2, 5) });
            var data = sampleDataGenerator.GetData(count: 1);

            Trace.TraceInformation("Random 2-5 was {0}", data);

            Assert.Greater(int.Parse(data[0].ToString()), 1);
        }

        [Test]
        public void SampleDataGenerator_RandomInt_ResultRespectsMax()
        {

            var sampleDataGenerator = new SampleDataGenerator("{0}", new[] { new RandomIntDataGenerationRule(1, 5) });
            var data = sampleDataGenerator.GetData(count: 1);

            Trace.TraceInformation("Random 1-5 was {0}", data);

            Assert.Less(int.Parse(data[0].ToString()), 6);
        }
    }
}
