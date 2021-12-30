using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;


namespace PL.Model.Po
{
    public static class StringUtilitiesExtension
    {
        public static string ToStringProperties<T>(this T obj)
        {
            Type type = obj.GetType();
            StringBuilder description = new($"<{type.Name}> {{");

            foreach (var prop in type.GetProperties())
            {
                description.Append($"{prop.Name} = ");

                var propValue = prop.GetValue(obj);
                var countProperty = propValue?.GetType()?.GetProperty("Count");

                // Is the property a list?
                if (countProperty != null)
                {
                    var listCount = countProperty.GetValue(propValue);
                    var listType = propValue.GetType().GetGenericArguments()[0].Name;

                    description.Append($"<List[{listType}](Count = {listCount})");
                }
                else if (Attribute.IsDefined(prop, typeof(SexadecimalLatitudeAttribute)))
                {
                    description.Append(Sexadecimal.Latitude((double)propValue));
                }
                else if (Attribute.IsDefined(prop, typeof(SexadecimalLongitudeAttribute)))
                {
                    description.Append(Sexadecimal.Longitde((double)propValue));
                }
                else
                {
                    description.Append(propValue?.ToString() ?? "null");
                }
                description.Append(", ");
            }

            string result = description.ToString();

            // Remove the last comma
            return result[..result.LastIndexOf(',')] + '}';
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class SexadecimalLongitudeAttribute : Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class SexadecimalLatitudeAttribute : Attribute
        {
        }

        public static class Sexadecimal
        {
            public static string Longitde(double longitude)
            {
                string ch = "E";
                if (longitude < 0)
                {
                    ch = "W";
                    longitude = -longitude;
                }

                int deg = (int)longitude;
                int min = (int)(60 * (longitude - deg));
                double sec = (longitude - deg) * 3600 - min * 60;
                return $"{deg}° {min}′ {sec}″ {ch}";

            }

            public static string Latitude(double latitude)
            {
                string ch = "N";
                if (latitude < 0)
                {
                    ch = "S";
                    latitude = -latitude;
                }
                int deg = (int)latitude;
                int min = (int)(60 * (latitude - deg));
                double sec = (latitude - deg) * 3600 - min * 60;
                return $"{deg}° {min}′ {sec}″ {ch}";
            }
        }
    }
}
