using System;
using System.IO;
using System.Net;
using Microsoft.Hadoop.MapReduce;

namespace Elastacloud.Hadoop.SampleDataMapReduceJob
{
    public class SampleDataJob : HadoopJob<SampleMapper, SampleReducer>
    {
        public override HadoopJobConfiguration Configure(ExecutorContext context)
        {
            HadoopJobConfiguration config = new HadoopJobConfiguration();
            config.InputPath = "/input/input.log";
            config.OutputFolder = "/output/output" + DateTime.Now.ToString("yyyyMMddhhmmss");

            return config;
        }

        public override void Initialize(ExecutorContext context)
        {
            CreateInput();

            base.Initialize(context);
        }

        private void CreateInput()
        {
            var target = "/input/input.log";

            if (!HdfsFile.Exists(target))
            {
                try
                {
                    var tempPath = Path.GetTempFileName();
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile("https://pastagrid.blob.core.windows.net/sampledata/sampledata.txt", tempPath);
                    webClient.Dispose();

                    HdfsFile.CopyFromLocal(tempPath, "/input/input.log");
                }
                catch
                {
                }
            }
        }
    }
}