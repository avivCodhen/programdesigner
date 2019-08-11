using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WorkoutGenerator.Extentions
{
    public static class EnumerationExtensions
    {
        public static string Description(this Enum value)
        {
            // get attributes  
            FieldInfo field = value.GetType().GetField(value.ToString());
            object[] attributes = field.GetCustomAttributes(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any()) displayAttribute = attributes.ElementAt(0);

            // return description
            return displayAttribute?.Description ??
                   throw new Exception("Description for enum not found for enum: " + value);
        }
    }
}
