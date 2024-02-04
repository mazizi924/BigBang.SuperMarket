using BigBang.WebServer.Common.Attributes;
using BigBang.WebServer.Common.Events;
using BigBang.App.SuperMarket.Domain;
using BigBang.WebServer.Common.Services;

namespace BigBang.App.SuperMarket.Business.Order.BusinessEvents
{
	  [EventSubscription(typeof(IBusinessAddingEvent<SMOrder>))]
    public class OrderAddingEvent : IBusinessAddingEvent<SMOrder>
    {
        private readonly IBigBangIdentityService _bigBangIdentityService;

        public OrderAddingEvent(IBigBangIdentityService bigBangIdentityService)
        {
            _bigBangIdentityService = bigBangIdentityService;
        }
        public void OnAdding(IBusinessAddingEventContext<SMOrder> context)
        {
            var contact = new BigBang.Domain.BBContact
            {
                Id = _bigBangIdentityService.Identity.CurrentCOP.ContactId
            };
            context.Entity.Contact = contact;
        }
    }
}