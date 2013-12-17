using System.Collections.Generic;
using System.Web.Mvc;

namespace JsMvcServerSide
{
    public interface IEnumerableSelectItems
    {
        IEnumerable<SelectListItem> ListItems { get; }
    }
}