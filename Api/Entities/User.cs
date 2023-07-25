using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastUpdatedDate { get; set; }
        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        public bool IsDeleted { get; set; } = false;
    }
}
