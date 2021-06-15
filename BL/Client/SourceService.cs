using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BL
{
    public class SourceService
    {
        public SourceService(MapperService mapper)
        {
            this.mapper = mapper;
        }
        public IEnumerable<dynamic> Resources { get; private set; }

        public bool IsSetted => Resources != null;

        public string Header { get; set; } = "Наши сертификаты";
        public bool IsDetails { get; private set; }

        public void SetImagesSerts(Window _curWindow)
        {
            IsDetails = false;
            Resources = new List<dynamic>
            {
                new
                {
                    Content = _curWindow.FindResource("SertImg1"),
                    Source = (_curWindow.FindResource("SertImg1") as Image).Source,
                     Header = "Сертификат на модули УГП"
                },

                new
                {
                    //Content = _curWindow.FindResource("SertImg2"),
                    Source = (_curWindow.FindResource("SertImg2") as Image).Source,
                    Header = "Сертификат_ПБ_Модули",
                },
                new
                {
                    //Content = _curWindow.FindResource("SertImg3"),
                    Source = (_curWindow.FindResource("SertImg3") as Image).Source,
                    Header = "Сертификат_ПБ_МУПТВ",
                },
                new
                {
                    //Content = _curWindow.FindResource("SertImg4"),
                    Source = (_curWindow.FindResource("SertImg4") as Image).Source,
                    Header = "Сертификат_ТР_ТС_л.1.",
                },
                new
                {
                    //Content = _curWindow.FindResource("SertImg5"),
                    Source = (_curWindow.FindResource("SertImg5") as Image).Source,
                    Header = "Сертификат_ТР_ТС_л.2.",
                },
                new
                {
                    //Content = _curWindow.FindResource("SertImg6"),
                    Source = (_curWindow.FindResource("SertImg6") as Image).Source,
                     Header = "Сертификат_ТР_ТС_л.3.",
                },
            };
        }

        public void SetWorkExampleImages(Window _curWindow)
        {
            IsDetails = true;
            
            Resources = new List<dynamic>
            {
                new
                {
                    Source = _curWindow.FindResource("ImgEx1"),
                     Header = "Международные аэропорты",
                     Address = "Международный аэропорт \"Пулково\" (г. Санкт-Петербург), Междунарожный аэропорт \"Бегишево\" (г. Нижнекамск), Международной аэропорт \"Шереметьево\" (г. Москва)",
                     Year = 2015,
                     Descr = "Установлена многоуровневая система пожарной безопасности соглассно всем совеременным международным стандартам систем пожарной безопасности на объектах, установлено более 30 тыс. дымовых датчиков, систем пожарутушения и оповещения. Обновлена панель управления, связывающая все датчики в единую систему безопасности"
                },

                new
                {
                    Source = _curWindow.FindResource("ImgEx2"),
                    Header = "Больше 100 продуктовых магазинов",
                    Address = "Более 25 городов по всей России из различных областей и республик",
                    Year = 2016,
                     Descr = "Обновлено и заменено старое техническое оборудование, установлены датчики дыма и устройства пожаротушения в каждом магазине"
                },
                new
                {
                    Source = _curWindow.FindResource("ImgEx3"),
                    Header = "Министерство обороны РФ",
                    Address = "г. Москва",
                    Year = 2014,
                     Descr = "Обновленно техническое оборудование, налажена многоуровневая система пожарной безопасности, установлено более 200 дымовых датчиков, систем пожарутушения и оповещения."
                },
            };
        }

        IEnumerable<Worker> workers;
        private readonly MapperService mapper;

        public void Reload(Func<string, string> pathGetter)
        {
            workers = new List<Worker>
            {
                new Worker
                {
                    Gender = true,
                    Name = "Иван",

                    Special = "Монатжник",
                    StartWorkingDate = new System.DateTimeOffset(new DateTime(2011, 2, 3)),
                    WorkerAnket = new WorkerAnket
                    {
                        Quotation = "Работал уже более чем на 100 объектах, мне нравится тут",
                        Image = "male4",
                    }
                },
                new Worker
                {
                    Gender = false,
                    Name = "Аня",
                    Special = "Менеджер",
                    StartWorkingDate = new System.DateTimeOffset(new DateTime(2015, 8, 21)),
                    WorkerAnket = new WorkerAnket
                    {
                        Quotation = "Ну что сказать, я действительно довольна качеством наших услуг",
                        Image = "female1"
                    }
                },
                new Worker
                {
                    Gender = true,
                    Name = "Сергей",
                    Special = "Управляющий",
                    StartWorkingDate = new System.DateTimeOffset(new DateTime(2010, 9, 12)),
                    WorkerAnket = new WorkerAnket
                    {
                        Quotation = "Помню с чего мы начинали, и, смотря теперь на наши результаты, я горжусь нашим коллективом!",
                        Image = "male1"
                    }
                },
                new Worker
                {
                    Gender = false,
                    Name = "Алена",
                    Special = "Оператор кол-центра",
                    StartWorkingDate = new System.DateTimeOffset(new DateTime(2017, 11, 5)),
                    WorkerAnket = new WorkerAnket
                    {
                        Quotation = "Не знаю кому как, мне нормас",
                        Image = "female2"
                    }
                },
                new Worker
                {
                    Gender = true,
                    Name = "Петр",
                    Special = "Старший инженер",
                    StartWorkingDate = new System.DateTimeOffset(new DateTime(2013, 4, 21)),
                    WorkerAnket = new WorkerAnket
                    {
                        Quotation = "Нигде еще не видел такого уровня ответсвтенности за работу",
                        Image = "male2"
                    }
                },
                new Worker
                {
                    Gender = true,
                    Name = "Семен",
                    Special = "Электромонтер",
                    StartWorkingDate = new System.DateTimeOffset(new DateTime(2014, 2, 21)),
                    WorkerAnket = new WorkerAnket
                    {
                        Quotation = "Кто здесь?!",
                        Image = "male3"
                    }
                },

            };

            foreach (var w in workers)
            {
                w.WorkerAnket.ImagePath = pathGetter?.Invoke(w.WorkerAnket.Image);
            }
        }


        public IEnumerable<Worker> GetWorkers()
        {            
            return workers;
        }
    }
}