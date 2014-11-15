using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace SEImplementation.Models
{
    public class PermissionModel
    {
        [Required]
        [Display(Name = "Permission ID")]
        public int permissionID { get; set; }
        [Required]
        [Display(Name = "Permission ID")]
        public string permissionName { get; set; }
        [Required]
        [Display(Name = "Permission ID")]
        public string permissionDesc { get; set; }
        [Required]
        [Display(Name = "Role ID")]
        public int RoleID { get; set; }
    }

}