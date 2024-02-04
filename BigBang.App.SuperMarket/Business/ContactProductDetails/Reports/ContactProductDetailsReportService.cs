using System;
using System.Linq;
using BigBang.App.SuperMarket.Domain;
using BigBang.WebServer.Common;
using BigBang.WebServer.Common.Attributes;
using NHibernate;

namespace BigBang.App.SuperMarket.Business.ContactProductDetails
{
    [ReportService]
    public class ContactProductDetailsReportService
    {
        public virtual object DataSource(Guid id)
        {
            var session = ServiceLocator.Get<ISession>();
            return session.Query<SMContactProduct>().Where(x => x.Contact.Id == id).Select(x => x.Product).ToList();
        }
    }
}