using System;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model.Base
{
    public class EntityMaster
    {
        [Key]
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public int DefaultNo { get; set; }
        public string EntityQuery { get; set; }
        public string Separator { get; set; }
    }

    public class EntitySerialMaster
    {
        [Key]
        public long Id { get; set; }
        public int EntityId { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public long StartFrom { get; set; }
        public long? NextNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModidfiedBy { get; set; }
        public int StateId { get; set; }
    }
}
