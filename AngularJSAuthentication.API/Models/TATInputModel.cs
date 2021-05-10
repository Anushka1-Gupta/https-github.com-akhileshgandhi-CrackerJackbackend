using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Models
{
    public class TATInputModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WarehouseID { get; set; }
        public List<string> SPList { get; set; }
        public string DboyMobileNo { get; set; }
    }
}