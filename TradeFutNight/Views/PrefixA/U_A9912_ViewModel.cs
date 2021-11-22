using AutoMapper;
using ChangeTracking;
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
    public class U_A9912_ViewModel : ViewModelParent<UIModel_A9912>
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

        public IList<ItemInfo> PhaltTypeInfos
        {
            get { return GetProperty(() => PhaltTypeInfos); }
            set { SetProperty(() => PhaltTypeInfos, value); }
        }

        public IList<ItemInfo> PhaltMsgTypeInfos
        {
            get { return GetProperty(() => PhaltMsgTypeInfos); }
            set { SetProperty(() => PhaltMsgTypeInfos, value); }
        }

        public U_A9912_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_A9912>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PHALT, UIModel_A9912>().ReverseMap();
            }));

            using (var das = Factory.CreateDalSession())
            {
                var dA9912 = new D_A9912<UIModel_A9912>(das);
                MainGridData = dA9912.ListByDate(StartDate, EndDate, "").ToList();
            }
            PhaltTypeInfos = DropDownItems.PhaltType();
            PhaltMsgTypeInfos = DropDownItems.PhaltMsgType();
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
                    var dA9912 = new D_A9912<UIModel_A9912>(das);
                    MainGridData = dA9912.ListByDate(StartDate, EndDate, prodId).ToList();
                }
            });

            await task;
        }
    }

    public class UIModel_A9912 : PHALT
    {
        public virtual string PDK_SUBTYPE { get; set; } // char(6)
    }
}