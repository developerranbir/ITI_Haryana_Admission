using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace HigherEducation.Models
{
    public class Log
    {
        public void LogWrite(string logMessage)
        {
            string LogPath = ConfigurationManager.AppSettings["LogFilePath"]; 

            string dir = LogPath;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string newPath = System.IO.Path.Combine(dir);
            try
            {
                string foldername = DateTime.Now.ToString("dd-MM-yyyy");
                using (StreamWriter w = File.AppendText(newPath + "\\" + foldername + ".txt"))
                {
                    Logcls(logMessage, w);
                }
            }
            catch //(Exception ex)
            {
            }
        }
        public void Logcls(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}