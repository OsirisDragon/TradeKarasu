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
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix8
{
    public class U_80014_ViewModel : ViewModelParent<UIModel_80014>
    {
        public IList<ItemInfo> UpfUserName
        {
            get { return GetProperty(() => UpfUserName); }
            set { SetProperty(() => UpfUserName, value); }
        }

        public IList<ItemInfo> OriUpfUserName
        {
            get { return GetProperty(() => OriUpfUserName); }
            set { SetProperty(() => OriUpfUserName, value); }
        }

        public IList<ItemInfo> CurUpfUserName
        {
            get { return GetProperty(() => CurUpfUserName); }
            set { SetProperty(() => CurUpfUserName, value); }
        }

        public IList<UIModel_80014_Add> PrintGridData
        {
            get { return GetProperty(() => PrintGridData); }
            set { SetProperty(() => PrintGridData, value); }
        }

        public U_80014_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_80014>();
            PrintGridData = new ObservableCollection<UIModel_80014_Add>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TXN, UIModel_80014>().ReverseMap();
            }));

            UpfUserName = DropDownItems.UpfUserName(false);
            OriUpfUserName = new ObservableCollection<ItemInfo>(DropDownItems.UpfUserName(false));
            CurUpfUserName = new ObservableCollection<ItemInfo>();
            await Query();
        }

        public void Insert()
        {
        }

        public void Delete(object item)
        {
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dTXN = new D_TXN(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_80014>>(dTXN.ListNotDefault()).AsTrackable();
                }
            });

            await task;
        }

        public void AddUser(ItemInfo item)
        {
            OriUpfUserName.Remove(item);
            OriUpfUserName = OriUpfUserName.OrderBy(c => c.Value).ToList();
            CurUpfUserName.Add(item);
            CurUpfUserName = CurUpfUserName.OrderBy(c => c.Value).ToList();
        }

        public void RemoveUser(ItemInfo item)
        {
            CurUpfUserName.Remove(item);
            CurUpfUserName = CurUpfUserName.OrderBy(c => c.Value).ToList();
            OriUpfUserName.Add(item);
            OriUpfUserName = OriUpfUserName.OrderBy(c => c.Value).ToList();
        }
    }

    public class UIModel_80014 : TXN
    {
        public virtual bool UTP_YN_CODE { get; set; }
    }

    public class UIModel_80014_UTP
    {
        public virtual string UTP_USER_ID { get; set; }
        public virtual string UTP_TXN_ID { get; set; }
        public virtual string TXN_NAME { get; set; }
        public virtual string UTP_YN_CODE { get; set; }
    }

    public class UIModel_80014_Add
    {
        public virtual string C_TYPE { get; set; }
        public virtual string UTP_TXN_ID { get; set; }
        public virtual string TXN_NAME { get; set; }
        public virtual string UTP_USER_ID { get; set; }
        public virtual DateTime W_TIME { get; set; }
    }
}