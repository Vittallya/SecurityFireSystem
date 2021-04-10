using BL;
using MVVM_Core;

namespace Main.ViewModels
{
    public class OrderResultViewModel : BasePageViewModel
    {
        private readonly OrderService orderService;
        private readonly ServicesService servicesService;
        private readonly UserService userService;

        public OrderResultViewModel(PageService pageservice, 
            OrderService orderService, ServicesService servicesService) : base(pageservice)
        {
            this.orderService = orderService;
            this.servicesService = servicesService;
            Init();
        }

        public string Message { get; set; }

        public bool IsAnimVisible { get; set; }

        async void Init()
        {
            IsAnimVisible = true;
            bool res = await orderService.ApplyOrder();
            Message = res ? "Оформлено!" : orderService.ErrorMessage;
            orderService.Clear();
            pageservice.ClearHistoryByPool(PoolIndex);
            IsAnimVisible = false;
        }


        protected override void Next()
        {
            pageservice.ChangePage<Pages.HomePage>(PoolIndex, DisappearAnimation.Default);
        }

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}