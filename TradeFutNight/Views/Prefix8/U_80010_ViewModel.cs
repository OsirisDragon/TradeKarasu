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
    public class U_80010_ViewModel : ViewModelParent<UIModel_80010>
    {
        public IList<ItemInfo> OperationType
        {
            get { return GetProperty(() => OperationType); }
            set { SetProperty(() => OperationType, value); }
        }

        public IList<ItemInfo> TxnTypeInfos
        {
            get { return GetProperty(() => TxnTypeInfos); }
            set { SetProperty(() => TxnTypeInfos, value); }
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

        public IList<ItemInfo> OpfKind
        {
            get { return GetProperty(() => OpfKind); }
            set { SetProperty(() => OpfKind, value); }
        }

        public IList<ItemInfo> Dpt
        {
            get { return GetProperty(() => Dpt); }
            set { SetProperty(() => Dpt, value); }
        }

        public IList<UIModel_80010_TxnId> TxnIdGridData
        {
            get { return GetProperty(() => TxnIdGridData); }
            set { SetProperty(() => TxnIdGridData, value); }
        }

        public IList<UIModel_80010_TxnTxn> TxnTxnGridData
        {
            get { return GetProperty(() => TxnTxnGridData); }
            set { SetProperty(() => TxnTxnGridData, value); }
        }

        public IList<UIModel_80010_TxnUser> TxnUserGridData
        {
            get { return GetProperty(() => TxnUserGridData); }
            set { SetProperty(() => TxnUserGridData, value); }
        }

        public IList<UIModel_80010_TxnDpt> TxnDptGridData
        {
            get { return GetProperty(() => TxnDptGridData); }
            set { SetProperty(() => TxnDptGridData, value); }
        }

        public IList<UIModel_80010_OpfId> OpfIdGridData
        {
            get { return GetProperty(() => OpfIdGridData); }
            set { SetProperty(() => OpfIdGridData, value); }
        }

        public IList<UIModel_80010_OpfTxn> OpfTxnGridData
        {
            get { return GetProperty(() => OpfTxnGridData); }
            set { SetProperty(() => OpfTxnGridData, value); }
        }

        public U_80010_ViewModel()
        {
        }

        public void Open()
        {
            TxnTypeInfos = DropDownItems.TxnType();
            OperationType = DropDownItems.OperationType();
            OperationTypeVal = "Txn";
            SearchType = DropDownItems.SearchType();
            SearchTypeVal = "Txn";

            OpfKind = DropDownItems.OpfKind();
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
                                var d80010Txn = new D_80010<UIModel_80010_TxnTxn>(das);
                                TxnTxnGridData = d80010Txn.ListByTxnId(SearchSubTypeVal.ToString()).ToList().AsTrackable();
                                break;

                            case "User":
                                var d80010User = new D_80010<UIModel_80010_TxnUser>(das);
                                TxnUserGridData = d80010User.ListByUserId(SearchSubTypeVal.ToString()).ToList().AsTrackable();
                                break;

                            case "Dpt":
                                var d80010Dpt = new D_80010<UIModel_80010_TxnDpt>(das);
                                TxnDptGridData = d80010Dpt.ListByDptId(SearchSubTypeVal.ToString()).ToList().AsTrackable();
                                break;

                            case "Id":
                                var dTxn = new D_TXN(das);
                                MapperInstance = new Mapper(new MapperConfiguration(cfg =>
                                {
                                    cfg.CreateMap<TXN, UIModel_80010_TxnId>().ReverseMap();
                                }));
                                TxnIdGridData = MapperInstance.Map<IList<UIModel_80010_TxnId>>(dTxn.ListAll());
                                break;
                        }
                    }
                    else if (OperationTypeVal == "Opf")
                    {
                        switch (SearchTypeVal)
                        {
                            case "Txn":
                                var dUpf = new D_UPF(das);
                                MapperInstance = new Mapper(new MapperConfiguration(cfg =>
                                {
                                    cfg.CreateMap<UPF, UIModel_80010_OpfTxn>().ReverseMap();
                                }));
                                OpfTxnGridData = MapperInstance.Map<IList<UIModel_80010_OpfTxn>>(dUpf.ListByDpt('I')).AsTrackable();
                                break;

                            case "Id":
                                var dOpf = new D_OPF(das);
                                MapperInstance = new Mapper(new MapperConfiguration(cfg =>
                                {
                                    cfg.CreateMap<OPF, UIModel_80010_OpfId>().ReverseMap();
                                }));
                                OpfIdGridData = MapperInstance.Map<IList<UIModel_80010_OpfId>>(dOpf.ListAll()).AsTrackable();

                                var kindList = OpfIdGridData.Select(c => c.OPF_KIND).Distinct();
                                foreach (var k in kindList)
                                {
                                    if (OpfKind.Count(c => c.Value.ToString() == k.ToString()) == 0)
                                    {
                                        OpfKind.Add(new ItemInfo() { Text = k.ToString(), Value = (char)k });
                                    }
                                }

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
                        SearchSubType = DropDownItems.Txn();
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

    public class UIModel_80010
    {
    }

    public class UIModel_80010_TxnId : TXN
    {
    }

    public class UIModel_80010_TxnTxn
    {
        public virtual string TXN_NAME { get; set; }
        public virtual string UTP_USER_ID { get; set; }
        public virtual string UTP_YN_CODE { get; set; }
        public virtual string UTP_TXN_ID { get; set; }
        public virtual string UPF_USER_NAME { get; set; }
    }

    public class UIModel_80010_TxnUser
    {
        public virtual string UTP_USER_ID { get; set; }
        public virtual string UTP_TXN_ID { get; set; }

        public virtual string TXN_NAME { get; set; }
        public virtual string UPF_USER_NAME { get; set; }

        public virtual char UPF_DEPT_ID { get; set; }
    }

    public class UIModel_80010_TxnDpt
    {
        public virtual string UTP_USER_ID { get; set; }
        public virtual string UTP_TXN_ID { get; set; }

        public virtual string TXN_NAME { get; set; }
        public virtual string UPF_USER_NAME { get; set; }

        public virtual char UPF_DEPT_ID { get; set; }
    }

    public class UIModel_80010_OpfId : OPF
    {
    }

    public class UIModel_80010_OpfTxn : UPF
    {
    }
}