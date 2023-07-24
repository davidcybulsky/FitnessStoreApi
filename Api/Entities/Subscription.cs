namespace Api.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public required int PlanId { get; set; }
        public virtual Plan Plan { get; set; }
        public required int UserId { get; set; }
        public virtual User User { get; set; }
        public required DateTime BuyDate { get; set; } = DateTime.UtcNow;
    }
}
