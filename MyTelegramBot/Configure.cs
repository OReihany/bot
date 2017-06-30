using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Castle.Windsor;

namespace MyTelegramBot
{
    public class Configure
    {
        private static IWindsorContainer _windsorContainer;
        private static Dictionary<int, string> _majorTypeString;
        
        public static IWindsorContainer Container
        {
            get { return _windsorContainer ?? (_windsorContainer = new WindsorContainer()); }
        }

        public static Dictionary<int, string> MajorTypeString
        {
            get
            {
                return _majorTypeString ?? (_majorTypeString = (from MajorType n in Enum.GetValues(typeof(MajorType))
                                                                select new IntVal { Id = (int)n, Name = GetEnumDescription(n) }).ToDictionary(it => it.Id,
                        it => it.Name));
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }
        
        public static string ConvertDigitsToLatin(string s)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    //Persian digits
                    case '\u06f0':
                        sb.Append('0');
                        break;
                    case '\u06f1':
                        sb.Append('1');
                        break;
                    case '\u06f2':
                        sb.Append('2');
                        break;
                    case '\u06f3':
                        sb.Append('3');
                        break;
                    case '\u06f4':
                        sb.Append('4');
                        break;
                    case '\u06f5':
                        sb.Append('5');
                        break;
                    case '\u06f6':
                        sb.Append('6');
                        break;
                    case '\u06f7':
                        sb.Append('7');
                        break;
                    case '\u06f8':
                        sb.Append('8');
                        break;
                    case '\u06f9':
                        sb.Append('9');
                        break;

                    //Arabic digits    
                    case '\u0660':
                        sb.Append('0');
                        break;
                    case '\u0661':
                        sb.Append('1');
                        break;
                    case '\u0662':
                        sb.Append('2');
                        break;
                    case '\u0663':
                        sb.Append('3');
                        break;
                    case '\u0664':
                        sb.Append('4');
                        break;
                    case '\u0665':
                        sb.Append('5');
                        break;
                    case '\u0666':
                        sb.Append('6');
                        break;
                    case '\u0667':
                        sb.Append('7');
                        break;
                    case '\u0668':
                        sb.Append('8');
                        break;
                    case '\u0669':
                        sb.Append('9');
                        break;
                    default:
                        sb.Append(s[i]);
                        break;
                }
            }
            return sb.ToString();
        }
    }

    public class IntVal
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}