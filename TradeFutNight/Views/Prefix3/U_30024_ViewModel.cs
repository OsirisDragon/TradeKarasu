using AutoMapper;
using ChangeTracking;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30024_ViewModel : ViewModelBase
    {
        public Mapper MapperInstance { get; set; }

        public IList<UIModel_30024> MainGridData
        {
            get { return GetProperty(() => MainGridData); }
            set { SetProperty(() => MainGridData, value); }
        }

        public U_30024_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30024>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPINTD, UIModel_30024>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30024>().ToList().AsTrackable();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30024()
            {
                TPPINTD_FIRST_KIND_ID = "",
                TPPINTD_SECOND_KIND_ID = "",
                TPPINTD_KIND_ID_FUT = ""
            });
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30024)item);
        }
    }

    public class UIModel_30024 : TPPINTD
    {
    }
}