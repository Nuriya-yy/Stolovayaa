using System.Collections.Generic;
using System.Linq;
using Stolovayaa.DTOs;
using Stolovayaa.Model;
using Stolovayaa.Repositories;

namespace Stolovayaa.Servise
{
    public class OrderService : IOrderService
    {
        private readonly ISchoolboyRepository _schoolboyRepo;
        private readonly IDaylyMenuRepository _menuRepo;
        private readonly IOrderRepository _orderRepo;

        public OrderService(ISchoolboyRepository schoolboyRepo,
                            IDaylyMenuRepository menuRepo,
                            IOrderRepository orderRepo)
        {
            _schoolboyRepo = schoolboyRepo;
            _menuRepo = menuRepo;
            _orderRepo = orderRepo;
        }

        public List<SchoolboyDto> GetSchoolboys()
        {
            return _schoolboyRepo.GetAll()
                .Select(s => new SchoolboyDto
                {
                    Id = s.ID,
                    FirstName = s.FirstName,
                    ClassNumber = s.Class,
                    Balans = s.Balans
                }).ToList();
        }

        public List<MenuDto> GetMenus()
        {
            return _menuRepo.GetAll()
                .Select(m => new MenuDto
                {
                    ID = m.ID,
                    Date = m.Date,
                    DishesID = m.DishesID
                }).ToList();
        }

        public List<DishDto> GetDishesByMenuId(int menuId)
        {
            return _menuRepo.GetDishesByMenuId(menuId)
                .Select(d => new DishDto
                {
                    ID = d.ID,
                    Title = d.Title,
                    Category = d.Category,
                    Price = d.Price
                }).ToList();
        }

        public void CreateOrder(OrderDto orderDto)
        {
            var order = new Orders
            {
                SchoolboyID = orderDto.SchoolboyID,
                OrderDate = orderDto.OrderDate,
                Status = orderDto.Status,
                TotalAmount = orderDto.TotalAmount
            };

            _orderRepo.Add(order);
            _orderRepo.SaveChanges();

            foreach (var item in orderDto.Items)
            {
                var orderItem = new OrdersItems
                {
                    OrdersID = order.ID,
                    DishesID = item.DishId,
                    Count = item.Count,
                    PriceAtOrder = item.PriceAtOrder
                };
                _orderRepo.AddOrderItem(orderItem);
            }

            _orderRepo.SaveChanges();
        }
    }
}