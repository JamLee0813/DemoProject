using System;
using System.ComponentModel;

namespace DemoProject.Common.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        ///     获取枚举值上的Description特性的说明
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="obj">枚举值</param>
        /// <returns>特性的说明</returns>
        public static string GetEnumDescription<T>(T obj)
        {
            var type = obj.GetType();
            var field = type.GetField(Enum.GetName(type, obj) ?? string.Empty);

            return !(Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute descAttr)
                ? string.Empty
                : descAttr.Description;
        }

        /// <summary>
        ///     返回 枚举描述文本
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum obj)
        {
            var type = obj.GetType();
            var field = type.GetField(Enum.GetName(type, obj) ?? string.Empty);

            return !(Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute descAttr)
                ? string.Empty
                : descAttr.Description;
        }

        /// <summary>
        ///     获取枚举值上的Description特性的说明
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="obj">枚举值</param>
        /// <returns>特性的说明</returns>
        public static string GetEnumDescriptionByName<T>(string obj)
        {
            var field = typeof(T).GetField(obj);

            return !(Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute descAttr)
                ? string.Empty
                : descAttr.Description;
        }

        /// <summary>
        ///     是否存在枚举值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static bool HasEnumValue<TEnum>(object value) => Enum.IsDefined(typeof(TEnum), Convert.ToInt32(value));

        /// <summary>
        ///     是否存在枚举值(根据枚举名称)
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static bool HasEnumName<TEnum>(object value) => Enum.IsDefined(typeof(TEnum), value);

        /// <summary>
        ///     获取枚举类型
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static TEnum GetEnum<TEnum>(object value) where TEnum : struct =>
            Enum.Parse<TEnum>(Convert.ToString(value));

        /// <summary>
        ///     获取枚举类型
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="name">枚举名</param>
        /// <returns></returns>
        public static TEnum GetEnumByName<TEnum>(object name) where TEnum : struct =>
            (TEnum)Enum.Parse(typeof(TEnum), Convert.ToString(name));

        /// <summary>
        ///     根据描述获取枚举
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static TEnum GetEnumByDescription<TEnum>(string description) where TEnum : struct
        {
            var fields = typeof(TEnum).GetFields();
            foreach (var field in fields)
            {
                var objects = field.GetCustomAttributes(typeof(DescriptionAttribute), false); //获取描述属性
                if (objects.Length > 0 && (objects[0] as DescriptionAttribute)?.Description == description)
                {
                    return (TEnum)field.GetValue(null);
                }
            }

            throw new ArgumentException($"{description} 未能找到对应的枚举.", nameof(description));
        }
    }
}