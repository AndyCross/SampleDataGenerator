using System;
using Microsoft.Hadoop.MapReduce;

namespace Elastacloud.Hadoop.SampleDataMapReduceJob
{
    public class SampleMapper : MapperBase
    {
        public override void Map(string inputLine, MapperContext context)
        {
            try
            {
                context.IncrementCounter("Line Processed");
                
                var segments = inputLine.Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                context.IncrementCounter("country", segments[2], 1);

                context.EmitKeyValue(segments[2], inputLine);
                context.IncrementCounter("Text chars processed", inputLine.Length);
            }
            catch(IndexOutOfRangeException ex)
            {
                //we still allow other exceptions to throw and set and error state on the task but this 
                //exception type we are confident is due to the input not having >3 \t separated segments 
                context.IncrementCounter("Logged recoverable error", "Input Format Error", 1);
                context.Log(string.Format("Input Format Error on line {0} in {1} - {2} was {3}", inputLine, context.InputFilename, 
                    context.InputPartitionId, ex.ToString()));

                context.SetStatus("Running with recoverable errors");
            }
        }
    }

    public static class ContextBaseExtension
    {
        public static void SetStatus(this ContextBase context, string statusMessage)
        {
            Console.Error.WriteLine("reporter:status:{0}", statusMessage);
        }
    }
}