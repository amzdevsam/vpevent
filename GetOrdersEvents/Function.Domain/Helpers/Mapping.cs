using GetOrdersEvents.Function.Domain.Models;
using GetOrdersEvents.Function.Domain.Models.Akanea;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOrdersEvents.Function.Domain.Helpers
{
    static public class Mapping
    {
        public static VirEvent FromFluxAkanea(Flux flux)
        {
            VirEvent virEvent = new()
            {
                Events = new List<Event>()
                
            };

            foreach (var evnmt in flux.Evenements.Evenement)
            {
                virEvent.Events.Add(new Event()
                {
                    Code = evnmt.CodeSitu + evnmt.CodeJustif,
                    EventDate = evnmt.DateEvt,
                    AppointmentDate = evnmt.DateRdv,
                    Information = evnmt.TexteInfo,
                    OrderInfo = new Order()
                    {
                        Reference1 = evnmt.OT.Ref1,
                        Reference2 = evnmt.OT.Ref2,
                        Agency = evnmt.OT.Agence,
                        AppointmentStartDate = evnmt.OT.DateDebutRdv,
                        AppointmentEndDate = evnmt.OT.DateFinRdv,
                        CreationDate = evnmt.OT.DateCreation,
                        PreparationDate = evnmt.OT.DatePrepa,
                        DeliveryDate = evnmt.OT.DateLiv
                    }
                });
            }
            return virEvent;
        }
    }
}
