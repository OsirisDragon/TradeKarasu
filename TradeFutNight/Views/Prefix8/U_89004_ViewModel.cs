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
    public class U_89004_ViewModel : ViewModelParent<UIModel_89004>
    {
        public IList<ItemInfo> UpfUserName
        {
            get { return GetProperty(() => UpfUserName); }
            set { SetProperty(() => UpfUserName, value); }
        }

        public IList<UIModel_89004_Change> PrintGridData
        {
            get { return GetProperty(() => PrintGridData); }
            set { SetProperty(() => PrintGridData, value); }
        }

        public string UserInfo
        {
            get { return GetProperty(() => UserInfo); }
            set { SetProperty(() => UserInfo, value); }
        }

        public U_89004_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_89004>();
            PrintGridData = new ObservableCollection<UIModel_89004_Change>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<YUTP, UIModel_89004>().ReverseMap();
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
                    var d89004 = new D_89004<UIModel_89004>(das);
                    MainGridData = d89004.ListByUserId(userId).ToList().AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_89004 : YUTP
    {
        public virtual string YTXN_NAME { get; set; }
    }

    public class UIModel_89004_Change
    {
        public virtual string C_TYPE { get; set; }
        public virtual string YUTP_YTXN_ID { get; set; }
        public virtual string YTXN_NAME { get; set; }
        public virtual string YUTP_USER_ID { get; set; }
        public virtual DateTime W_TIME { get; set; }
    }
}