using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Lib.Models
{
    public static class DataTableExtension
    {
        private static Dictionary<Type, List<PropertyInfo>> typeDictionary = new Dictionary<Type, List<PropertyInfo>>();
        public static List<PropertyInfo> GetPropertiesForType<T>()
        {
            var type = typeof(T);
            if (!typeDictionary.ContainsKey(typeof(T)))
            {
                typeDictionary.Add(type, type.GetProperties().ToList());
            }
            return typeDictionary[type];
        }

        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<PropertyInfo> properties = GetPropertiesForType<T>();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        public static T ToObject<T>(this DataRow row) where T : new()
        {
            List<PropertyInfo> properties = GetPropertiesForType<T>();
            T result = new T();
            result = CreateItemFromRow<T>((DataRow)row, properties);
            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            try
            {
                foreach (var property in properties)
                {
                    if (row.Table.Columns.Contains(property.Name))
                    {
                        if (row[property.Name] == DBNull.Value)
                        {
                            property.SetValue(item, null, null);
                        }
                        else
                        {
                            property.SetValue(item, row[property.Name], null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

    }
}
