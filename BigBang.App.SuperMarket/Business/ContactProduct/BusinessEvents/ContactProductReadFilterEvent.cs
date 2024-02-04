using BigBang.WebServer.Common.Attributes;
using BigBang.WebServer.Common.Events;
using BigBang.App.SuperMarket.Domain;
using NHibernate;
using BigBang.WebServer.Common;
using System.Linq;
using System;

namespace BigBang.App.SuperMarket.Business.ContactProduct.BusinessEvents
{
    [EventSubscription(typeof(IBusinessReadFilterEvent<SMContactProduct>))]
    public class ContactProductBusinessReadFilterEvent : IBusinessReadFilterEvent<SMContactProduct>
    {
        private readonly IBusinessContext _businessContext;

        public ContactProductBusinessReadFilterEvent(IBusinessContext businessContext)
        {
            _businessContext = businessContext;
        }
        public void OnRead(IBusinessReadFilter<SMContactProduct> filter)
        { 
            if (_businessContext.HasKey("IsPerson"))
            {
                var session = ServiceLocator.Get<ISession>();
                if (Convert.ToBoolean(_businessContext["IsPerson"]))
                {
                    var personIds = session.Query<SMPerson>().Select(x => x.Id).ToList();
                    filter.QueryOverOf().WhereRestrictionOn(x => x.Contact.Id).IsIn(personIds);
                }
                else
                {
                    var personIds = session.Query<SMOrganization>().Select(x => x.Id).ToList();
                    filter.QueryOverOf().WhereRestrictionOn(x => x.Contact.Id).IsIn(personIds);
                }
            } 
        }
    }
}