using BL;
using DAL.Models;
using MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class WorkersViewModel : BasePageViewModel
    {
        private readonly SourceService sourceService;
        ISliderAnimation prevAnim = DisappearAndToSlideAnim.ToRight;
        ISliderAnimation nextAnim = DisappearAndToSlideAnim.ToLeft;

        int curIndex;

        List<Worker> workers;

        public WorkersViewModel(PageService pageservice, BL.SourceService sourceService) : base(pageservice)
        {
            this.sourceService = sourceService;
            Init();
        }

        private void Init()
        {
            sourceService.Reload(
                img => $"{Environment.CurrentDirectory}\\Images\\{img}.jpg");

            workers = sourceService.GetWorkers().ToList();
            if (workers.Count > 0)
            {
                SetWorker(0);
            }
        }

        async void SetWorker(int index, ISliderAnimation anim = null)
        {
            IsLeftMaximum = index == 0;
            IsRightMaximum = index == workers.Count - 1;

            if(anim != null)
                await anim.AnimateOldPage(Control.Content as UIElement);

            CurrentWorker = workers[index];

            if(anim != null)
                await anim.AnimateNewPage(Control.Content as UIElement);

        }

        void NextSlide()
        {            
            if (curIndex + 1 < workers.Count)
            {
                IsLeftMaximum = false;
                SetWorker(++curIndex, nextAnim);
            }

        }

        void PrevSlide()
        {
            if (curIndex > 0)
            {
                IsRightMaximum = false;
                SetWorker(--curIndex, prevAnim);
            }
        }

        public ContentControl Control { get; set; }
        public bool IsRightMaximum { get; set; }
        public bool IsLeftMaximum { get; set; }
        public Worker CurrentWorker { get; set; }

        public ICommand GetStackCommand => new Command(x =>
        {
            if(x is ContentControl s)
            {
                Control = s;
            }
        });

        public ICommand NextSlideCommand => new Command(x => NextSlide());
        public ICommand PrevSlideCommand => new Command(x => PrevSlide());

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}