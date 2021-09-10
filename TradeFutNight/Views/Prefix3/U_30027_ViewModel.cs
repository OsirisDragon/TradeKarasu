using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30027_ViewModel : ViewModelParent<UIModel_30027>
    {
        public IList<ItemInfo> IdxGroupInfos
        {
            get { return GetProperty(() => IdxGroupInfos); }
            set { SetProperty(() => IdxGroupInfos, value); }
        }

        public IList<ItemInfo> TypeInfos
        {
            get { return GetProperty(() => TypeInfos); }
            set { SetProperty(() => TypeInfos, value); }
        }

        public U_30027_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30027>();
            IdxGroupInfos = new ObservableCollection<ItemInfo>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPVOL, UIModel_30027>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30027>().ToList().AsTrackable();

            var idxGroupInfos = new List<ItemInfo>();
            idxGroupInfos.Add(new ItemInfo() { Text = "其他", Value = (sbyte)0 });
            idxGroupInfos.Add(new ItemInfo() { Text = "上市/櫃", Value = (sbyte)1 });
            idxGroupInfos.Add(new ItemInfo() { Text = "日本", Value = (sbyte)3 });
            idxGroupInfos.Add(new ItemInfo() { Text = "美國", Value = (sbyte)5 });
            IdxGroupInfos = idxGroupInfos;

            var typeInfos = new List<ItemInfo>();
            typeInfos.Add(new ItemInfo() { Text = "歷史波動", Value = "1" });
            typeInfos.Add(new ItemInfo() { Text = "瞬時波動", Value = "2" });
            typeInfos.Add(new ItemInfo() { Text = "CBOE VIX", Value = "4" });
            typeInfos.Add(new ItemInfo() { Text = "日經VIX", Value = "JNIV" });
            typeInfos.Add(new ItemInfo() { Text = "JPX東證期貨漲跌幅", Value = "TOPX" });
            typeInfos.Add(new ItemInfo() { Text = "CME E-mini S&P500期貨漲跌幅", Value = "SPX" });
            TypeInfos = typeInfos;
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30027()
            {
                TPPVOL_TYPE = ""
            });
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30027)item);
        }
    }

    public class UIModel_30027 : TPPVOL
    {
    }
}