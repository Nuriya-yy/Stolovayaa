using System.Collections.Generic;
using Stolovayaa.DTOs;

namespace Stolovayaa.Servise
{
    public interface IOrderService
    {
        List<SchoolboyDto> GetSchoolboys();
        List<MenuDto> GetMenus();
        List<DishDto> GetDishesByMenuId(int menuId);
        void CreateOrder(OrderDto orderDto);
    }
}