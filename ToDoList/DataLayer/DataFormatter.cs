using System;
using System.Configuration;

namespace ToDoList.DataLayer
{
    public class DataFormatter
    {
        /// <summary>
        /// this function Convert Datetime to a specific format
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static string FormattedDate(object date)
        {
            if (date != null && date != DBNull.Value && Convert.ToString(date).Length > 0)
                return Convert.ToDateTime(date).ToString(ToDoList.Constants.Constants.datetimeformat);
            else
                return string.Empty;
        }

        /// <summary>
        /// this function Convert Datetime to a specific format
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static string FormattedDateTime(object date)
        {
            if (date != null && date != DBNull.Value && Convert.ToString(date).Length > 0)
                return Convert.ToDateTime(date).ToString(ToDoList.Constants.Constants.datetimesecformat);
            else
                return string.Empty;
        }
    }
}