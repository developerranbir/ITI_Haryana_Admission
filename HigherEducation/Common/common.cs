using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HigherEducation.Common
{
    public static class Common
    {
        public static string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        public static string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "2345679ACEFGHKLMNPRSWXZabcdefghkhmnpqrstuvwxyz";
            string digit = "2345679";
            Random r = new Random();
            for (int j = 0; j < 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            randomText.Append(digit[r.Next(digit.Length)]);
            return randomText.ToString();
        }
        public static List<Dictionary<string, string>> ToDictionary(DataTable dt)   // convert datatable to Dictionary
        {
            return dt.AsEnumerable().Select(
                row => dt.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column].ToString()
                )).ToList();
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

    }
}