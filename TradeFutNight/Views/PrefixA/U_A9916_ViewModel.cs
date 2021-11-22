using AutoMapper;
using CrossModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.PrefixA;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixA
{
    public class U_A9916_ViewModel : ViewModelParent<UIModel_A9916>
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

        public IList<ItemInfo> TpphaltTypeInfos
        {
            get { return GetProperty(() => TpphaltTypeInfos); }
            set { SetProperty(() => TpphaltTypeInfos, value); }
        }

        public IList<ItemInfo> TpphaltMsgTypeInfos
        {
            get { return GetProperty(() => TpphaltMsgTypeInfos); }
            set { SetProperty(() => TpphaltMsgTypeInfos, value); }
        }

        public IList<ItemInfo> TpphaltRangeInfos
        {
            get { return GetProperty(() => TpphaltRangeInfos); }
            set { SetProperty(() => TpphaltRangeInfos, value); }
        }

        public U_A9916_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_A9916>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPHALT, UIModel_A9916>().ReverseMap();
            }));

            using (var das = Factory.CreateDalSession())
            {
                var dA9916 = new D_A9916<UIModel_A9916>(das);
                MainGridData = dA9916.ListByDate(StartDate, EndDate, "").ToList();
            }
            TpphaltTypeInfos = DropDownItems.TpphaltType();
            TpphaltMsgTypeInfos = DropDownItems.TpphaltMsgType();
            TpphaltRangeInfos = DropDownItems.TpphaltRange();
        }

        public void Insert()
        {
        }

        public void Delete(object item)
        {
        }

        public async Task Query(string prodId)
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dA9916 = new D_A9916<UIModel_A9916>(das);
                    MainGridData = dA9916.ListByDate(StartDate, EndDate, prodId).ToList();
                }
            });

            await task;
        }
    }

    public class UIModel_A9916 : TPPHALT
    {
        public virtual string PDK_SUBTYPE { get; set; } // char(6)
    }
}