namespace Api.Dtos
{
    public class CreateAddressDto
    {
        public int Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
