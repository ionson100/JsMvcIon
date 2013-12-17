using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace JsMvcServerSide
{
    public static class UtilsCore
    {
        /// <summary>
        /// Json generation based on an attribute
        /// </summary>
        /// <param name="model">object whose properties marked attribute JsMvcBaseAtribute</param>
        /// <returns>Joson текст</returns>
        public static string GetJson(object model)
        {
            var list = new List<JsMvcBaseAttribute>();
            foreach (var basep in model.GetType().GetProperties())
            {
                var atr = basep.GetCustomAttribute<JsMvcBaseAttribute>();
                if (atr == null) continue;
                if (string.IsNullOrWhiteSpace(atr.Name))
                {
                    atr.Name = basep.Name;
                }
                var atrDisName = basep.GetCustomAttribute<DisplayNameAttribute>();
                if (atrDisName != null)
                {
                    atr.DisplayName = string.IsNullOrWhiteSpace(atrDisName.DisplayName)
                        ? basep.Name
                        : atrDisName.DisplayName;

                }
                else
                {
                    atr.DisplayName = basep.Name;
                }
                var atrDescr = basep.GetCustomAttribute<DescriptionAttribute>();
                if (atrDescr != null)
                {
                    atr.Description = atrDescr.Description;
                }
                atr.Value = basep.GetValue(model);
                atr.PropertyType = basep.PropertyType;

                var required = basep.GetCustomAttribute<RequiredAttribute>();
                if (required != null)
                {

                    atr.RequiredAttribute = required;
                }

                var lengthString = basep.GetCustomAttribute<StringLengthAttribute>();
                if (lengthString != null)
                {
                    atr.StringLengthAttribute = lengthString;

                }

                var exp = basep.GetCustomAttribute<RegularExpressionAttribute>();
                if (exp != null)
                {
                    atr.RegularExpressionAttribute = exp;

                }
                var range = basep.GetCustomAttribute<RangeAttribute>();
                if (range != null)
                {
                    atr.RangeAttribute = range;

                }
                var compare = basep.GetCustomAttribute<System.Web.Mvc.CompareAttribute>();
                if (compare != null)
                {
                    atr.CompareAttribute = compare;
                }
                var remote = basep.GetCustomAttribute<RemoteAttribute>();
                if (remote != null)
                {
                    remote.AdditionalFields = basep.Name;
                    var ee = (System.Web.Routing.RouteValueDictionary)remote.GetType().GetProperty("RouteData", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic).GetValue(remote);
                    var err = ee.Values;
                    atr.RemoteUrl = string.Format("/{0}", string.Join("/", err));
                    atr.RemoteAttribute = remote;
                }
                list.Add(atr);
            }
            list.Sort(CompareBaseAtribute);
            var jsonsL = new List<ItemJson>();
            foreach (var bb in list)
            {
                if (bb.DisplayMode == DisplayMode.CheckBoxGroup && bb.EnumerableSelectItems == null) throw new Exception("With type DisplayMode.ListBox, attribute, you must specify the type implements IEnumerableSelectItems");
                if (bb.DisplayMode == DisplayMode.Select && bb.EnumerableSelectItems == null) throw new Exception("With type DisplayMode.ListBox, attribute, you must specify the type implements IEnumerableSelectItems");
                if (bb.DisplayMode == DisplayMode.RadioButtom && bb.EnumerableSelectItems == null) throw new Exception("With C type DisplayMode.RadioButtom, attribute, you need to specify the type implements IEnumerableSelectItems");
                if (bb.DisplayMode == DisplayMode.CheckBox && bb.PropertyType != typeof(bool)) throw new Exception("DisplayMode.CheckBox parameter applies only to the type - bool");
                jsonsL.Add(new ItemJson { element = bb.GetCoreElement(), sorted = bb.Sorted });
            }

            var res = new JavaScriptSerializer().Serialize(jsonsL);
            return res;
        }

        private static int CompareBaseAtribute(JsMvcBaseAttribute x, JsMvcBaseAttribute y)
        {
            return x.Sorted.CompareTo(y.Sorted);
        }
        internal static string GetStringValidate(JsMvcBaseAttribute bb)
        {

            var sb = new StringBuilder();
            if (bb.CompareAttribute != null)
            {
                var ee = bb.CompareAttribute.GetType().GetProperty("ErrorMessageString", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(bb.CompareAttribute);
                sb.AppendFormat(" data-val-equalto=\"" + GetErrorMessage(bb.CompareAttribute).Replace("\"", "`") + "\" ", bb.DisplayName,bb.CompareAttribute.OtherProperty);
                sb.AppendFormat(" data-val-equalto-other=\"*.{0}\" ", bb.CompareAttribute.OtherProperty);

            }
            if (bb.RangeAttribute != null)
            {
                sb.AppendFormat(" data-val-range=\"" + GetErrorMessage(bb.RangeAttribute).Replace("\"", "`") + "\" ", bb.DisplayName, bb.RangeAttribute.Minimum, bb.RangeAttribute.Maximum);
                sb.AppendFormat(" data-val-range-min=\"{0}\" ", bb.RangeAttribute.Minimum);
                sb.AppendFormat(" data-val-range-max=\"{0}\" ", bb.RangeAttribute.Maximum);
            }
            if (bb.RequiredAttribute != null)
            {
                sb.AppendFormat("data-val-required=\"" + GetErrorMessage(bb.RequiredAttribute).Replace("\"", "`") + "\" ", bb.DisplayName);
            }
            if (bb.StringLengthAttribute != null)
            {
                sb.AppendFormat(" data-val-length=\"" + GetErrorMessage(bb.StringLengthAttribute).Replace("\"", "`") + "\" ", bb.DisplayName, bb.StringLengthAttribute.MaximumLength, bb.StringLengthAttribute.MinimumLength);//
                if (bb.StringLengthAttribute.MinimumLength > 0)
                {
                    sb.AppendFormat(" data-val-length-min=\"{0}\" ", bb.StringLengthAttribute.MinimumLength);
                }
                sb.AppendFormat(" data-val-length-max=\"{0}\" ", bb.StringLengthAttribute.MaximumLength);
            }
            if (bb.RegularExpressionAttribute != null)
            {
                sb.AppendFormat(" data-val-regex=\"" + GetErrorMessage(bb.RegularExpressionAttribute).Replace("\"", "`")+"\" ", bb.DisplayName, bb.RegularExpressionAttribute.Pattern);
                sb.AppendFormat(" data-val-regex-pattern=\"{0}\" ", bb.RegularExpressionAttribute.Pattern);
            }
            if (bb.RemoteAttribute != null)
            {
               
                sb.AppendFormat("data-val-remote=\"" + GetErrorMessage(bb.RemoteAttribute).Replace("\"", "`") + "\" ", bb.DisplayName);
                sb.AppendFormat(" data-val-remote-url= \"{0}\" ", bb.RemoteUrl);
                sb.AppendFormat(" data-val-remote-additionalfields=\"*.{0}\" ", bb.Name);
            }

            if (sb.Length == 0) return null;
            sb.Append(" data-val=\"true\" ");
            return sb.ToString().Replace("\"", "'");
        }

        internal static string GetErrorMessage(object o)
        {
            var ee = o.GetType().GetProperty("ErrorMessageString", BindingFlags.Instance | BindingFlags.NonPublic);
            if (ee != null)
            {
                return (string)ee.GetValue(o);
            }
            return null;
        }
    }
}
