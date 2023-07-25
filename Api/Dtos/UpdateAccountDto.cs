namespace Api.Dtos
{
    public class UpdateAccountDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UpdateAddressDto Address { get; set; }
    }
}
