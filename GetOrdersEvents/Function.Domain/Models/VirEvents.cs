using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOrdersEvents.Function.Domain.Models
{
    public class VirEvent
    {
        public List<Event> Events { get; set; }
    }

    public class Event
    {
        public string Code { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Information { get; set; }
        public Order OrderInfo { get; set; }
    }

    public class Order
    {
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string Agency { get; set; }
        public DateTime AppointmentStartDate { get; set; }
        public DateTime AppointmentEndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PreparationDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
