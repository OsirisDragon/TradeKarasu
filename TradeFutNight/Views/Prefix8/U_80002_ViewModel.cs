using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix8;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix8
{
    public class U_80002_ViewModel : ViewModelParent<UIModel_80002>
    {
        public IList<ItemInfo> Dpt
        {
            get { return GetProperty(() => Dpt); }
            set { SetProperty(() => Dpt, value); }
        }

        public U_80002_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_80002>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UPF, UIModel_80002>().ReverseMap();
            }));

            Dpt = DropDownItems.Dpt();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_80002());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_80002)item);
        }

        public async Task Query(string dptId)
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var d80002 = new D_80002<UIModel_80002>(das);
                    MainGridData = d80002.List(dptId).ToList().AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_80002 : UPF
    {
        public virtual string UPFCRD_CARD_NO { get; set; } // char(6)
    }
}