using System;

namespace POS.API.Models
{
    public class LoginReq
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
    public class LoginRes
    {
        public string Status { get; set; }

        public string Message { get; set; }
    }
}
