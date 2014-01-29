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
    public class HierarchicalDataGenerationTests
    {
        [Test]
        public void HierarchicalDayAndCountry_MostProbablyDayInput_CountryGenerated()
        {
            var countryProbabilitiesForToday = new Dictionary<string, double>();
            countryProbabilitiesForToday.Add("USA", 0.2);
            countryProbabilitiesForToday.Add("UK", 0.6);
            countryProbabilitiesForToday.Add("France", 0.2);

            var countryProbabilitiesForTomorrow = new Dictionary<string, double>();
            countryProbabilitiesForTomorrow.Add("USA", 0.9);
            countryProbabilitiesForTomorrow.Add("UK", 0.1);
            countryProbabilitiesForTomorrow.Add("France", 0.1);

            var probabilityMatrix = new Dictionary<int, Dictionary<string, double>>();
            probabilityMatrix.Add(DateTime.UtcNow.DayOfYear, countryProbabilitiesForToday);
            probabilityMatrix.Add(DateTime.UtcNow.AddDays(1).DayOfYear, countryProbabilitiesForTomorrow);

            var rule = new HierarchicalDataGenerationRule<int, string>(probabilityMatrix);

            var data = rule.GenerateValue(1, DateTime.UtcNow.AddDays(1).DayOfYear);

            Assert.AreEqual("USA", data);
        }

    }
}
