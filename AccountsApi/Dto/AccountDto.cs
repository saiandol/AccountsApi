using System.ComponentModel.DataAnnotations;
using AccountsApi.Entities;

namespace AccountsApi.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public AccountType Type { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public string Address { get; set; }
    }
}
