﻿using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30028_ViewModel : ViewModelParent<UIModel_30028>
    {
        public IList<ItemInfo> IdxGroupInfos
        {
            get { return GetProperty(() => IdxGroupInfos); }
            set { SetProperty(() => IdxGroupInfos, value); }
        }

        public IList<ItemInfo> TypeInfos
        {
            get { return GetProperty(() => TypeInfos); }
            set { SetProperty(() => TypeInfos, value); }
        }

        public U_30028_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30028>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPVOL, UIModel_30028>().ReverseMap();
            }));

            await Query();

            IdxGroupInfos = DropDownItems.TppIndexGrp();
            TypeInfos = DropDownItems.TppType();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30028());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30028)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dTPPVOL = new D_TPPVOL(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_30028>>(dTPPVOL.ListAll()).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_30028 : TPPVOL
    {
    }
}