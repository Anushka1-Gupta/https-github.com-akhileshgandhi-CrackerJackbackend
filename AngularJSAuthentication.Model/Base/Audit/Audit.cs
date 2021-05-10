using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.Model.Base.Audit
{
    public class Audit
    {
        public Audit()
        {
            AuditFields = new List<AuditFields>();
        }
        [Key]
        public long AuditId { get; set; }
        public string PkValue { get; set; }
        public string PkFieldName { get; set; }
        public DateTime AuditDate { get; set; }
        public string UserName { get; set; }
        public string AuditAction { get; set; }
        public string AuditEntity { get; set; }
        public string TableName { get; set; }
        public string GUID { get; set; }
        [NotMapped]
        public List<AuditFields> AuditFields { get; set; }
    }

    public class AuditFields
    {
        [Key]
        public long AuditFieldId { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string AuditGuid { get; set; }
    }


    public class AuditHistory
    {
        public long AuditId { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime AuditDate { get; set; }
        public string AuditAction { get; set; }
        public string AuditEntity { get; set; }
        public string UserName { get; set; }
    }

    public class AuditBaseToShow
    {
        public string AuditEntity { get; set; }
        public List<string> FieldNames { get; set; }
        public List<AuditHistoryToShow> AuditHistory { get; set; }
    }

    public class AuditHistoryToShow
    {
        public DateTime AuditDate { get; set; }
        public string AuditAction { get; set; }
        public string UserName { get; set; }
        public List<AuditFieldsToShow> AuditFields { get; set; }
    }

    public class AuditFieldsToShow
    {
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
