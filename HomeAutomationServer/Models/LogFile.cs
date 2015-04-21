using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeAutomationServer.Models
{
    static public class LogFile
    {
        static private List<string> logfile = new List<string>();

        static public bool AddLog(string log)
        {
            logfile.Add(log);
            if (logfile.Last() == log) return true;
            else return false;
        }

        static public string GetLog()
        {
            return string.Join("\n", logfile.ToArray());
        }

        static public void DeleteLog()
        {
            logfile.Clear();
        }

        static public int GetCount()
        {
            return logfile.Count;
        }
    }
}