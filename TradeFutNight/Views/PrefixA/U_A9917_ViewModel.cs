using AutoMapper;
using ChangeTracking;
using CrossModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Gates.Specific.PrefixA;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixA
{
    public class U_A9917_ViewModel : ViewModelParent<UIModel_A9917>
    {
        public DateTime StartDateTime
        {
            get { return GetProperty(() => StartDateTime); }
            set { SetProperty(() => StartDateTime, value); }
        }

        public DateTime EndDateTime
        {
            get { return GetProperty(() => EndDateTime); }
            set { SetProperty(() => EndDateTime, value); }
        }

        public IList<ItemInfo> TPPINTDFirstKindIdTwoChar
        {
            get { return GetProperty(() => TPPINTDFirstKindIdTwoChar); }
            set { SetProperty(() => TPPINTDFirstKindIdTwoChar, value); }
        }

        public U_A9917_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_A9917>();
            TPPINTDFirstKindIdTwoChar = DropDownItems.TppintdFirstKindIdTwoChar();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPADJ, UIModel_A9917>().ReverseMap();
            }));

            var result = new List<UIModel_A9917>();

            using (var das = Factory.CreateDalSession())
            {
                var tppintd = new D_TPPINTD(das).ListSingleKind();
                var tppst = new D_TPPST(das).ListSingleMonth();

                IList<TPPST> tppstFilter = new List<TPPST>();
                IList<TPPINTD> tppintdFilter = new List<TPPINTD>();

                // 填入TPPINTD或TPPST相關資料，照著月份序號順序填入
                foreach (var item in result)
                {
                }
            }

            MainGridData = result.AsTrackable();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_A9917());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_A9917)item);
        }
    }

    public class UIModel_A9917 : TPPINTD
    {
    }
}