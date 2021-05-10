using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using AngularJSAuthentication.Model.Base;

namespace AngularJSAuthentication.Model
{
    //[Table("People")]
    public class People : BaseModel
    {
        public People()
        {
            if (this.DisplayName == null)
            {
                this.DisplayName = PeopleFirstName + " " + PeopleLastName;
            }
        }
        [Key]
        public int PeopleID { get; set; }
        public int CompanyId { get; set; }

        public int WarehouseId { get; set; }
        [DefaultValue("")]
        public string PeopleFirstName { get; set; }
        [DefaultValue("")]
        public string PeopleLastName { get; set; }
        // public string FullName { get; set; }
        public string Email { get; set; }
        [DefaultValue("")]
        public string DisplayName { get; set; }
        public string Country { get; set; }

        public int? Stateid { get; set; }
        public string state { get; set; }
        public int? Cityid { get; set; }
        public string city { get; set; }

        public string Mobile { get; set; }//new fields 07/09/2015
        public string Password { get; set; }
        public int? RoleId { get; set; }//new fields 07/09/2015
        //  public string RoleTitle { get; set; }

        //end
        public string Department { get; set; }
        public double BillableRate { get; set; }
        public string CostRate { get; set; }
        public string Permissions { get; set; }
        public string SUPPLIERCODES { get; set; }

        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public bool Deleted { get; set; }
        [DefaultValue("false")]
        public bool EmailConfirmed { get; set; }
        [DefaultValue("true")]
        public bool Approved { get; set; }
        [DefaultValue("true")]
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public double VehicleCapacity { get; set; }
        public string LScode { get; set; }
        public string AgentCode { get; set; }
        public string Salesexecutivetype { get; set; }

        public decimal AgentAmount { get; set; }

        // Add New Fields For Employee Created By shoaib
        public string Empcode { get; set; }
        public string Desgination { get; set; }
        public string Status { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? DataOfJoin { get; set; }
        public DateTime? DataOfMarriage { get; set; }
        public DateTime? EndDate { get; set; }
        public string Unit { get; set; }
        public int Salary { get; set; }
        public string Reporting { get; set; }
        public string IfscCode { get; set; }
        public int Account_Number { get; set; }
        public object PeopleId { get; set; }

        public double DepositAmount { get; set; }
        [NotMapped]
        public string DeleteComment { get; set; }
        public string DeviceId { get; set; }
        public string FcmId { get; set; }
        public string CurrentAPKversion { get; set; }  
        public string PhoneOSversion { get; set; }   
        public string UserDeviceName { get; set; }  
        public string AddressProof { get; set; }
        public string IMEI { get; set; }
        public string IdProof { get; set; } 
        public string pVerificationCopy { get; set; } 

        [NotMapped]
        public string OTP { get; set; }   
        public string UserName { get; set; }
        public bool tempdel { get; set; }
    }
}
