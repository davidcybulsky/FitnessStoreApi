namespace Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string HashedPassword { get; set; }
        public required string PhoneNumber { get; set; }
        public required virtual List<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
