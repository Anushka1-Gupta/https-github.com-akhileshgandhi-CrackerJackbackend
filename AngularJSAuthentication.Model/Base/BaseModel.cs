using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.Model.Base
{
    public class BaseModel
    {
        public BaseModel()
        {
            GUID = Guid.NewGuid();
        }

        [NotMapped]
        public Guid GUID { get; set; }
    }
}
