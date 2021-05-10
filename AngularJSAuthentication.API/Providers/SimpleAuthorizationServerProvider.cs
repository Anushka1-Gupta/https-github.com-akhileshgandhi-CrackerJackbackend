
using AngularJSAuthentication.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularJSAuthentication.API.Providers;
using System.Web;
using AngularJSAuthentication.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using AngularJSAuthentication.API.Managers;
using Newtonsoft.Json;
using System.Configuration;

namespace AngularJSAuthentication.API.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        AuthContext dc = new AuthContext();
        public Logger logger = LogManager.GetCurrentClassLogger();
        int User_Id;
        string ac_Type;
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);


        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";
            try
            {


                //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

                ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                if (!user.EmailConfirmed)
                {
                    context.SetError("invalid_grant", "User did not confirm email.");
                    return;
                }

                iAuthContext con = new AuthContext();

                if (string.IsNullOrEmpty(user.ApkName))
                {
                    People p = con.getPersonIdfromEmail(user.Email);
                    int UserId = p.PeopleID;
                    if (!p.Active)
                    {
                        context.SetError("invalid_grant", "Please check your registered email address to validate email address.");
                        return;
                    }
                    if (UserId == 0)
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }

                    ClaimsIdentity identity = await user.GenerateUserIdentityAsync(userManager, "JWT");
                    identity.AddClaims(ExtendedClaimsProvider.GetClaims(user));
                    identity.AddClaims(RolesFromClaims.CreateRolesBasedOnClaims(identity));

                    //UserAccessPermission uap = con.getRoleDetail(p.Permissions); // Get Role Access Detail
                    var rolesIds = user.Roles.Select(x => x.RoleId).ToList();


                    //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    //identity.AddClaims(ExtendedClaimsProvider.GetClaims(user));
                    identity.AddClaims(RolesFromClaims.CreateRolesBasedOnClaims(identity));
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, p.Permissions));                   
                    identity.AddClaim(new Claim("firsttime", "true"));
                    identity.AddClaim(new Claim("compid", p.CompanyId.ToString()));
                    identity.AddClaim(new Claim("email", user.Email));
                    identity.AddClaim(new Claim("Email", user.Email.ToString()));
                    identity.AddClaim(new Claim("Level", user.Level.ToString()));
                    identity.AddClaim(new Claim("userid", UserId.ToString()));
                    identity.AddClaim(new Claim("DisplayName", p.DisplayName));
                    identity.AddClaim(new Claim("username", (p.PeopleFirstName + " " + p.PeopleLastName).ToString()));
                    identity.AddClaim(new Claim("userid", UserId.ToString()));
                    identity.AddClaim(new Claim("Roleids", string.Join(",", rolesIds)));
                    //identity.AddClaim(new Claim("pagePermissions", JsonConvert.SerializeObject(pagePermissions)));
                    User_Id = UserId;
                    ac_Type = p.Permissions;
                    var props = new AuthenticationProperties(new Dictionary<string, string> { });
                    if (p.Permissions == "HQ Master login")
                    {
                        props = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            { "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId },
                            { "userName", context.UserName },
                            { "compid", p.CompanyId.ToString() },
                            { "Level", user.Level.ToString() },
                            { "role" ,p.Permissions },
                            { "email" ,user.Email },
                            { "userid", UserId.ToString() },
                            { "LScode",  p.LScode },

                    
                   // { "pagePermissions", JsonConvert.SerializeObject(pagePermissions)}
               });
                    }
                    else
                    {
                        props = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            { "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId },
                            { "userName", context.UserName },
                            { "compid", user.PhoneNumber },
                            { "role" ,p.Permissions },
                            { "Level", user.Level.ToString() },
                            { "email" ,user.Email },
                            { "userid", UserId.ToString() },

                   
                    //{ "pagePermissions", JsonConvert.SerializeObject(pagePermissions)}
                        });
                    }
                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                }
                else  // Authorize APK 
                {
                    ClaimsIdentity identity = await user.GenerateUserIdentityAsync(userManager, "JWT");
                    identity.AddClaims(ExtendedClaimsProvider.GetClaims(user));
                    identity.AddClaims(RolesFromClaims.CreateRolesBasedOnClaims(identity));
                    identity.AddClaim(new Claim("AppName", user.ApkName));

                    var props = new AuthenticationProperties(new Dictionary<string, string> { });

                    props = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            { "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId },
                            { "userName", context.UserName },
                            { "AppName", user.ApkName}
                        });


                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                }

            }
            catch (Exception ex)
            {
                logger.Error("Unable to validate user {0}", context.UserName);
                logger.Error(ex.InnerException != null ? ex.InnerException.ToString() : ex.ToString());
            }
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;
            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }
            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            //User traking Code strated....
            //UserTraking us = new UserTraking();
            //us.PeopleId = User_Id + "";
            //us.Type = ac_Type;
            //us.LoginTime = DateTime.Now;
            //us.Remark = "login page ,";
            //dc.UserTrakings.Add(us);
            //dc.SaveChanges();
            //END User traking Code....
            return Task.FromResult<object>(null);
        }
    }
}