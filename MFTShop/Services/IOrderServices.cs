﻿using MFTShop.Models.DbModels;
using MFTShop.Models.ViewModels;
using System.Collections.Generic;

namespace MFTShop.Services
{
    public interface IOrderServices
    {
        bool addAuthorityToOrder(string username, string authority, int? orderId = null);
        OrderViewModel CustomerOrderDetails(int OrderId, string Username);
        List<Order> CustomerOrders(string username, int? orderId = null, OrderStatusTypes? status = OrderStatusTypes.Open, bool withIncludes = false);
        Customer getCustomer(string Username);
        Order getOrder(string username, int? orderId = null, OrderStatusTypes? status = OrderStatusTypes.Open, bool withIncludes = false);
        OrderViewModel getOrderDetails(int OrderId, string Username);
        Product getProduct(int productId);
        ProductAddResponseViewModel saveOrder(string Username, int productId, int quantity = 1);
        int validateOrders();
        bool PayForOrder(int Amount, string Username);
    }
}