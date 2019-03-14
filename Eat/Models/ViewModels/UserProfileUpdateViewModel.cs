using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eat.Models.ViewModels
{
    public class UserProfileUpdateViewModel
    {
        [HiddenInput]
        public string Id { get; set; }

        [StringLength(247)]
        public string Avatar { get; set; }

        [NotMapped]
        public HttpPostedFileBase AvatarImageFile { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}