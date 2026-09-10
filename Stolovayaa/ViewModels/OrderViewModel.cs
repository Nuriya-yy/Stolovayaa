using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Stolovayaa.Commands;
using Stolovayaa.DTOs;
using Stolovayaa.Servise;

namespace Stolovayaa.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private readonly IOrderService _orderService;

        public OrderViewModel(IOrderService orderService)
        {
            _orderService = orderService;

            Schoolboys = new ObservableCollection<SchoolboyDto>(_orderService.GetSchoolboys());
            Menus = new ObservableCollection<MenuDto>(_orderService.GetMenus());
            Dishes = new ObservableCollection<DishDto>();
            Cart = new ObservableCollection<OrderItemDto>();

            AddDishCommand = new RelayCommand(_ => AddDish(), _ => SelectedDish != null);
            RemoveDishCommand = new RelayCommand(_ => RemoveDish(), _ => SelectedCartItem != null);
            SaveOrderCommand = new RelayCommand(_ => SaveOrder(), _ => CanSaveOrder());
        }

        public ObservableCollection<SchoolboyDto> Schoolboys { get; }
        public ObservableCollection<MenuDto> Menus { get; }
        public ObservableCollection<DishDto> Dishes { get; }
        public ObservableCollection<OrderItemDto> Cart { get; }

        private SchoolboyDto _selectedSchoolboy;
        public SchoolboyDto SelectedSchoolboy
        {
            get => _selectedSchoolboy;
            set => SetProperty(ref _selectedSchoolboy, value);
        }

        private MenuDto _selectedMenu;
        public MenuDto SelectedMenu
        {
            get => _selectedMenu;
            set
            {
                if (SetProperty(ref _selectedMenu, value))
                {
                    LoadDishes();
                }
            }
        }

        private DishDto _selectedDish;
        public DishDto SelectedDish
        {
            get => _selectedDish;
            set => SetProperty(ref _selectedDish, value);
        }

        private OrderItemDto _selectedCartItem;
        public OrderItemDto SelectedCartItem
        {
            get => _selectedCartItem;
            set => SetProperty(ref _selectedCartItem, value);
        }

        private int _portions = 1;
        public int Portions
        {
            get => _portions;
            set => SetProperty(ref _portions, value);
        }

        public decimal TotalAmount => Cart.Sum(i => i.Count * i.PriceAtOrder);

        public ICommand AddDishCommand { get; }
        public ICommand RemoveDishCommand { get; }
        public ICommand SaveOrderCommand { get; }

        private void LoadDishes()
        {
            Dishes.Clear();
            if (SelectedMenu == null) return;

            var dishes = _orderService.GetDishesByMenuId(SelectedMenu.ID);
            foreach (var d in dishes)
                Dishes.Add(d);
        }

        private void AddDish()
        {
            if (SelectedDish == null || Portions <= 0) return;

            Cart.Add(new OrderItemDto
            {
                DishId = SelectedDish.ID,
                Count = Portions,
                PriceAtOrder = SelectedDish.Price
            });

            OnPropertyChanged(nameof(TotalAmount));
        }

        private void RemoveDish()
        {
            if (SelectedCartItem == null) return;
            Cart.Remove(SelectedCartItem);
            OnPropertyChanged(nameof(TotalAmount));
        }

        private bool CanSaveOrder()
        {
            return SelectedSchoolboy != null && Cart.Count > 0;
        }

        private void SaveOrder()
        {
            try
            {
                var orderDto = new OrderDto
                {
                    SchoolboyID = SelectedSchoolboy.Id,
                    OrderDate = DateTime.Now,
                    Status = "Новый",
                    TotalAmount = TotalAmount,
                    Items = Cart.ToList()
                };

                _orderService.CreateOrder(orderDto);
                MessageBox.Show("Заказ сохранён!", "Успех");
                Cart.Clear();
                OnPropertyChanged(nameof(TotalAmount));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}