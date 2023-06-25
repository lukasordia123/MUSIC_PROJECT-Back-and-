using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("FirstName")]

        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("LastName")]

        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [MaxLength(256)]
        public string PasswordHash { get; set; }
        [MaxLength]
        public string PasswordSalt { get; set; }
    }
}
