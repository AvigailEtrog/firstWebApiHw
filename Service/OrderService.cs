using Entities;
using Microsoft.Extensions.Logging;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IProductRepository _productRepository;
        ILogger<OrderService> _logger;
        public OrderService(IOrderRepository orderRepository,IProductRepository productRepository,ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<Order> createNewOrder(Order order)
        {
            double? orderSum = 0;
            int[] prouctsIds=new int[order.OrderItems.Count];
           for(int i = 0; i < order.OrderItems.Count; i++)
            {
                prouctsIds[i] = order.OrderItems.ElementAt(i).ProductId;
            }
            List<Product> productsOrder = await _productRepository.getCertainProducts(prouctsIds);
            for (int i = 0; i < order.OrderItems.Count; i++)
            {
                orderSum += order.OrderItems.ElementAt(i).Quantity * productsOrder.ElementAt(i).ProductPrice;
            }
            if(orderSum!=(double)order.OrderSum)
            {
                _logger.LogWarning($"user {order.UserId} try stole from superMarket instead pay {orderSum}$ he tried pay {order.OrderSum}");
                return null;
            }
            else { 
            Order newOrder = await _orderRepository.createNewOrder(order);
            return newOrder != null ? newOrder : null;}
        }
    }
}
