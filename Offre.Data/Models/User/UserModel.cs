using Offre.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Offre.Data.Models.User
{
    public class UserModel
    {
        [Key]
        [IgnorePopulate]
        public long Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [JsonIgnore]
        [Required]
        [IgnorePopulate]
        public string Password { get; set; }
        [IgnorePopulate]
        public int Status { get; set; }
        [IgnorePopulate]
        public DateTime SaveDate { get; set; }
        [IgnorePopulate]
        public DateTime ModifyDate { get; set; }
    }
}
