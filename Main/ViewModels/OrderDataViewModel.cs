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
        private readonly EventBus eventBus;

        public bool IsAutorized { get; set; }

        public OrderDto OrderDto { get; set; }

        public ClientDto ClientDto { get; set; } = new ClientDto();

        public DateTime StartDate { get; set; }

        public OrderDataViewModel(PageService pageservice, 
            OrderService orderService, UserService userService, 
            RegisterService registerService, EventBus eventBus) : base(pageservice)
        {
            this.orderService = orderService;
            this.userService = userService;
            this.registerService = registerService;
            this.eventBus = eventBus;
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
                    MessageBox.Show("Поле 'ФИО' должно быть заполнено", "", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (!IsAutorized)
            {
                registerService.SetupClient(ClientDto);
                pageservice.ChangePage<Pages.ProfileRegisterPage>(PoolIndex, DisappearAnimation.Default);
            }
            else
            {
                pageservice.ChangePage<Pages.OrderResultPage>(DisappearAnimation.Default);
            }
        }



        public override int PoolIndex => Rules.Pages.MainPool;
    }
}