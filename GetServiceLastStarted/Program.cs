using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GetServiceLastStarted
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Key = PID, Value = Date
                Dictionary<String, String> dtDict = new Dictionary<String, String>();
                string processName = String.Empty;

                Console.Write("Please Enter the Process Name: $> ");
                processName = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(processName))
                    dtDict = GetStartTime(processName);

                if(dtDict != null && dtDict.Any())
                {
                    foreach(var dtDic in dtDict)
                    {
                        Console.WriteLine("Process PID: " + dtDic.Key + " Process Start Time: " + dtDic.Value);
                    }                   
                }                   
                else
                    throw new Exception("Process name string was empty.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.Write("Press Enter to close this application");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Gets the start time.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns></returns>
        private static Dictionary<String, String> GetStartTime(string processName)
         {
            //Key = PID, Value = Date
             Dictionary<String, String> dtDict = new Dictionary<String, String>();

            Process[] processes = 
                Process.GetProcessesByName(processName);
            if (processes.Length == 0) 
                throw new ApplicationException(string.Format(
                    "Process {0} is not running.", processName));
            // -----------------------------
            DateTime retVal = DateTime.Now;
            foreach(Process p in processes)
                if (p.StartTime < retVal && p.Id != 0)
                    dtDict.Add(p.Id.ToString(), p.StartTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture));

            return dtDict;
         }
    }
}
