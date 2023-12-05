using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> createNewOrderAsync(Order order);
        Task<Order> getOrderByIdAsync(int id);
    }
}