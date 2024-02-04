using BigBang.WebServer.Common.Attributes;
using BigBang.WebServer.Common.Events;
using BigBang.App.SuperMarket.Domain;
using BigBang.WebServer.Common;
using NHibernate;
using System.Linq;
using BigBang.WebServer.Common.Exceptions;

namespace BigBang.App.SuperMarket.Business.OrderDetail.BusinessEvents
{
	  [EventSubscription(typeof(IBusinessAddingEvent<SMOrderDetail>))]
    public class OrderDetailAddingEvent : IBusinessAddingEvent<SMOrderDetail>
    {
        public void OnAdding(IBusinessAddingEventContext<SMOrderDetail> context)
        {

            var quantity = context.Entity.Quantity;
            var session = ServiceLocator.Get<ISession>();
            var product = session.Query<SMProduct>().FirstOrDefault(x => x.Id == context.Entity.Product.Id);
            if (product is null)
                throw new BigBangException("محصولی انتخاب نشده");
            else if (product.Inventory< quantity)
                throw new BigBangException("تعداد بیشتر از موجودی محصول است");
            else { 
                product.Inventory -= quantity; 
            }
        }
    }
}