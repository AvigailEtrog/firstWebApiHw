using Entities;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> createNewOrderAsync(Order order);
        Task<Order> getOrderByIdAsync(int id);
    }
}