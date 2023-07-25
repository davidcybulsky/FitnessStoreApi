using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
