using AutoMapper;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix5;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix5
{
    public class U_50333_ViewModel : ViewModelParent<UIModel_50333>
    {
        public IList<ItemInfo> MarketClose
        {
            get { return GetProperty(() => MarketClose); }
            set { SetProperty(() => MarketClose, value); }
        }

        public U_50333_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50333>();
        }

        public void Open()
        {
            MarketClose = DropDownItems.MarketClose();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_50333());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_50333)item);
        }

        public async Task Query()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MTF, UIModel_50333>().ReverseMap();
            }));

            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var d50333 = new D_50333<UIModel_50333>(das);
                    MainGridData = d50333.List().ToList();
                }
            });

            await task;
        }
    }

    public class UIModel_50333 : MTF
    {
        public virtual string PROD_ID_OUT { get; set; }
        public virtual string PROD_SETTLE_DATE { get; set; }
        public virtual string PDK_MARKET_CLOSE { get; set; }
        public virtual byte PGRP_OSW_GRP { get; set; }
        public virtual string PDK_SUBTYPE { get; set; }
    }
}