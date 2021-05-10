using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NLog;
using AngularJSAuthentication.API.Controllers;
using AngularJSAuthentication.Model;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Drawing;
using BarcodeLib;
using QRCoder;
using AngularJSAuthentication.Model.Base.Audit;
using System.Data.Entity.Infrastructure;
using AngularJSAuthentication.Model.Base;
using Nito.AspNetBackgroundTasks;
using AngularJSAuthentication.API.Helpers;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Threading.Tasks;
using LinqKit;

namespace AngularJSAuthentication.API
{
    public class AuthContext : IdentityDbContext<IdentityUser>, iAuthContext
    {
        //nlogger
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private List<Audit> auditEntityList;
        public  DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        internal readonly IEnumerable<object> CaseModules;

        public AuthContext() : base("AuthContext")
        {

            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public static AuthContext Create()
        {
            return new AuthContext();
        }
        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        #region Db set
        public DbSet<People> Peoples { get; set; }
        public DbSet<UserAccessPermission> UserAccessPermissionDB { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        #endregion

        public UserAccessPermission getRoleDetail(string RoleName)
        {
            UserAccessPermission uap = new UserAccessPermission();
            uap = UserAccessPermissionDB.Where(x => x.RoleName == RoleName).SingleOrDefault();
            string id = "0";
            if (uap != null)
            {
                id = uap.RoleId;
            }
            return uap;
        }

        public People getPersonIdfromEmail(string email)
        {
            People ps = new People();
            ps = Peoples.Where(x => x.Deleted == false).Where(p => p.Email.Trim().Equals(email.Trim())).FirstOrDefault();
            int id = 0;
            if (ps != null)
            {
                id = ps.PeopleID;
            }
            return ps;
        }

    }
}
