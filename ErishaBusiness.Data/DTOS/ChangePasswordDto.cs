using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class ChangePasswordDto
    {
        public Guid Id { get; set; }
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }

        [DisplayName("New Password")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
