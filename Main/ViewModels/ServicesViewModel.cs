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

        public ServiceDto Selected { get; set; }

        async void Init()
        {
            Services = new ObservableCollection<ServiceDto>(await servicesService.GetAllServices());
        }

        public ICommand ChooseService => new Command(x =>
        {
            orderService.SetupService(Selected.Id, Selected.Cost);
            pageservice.ChangePage<Pages.OrderDataPage>(PoolIndex, DisappearAnimation.Default);
            
        }, y => Selected != null);


        public ObservableCollection<ServiceDto> Services { get; set; }

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}