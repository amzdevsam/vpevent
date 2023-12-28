namespace GetOrdersEvents
{
    public class RequestEvent
    {
        public string OrderingPartyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}