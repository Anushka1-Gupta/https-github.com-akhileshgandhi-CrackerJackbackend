using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using AngularJSAuthentication.API.Controllers;


namespace AngularJSAuthentication.API
{
   
    public interface iAuthContext
    {
        People getPersonIdfromEmail(string email);




    }
}