using BL;
using DAL.Dto;
using MVVM_Core;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class ServicesViewModel : BasePageViewModel
    {
        private readonly ServicesService servicesService;
        private readonly EventBus eventBus;
        private readonly OrderService orderService;

        public ServicesViewModel(PageService pageservice, 
            ServicesService servicesService, EventBus eventBus, OrderService orderService) : base(pageservice)
        {
            this.servicesService = servicesService;
            this.eventBus = eventBus;
            this.orderService = orderService;
            Init();
        }

        async void Init()
        {
            Services = new ObservableCollection<ServiceDto>(await servicesService.GetAllServices());
        }

        public ICommand ChooseService => new Command(x =>
        {
            if (x is ServiceDto dto)
            {
                orderService.SetupService(dto.Id, dto.Cost);
                pageservice.ChangePage<Pages.OrderDataPage>(PoolIndex, DisappearAnimation.Default);
            }
        });


        async Task Reload()
        {
            await servicesService.ReloadAsync();
        }

        public ObservableCollection<ServiceDto> Services { get; set; }

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}