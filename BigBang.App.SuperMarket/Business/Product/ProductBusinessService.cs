using BigBang.WebServer.Common.Attributes;
using BigBang.App.SuperMarket.Domain;
using BigBang.WebServer.Common;
using NHibernate;
using BigBang.WebServer.Common.Services;
using System.Linq;

namespace BigBang.App.SuperMarket.Business.Product
{
	[BusinessService(typeof(SMProduct))]
	public class ProductBusinessService
	{
        public ProductBusinessService()
        {

        }
        private readonly IBigBangIdentityService _bigBangIdentityService;
        public ProductBusinessService(IBigBangIdentityService bigBangIdentityService)
        {
            _bigBangIdentityService = bigBangIdentityService;
        }
        public virtual void AddToStore(SMProduct Product, int Quantity)
        {
            var session = ServiceLocator.Get<ISession>();
            Product.Inventory += Quantity;
            var contact = new BigBang.Domain.BBContact
            {
                Id = _bigBangIdentityService.Identity.CurrentCOP.ContactId
            };
            var store = new SMStore
            {
                Product = Product,
                Quantity = Quantity,
                Contact= contact,
                AddToStoreDate = System.DateTime.Now
            };
            session.Save(store);
        }
	}
}