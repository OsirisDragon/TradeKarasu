using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30112_ViewModel : ViewModelParent<UIModel_30112>
    {
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
            }
            PdkParamKeysCanQuote = DropDownItems.PdkParamKeysCanQuote();
            SltPriceFlucItemInfos = DropDownItems.PriceFlucItem();
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