
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Lib.Models
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLogin
    {
        public long TotRec { get; set; }
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}
