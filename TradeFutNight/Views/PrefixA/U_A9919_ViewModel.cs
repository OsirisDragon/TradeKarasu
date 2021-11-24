using AutoMapper;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.PrefixA;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixA
{
    public class U_A9919_ViewModel : ViewModelParent<UIModel_A9919>
    {
        public DateTime StartDate
        {
            get { return GetProperty(() => StartDate); }
            set { SetProperty(() => StartDate, value); }
        }

        public DateTime EndDate
        {
            get { return GetProperty(() => EndDate); }
            set { SetProperty(() => EndDate, value); }
        }

        public string ProdId
        {
            get { return GetProperty(() => ProdId); }
            set { SetProperty(() => ProdId, value); }
        }

        public U_A9919_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_A9919>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MHALT, UIModel_A9919>().ReverseMap();
            }));

            await Query();
        }

        public void Insert()
        {
        }

        public void Delete(object item)
        {
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dA9919 = new D_A9919<UIModel_A9919>(das);
                    MainGridData = dA9919.ListByDate(StartDate, EndDate, ProdId).ToList();
                }
            });

            await task;
        }
    }

    public class UIModel_A9919 : MHALT
    {
        public virtual string PDK_SUBTYPE { get; set; } // char(6)
    }
}