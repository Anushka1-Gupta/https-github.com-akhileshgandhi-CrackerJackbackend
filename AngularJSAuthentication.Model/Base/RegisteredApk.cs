using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model.Base
{
    public class RegisteredApk
    {
        [Key]
        public int Id { get; set; }
        public string ApkName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
