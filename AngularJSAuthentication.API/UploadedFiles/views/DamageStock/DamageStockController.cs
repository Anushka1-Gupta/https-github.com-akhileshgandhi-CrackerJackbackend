using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/damagestock")]
    public class DamageStockController : ApiController
    {
        AuthContext db = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        [Route("get")]
        [HttpGet]
        public PaggingDatastock get(int list, int page, int WarehouseId)
        {

            try
            {
                    var identity = User.Identity as ClaimsIdentity;
                    int compid = 0, userid = 0;
                int Warehouse_id = 0;


                    foreach (Claim claim in identity.Claims)
                    {
                        if (claim.Type == "compid")
                        {
                            compid = int.Parse(claim.Value);
                        }
                        if (claim.Type == "userid")
                        {
                            userid = int.Parse(claim.Value);
                        }
                    if (claim.Type == "Warehouseid")
                    {
                        Warehouse_id = int.Parse(claim.Value);
                    }

                }
                 logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                PaggingDatastock data = new PaggingDatastock();
                var total_count = db.DamageStockDB.Where(x => x.Deleted == false && x.CompanyId==compid && x.WarehouseId == WarehouseId).Count();
                var damagest = db.DamageStockDB.Where(x => x.Deleted == false && x.CompanyId == compid && x.WarehouseId == WarehouseId).OrderByDescending(x => x.DamageStockId).Skip((page - 1) * list).Take(list).ToList();
                data.damagest = damagest;
                data.total_count = total_count;
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(int WarehouseId)//get all Issuances which are active for the delivery boy
        {

            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
        

                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                   
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                var DamageitemData = db.DamageStockDB.Where(x => x.Deleted == false && x.DamageInventory >0 && x.CompanyId==compid && x.WarehouseId == WarehouseId).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, DamageitemData);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("search")]
        [HttpGet, HttpPost]
        public dynamic search(int ItemId)
        {

            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                int Warehouse_id = 0;

                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "Warehouseid")
                    {
                        Warehouse_id = int.Parse(claim.Value);
                    }

                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                if (ItemId != 0 && ItemId > 0)
                {
                    var data = db.itemMasters.Where(x => x.Deleted == false && x.active==true && x.ItemId==ItemId && x.CompanyId==compid && x.WarehouseId == Warehouse_id).SingleOrDefault();

                    return data;
                }
                else {
                   
                    return null;
                }
                    
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        [Route("damage")]
        [HttpPost]
        [AcceptVerbs("POST")]
        public DamageStock Post(DamageStock DamageStock)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                //int warehouseid = 0;

                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                    //if (claim.Type == "Warehouseid")
                    //{
                    //    warehouseid = int.Parse(claim.Value);
                    //}
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                DamageStock.CompanyId = compid;
                //DamageStock.WarehouseId = warehouseid;
                return db.Adddemand(DamageStock);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        ///////////////////new code/////////////


        [Route("filtre")]
        [HttpGet, HttpPost]
        public dynamic get(DBOYinfo1 DBI)
        {

            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;

                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);

                List<ItemMaster> returnlist = new List<ItemMaster>();
                AuthContext context = new AuthContext();
                foreach (var i in DBI.ids)
                {
                    var lst = context.AddDamageStock(i.id, DBI.Warehouseid ,compid);
                    if (lst != null)
                    {
                        List<ItemMaster> os = lst.otmaster;
                        returnlist.AddRange(os);
                    }
                }
                return returnlist;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public class DBOYinfo1
        {
            public List<dbinf> ids { get; set; }
            public int Warehouseid { get; set; }
        }
        public class dbinf
        {
            public int id { get; set; }
          

        }

        [Route("Custall")]
        [HttpGet]
        public HttpResponseMessage Custall( int WarehouseId)//get all Issuances which are active for the delivery boy
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;

                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                    
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                var displist = (from j in db.Customers
                                where j.Warehouseid == WarehouseId
                                join i in db.Customers on j.CustomerId equals i.CustomerId
                                join k in db.Warehouses on i.Warehouseid equals k.WarehouseId 
                                select new CustomerDTOM
                                {
                                    CustomerId = j.CustomerId,
                                    CompanyId = j.CompanyId,
                                    Active = i.Active,
                                    City = i.City,
                                    WarehouseId = j.Warehouseid,
                                    WarehouseName = k.WarehouseName,
                                    ExecutiveId = j.ExecutiveId,
                                    Day = j.Day,
                                    BeatNumber = j.BeatNumber,
                                    CreatedDate = i.CreatedDate,
                                    UpdatedDate = i.UpdatedDate,
                                    LScode = i.LScode,
                                    Mobile = i.Mobile,
                                    ShopName = i.ShopName,
                                    BillingAddress = i.BillingAddress,
                                    ShippingAddress = i.ShippingAddress,
                                    Cityid = i.Cityid,
                                    Emailid = i.Emailid,
                                    Name = i.Name
                                }).Distinct().OrderBy(x => x.CustomerId).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, displist);
            }
            
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
}
}
