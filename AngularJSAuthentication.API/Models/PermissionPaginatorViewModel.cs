using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Models
{
    public class PermissionPaginatorViewModel
    {
        public string Contains { get; set; }
        public int First { get; set; }
        public int Last { get; set; }
        public string ColumnName { get; set; }
        public Boolean IsAscending { get; set; }
    }
}


