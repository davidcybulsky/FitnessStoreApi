namespace Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}
