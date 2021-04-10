using BL;
using DAL.Dto;
using MVVM_Core;
using System;
using System.Windows;

namespace Main.ViewModels
{
    public class OrderDataViewModel : BasePageViewModel
    {
        private readonly OrderService orderService;
        private readonly UserService userService;
        private readonly RegisterService registerService;

        public bool IsAutorized { get; set; }

        public OrderDto OrderDto { get; set; }

        public ClientDto ClientDto { get; set; } = new ClientDto();

        public DateTime StartDate { get; set; }

        public OrderDataViewModel(PageService pageservice, 
            OrderService orderService, UserService userService, RegisterService registerService) : base(pageservice)
        {
            this.orderService = orderService;
            this.userService = userService;
            this.registerService = registerService;
            Init();
        }

        void Init()
        {
            
            OrderDto = orderService.GetOrder();
            OrderDate = OrderDto.OrderDate.DateTime;
            StartDate = OrderDate;
            IsAutorized = userService.IsAutorized;
        }

        public DateTime OrderDate { get; set; }

        bool Check()
        {
            if (!IsAutorized)
            {
                if (string.IsNullOrEmpty(ClientDto.Name))
                {
                    MessageBox.Show("Поле 'Имя' должно быть заполнено", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(ClientDto.Phone))
                {
                    MessageBox.Show("Поле 'Номер телефона' должно быть заполнено", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(OrderDto.Address))
            {
                MessageBox.Show("Поле 'Адрес' должно быть заполнено", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (OrderDate < DateTime.Now)
            {
                MessageBox.Show("Занчение поля 'Дата' не должно быть раньше, чем сегодня", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        protected async override void Next()
        {
            if (!Check())
            {
                return;
            }
            OrderDto.OrderDate = new DateTimeOffset(OrderDate);
            orderService.SetupFilledOrder(OrderDto);

            int userId;

            if (IsAutorized)
            {
                userId = userService.CurrentUser.Id;
            }
            else
            {
                registerService.SetupClient(ClientDto);
                var res = await registerService.RegisterAsync();
                userId = res.Item2;
            }
            orderService.SetupClient(userId);
            pageservice.ChangePage<Pages.OrderResultPage>(DisappearAnimation.Default);
        }

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}