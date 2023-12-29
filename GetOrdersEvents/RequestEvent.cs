using System.ComponentModel.DataAnnotations;

namespace GetOrdersEvents
{
    public class RequestEvent
    {
        [Required]
        public string OrderingPartyId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }

}