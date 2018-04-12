using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EHContactMVC.Models
{
    public class mvcEHContactModel
    {
        public int ContactID { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [Required]
        public byte Status { get; set; }
    }

    //public static class DDLHelper
    //{
    //    public static IList<SelectListItem> GetStatus()
    //    {
    //        IList<SelectListItem> _result = new List<SelectListItem>();
    //        _result.Add(new SelectListItem { Value = "1", Text = "Active" });
    //        _result.Add(new SelectListItem { Value = "0", Text = "Inactive" });
    //        return _result;
    //    }
    //}
}