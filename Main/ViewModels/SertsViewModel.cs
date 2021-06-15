using BL;
using MVVM_Core;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class SertsViewModel : BasePageViewModel
    {
        public override int PoolIndex => Rules.Pages.MainPool;

        private dynamic selectedItem;
        private readonly SourceService sourceService;

        public string Header { get; set; }

        public ObservableCollection<dynamic> Images { get; set; }
        public SertsViewModel(PageService pageservice, SourceService sourceService) : base(pageservice)
        {
            this.sourceService = sourceService;
            IsDetails = sourceService.IsDetails;
            if (sourceService.IsSetted)
            {
                Images = new ObservableCollection<dynamic>(sourceService.Resources);
                Header = sourceService.Header;
            }
        }

        public bool IsDetails { get; set; }

        public bool IsCentralImageVis { get; set; }

        public ICommand Close => new Command(x =>
        {
            IsCentralImageVis = false;
        });

        public dynamic SelectedItem 
        { 
            get => selectedItem; 
            set
            {
                IsCentralImageVis = value != null;
                selectedItem = value; 
            } 
        }

    }
}