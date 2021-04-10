using BL;
using DAL.Dto;
using MVVM_Core;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class ProfileViewModel : BasePageViewModel
    {
        private readonly UserService userService;
        private readonly OrderService orderService;

        public ClientDto ClientDto { get; set; }

        public OrderDto SelectedOrder { get; set; }

        public ObservableCollection<ServiceDto> OrderedServices { get; set; }

        public ProfileViewModel(PageService pageservice, UserService userService, OrderService orderService) : base(pageservice)
        {
            this.userService = userService;
            this.orderService = orderService;
            Init();
        }

        async void Init()
        {
            Orders = new ObservableCollection<OrderDto>(await orderService.GetAllOrders(userService.CurrentUser.Id));
            ClientDto = userService.CurrentUser;
        }

        public ICommand CancelOrder => new CommandAsync(async x =>
        {
            if(x is OrderDto dto && 
            MessageBox.Show("Отменить заказ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int index = Orders.IndexOf(dto);
                Orders[index] = await orderService.CancelOrder(dto.Id);
            }
        });

        public bool IsLoadingVisible { get; set; }

        public bool IsServicesExist { get; set; }

        public ICommand SelectOrder => new CommandAsync(async x =>
        {
            if (x is OrderDto dto)
            {
                IsLoadingVisible = true;
                SelectedOrder = dto;

                IsServicesExist = OrderedServices.Count > 0;

                IsLoadingVisible = false;

            }
        });

        protected override void Back()
        {
        }

        public ObservableCollection<OrderDto> Orders { get; set; }

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}