using System.Collections.Generic;
using BigBang.WebServer.Common.Attributes;
using BigBang.WebServer.Common.Events;
using BigBang.App.SuperMarket.Domain;

namespace BigBang.App.SuperMarket.Business.Category.BusinessEvents
{
    [EventSubscription(typeof(IBusinessLoadedEvent<SMCategory>))]
    public class CategoryBusinessLoadedEvent : IBusinessLoadedEvent<SMCategory>
    {
        public void OnLoaded(IBusinessLoadedEventContext<SMCategory> context)
        {
            foreach (var item in context.Items)
            {
                item.VirtualProductsCount = item.Products.Count;
            }
        }
    }
}