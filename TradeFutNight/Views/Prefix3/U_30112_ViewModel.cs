using AutoMapper;
using ChangeTracking;
using CrossModel;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30112_ViewModel : ViewModelBase
    {
        public Mapper MapperInstance { get; set; }

        public IList<UIModel_30112> MainGridData
        {
            get { return GetProperty(() => MainGridData); }
            set { SetProperty(() => MainGridData, value); }
        }

        public IList<ItemInfo> PdkParamKeysCanQuote
        {
            get { return GetProperty(() => PdkParamKeysCanQuote); }
            set { SetProperty(() => PdkParamKeysCanQuote, value); }
        }

        public IList<ItemInfo> SltPriceFlucItemInfos
        {
            get { return GetProperty(() => SltPriceFlucItemInfos); }
            set { SetProperty(() => SltPriceFlucItemInfos, value); }
        }

        public U_30112_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30112>();
            PdkParamKeysCanQuote = new ObservableCollection<ItemInfo>();
            SltPriceFlucItemInfos = new ObservableCollection<ItemInfo>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SLT, UIModel_30112>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30112>().ToList().AsTrackable();

            using (var das = Factory.CreateDalSession())
            {
                var dSlt = new D_SLT(das);
                MainGridData = MapperInstance.Map<IList<UIModel_30112>>(dSlt.ListAll()).AsTrackable();

                var dPdk = new D_PDK(das);
                PdkParamKeysCanQuote = dPdk.ListDistinctParamKeyCanQuote().Select(c => new ItemInfo() { Text = c.PDK_PARAM_KEY, Value = c.PDK_PARAM_KEY }).ToList();
            }

            var priceFlucItemInfos = new List<ItemInfo>();
            priceFlucItemInfos.Add(new ItemInfo() { Text = "百分比", Value = 'P' });
            priceFlucItemInfos.Add(new ItemInfo() { Text = "固定點數", Value = 'F' });
            SltPriceFlucItemInfos = priceFlucItemInfos;
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30112());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30112)item);
        }
    }

    public class UIModel_30112 : SLT
    {
    }
}