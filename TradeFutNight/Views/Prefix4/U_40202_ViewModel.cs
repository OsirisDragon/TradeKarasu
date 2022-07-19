using AutoMapper;
using ChangeTracking;
using CrossModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_40202_ViewModel : ViewModelParent<UIModel_40202>
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

        public IList<ItemInfo> PhaltTypeTMsgTypeInfos
        {
            get { return GetProperty(() => PhaltTypeTMsgTypeInfos); }
            set { SetProperty(() => PhaltTypeTMsgTypeInfos, value); }
        }

        public U_40202_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_40202>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PHALT, UIModel_40202>().ReverseMap();
            }));

            await Query();

            PhaltTypeTMsgTypeInfos = DropDownItems.PhaltTypeTMsgType();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_40202());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_40202)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dPHALT = new D_PHALT(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_40202>>(dPHALT.ListByDate(StartDate, EndDate)).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_40202 : PHALT
    {
    }
}