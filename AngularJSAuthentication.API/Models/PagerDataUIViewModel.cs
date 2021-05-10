using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Models
{
    public class PagerDataUIViewModel
    {
        public int First { get; set; }
        public int Last { get; set; }
        public String ColumnName { get; set; }
        public bool IsAscending { get; set; }
        public string Contains { get; set; }
    }
}