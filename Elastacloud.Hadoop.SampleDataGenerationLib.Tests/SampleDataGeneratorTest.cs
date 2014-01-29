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
    public class SampleDataGeneratorTest
    {
        [Test]
        public void SampleDataGenerator_RandomInt_Count1_Returns1()
        {

            var sampleDataGenerator = new SampleDataGenerator("{0}", new[] { new RandomIntDataGenerationRule(1,1)});
            var data = sampleDataGenerator.GetData(count: 1);

            Assert.AreEqual(1, data.Length);
        }
        [Test]
        public void SampleDataGenerator_RandomInt_Count10_Returns10()
        {

            var sampleDataGenerator = new SampleDataGenerator("{0}", new[] { new RandomIntDataGenerationRule(1, 1) });
            var data = sampleDataGenerator.GetData(count: 10);

            Assert.AreEqual(10, data.Length);
        }

        

    }
}
