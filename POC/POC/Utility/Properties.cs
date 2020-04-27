using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POC.Utility
{
    static class Properties
    {
       public static string SceenShotLocation = @"..\Reports\";
        private static Hashtable ht = new Hashtable();
        public static void loadProperties(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            bool readFlag = false;
            foreach (string line in lines)
            {
                //string text = Regex.Replace(line, @"\s+", "");
                string text = line;
                readFlag = checkSyntax(text);
                if (readFlag)
                {
                    string[] splitText = text.Split('=');
                    ht.Add(splitText[0].ToLower(), splitText[1]);
                }
            }
        }

        private static bool checkSyntax(string line)
        {
            if (String.IsNullOrEmpty(line) || line[0].Equals('['))
            {
                return false;
            }

            if (line.Contains("=") && !String.IsNullOrEmpty(line.Split('=')[0]) && !String.IsNullOrEmpty(line.Split('=')[1]))
            {
                return true;
            }
            else
            {
                throw new Exception("Can not Parse Properties file please verify the syntax");
            }
        }

        public static string getProperty(string key)
        {
            if (ht.Contains(key))
            {
                return ht[key].ToString();
            }
            else
            {
                throw new Exception("Property:" + key + "Does not exist");
            }

        }

    }
    }
