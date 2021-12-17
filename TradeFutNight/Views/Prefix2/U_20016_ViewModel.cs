using AutoMapper;
using ChangeTracking;
using CrossModel;
using Shield.File;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix2
{
    public class U_20016_ViewModel : ViewModelParent<UIModel_20016>
    {
        public int GenerateYear
        {
            get { return GetProperty(() => GenerateYear); }
            set { SetProperty(() => GenerateYear, value); }
        }

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

        public IList<ItemInfo> DayOfWeek
        {
            get { return GetProperty(() => DayOfWeek); }
            set { SetProperty(() => DayOfWeek, value); }
        }

        public IList<ItemInfo> CboeOpenCode
        {
            get { return GetProperty(() => CboeOpenCode); }
            set { SetProperty(() => CboeOpenCode, value); }
        }

        public U_20016_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_20016>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MOCFEX, UIModel_20016>().ReverseMap();
            }));

            await Query();

            DayOfWeek = DropDownItems.DayOfWeek();

            var cboeOpenCode = new List<ItemInfo>();
            cboeOpenCode.Add(new ItemInfo() { Text = "交易日", Value = 'Y' });
            cboeOpenCode.Add(new ItemInfo() { Text = "休市日", Value = 'N' });
            CboeOpenCode = cboeOpenCode;
        }

        public void Add(UIModel_20016 uiModel)
        {
            MainGridData.Add(uiModel);
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_20016)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dMOCFEX = new D_MOCFEX(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_20016>>(dMOCFEX.ListByDate(StartDate, EndDate)).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_20016 : MOCFEX
    {
    }
}