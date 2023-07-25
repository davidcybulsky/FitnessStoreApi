namespace Api.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public virtual Plan Plan { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime BuyDate { get; set; } = DateTime.UtcNow;
    }
}
