using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var attribute = (Attribute)attributes.First();
            var descriptionProperty = attribute.GetType().GetProperty("Description");
            return descriptionProperty.GetValue(attribute).ToString() ?? value.ToString();
        }
    }
}
