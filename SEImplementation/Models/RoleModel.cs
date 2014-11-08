using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace SEImplementation.Models
{
    public class RoleModel
    {
        [Required]
        [Display(Name = "User ID")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Role ID")]
        public int RoleID { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "Role Description")]
        public string RoleDesc { get; set; }
    }

}