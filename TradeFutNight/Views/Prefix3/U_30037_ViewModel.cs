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
    public class U_30037_ViewModel : ViewModelParent<UIModel_30037>
    {
        public string CmNo
        {
            get { return GetProperty(() => CmNo); }
            set { SetProperty(() => CmNo, value); }
        }

        public IList<ItemInfo> PcmConstrictType
        {
            get { return GetProperty(() => PcmConstrictType); }
            set { SetProperty(() => PcmConstrictType, value); }
        }

        public U_30037_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30037>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PCM, UIModel_30037>().ReverseMap();
            }));

            PcmConstrictType = DropDownItems.PcmConstrict();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30037());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30037)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dPCM = new D_PCM(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_30037>>(dPCM.ListByCmNo(CmNo)).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_30037 : PCM
    {
    }
}