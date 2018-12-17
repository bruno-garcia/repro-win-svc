using System;
using System.IO;
using System.ServiceProcess;
using Sentry;

namespace WindowsService1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (SentrySdk.Init("https://5fd7a6cda8444965bade9ccfd3df9882@sentry.io/1188141"))
            {
                if (Environment.UserInteractive)
                {
                    ReadFile();
                }
                else
                {
                    var servicesToRun = new ServiceBase[]
                    {
                        new Service1()
                    };
                    ServiceBase.Run(servicesToRun);
                }
            }
        }

        public static void ReadFile()
        {
            const string basePath = @"C:\temp";
            var source = Path.Combine(basePath, "file1");
            var dest = Path.Combine(basePath, "file2");

            File.WriteAllText(source,"some data");
            var bytes = File.ReadAllBytes(source);
            File.WriteAllBytes(dest, bytes);
        }
    }
}
