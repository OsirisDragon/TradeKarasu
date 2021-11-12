using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Eagle
{
    public class Command
    {
        public static int Run(string filePath, string commandArgs)
        {
            int myReturnValue = 0;

            using (new ErrorModeContext(ErrorModes.FailCriticalErrors | ErrorModes.NoGpFaultErrorBox))
            {
                using (Process process = new Process())
                {
                    ProcessStartInfo processInfo = new ProcessStartInfo();

                    processInfo.FileName = filePath;
                    processInfo.Arguments = commandArgs;
                    processInfo.RedirectStandardOutput = true;
                    processInfo.RedirectStandardError = true;
                    processInfo.RedirectStandardInput = true;
                    processInfo.UseShellExecute = false;
                    processInfo.CreateNoWindow = true;
                    processInfo.WorkingDirectory = Path.GetDirectoryName(filePath);

                    process.StartInfo = processInfo;
                    process.Start();

                    string myOutput = process.StandardOutput.ReadToEnd();
                    string myError = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (myError != "")
                    {
                        throw new Exception(myError);
                    }

                    myReturnValue = process.ExitCode;
                }
            }

            return myReturnValue;
        }

        public static string RunAndReceiveData(string filePath, string commandArgs)
        {
            string myReturnValue = "";

            using (new ErrorModeContext(ErrorModes.FailCriticalErrors | ErrorModes.NoGpFaultErrorBox))
            {
                using (Process process = new Process())
                {
                    ProcessStartInfo processInfo = new ProcessStartInfo();

                    processInfo.FileName = filePath;
                    processInfo.Arguments = commandArgs;
                    processInfo.RedirectStandardOutput = true;
                    processInfo.RedirectStandardError = true;
                    processInfo.RedirectStandardInput = true;
                    processInfo.UseShellExecute = false;
                    processInfo.CreateNoWindow = true;
                    processInfo.WorkingDirectory = Path.GetDirectoryName(filePath);

                    process.StartInfo = processInfo;
                    process.Start();

                    string myOutput = process.StandardOutput.ReadToEnd();
                    string myError = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (myError != "")
                    {
                        throw new Exception(myError);
                    }

                    if (myOutput != "")
                    {
                        myReturnValue = getBetween(myOutput, "@@ReceivedData-START@@", "@@ReceivedData-END@@");
                    }
                }
            }

            return myReturnValue;
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }
    }

    public class ErrorModeContext : IDisposable
    {
        private readonly int _oldMode;

        public ErrorModeContext(ErrorModes mode)
        {
            _oldMode = SetErrorMode((int)mode);
        }

        ~ErrorModeContext()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            SetErrorMode(_oldMode);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [DllImport("kernel32.dll")]
        private static extern int SetErrorMode(int newMode);
    }

    [Flags]
    public enum ErrorModes
    {
        Default = 0x0,
        FailCriticalErrors = 0x1,
        NoGpFaultErrorBox = 0x2, // &lt;- this is the one we need
        NoAlignmentFaultExcept = 0x4,
        NoOpenFileErrorBox = 0x8000,
    }
}