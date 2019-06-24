using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SimpleObjects2CSV
{
    public class CSV
    {
        public static string GetCSVString<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] pInfo = typeof(T).GetProperties();
            for (int i = 0; i <= pInfo.Length - 1; i++)
            {
                sb.Append(pInfo[i].Name);
                if (i < pInfo.Length - 1)
                {
                    sb.Append(",");
                }
            }
            sb.AppendLine();

            for (int i = 0; i <= list.Count - 1; i++)
            {
                T item = list[i];
                for (int j = 0; j <= pInfo.Length - 1; j++)
                {
                    object oInfo = item.GetType().GetProperty(pInfo[j].Name).GetValue(item, null);
                    if (oInfo != null)
                    {
                        string value = oInfo.ToString();
                        if (value.Contains(","))
                        {
                            value = string.Concat("\"", value, "\"");
                        }
                        if (value.Contains("\r"))
                        {
                            value = value.Replace("\r", " ");
                        }
                        if (value.Contains("\n"))
                        {
                            value = value.Replace("\n", " ");
                        }
                        sb.Append(value);
                    }

                    if (j < pInfo.Length - 1)
                    {
                        sb.Append(",");
                    }

                }
                sb.AppendLine();
            }
            return sb.ToString();
        }


        public static void ExportCSV<T>(List<T> list, string filename, string path)
        {
            string csv = GetCSVString(list);
            ExportCSV(csv, filename, path);
        }

        private static void ExportCSV(string csv, string filename, string path)
        {
            Directory.CreateDirectory(path);
            using (StreamWriter writer = new StreamWriter(path + "\\" + filename))
            {
                writer.WriteLine(csv);
            }
         }

    }

}
