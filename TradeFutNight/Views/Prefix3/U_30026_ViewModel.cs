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

namespace TradeFutNight.Views.Prefix3
{
    public class U_30026_ViewModel : ViewModelParent<UIModel_30026>
    {
        public IList<ItemInfo> TPPINTDFirstKindId
        {
            get { return GetProperty(() => TPPINTDFirstKindId); }
            set { SetProperty(() => TPPINTDFirstKindId, value); }
        }

        public IList<ItemInfo> TPPINTDSecondKindId
        {
            get { return GetProperty(() => TPPINTDSecondKindId); }
            set { SetProperty(() => TPPINTDSecondKindId, value); }
        }

        public U_30026_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30026>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPINTD, UIModel_30026>().ReverseMap();
            }));

            await Query("%", "%");

            TPPINTDFirstKindId = DropDownItems.TppintdFirstKindId();
            TPPINTDSecondKindId = DropDownItems.TppintdSecondKindId();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30026());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30026)item);
        }

        public async Task Query(string firstKindId, string secondKindId)
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dTPPINTD = new D_TPPINTD(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_30026>>(dTPPINTD.ListByKindID(firstKindId, secondKindId)).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_30026 : TPPINTD
    {
    }
}