using BigBang.WebServer.Common.Attributes;
using BigBang.App.SuperMarket.Services;
using BigBang.WebServer.Common.Services.Print;
using System.Collections.Generic;
using BigBang.WebServer.Common.Exceptions;
using BigBang.WebServer.Common;
using NHibernate;
using BigBang.App.SuperMarket.Domain;
using System.Linq;
using BigBang.WebServer.Common.Services.Print.DataSources;

namespace BigBang.App.SuperMarket.Services
{
    public class ContactProductPrintService : IPrintTemplateSource
    {
        public IPrintDataSource Load(IDictionary<string, object> parameters)
        {
            int.TryParse(parameters["ProductId"].ToString(), out int productId);
            var session = ServiceLocator.Get<ISession>();
            var product = session.Query<SMProduct>().First(x => x.Id == productId);

            var person = (from contactPrduct in session.Query<SMContactProduct>().Where(x=> x.Product.Id == productId)
                        join persn in session.Query<SMPerson>()
                        on contactPrduct.Contact.Id equals persn.Id
                        select persn.Name).ToList();

            var organization = (from contactPrduct in session.Query<SMContactProduct>().Where(x => x.Product.Id == productId)
                                join org in session.Query<SMOrganization>()
                        on contactPrduct.Contact.Id equals org.Id 
                        select org.Name).ToList();

            //SinglePrintDataSource data = new SinglePrintDataSource();
            //data.Data = product;
            //return data;
            MultiplePrintDataSource dataSource = new MultiplePrintDataSource();
            dataSource.Add("Product", product);
            dataSource.Add("Person", person);
            dataSource.Add("Organization", organization);
            return dataSource;
        }
    }
}