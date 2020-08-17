using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsApi.Entities
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; }
       
        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }

        [Required]
        public AccountType Type { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}
