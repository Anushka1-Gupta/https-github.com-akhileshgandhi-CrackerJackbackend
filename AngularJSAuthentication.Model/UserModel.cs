using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Models
{
    public class UserModel
    {

       
        [Display(Name = "DepartmentId")]
        public string DepartmentId { get; set; }

        [Required]
        [Display(Name = "User name")]
        [DefaultValue("")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }


        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Company Zip")]
        public string CompanyZip { get; set; }
        public string Address { get; set; }

        [Required]
        [Display(Name = "Company Phone")]
        public string CompanyPhone { get; set; }

        [Required]
        [Display(Name = "Company Phone")]
        public int Employees { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string PeopleFirstName { get; set; }
        public string PeopleLastName { get; set; }
        public int WarehouseId { get; set; }
        public int Stateid { get; set; }
        public int Cityid { get; set; }
        public string Mobile { get; set; }
        public string Department { get; set; }
        public string LScode { get; set; }
        public string Permissions { get; set; }
        public bool Active { get; set; }
        public string SUPPLIERCODES { get; set; }
        public string Salesexecutivetype { get; set; }
        public string AgentCode { get; set; }

        //New Fields Add 28/11/2018
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



    }

   
}