using System;
using System.Linq;
using BigBang.WebServer.Common;
using BigBang.WebServer.Common.Attributes;
using NHibernate;

namespace BigBang.App.SuperMarket.Business.ContactProduct
{
    [ReportService]
    public class ContactProductReportService
    {
        public virtual object Report()
        { 
            var session = ServiceLocator.Get<ISession>();
            return session.Query<BigBang.Domain.BBContact>().ToList(); 
        }
    }
}