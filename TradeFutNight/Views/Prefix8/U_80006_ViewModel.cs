using AutoMapper;
using CrossModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views.Prefix8
{
    public class U_80006_ViewModel : ViewModelParent<UIModel_80006>
    {
        public DateTime StartDate
        {
            get { return GetProperty(() => StartDate); }
            set { SetProperty(() => StartDate, value); }
        }

        public DateTime EndDate
        {
            get { return GetProperty(() => EndDate); }
            set { SetProperty(() => EndDate, value); }
        }

        public IList<ItemInfo> UpfUserName
        {
            get { return GetProperty(() => UpfUserName); }
            set { SetProperty(() => UpfUserName, value); }
        }

        public IList<ItemInfo> LogfKind
        {
            get { return GetProperty(() => LogfKind); }
            set { SetProperty(() => LogfKind, value); }
        }

        public IList<ItemInfo> Dpt
        {
            get { return GetProperty(() => Dpt); }
            set { SetProperty(() => Dpt, value); }
        }

        public U_80006_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_80006>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LOGF, UIModel_80006>().ReverseMap();
            }));

            UpfUserName = DropDownItems.UpfUserName();
            LogfKind = DropDownItems.LogfKind();
            Dpt = DropDownItems.Dpt();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_80006());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_80006)item);
        }

        public async Task Query(string userId)
        {
            var task = Task.Run(() =>
            {
                var uiModels = new List<UIModel_80006>();
                using (var das = Factory.CreateDalSession())
                {
                    var dLOGF = new D_LOGF(das);
                    var listLogf = dLOGF.ListByDateAndKey("80004", "80014", StartDate, EndDate, "%" + userId + "%");
                    foreach (var item in listLogf)
                    {
                        string[] data = item.LOGF_KEY_DATA.AsString().Split(',');
                        if (data.Length >= 8)
                        {
                            var model = new UIModel_80006();
                            model.OP_USER_ID = data[0];
                            model.OP_USER_NAME = data[1];
                            model.OP_W_TIME = data[2].AsDateTime();
                            model.DEPT_ID = data[3];
                            model.CHANGE_USER = data[4];
                            model.TXN_ID = data[5];
                            model.TXN_NAME = data[6];
                            model.KIND = data[7];
                            uiModels.Add(model);
                        }
                    }
                    MainGridData = uiModels;
                }
            });

            await task;
        }
    }

    public class UIModel_80006
    {
        public virtual string OP_USER_ID { get; set; }

        public virtual string OP_USER_NAME { get; set; }

        public virtual DateTime OP_W_TIME { get; set; }

        public virtual string DEPT_ID { get; set; }

        public virtual string CHANGE_USER { get; set; }

        public virtual string TXN_ID { get; set; }

        public virtual string TXN_NAME { get; set; }

        public virtual string KIND { get; set; }
    }
}