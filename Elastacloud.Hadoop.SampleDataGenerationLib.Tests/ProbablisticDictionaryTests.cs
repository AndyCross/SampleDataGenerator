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
    public class ProbablisticDictionaryTests
    {
        [Test]
        public void Probablistic_ItemWithProbability1_Selected()
        {
            var listOfTuples = new List<ProbablisticTuple>();
            listOfTuples.Add(new ProbablisticTuple(1d, "hello"));
            var rule = new ProbablisticDictionaryStringDataGenerationRule(listOfTuples);

            var generator = new SampleDataGenerator("{0}", new[] {rule});
            var data = generator.GetData(1);

            Assert.AreEqual("hello", data[0]);

        }

        [Test]
        public void Probablistic_ItemWithProbabilitypoint8_Selected()
        {
            var listOfTuples = new List<ProbablisticTuple>();
            listOfTuples.Add(new ProbablisticTuple(0.8d, "hello"));
            var rule = new ProbablisticDictionaryStringDataGenerationRule(listOfTuples);

            var generator = new SampleDataGenerator("{0}", new[] { rule });
            var data = generator.GetData(1);

            Assert.AreEqual("hello", data[0]);
        }

        [Test]
        public void Probablistic_TwoItemsProbabilityRange_Selected()
        {
            var listOfTuples = new List<ProbablisticTuple>();
            listOfTuples.Add(new ProbablisticTuple(0.3d, "hello"));
            listOfTuples.Add(new ProbablisticTuple(0.6d, "world"));
            var rule = new ProbablisticDictionaryStringDataGenerationRule(listOfTuples);

            var generator = new SampleDataGenerator("{0}", new[] { rule });
            var data = generator.GetData(10);

            int helloCount = 0, worldCount = 0;
            for (int i = 0; i < data.Length; i++)
            {
                Trace.TraceInformation("Data was {0} at {1}", data[i], i);

                Assert.IsTrue(data[i] == "hello" || data[i] == "world");
                if (data[i] == "hello") helloCount++;
                if (data[i] == "world") worldCount++;
            }

            Trace.TraceInformation("{0} hellos, {1} worlds", helloCount, worldCount);

            Assert.Greater(helloCount, 0);
            Assert.Greater(worldCount, 0);
        }
    }
}
