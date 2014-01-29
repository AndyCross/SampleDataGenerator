using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Hadoop.MapReduce;

namespace Elastacloud.Hadoop.SampleDataMapReduceJob
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                var data = @"823708	rz=q	UK
806439	rz=q	UK
473709	sf=21	France
713282	wt.p=n	UK
356149	sf=1	UK
595722	wt.p=n	France
238589	sf=1	France
478163	sf=21	France
971029	rz=q	France".Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim());

                var result = StreamingUnit.Execute<SampleMapper, SampleReducer>(data);

                foreach (var s in result.MapperResult)
                {
                    Console.WriteLine(s);
                }
                Console.ReadLine();
            }

            HadoopJobExecutor.ExecuteJob<SampleDataJob>();
        }
    }
}
