using AutoMapper;
using ChangeTracking;
using CrossModel;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix2
{
    public class U_21032_ViewModel : ViewModelParent<UIModel_21032>
    {
        public string BrkNo
        {
            get { return GetProperty(() => BrkNo); }
            set { SetProperty(() => BrkNo, value); }
        }

        public IList<ItemInfo> BrkType
        {
            get { return GetProperty(() => BrkType); }
            set { SetProperty(() => BrkType, value); }
        }

        public IList<ItemInfo> BrkOpenCode
        {
            get { return GetProperty(() => BrkOpenCode); }
            set { SetProperty(() => BrkOpenCode, value); }
        }

        public IList<ItemInfo> BrkOthOpenCode
        {
            get { return GetProperty(() => BrkOthOpenCode); }
            set { SetProperty(() => BrkOthOpenCode, value); }
        }

        public U_21032_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_21032>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BRK, UIModel_21032>().ReverseMap();
            }));
            using (var das = Factory.CreateDalSession())
            {
                var dBRK = new D_BRK(das);
                MainGridData = MapperInstance.Map<IList<UIModel_21032>>(dBRK.ListAll()).AsTrackable();
            }

            BrkType = DropDownItems.BrkType();
            BrkOpenCode = DropDownItems.OpenCode();
            BrkOthOpenCode = DropDownItems.BrkOthOpenCode();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_21032());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_21032)item);
        }

        public IEnumerable<ExportModel_21032> Query()
        {
            using (var das = Factory.CreateDalSession())
            {
                var dBRK = new D_BRK<ExportModel_21032>(das);
                return MapperInstance.Map<IList<ExportModel_21032>>(dBRK.ListByBrkNo(BrkNo));
            }
        }
    }

    public class UIModel_21032 : BRK
    {
    }

    public class ExportModel_21032
    {
        [Column("期貨商代號")]
        public virtual string BRK_NO { get; set; }

        [Column("期貨商名稱")]
        public virtual string BRK_NAME { get; set; }

        [Column("期貨商種類")]
        public virtual string BRK_TYPE { get; set; }

        [Column("期貨")]
        public virtual string BRK_OPEN_CODE_F { get; set; }

        [Column("選擇權")]
        public virtual string BRK_OPEN_CODE_O { get; set; }

        [Column("利率")]
        public virtual string BRK_OPEN_CODE_3 { get; set; }

        [Column("建檔日期")]
        public virtual DateTime BRK_CRE_DATE { get; set; }
    }
}