using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNight.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30111_ViewModel : ViewModelParent<UIModel_30111>
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

        public U_30111_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30111>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SLT, UIModel_30111>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30111>().ToList().AsTrackable();

            PdkParamKeysCanQuote = DropDownItems.PdkParamKeysCanQuote();
            SltPriceFlucItemInfos = DropDownItems.PriceFlucItem();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30111());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30111)item);
        }
    }

    public class UIModel_30111 : SLT
    {
    }
}