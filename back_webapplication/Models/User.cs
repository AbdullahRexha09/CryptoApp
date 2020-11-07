using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapplication.Models
{
    public class User
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column("name"), Required]
        public string FirstName { get; set; }
        [Column("lastname"), Required]
        public string lastName { get; set; }
        [Column("email"), Required,EmailAddress]
        public string Email { get; set; }
        [Column("password"), Required]
        public string Password { get; set; }
        [Column("refreshtoken ")]
        public string RefreshToken { get; set; }
        [Column("refreshtokenexpirytime")]
        public DateTime RefreshTokenExpiryTime { get; set; }
        [Column("role")]
        public string Role { get; set; } = "User";
    }
}
