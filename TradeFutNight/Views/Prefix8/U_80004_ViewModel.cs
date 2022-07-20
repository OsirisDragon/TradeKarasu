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
using TradeFutNightData.Gates.Specific.Prefix8;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix8
{
    public class U_80004_ViewModel : ViewModelParent<UIModel_80004>
    {
        public IList<ItemInfo> UpfUserName
        {
            get { return GetProperty(() => UpfUserName); }
            set { SetProperty(() => UpfUserName, value); }
        }

        public IList<UIModel_80004_Change> PrintGridData
        {
            get { return GetProperty(() => PrintGridData); }
            set { SetProperty(() => PrintGridData, value); }
        }

        public string UserInfo
        {
            get { return GetProperty(() => UserInfo); }
            set { SetProperty(() => UserInfo, value); }
        }

        public U_80004_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_80004>();
            PrintGridData = new ObservableCollection<UIModel_80004_Change>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UTP, UIModel_80004>().ReverseMap();
            }));

            UpfUserName = DropDownItems.UpfUserName(false);
        }

        public void Insert()
        {
        }

        public void Delete(object item)
        {
        }

        public async Task Query(string userId)
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var d80004 = new D_80004<UIModel_80004>(das);
                    MainGridData = d80004.ListByUserId(userId).ToList().AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_80004 : UTP
    {
        public virtual string TXN_NAME { get; set; }
    }

    public class UIModel_80004_Change
    {
        public virtual string C_TYPE { get; set; }
        public virtual string UTP_TXN_ID { get; set; }
        public virtual string TXN_NAME { get; set; }
        public virtual string UTP_USER_ID { get; set; }
        public virtual DateTime W_TIME { get; set; }
    }
}