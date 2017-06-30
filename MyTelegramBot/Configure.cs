using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Castle.Windsor;

namespace MyTelegramBot
{
    public class Configure
    {
        public static IWindsorContainer Container
        {
            get { return _windsorContainer ?? (_windsorContainer = new WindsorContainer()); }
        }

        private static IWindsorContainer _windsorContainer;
        private static Dictionary<int, string> _majorTypeString;
        public static Dictionary<int, string> MajorTypeString
        {
            get
            {
                return _majorTypeString ?? (_majorTypeString = (from MajorType n in Enum.GetValues(typeof(MajorType))
                                                                select new IntVal { Id = (int)n, Name = GetEnumDescription(n) }).ToDictionary(it => it.Id, it => it.Name));
            }
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }

    public class IntVal
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}