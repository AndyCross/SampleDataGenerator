using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Elastacloud.Hadoop.SampleDataGenerationLib;
using Elastacloud.Hadoop.SampleDataGenerationLib.Rule;

//todo: fix namespace on this console to not hide the real class
using SampleDataGeneratorClass = Elastacloud.Hadoop.SampleDataGenerationLib.SampleDataGenerator;

namespace Elastacloud.Hadoop.SampleDataGenerator
{
    class Program
    {
        private static DateTime _startingDate = new DateTime(2013, 03, 01);

        static void Main(string[] args)
        {
            const string format = "{0}\t{1}\t{2}\t{3}\t{4}\t{5}";

            var dataRuleDate = new DateTimeIncrement(_startingDate, 4);
            var dataRuleInt = new RandomIntDataGenerationRule(150000, 200000);
            var usageRuleDictionary = new DictionaryStringDataGenerationRule(new[] { "Regular", "Infrequent", "New" });

            var countryGenerationRule = CreateDayToCountryHierarchicalRule();

            var dataRuleDictionaryPage =  new DictionaryStringDataGenerationRule(new[] { "Adword", "Organic Search", "No Referrer" });

            var probalityRuleAction = CreateCountryToActionHierarchicalRule(); 

            var retValue = new List<string>();
            var numRecords = int.Parse(args[0]);

            for (int i = 0; i < numRecords; i++)
            {
                var date = dataRuleDate.GenerateValue(i);
                var id = dataRuleInt.GenerateValue(i);
                var usage = usageRuleDictionary.GenerateValue(i);
                var referrer = dataRuleDictionaryPage.GenerateValue(i);                
                var country = countryGenerationRule.GenerateValue(i, ((DateTime)date).DayOfYear);
                var action = probalityRuleAction.GenerateValue(i, country.ToString());

                Console.WriteLine(i);
                Thread.Sleep(1);
                retValue.Add(string.Format(format, date, id, usage, country, referrer, action));                
            }            

            var path = "C:\\Data\\" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture) + ".log";

            File.WriteAllLines(path, retValue.ToArray());

            Console.WriteLine("Written to {0}", path);

            Console.ReadLine();
        }

        private static HierarchicalDataGenerationRule<int, string> CreateDayToCountryHierarchicalRule()
        {
            var countryProbabilitiesForMarchFirst = new Dictionary<string, double>();
            countryProbabilitiesForMarchFirst.Add("USA", 0.25);
            countryProbabilitiesForMarchFirst.Add("UK", 0.25);
            countryProbabilitiesForMarchFirst.Add("France", 0.25);
            countryProbabilitiesForMarchFirst.Add("Germany", 0.25);

            var countryProbabilitiesForMarchSecond = new Dictionary<string, double>();
            countryProbabilitiesForMarchSecond.Add("USA", 0.05);
            countryProbabilitiesForMarchSecond.Add("UK", 0.1);
            countryProbabilitiesForMarchSecond.Add("France", 0.75);
            countryProbabilitiesForMarchSecond.Add("Germany", 0.1);

            var countryProbabilitiesForMarchThird = new Dictionary<string, double>();
            countryProbabilitiesForMarchThird.Add("USA", 0.6);
            countryProbabilitiesForMarchThird.Add("UK", 0.1);
            countryProbabilitiesForMarchThird.Add("France", 0.2);
            countryProbabilitiesForMarchThird.Add("Germany", 0.1);

            var countryProbabilitiesForMarchFourth = new Dictionary<string, double>();
            countryProbabilitiesForMarchFourth.Add("USA", 0.3);
            countryProbabilitiesForMarchFourth.Add("UK", 0.25);
            countryProbabilitiesForMarchFourth.Add("France", 0.25);
            countryProbabilitiesForMarchFourth.Add("Germany", 0.2);

            var dayToCountryProbabilityMatrix = new Dictionary<int, Dictionary<string, double>>
                                                    {
                                                        {
                                                            _startingDate.DayOfYear,
                                                            countryProbabilitiesForMarchFirst
                                                        },
                                                        {
                                                            _startingDate.DayOfYear + 1,
                                                            countryProbabilitiesForMarchSecond
                                                        },
                                                        {
                                                            _startingDate.DayOfYear + 2,
                                                            countryProbabilitiesForMarchThird
                                                        },
                                                        {
                                                            _startingDate.DayOfYear + 3,
                                                            countryProbabilitiesForMarchFourth
                                                        }
                                                    };

            return new HierarchicalDataGenerationRule<int, string>(dayToCountryProbabilityMatrix);
        }

        private static HierarchicalDataGenerationRule<string, string> CreateCountryToActionHierarchicalRule()
        {
            var actionProbabilitiesForUSA = new Dictionary<string, double>();
            actionProbabilitiesForUSA.Add("Purchase", 0.6);
            actionProbabilitiesForUSA.Add("Add to Cart", 0.1);
            actionProbabilitiesForUSA.Add("Search", 0.1);
            actionProbabilitiesForUSA.Add("View Product", 0.2);

            var actionProbabilitiesForUK = new Dictionary<string, double>();
            actionProbabilitiesForUK.Add("Purchase", 0.4);
            actionProbabilitiesForUK.Add("Add to Cart", 0.2);
            actionProbabilitiesForUK.Add("Search", 0.2);
            actionProbabilitiesForUK.Add("View Product", 0.2);

            var actionProbabilitiesForFrance = new Dictionary<string, double>();
            actionProbabilitiesForFrance.Add("Purchase", 0.2);
            actionProbabilitiesForFrance.Add("Add to Cart", 0.2);
            actionProbabilitiesForFrance.Add("Search", 0.4);
            actionProbabilitiesForFrance.Add("View Product", 0.2);

            var actionProbabilitiesForGermany = new Dictionary<string, double>();
            actionProbabilitiesForGermany.Add("Purchase", 0.3);
            actionProbabilitiesForGermany.Add("Add to Cart", 0.25);
            actionProbabilitiesForGermany.Add("Search", 0.25);
            actionProbabilitiesForGermany.Add("View Product", 0.2);

            var countryToActionProbabilityMatrix = new Dictionary<string, Dictionary<string, double>>
                                                    {
                                                        {
                                                            "USA",
                                                            actionProbabilitiesForUSA
                                                        },
                                                        {
                                                            "UK",
                                                            actionProbabilitiesForUK
                                                        },
                                                        {
                                                            "France",
                                                            actionProbabilitiesForFrance
                                                        },
                                                        {
                                                            "Germany",
                                                            actionProbabilitiesForGermany
                                                        }
                                                    };

            return new HierarchicalDataGenerationRule<string, string>(countryToActionProbabilityMatrix);
        }

    }
}
