using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.Common
{
    public class ErrorMessage
    {
        public static string InvalidObject()
        {
            return "Invalid properties";
        }

        public static string FailInsert_PropertyNotExist(string propertyName)
        {
            return "Create failed! " + propertyName + " doesn't exist";
        }

        public static string FailUpdate_PropertyNotExist(string propertyName)
        {
            return "Update failed! " + propertyName + " doesn't exist";
        }

        public static string FailInsert_PropertiesValuesExist()
        {
            return "Create failed! Values of properties exist";
        }

        public static string FailUpdate_PropertiesValuesExist()
        {
            return "Update failed! Values of properties exist";
        }

        public static string InvalidId()
        {
            return "Invalid Id";
        }
    }
}
