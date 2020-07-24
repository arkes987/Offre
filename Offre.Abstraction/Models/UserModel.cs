using System;

namespace Offre.Abstraction.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public DateTime SaveDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
