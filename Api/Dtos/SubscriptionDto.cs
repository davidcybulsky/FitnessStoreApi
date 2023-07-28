namespace Api.Dtos
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public DateTime BuyDate { get; set; }
    }
}
