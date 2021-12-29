using AutoMapper;
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

namespace TradeFutNight.Views.Prefix2
{
    public class U_21002_ViewModel : ViewModelParent<UIModel_21002>
    {
        public IList<ItemInfo> Pdks
        {
            get { return GetProperty(() => Pdks); }
            set { SetProperty(() => Pdks, value); }
        }

        public IList<ItemInfo> ProdSettleDates
        {
            get { return GetProperty(() => ProdSettleDates); }
            set { SetProperty(() => ProdSettleDates, value); }
        }

        public IList<ItemInfo> ProdExpireCode
        {
            get { return GetProperty(() => ProdExpireCode); }
            set { SetProperty(() => ProdExpireCode, value); }
        }

        public U_21002_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_21002>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PROD, UIModel_21002>().ReverseMap();
            }));

            using (var das = Factory.CreateDalSession())
            {
                var dPdk = new D_PDK(das);
                Pdks = dPdk.ListAll().Select(p => new ItemInfo { Text = p.PDK_KIND_ID, Value = p.PDK_KIND_ID }).ToList();

                var dProd = new D_PROD(das);
                var prods = dProd.ListDistinctMonth().Select(p => new ItemInfo { Text = p.PROD_SETTLE_DATE, Value = p.PROD_SETTLE_DATE }).ToList();
                prods.Insert(0, new ItemInfo { Text = "", Value = "" });
                ProdSettleDates = prods;
            }

            ProdExpireCode = DropDownItems.ProdExpireCode();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_21002());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_21002)item);
        }

        public async Task Query(string kindId, string month)
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dPROD = new D_PROD(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_21002>>(dPROD.ListByKindIDAndMonth(kindId, month)).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_21002 : PROD
    {
    }
}