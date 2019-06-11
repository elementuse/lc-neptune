using Neptune.Yibao.{{channel_name}}.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Neptune.Yibao.{{channel_name}}
{
    public class MessageSerializer
    {
        public static string SerializeRequest<T>(T data)
            where T : RequestBase
        {
            return "$$" + SerializeSingle(data) + "$$";
        }

        public static T Deserialize<T>(string value)
            where T : ResponseBase, new()
        {
            return DeserializeSingle(value.Replace("$$", ""), typeof(T)) as T;
        }

        private static string SerializeList(IList listData)
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < listData.Count; i++)
            {
                sb.AppendFormat("{0}", SerializeSingle(listData[i]));
            }

            return sb.ToString();
        }

        private static string SerializeSingle(object data)
        {
            StringBuilder sb = new StringBuilder();

            var type = data.GetType();
            var fields = GetFields(type);

            var compositeAttr = GetCustomAttribute<CompositeAttribute>(type);
            string separator = compositeAttr == null ? "~" : "%%";

            for (int i = 1; i <= fields.Count(); i++)
            {
                var field = fields[i];
                if (typeof(IList).IsAssignableFrom(field.PropertyType))
                {
                    sb.Append(SerializeList(field.GetValue(data, null) as IList));
                }
                else if (!field.PropertyType.IsPrimitive && field.PropertyType != typeof(string))
                {
                    compositeAttr = GetCustomAttribute<CompositeAttribute>(field.PropertyType);
                    if (compositeAttr != null)
                    {
                        sb.Append(SerializeSingle(field.GetValue(data, null)));
                    }
                }
                else
                {
                    sb.Append(BasicTypeToString(field.GetValue(data, null)));
                }

                if (compositeAttr != null || i < fields.Count)
                {
                    sb.Append(separator);
                }
            }

            return sb.ToString();
        }

        private static IList DeserializeList(string value, Type type)
        {
            var listType = typeof(List<>).MakeGenericType(type);
            var list = Activator.CreateInstance(listType) as IList;

            var fields = GetFields(type);
            string[] values = value.Split(new string[] { "%%" }, StringSplitOptions.None);
            if (!string.IsNullOrEmpty(value) && values.Length % fields.Count != 0)
            {
                throw new Exception("返回数据和定义不一致！");
            }
            for (int i = 0; i < values.Length / fields.Count; i++)
            {
                string val = string.Join("%%", values.Skip(i * fields.Count).Take(fields.Count));
                list.Add(DeserializeSingle(val, type));
            }

            return list;
        }

        private static object DeserializeSingle(string value, Type type)
        {
            var fields = GetFields(type);

            var compositeAttr = GetCustomAttribute<CompositeAttribute>(type);
            string separator = compositeAttr == null ? "~" : "%%";

            string[] values = value.Split(new string[] { separator }, StringSplitOptions.None);
            if (string.IsNullOrEmpty(value) && compositeAttr != null)
            {
                return null;
            }
            else if (fields.Count > values.Length)
            {
                throw new Exception("返回数据和定义不一致！");
            }

            var dict = new Dictionary<string, object>();
            for (int i = 0; i < fields.Count; i++)
            {
                var field = fields[i + 1];
                if (field.PropertyType.IsPrimitive || field.PropertyType == typeof(string))
                {
                    dict[field.Name] = values[i];
                }
            }

            var json = JsonConvert.SerializeObject(dict);
            var data = JsonConvert.DeserializeObject(json, type);
            if (data == null)
            {
                return null;
            }

            for (int i = 0; i < fields.Count; i++)
            {
                var field = fields[i + 1];
                if (field.PropertyType.IsPrimitive || field.PropertyType == typeof(string))
                {
                    continue;
                }

                if (typeof(IList).IsAssignableFrom(field.PropertyType))
                {
                    var listData = DeserializeList(values[i], field.PropertyType.GetGenericArguments()[0]);
                    field.SetValue(data, listData, null);
                }
                else
                {
                    compositeAttr = GetCustomAttribute<CompositeAttribute>(field.PropertyType);
                    if (compositeAttr != null)
                    {
                        field.SetValue(data, DeserializeSingle(values[i], field.PropertyType), null);
                    }
                }
            }

            return data;
        }

        private const string InputRegex = @"\${2}|~|\%{2}";
        private static string BasicTypeToString(object value)
        {
            return value == null ? "" : Regex.Replace(value.ToString(), InputRegex, "");
        }

        static Dictionary<Type, Dictionary<int, PropertyInfo>> _typeCache = new Dictionary<Type, Dictionary<int, PropertyInfo>>();
        private static Dictionary<int, PropertyInfo> GetFields(Type type)
        {
            if (!_typeCache.ContainsKey(type))
            {
                var props = from p in type.GetProperties()
                            where GetCustomAttribute<MessageFieldAttribute>(p) != null
                            select p;

                var propDict = new Dictionary<int, PropertyInfo>();
                foreach (var prop in props)
                {
                    var attr = GetCustomAttribute<MessageFieldAttribute>(prop);
                    propDict[attr.Sequence] = prop;
                }

                for (int i = 1; i < props.Count(); i++)
                {
                    if (!propDict.ContainsKey(i))
                    {
                        throw new Exception($"消息定义有误,没有序列号{i}的字段");
                    }
                }

                _typeCache[type] = propDict;
            }

            return _typeCache[type];
        }

        private static T GetCustomAttribute<T>(MemberInfo memberInfo)
            where T : Attribute
        {
            var attrs = memberInfo.GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                if (attr is T)
                {
                    return (T)attr;
                }
            }
            return null;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MessageFieldAttribute : Attribute
    {
        public string DisplayName { set; get; }
        public int Sequence { set; get; }
        public MessageFieldAttribute(string displayName, int sequence)
        {
            DisplayName = displayName;
            Sequence = sequence;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CompositeAttribute : Attribute
    {

    }
}
