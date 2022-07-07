using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNight.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix8
{
    public class U_80001_ViewModel : ViewModelParent<UIModel_80001>
    {
        public IList<ItemInfo> Dpt
        {
            get { return GetProperty(() => Dpt); }
            set { SetProperty(() => Dpt, value); }
        }

        public U_80001_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_80001>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UPF, UIModel_80001>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_80001>().ToList().AsTrackable();
            Dpt = DropDownItems.Dpt();
        }

        public void Insert()
        {
            MainGridData.Add(new UIModel_80001()
            {
                UPF_PASSWORD = "0000000000"
            });
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_80001)item);
        }
    }

    public class UIModel_80001 : UPF
    {
        public virtual string UPFCRD_CARD_NO { get; set; } // char(6)
    }
}