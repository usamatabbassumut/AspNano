using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Common.ApplicationExtensions
{
    /// <summary>
    /// This class contains the helper Extension methods
    /// </summary>
    public static class NanoExtension
    {
        /// <summary>
        /// Get Enum Description
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
