using BigBang.WebServer.Common.Attributes;
using BigBang.WebServer.Common.Events;
using BigBang.App.SuperMarket.Domain;
using BigBang.WebServer.Common;
using BigBang.WebServer.Common.Services;
using NHibernate;
using BigBang.App.SuperMarket.Domain.Enums;
using System;
using System.Linq;

namespace BigBang.App.SuperMarket.Business.Product.BusinessEvents
{
    [EventSubscription(typeof(IBusinessReadFilterEvent<SMProduct>))]
    public class ProductBusinessReadFilterEvent : IBusinessReadFilterEvent<SMProduct>
    {
        private readonly IBusinessContext _businessContext;
        private readonly IBigBangIdentityService _bigBangIdentityService;

        public ProductBusinessReadFilterEvent(IBusinessContext businessContext, IBigBangIdentityService bigBangIdentityService)
        {
            _businessContext = businessContext;
            _bigBangIdentityService = bigBangIdentityService;
        }
        public void OnRead(IBusinessReadFilter<SMProduct> filter)
        {
            var isActive = ServiceLocator.Get<IOptionsService>().GetOptions<SMActiveProduct>().IsActive; 

            if (isActive || _businessContext.HasKey("ActiveProduct"))
                filter.QueryOverOf().Where(x => x.IsActive == (int)ProductStatus.Active);

            if (_businessContext.HasKey("CurrentUserProduct"))
            {
                var session = ServiceLocator.Get<ISession>();
                Guid contactId = _bigBangIdentityService.Identity.CurrentCOP.ContactId;
                var productIds = session.Query<SMContactProduct>().Where(x => x.Contact.Id == contactId)
                    .Select(x => x.Product.Id).ToList();
                filter.QueryOverOf().WhereRestrictionOn(x => x.Id).IsIn(productIds);
            }
        }
    }
}