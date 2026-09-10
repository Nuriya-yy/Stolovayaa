using Stolovayaa.Model;

namespace Stolovayaa.Repositories
{
    public class OrderRepository : BaseRepository<Orders>, IOrderRepository
    {
        public OrderRepository(Dining_roomEntities1 context) : base(context) { }

        public void Add(Orders order)
        {
            _dbSet.Add(order);
        }

        public void AddOrderItem(OrdersItems item)
        {
            _context.OrdersItems.Add(item);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}