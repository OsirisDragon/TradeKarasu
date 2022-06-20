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

namespace TradeFutNight.Views.Prefix3
{
    public class U_30043_ViewModel : ViewModelParent<UIModel_30043>
    {
        public IList<ItemInfo> TPPSTKindId
        {
            get { return GetProperty(() => TPPSTKindId); }
            set { SetProperty(() => TPPSTKindId, value); }
        }

        public U_30043_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30043>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPST, UIModel_30043>().ReverseMap();
            }));

            await Query("%");

            TPPSTKindId = DropDownItems.TppstKindId();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30043());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30043)item);
        }

        public async Task Query(string kindId)
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dTPPST = new D_TPPST(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_30043>>(dTPPST.ListByKindId(kindId)).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_30043 : TPPST
    {
    }
}