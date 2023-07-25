namespace Api.Dtos
{
    public class SignUpDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string PhoneNumber { get; set; }
        public CreateAddressDto Address { get; set; }
    }
}
