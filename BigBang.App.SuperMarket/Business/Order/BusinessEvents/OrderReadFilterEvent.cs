using System.Collections.Generic;
using BigBang.WebServer.Common.Attributes;
using BigBang.WebServer.Common.Events;
using BigBang.App.SuperMarket.Domain;
using BigBang.WebServer.Common.Services;
using BigBang.WebServer.Common;
using NHibernate;
using System;
using System.Linq;

namespace BigBang.App.SuperMarket.Business.Order.BusinessEvents
{
    [EventSubscription(typeof(IBusinessReadFilterEvent<SMOrder>))]
    public class OrderBusinessReadFilterEvent : IBusinessReadFilterEvent<SMOrder>
    {
        private readonly IBigBangIdentityService _bigBangIdentityService;

        public OrderBusinessReadFilterEvent(IBigBangIdentityService bigBangIdentityService)
        { 
            _bigBangIdentityService = bigBangIdentityService;
        }
        public void OnRead(IBusinessReadFilter<SMOrder> filter)
        {  
            filter.QueryOverOf().Where(x => x.Contact.Id== _bigBangIdentityService.Identity.CurrentCOP.ContactId);
        }
    }
}