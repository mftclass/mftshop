using MFTShop.Models.DbModels;
using MFTShop.Models.ViewModels;

namespace MFTShop.Services
{
    public interface IOrderServices
    {
        Customer getCustomer(string Username);
        Order getOrder(int orderId, string username, bool withIncludes = false, OrderStatusTypes status = OrderStatusTypes.Open);
        OrderViewModel getOrderDetails(int OrderId, string Username);
        Product getProduct(int productId);
        ProductAddResponseViewModel saveOrder(string Username, int productId, int? OrderId, int quantity = 1);
    }
}