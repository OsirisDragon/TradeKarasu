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
    public class U_A9921_ViewModel : ViewModelParent<UIModel_A9921>
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

        public U_A9921_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_A9921>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPADJ, UIModel_A9921>().ReverseMap();
            }));

            using (var das = Factory.CreateDalSession())
            {
                var dA9921 = new D_A9921<UIModel_A9921>(das);
                MainGridData = dA9921.ListByDate(StartDate, EndDate, ProdId).ToList();
            }
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
                    var dA9921 = new D_A9921<UIModel_A9921>(das);
                    MainGridData = dA9921.ListByDate(StartDate, EndDate, ProdId).ToList();
                }
            });

            await task;
        }
    }

    public class UIModel_A9921 : TPPADJ
    {
        public virtual string PDK_SUBTYPE { get; set; } // char(6)
    }
}