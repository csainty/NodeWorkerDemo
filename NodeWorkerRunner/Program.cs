using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NLog;
using NLog.Config;

namespace NodeWorkerRunner
{
    internal class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            logger.Info("Starting NodeWorkerRunner");
            try
            {
                var scriptName = ConfigurationManager.AppSettings["EntryPoint"];
                logger.Info("Running " + scriptName);
                var process = Process.Start(new ProcessStartInfo
                {
                    Arguments = scriptName,
                    CreateNoWindow = true,
                    FileName = "node.exe",
                });
                using (process)
                {
                    process.WaitForExit();
                    logger.Info("Finished! ExitCode: " + process.ExitCode);
                    Environment.Exit(process.ExitCode); // Run a second time
                }
            }
            catch (Exception e)
            {
                logger.ErrorException("Failed", e);
                Environment.Exit(0);    // Don't try run again
            }
        }
    }
}