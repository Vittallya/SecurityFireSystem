using BL;
using DAL.Dto;
using Main.Events;
using MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Main.ViewModels
{
    [Singleton]
    public class HomeViewModel : BasePageViewModel
    {
        private readonly EventBus eventBus;
        private readonly UserService userService;
        private readonly SourceService sourceService;

        Window window = App.Current.MainWindow;

        public HomeViewModel(PageService pageservice, EventBus eventBus, UserService userService, SourceService sourceService) : base(pageservice)
        {
            this.eventBus = eventBus;
            this.userService = userService;
            this.sourceService = sourceService;
            Init();
        }

        void Init()
        {
            userService.Autorized += UserService_Autorized;
            userService.Exited += UserService_Exited;
        }

        private void UserService_Exited()
        {
            IsAutorized = false;
            User = null;
        }
        private void UserService_Autorized(ClientDto arg)
        {
            IsAutorized = true;
            User = arg;
        }

        public bool IsAutorized { get; set; }

        public ClientDto User { get; set; } 


        public ICommand ToAutorize => new Command(x =>
        {
            pageservice.ChangePage<Pages.LoginPage>(PoolIndex, DisappearAnimation.Default);
            eventBus.Subscribe<Events.AccountEntered, HomeViewModel>(OnEntered);
        });

        private async Task OnEntered(AccountEntered arg)
        {
            pageservice.ChangePage<Pages.HomePage>(DisappearAnimation.Default);
        }

        public ICommand ToServices => new Command(x =>
        {
            pageservice.ChangePage<Pages.ServicesPage>(DisappearAnimation.Default);
        });

        public ICommand ToSerts => new Command(x =>
        {
            sourceService.SetImagesSerts(window);
            sourceService.Header = "Наши сертификаты";
            pageservice.ChangePage<Pages.SertsPage>(PoolIndex, DisappearAnimation.Default);
        });

        public ICommand ToWorkers => new Command(x =>
        {
            pageservice.ChangePage<Pages.WorkersPage>(PoolIndex, DisappearAnimation.Default);
        });

        public ICommand ToWorks => new Command(x =>
        {
            sourceService.SetWorkExampleImages(window);
            sourceService.Header = "Где стоят наши пожарные системы";
            pageservice.ChangePage<Pages.SertsPage>(PoolIndex, DisappearAnimation.Default);

        });

        public ICommand LogoutCommand => new Command(x =>
        {
            userService.Logout();
        });

        public ICommand ToProfileView => new Command(x =>
        {
            pageservice.ChangePage<Pages.ClientPage>(DisappearAnimation.Default);
        });
       

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}
