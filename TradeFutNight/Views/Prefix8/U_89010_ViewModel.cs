using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Gates.Specific.Prefix8;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix8
{
    public class U_89010_ViewModel : ViewModelParent<UIModel_89010>
    {
        public IList<ItemInfo> OperationType
        {
            get { return GetProperty(() => OperationType); }
            set { SetProperty(() => OperationType, value); }
        }

        public IList<ItemInfo> YtxnTypeInfos
        {
            get { return GetProperty(() => YtxnTypeInfos); }
            set { SetProperty(() => YtxnTypeInfos, value); }
        }

        public string OperationTypeVal
        {
            get { return GetProperty(() => OperationTypeVal); }
            set { SetProperty(() => OperationTypeVal, value); }
        }

        public IList<ItemInfo> SearchType
        {
            get { return GetProperty(() => SearchType); }
            set { SetProperty(() => SearchType, value); }
        }

        public string SearchTypeVal
        {
            get { return GetProperty(() => SearchTypeVal); }
            set { SetProperty(() => SearchTypeVal, value); }
        }

        public IList<ItemInfo> SearchSubType
        {
            get { return GetProperty(() => SearchSubType); }
            set { SetProperty(() => SearchSubType, value); }
        }

        public object SearchSubTypeVal
        {
            get { return GetProperty(() => SearchSubTypeVal); }
            set { SetProperty(() => SearchSubTypeVal, value); }
        }

        public string SearchSubTypeName
        {
            get { return GetProperty(() => SearchSubTypeName); }
            set { SetProperty(() => SearchSubTypeName, value); }
        }

        public IList<ItemInfo> Dpt
        {
            get { return GetProperty(() => Dpt); }
            set { SetProperty(() => Dpt, value); }
        }

        public IList<UIModel_89010_YtxnId> YtxnIdGridData
        {
            get { return GetProperty(() => YtxnIdGridData); }
            set { SetProperty(() => YtxnIdGridData, value); }
        }

        public IList<UIModel_89010_YtxnYtxn> YtxnYtxnGridData
        {
            get { return GetProperty(() => YtxnYtxnGridData); }
            set { SetProperty(() => YtxnYtxnGridData, value); }
        }

        public IList<UIModel_89010_YtxnUser> YtxnUserGridData
        {
            get { return GetProperty(() => YtxnUserGridData); }
            set { SetProperty(() => YtxnUserGridData, value); }
        }

        public IList<UIModel_89010_YtxnDpt> YtxnDptGridData
        {
            get { return GetProperty(() => YtxnDptGridData); }
            set { SetProperty(() => YtxnDptGridData, value); }
        }

        public U_89010_ViewModel()
        {
        }

        public void Open()
        {
            YtxnTypeInfos = DropDownItems.YtxnType();
            OperationType = DropDownItems.OperationType();
            OperationType.RemoveAt(1);
            OperationTypeVal = "Txn";
            SearchType = DropDownItems.SearchType();
            SearchTypeVal = "Txn";

            Dpt = DropDownItems.Dpt();
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
                    if (OperationTypeVal == "Txn")
                    {
                        switch (SearchTypeVal)
                        {
                            case "Txn":
                                var d89010Ytxn = new D_89010<UIModel_89010_YtxnYtxn>(das);
                                YtxnYtxnGridData = d89010Ytxn.ListByTxnId(SearchSubTypeVal.ToString()).ToList().AsTrackable();
                                break;

                            case "User":
                                var d89010User = new D_89010<UIModel_89010_YtxnUser>(das);
                                YtxnUserGridData = d89010User.ListByUserId(SearchSubTypeVal.ToString()).ToList().AsTrackable();
                                break;

                            case "Dpt":
                                var d89010Dpt = new D_89010<UIModel_89010_YtxnDpt>(das);
                                YtxnDptGridData = d89010Dpt.ListByDptId(SearchSubTypeVal.ToString()).ToList().AsTrackable();
                                break;

                            case "Id":
                                var dYtxn = new D_YTXN(das);
                                MapperInstance = new Mapper(new MapperConfiguration(cfg =>
                                {
                                    cfg.CreateMap<YTXN, UIModel_89010_YtxnId>().ReverseMap();
                                }));
                                YtxnIdGridData = MapperInstance.Map<IList<UIModel_89010_YtxnId>>(dYtxn.ListAll());
                                break;
                        }
                    }
                }
            });

            await task;
        }

        public void GetSearchType()
        {
            SearchType = DropDownItems.SearchType(OperationTypeVal);
        }

        public void GetSearchSubType()
        {
            SearchSubType = null;
            SearchSubTypeName = "";

            if (OperationTypeVal == "Txn")
            {
                switch (SearchTypeVal)
                {
                    case "Txn":
                        SearchSubType = DropDownItems.Ytxn();
                        SearchSubTypeName = "作業代號：";
                        break;

                    case "User":
                        SearchSubType = DropDownItems.UpfUserName(false);
                        SearchSubTypeName = "使用者代號：";
                        break;

                    case "Dpt":
                        SearchSubType = DropDownItems.Dpt();
                        SearchSubTypeName = "部門：";
                        break;
                }
            }
            SearchSubTypeVal = null;
        }
    }

    public class UIModel_89010
    {
    }

    public class UIModel_89010_YtxnId : YTXN
    {
    }

    public class UIModel_89010_YtxnYtxn
    {
        public virtual string YTXN_NAME { get; set; }
        public virtual string YUTP_USER_ID { get; set; }
        public virtual string YUTP_YN_CODE { get; set; }
        public virtual string YUTP_YTXN_ID { get; set; }
        public virtual string UPF_USER_NAME { get; set; }
    }

    public class UIModel_89010_YtxnUser
    {
        public virtual string YUTP_USER_ID { get; set; }
        public virtual string YUTP_YTXN_ID { get; set; }

        public virtual string YTXN_NAME { get; set; }
        public virtual string UPF_USER_NAME { get; set; }

        public virtual char UPF_DEPT_ID { get; set; }
    }

    public class UIModel_89010_YtxnDpt
    {
        public virtual string YUTP_USER_ID { get; set; }
        public virtual string YUTP_YTXN_ID { get; set; }

        public virtual string YTXN_NAME { get; set; }
        public virtual string UPF_USER_NAME { get; set; }

        public virtual char UPF_DEPT_ID { get; set; }
    }
}