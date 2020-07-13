using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Offre.Data.Models.User
{
    public class UserModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [JsonIgnore]
        [Required]
        public string Password { get; set; }
        public int Status { get; set; }
        public DateTime SaveDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
