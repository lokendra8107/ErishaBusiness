using System;
using System.Collections.Generic;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class LoginDetails
    {
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public string RoleName { get; set; }
        public string ACCESS_LEVEL { get; set; }
        public string ProfilePic { get; set; }
        public string id { get; set; }
        public string token { get; set; }
        public string Logo { get; set; }
    }
}
