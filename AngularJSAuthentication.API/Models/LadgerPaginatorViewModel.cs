using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Models
{
    public class LadgerPaginatorViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int MaxRows { get; set; }
        public string PAN { get; set; }
        public string GSTno { get; set; }
    }
}