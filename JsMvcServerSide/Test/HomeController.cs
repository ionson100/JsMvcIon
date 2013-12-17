using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Services;
using JsMvcServerSide;

namespace JsMvcTest.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {


            return View();
        }
      

        [HttpPost]
        public ActionResult Index(ModelIon model)
        {
            return View();
        }

     

        [WebMethod]
        public JsonResult GetJosonText()
        {
            return this.Json(UtilsCore.GetJoson(new ModelIon(){  IsValid = true, Srok = new[] { 2, 3 },Date = DateTime.Now,Name = "sdasdasd", IsValid2 = true, Srok2 = 4 })); //
        }

        [WebMethod]
        public JsonResult GetJosonTextSend(ModelIon d)
        {
            return Json(UtilsCore.GetJoson(d));
        }

        public JsonResult CheckName(string name)
        {
            var result = string.Equals(name, "хуй");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

    public class ModelIon
    {
         //[Remote("CheckName", "Home",ErrorMessage = "isdpoifodf")]
        // [System.Web.Mvc.Compare("Count")]
         //[Range(2, 10)]
        // [Range(typeof(double), "23.0", "45.4")]
        // [RegularExpression(@"^\d+$")]
       //  [StringLength(5,MinimumLength = 2)]
       // [Required()]
        [Description("Description text")]
        [JsMvcBaseAtribute(Class = "kjskdjj", DisplayMode = DisplayMode.TextBox, Sorted = 203)]
        public string Name { get; set; }

        [Required()]
        [Description("Description text")]
        [DisplayName("Count items")]
        [JsMvcBaseAtribute(Class = "sdasd", DisplayMode = DisplayMode.TextBox, Type = "text", Sorted = 103)]
        public int Count { get; set; }

        [Required()]
        [Description("Description text")]
        [DisplayName("Meeting Date")]
        [JsMvcBaseAtribute(Class = "sdasd", DisplayMode = DisplayMode.TextBox, Type = "text", Sorted = 303)]
        public DateTime Date { get; set; }

        [JsMvcBaseAtribute(DisplayMode = DisplayMode.HiddenField, Sorted = 300)]
        public int Id { get; set; }


        [Description("Description text")]
        [DisplayName("Validate")]
        [JsMvcBaseAtribute(Class = "sdasd", DisplayMode = DisplayMode.RadioButtom, EnumerableSelectItems = typeof(ForBool), Sorted = 403)]
        public bool IsValid { get; set; }


        [Description("Description text")]
        [DisplayName("Validate2")]
        [JsMvcBaseAtribute(Class = "sdasd", DisplayMode = DisplayMode.CheckBox, Sorted = 203)]
        public bool IsValid2 { get; set; }

        [Required()]
        [Description("Description text")]
        [DisplayName("Message1")]
        [JsMvcBaseAtribute(Class = "sdasd", DisplayMode = DisplayMode.Select, Attributes = " multiple='' size='4'", EnumerableSelectItems = typeof(ForBoolForSelectAsInt), Sorted = 123)]
        public int[] Srok { get; set; }

        [Description("Description text")]
        [DisplayName("Message2")]
        [JsMvcBaseAtribute(Class = "sdasd", DisplayMode = DisplayMode.Select, Attributes = "  size='8'", EnumerableSelectItems = typeof(ForBoolForSelectAsInt), Sorted = 123)]
        public int Srok22 { get; set; }

        [Description("Description text")]
        [DisplayName("Message3")]
        [JsMvcBaseAtribute(Class = "sdasd", DisplayMode = DisplayMode.Select, EnumerableSelectItems = typeof(ForBoolForSelectAsInt), Sorted = 34)]
        public int Srok2 { get; set; }

        [Required()]
        [Description("Description text")]
        [DisplayName("AllMessages")]
        [JsMvcBaseAtribute(Class = "sdasd", DisplayMode = DisplayMode.TextArea, Sorted = 0)]
        public string TextMessage { get; set; }
    }

    public class ForBool : IEnumerableSelectItems
    {

        readonly List<SelectListItem> _list = new List<SelectListItem>();

        public ForBool()
        {
            _list.Add(new SelectListItem { Text = "Simple", Value = true.ToString() });
            _list.Add(new SelectListItem { Text = "НardSimple", Value = false.ToString() });
        }

        public IEnumerable<SelectListItem> ListItems
        {
            get
            {
                return _list;
            }
        }
    }
    public class ForBoolForSelectAsInt : IEnumerableSelectItems
    {
        readonly List<SelectListItem> _list = new List<SelectListItem>();

        public ForBoolForSelectAsInt()
        {
            _list.Add(new SelectListItem { Value = "0", Text = "00Assa_____123" });
            _list.Add(new SelectListItem { Value = "1", Text = "11Assa_____123" });
            _list.Add(new SelectListItem { Value = "2", Text = "22Assa_____123" });
            _list.Add(new SelectListItem { Value = "3", Text = "33Assa_____123" });
            _list.Add(new SelectListItem { Value = "4", Text = "44Assa_____123" });
            _list.Add(new SelectListItem { Value = "5", Text = "55Assa_____123" });
            _list.Add(new SelectListItem { Value = "6", Text = "66Assa_____123" });
            _list.Add(new SelectListItem { Value = "7", Text = "77Assa_____123" });
        }

        public IEnumerable<SelectListItem> ListItems
        {
            get
            {
                return _list;
            }
        }
    }


}
