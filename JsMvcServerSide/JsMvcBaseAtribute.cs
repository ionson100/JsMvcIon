using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace JsMvcServerSide
{
    /// <summary>
    ///Attribute for generating html.
    /// </summary>

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class JsMvcBaseAtribute : Attribute
    {
        internal System.Web.Mvc.CompareAttribute CompareAttribute { get; set; }
        internal RangeAttribute RangeAttribute { get; set; }
        internal RequiredAttribute RequiredAttribute { get; set; }
        internal StringLengthAttribute StringLengthAttribute { get; set; }
        internal RegularExpressionAttribute RegularExpressionAttribute { get; set; }
        internal RemoteAttribute RemoteAttribute { get; set; }
        internal string _type = "text";
        internal uint _sorted;
        internal string Name { get; set; }
        internal string DisplayName { get; set; }
        internal string Description { get; set; }
        internal string RemoteUrl { get; set; }
        internal object Value { get; set; }
        internal Type PropertyType { get; set; }

        /// <summary>
        /// Class css.
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// Element type.
        /// </summary>
        public DisplayMode DisplayMode { get; set; }
        /// <summary>
        ///Sort elements in a tabular arrangement allowed until 6 columns.Table can contain up to
        ///100 rows (each hundred values ​​Sorted this solbets).
        /// </summary>
        public uint Sorted
        {
            get { return _sorted; }
            set
            {
                if (value > 600) throw new Exception("Sorted value can range from 0 to 600.");
                _sorted = value;
            }
        }
        /// <summary>
        /// To implement this type of drop-down lists to inherit interface IEnumerableSelectItems.
        /// </summary>
        public Type EnumerableSelectItems { get; set; }
        /// <summary>
        /// Type the element in context html5.
        /// </summary>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// Custom attributes
        /// </summary>
        public string Attributes { get; set; }


        internal string GetCoreElement()
        {
            var des = Description == null ? "" : "title=\"" + Description + "\"";
            if (DisplayMode == DisplayMode.TextBox)
            {
                return string.Format("<label>{0}{1}<Br> <input type=\"{2}\" value=\"{3}\" name=\"{4}\" id=\"{4}\"  class=\"{5}\"   {6}  {7} data-js=\"0\" {8} />{1}</label>{1}{9}",
                    DisplayName, Environment.NewLine, Type, Value, Name, Class, Attributes, des, UtilsCore.GetStringValidate(this), GenerateSpan());
            }
            if (DisplayMode == DisplayMode.HiddenField)
            {
                return string.Format("<input data-js=\"1\" id=\"{0}\" name=\"{0}\" value=\"{1}\" type=\"hidden\" {2} />", Name, Value, Attributes);
            }
            if (DisplayMode == DisplayMode.CheckBox)
            {
                var check = ((bool)Value) ? "checked=\"checked\"" : "";
                var addch = "<input  type=\"hidden\" value=\"false\" name=\"" + Name + "\" />";
                return string.Format("<label>{0}<Br><input data-js=\"2\" id=\"{1}\" name=\"{1}\" {2} class=\"{6}\" type=\"checkbox\" {3} value=\"true\" {4} {7}/></label>{5}{8}",
                    DisplayName, Name, check, Attributes, des, addch, Class, UtilsCore.GetStringValidate(this), GenerateSpan());
            }
            if (DisplayMode == DisplayMode.RadioButtom)
            {
                var sb = new StringBuilder("<div class=\"radiob\" > " + DisplayName);
                var ee = Activator.CreateInstance(EnumerableSelectItems);
                var items = ee as IEnumerableSelectItems;
                if (items == null)
                {
                    throw new Exception("Type does not implement the IEnumerableSelectItemsCustom attributes");
                }
                var ie = 0;
                foreach (var item in items.ListItems)
                {
                    ie++;
                    var vv = "";
                    if (item.Value == Value.ToString())
                        vv = "checked=\"checked\"";
                    sb.AppendFormat("<label><Br><input data-js=\"3\" type=\"radio\" value=\"{0}\"  {1} name=\"{2}\" class=\"{8}\"  id=\"{2}{3}{4}\" {5} {6} />{7}</label>", item.Value, vv, Name, '_', ie, Attributes, des, item.Text, Class);
                }
                sb.Append("</div>");
                return sb.ToString();

            }
            if (DisplayMode == DisplayMode.Select)
            {
                var sb = new StringBuilder("<div class=\"listbox\" > " + DisplayName);
                sb.AppendFormat("</Br> <select  data-js=\"4\" id=\"{0}\" name=\"{0}\" class=\"{1}\" {2} {4} {6}>{3}{5}", Name, Class, Attributes, Environment.NewLine, UtilsCore.GetStringValidate(this), "", des);

                var ee = Activator.CreateInstance(EnumerableSelectItems);
                var items = ee as IEnumerableSelectItems;
                if (items == null)
                {
                    throw new Exception("Type does not implement the IEnumerableSelectItemsCustom attributes");
                }

                foreach (var item in items.ListItems)
                {
                    var vv = "";
                    if (Value != null)
                    {
                        var val = Value as IEnumerable;
                        if (val != null)
                        {
                            var t = val.GetEnumerator();
                            while (t.MoveNext())
                            {
                                if (t.Current.ToString() == item.Value)
                                    vv = "selected=\"selected\"";
                            }
                        }
                        else
                        {
                            if (item.Value == Value.ToString())
                                vv = "selected=\"selected\"";
                        }
                    }

                    sb.AppendFormat("<option {0} value=\"{1}\">{2}</option>{3}", vv, item.Value, item.Text, Environment.NewLine);
                }

                sb.Append("</select></div>" + GenerateSpan());

                return sb.ToString();
            }
            if (DisplayMode == DisplayMode.TextArea)
            {
                return string.Format("<label>{0}</Br>  <textarea data-js=\"5\" id=\"{1}\"  class=\"{2}\"  name=\"{1}\"  {3} {4} {7}>{5}</textarea></label>{6}",
                    DisplayName, Name, Class, Attributes, des, Value, GenerateSpan(), UtilsCore.GetStringValidate(this));

            }

            return null;
        }

        internal string GenerateSpan()
        {
            if (CompareAttribute == null && RangeAttribute == null && RequiredAttribute == null && StringLengthAttribute == null && RegularExpressionAttribute == null && RemoteAttribute == null)
            {
                return null;
            }
            return "</br><span class=\"field-validation-error\" data-valmsg-replace=\"true\" data-valmsg-for=\"" + Name + "\">  <span class=\"\" for=\"" + Name + "\" generated=\"true\"></span>  </span>";
        }
    }
}