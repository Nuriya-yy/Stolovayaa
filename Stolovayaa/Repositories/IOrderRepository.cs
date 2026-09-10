using Stolovayaa.Model;

namespace Stolovayaa.Repositories
{
    public interface IOrderRepository
    {
        void Add(Orders order);
        void AddOrderItem(OrdersItems item);
        void SaveChanges();
    }
}