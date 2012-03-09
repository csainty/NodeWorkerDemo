using System;
using System.Configuration;
using System.Diagnostics;
using NLog;

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
                var start = new ProcessStartInfo
                {
                    Arguments = "app/index.js",
                    FileName = "node.exe",
                    UseShellExecute = false
                };

                // Copy all the appSettings into environment variables. This allows them to be accessed from process.env in your node code.
                foreach (var setting in ConfigurationManager.AppSettings.AllKeys)
                {
                    start.EnvironmentVariables.Add(setting, ConfigurationManager.AppSettings[setting]);
                }

                using (var process = Process.Start(start))
                {
                    process.WaitForExit();
                    logger.Info("Finished! ExitCode: " + process.ExitCode);
                    Environment.Exit(process.ExitCode); // Pass through the exit code on a successful run. This puts node in control of worker lifetime.
                }
            }
            catch (Exception e)
            {
                logger.ErrorException("Failed - " + e.Message, e);
                Environment.Exit(0);    // Don't try run again
            }
        }
    }
}