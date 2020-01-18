using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryGuard
{
    public class ProcessGuard
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public string[] _runningProcessFileNames
        {
            get { return getRunningProcessFileNames(); }
        }
        public string[] _protectedProcessFileNames { get; }
        public string[] _protectedProcessNames { get; }

        public ProcessGuard(string[] protectedProcessFileNames)
        {            
            _protectedProcessFileNames = protectedProcessFileNames;
            _protectedProcessNames = getProcessNames(protectedProcessFileNames);
        }

        public string[] getProcessNames(string[] processFileNames)
        {
            // assume process name(Title in assembly infomation) are same as the assembly name
            string[] pNames = new string[processFileNames.Length];
            int i = 0;
            foreach(string processFileName in processFileNames)
            {
                pNames[i] = Path.GetFileName(processFileName).Split('.')[0];
                i++;                
            }

            return pNames;
        }

        public string[] getRunningProcessFileNames()
        {
            Process[] arrayProcess = Process.GetProcesses();
            string[] allProcessFileNames = new string[arrayProcess.Length];
            string[] processFileNames;

            int i = 0;
            foreach (Process p in arrayProcess)
            {                
                if (_protectedProcessNames.Contains(p.ProcessName))
                {
                    try
                    {
                        allProcessFileNames[i] = p.MainModule.FileName.ToString();                        
                        i++;
                    }
                    catch
                    {
                        Logger.Warn(string.Format("File name access deniel for {0}", p.ProcessName));
                    }
                }
            }

            processFileNames = new string[i];
            Array.Copy(allProcessFileNames, processFileNames, i);
            return processFileNames;
        }

        public bool isRunning(string processFileName)
        {
            return _runningProcessFileNames.Contains(processFileName);
        }

        public void runDeadProcess()
        {
            foreach (string fileName in _protectedProcessFileNames)
            {
                if (!isRunning(fileName))
                {   
                    Logger.Info(string.Format("Starting process {0}",fileName));
                    Process.Start(fileName);
                    Logger.Info(string.Format("Started process {0}", fileName));
                }
            }
        }
        public void protectProcess()
        {
            while (true)
            {
                runDeadProcess();
                Thread.Sleep(100);
            }
        }
    }
}
