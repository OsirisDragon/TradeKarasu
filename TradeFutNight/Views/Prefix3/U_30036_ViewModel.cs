using AutoMapper;
using ChangeTracking;
using CrossModel;
using DevExpress.Mvvm.DataAnnotations;
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
    public class U_30036_ViewModel : ViewModelParent<UIModel_30036>
    {
        public string CmNo
        {
            get { return GetProperty(() => CmNo); }
            set { SetProperty(() => CmNo, value); }
        }

        public IList<ItemInfo> CcmSubtype
        {
            get { return GetProperty(() => CcmSubtype); }
            set { SetProperty(() => CcmSubtype, value); }
        }

        public U_30036_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30036>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CCM, UIModel_30036>().ReverseMap();
            }));

            await Query();

            CcmSubtype = DropDownItems.SubtypeCode();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30036());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30036)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dCCM = new D_CCM(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_30036>>(dCCM.ListByCmNo(CmNo)).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_30036 : CCM
    {
    }
}