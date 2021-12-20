using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixS
{
    public class U_S1080_ViewModel : ViewModelParent<UIModel_S1080>
    {
        public IList<ItemInfo> ZTYPEPProdType
        {
            get { return GetProperty(() => ZTYPEPProdType); }
            set { SetProperty(() => ZTYPEPProdType, value); }
        }

        public IList<ItemInfo> ZTYPEPPriceQuote
        {
            get { return GetProperty(() => ZTYPEPPriceQuote); }
            set { SetProperty(() => ZTYPEPPriceQuote, value); }
        }

        public IList<ItemInfo> ZTYPEPSettlement
        {
            get { return GetProperty(() => ZTYPEPSettlement); }
            set { SetProperty(() => ZTYPEPSettlement, value); }
        }

        public U_S1080_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_S1080>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ZTYPEP, UIModel_S1080>().ReverseMap();
            }));

            await Query();
            ZTYPEPProdType = DropDownItems.ZtypepProdType();
            ZTYPEPPriceQuote = DropDownItems.ZtypepPriceQuote();
            ZTYPEPSettlement = DropDownItems.ZtypepSettlement();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_S1080());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_S1080)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dZTYPEP = new D_ZTYPEP(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_S1080>>(dZTYPEP.ListAll()).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_S1080 : ZTYPEP
    {
    }
}