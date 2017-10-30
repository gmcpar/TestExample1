using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataStore
{
    public static class Extensions
    {
        public static Dictionary<string, object> ConvertToDictionary(this object item)
        {
            Type type = item.GetType();

            var props = type.GetProperties()
                .Select(propinfo => new { Name = propinfo.Name, Value = propinfo.GetValue(item, null) });

            var fields = type.GetFields()
                .Select(fieldinfo => new { Name = fieldinfo.Name, Value = fieldinfo.GetValue(item) });

            return props.Union(fields).ToDictionary(k => k.Name, v => v.Value);
        }
    }
}