using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30113_ViewModel : ViewModelParent<UIModel_30113>
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

        public U_30113_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30113>();
            PdkParamKeysCanQuote = new ObservableCollection<ItemInfo>();
            SltPriceFlucItemInfos = new ObservableCollection<ItemInfo>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SLT, UIModel_30113>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30113>().ToList().AsTrackable();

            using (var das = Factory.CreateDalSession())
            {
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
            MainGridData.Insert(0, new UIModel_30113());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30113)item);
        }

        public async Task Query(string kindId)
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dSlt = new D_SLT(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_30113>>(dSlt.ListByKindID(kindId)).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_30113 : SLT
    {
    }
}