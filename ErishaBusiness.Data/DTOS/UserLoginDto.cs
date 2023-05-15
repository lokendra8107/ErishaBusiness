using System;
using System.Collections.Generic;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public partial class UserLoginDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string SaltKey { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public string MobileNo { get; set; }
        public string Title { get; set; }
    }
}
