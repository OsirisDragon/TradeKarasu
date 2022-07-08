using AutoMapper;
using CrossModel;
using DevExpress.DataAccess.ObjectBinding;
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
    public class U_80003_ViewModel : ViewModelParent<UIModel_80003>
    {
        public UIModel_80003 MainFormData
        {
            get { return GetProperty(() => MainFormData); }
            set { SetProperty(() => MainFormData, value); }
        }

        public UIModel_80003 MainFormDataOriginal
        {
            get; set;
        }

        public IList<ItemInfo> UpfUserIdName
        {
            get { return GetProperty(() => UpfUserIdName); }
            set { SetProperty(() => UpfUserIdName, value); }
        }

        public IList<ItemInfo> Dpt
        {
            get { return GetProperty(() => Dpt); }
            set { SetProperty(() => Dpt, value); }
        }

        public U_80003_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_80003>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UPF, UIModel_80003>().ReverseMap();
            }));

            // 下拉選單移除"全部"這個選項
            var tempUpfUser = DropDownItems.UpfUserName();
            tempUpfUser.Remove(tempUpfUser.SingleOrDefault(c => c.Value.ToString() == "%"));
            UpfUserIdName = tempUpfUser;

            var tempDpt = DropDownItems.Dpt();
            tempDpt.Remove(tempDpt.SingleOrDefault(c => c.Value.ToString() == "%"));
            Dpt = tempDpt;
        }

        public async Task Query(string userId)
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var d80003 = new D_80003<UIModel_80003>(das);
                    MainFormDataOriginal = d80003.GetUserAndCard(userId);
                    MainFormData = MainFormDataOriginal.ShallowCopy();
                }
            });

            await task;
        }
    }

    [HighlightedClass]
    public class UIModel_80003 : UPF
    {
        public UIModel_80003 ShallowCopy()
        {
            return (UIModel_80003)this.MemberwiseClone();
        }

        public virtual string UPFCRD_CARD_NO { get; set; }
    }
}