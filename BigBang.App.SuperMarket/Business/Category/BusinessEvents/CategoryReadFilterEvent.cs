using System.Collections.Generic;
using BigBang.WebServer.Common.Attributes;
using BigBang.WebServer.Common.Events;
using BigBang.App.SuperMarket.Domain;
using BigBang.WebServer.Common;
using BigBang.App.SuperMarket.Domain.Enums;

namespace BigBang.App.SuperMarket.Business.Category.BusinessEvents
{
    [EventSubscription(typeof(IBusinessReadFilterEvent<SMCategory>))]
    public class CategoryBusinessReadFilterEvent : IBusinessReadFilterEvent<SMCategory>
    {
        private readonly IBusinessContext _businessContext; 

        public CategoryBusinessReadFilterEvent(IBusinessContext businessContext)
        {
            _businessContext = businessContext; 
        }
        public void OnRead(IBusinessReadFilter<SMCategory> filter)
        {
            if (_businessContext.HasKey("ActiveCategory"))
            filter.QueryOverOf().Where(x => x.State == (int)CategoryStatus.Active);
        }
    }
}