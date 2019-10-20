using MFTShop.Models.ViewModels;

namespace MFTShop.Services
{
    public interface IOrderServices
    {
        OrderViewModel saveOrder(int customerId, int productId);
    }
}