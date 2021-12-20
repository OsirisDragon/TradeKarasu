using CrossModel;
using System;
using System.Collections.Generic;
using TradeFutNight.Common;

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
            TPPINTDFirstKindIdTwoChar = DropDownItems.TppintdFirstKindIdTwoChar();
        }

        public void Open()
        {
        }
    }

    public class UIModel_A9917
    { }
}