using AutoMapper;
using ChangeTracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_C9902_ViewModel : ViewModelParent<UIModel_C9902>
    {
        public IList<FileInfo> FileGridData
        {
            get { return GetProperty(() => FileGridData); }
            set { SetProperty(() => FileGridData, value); }
        }

        public U_C9902_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_C9902>();
            FileGridData = new ObservableCollection<FileInfo>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JCF, UIModel_C9902>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_C9902>().ToList().AsTrackable();
            FileGridData = new ObservableCollection<FileInfo>().ToList();

            using (var das = Factory.CreateDalSession())
            {
                var dJCF = new D_JCF(das);
                MainGridData = MapperInstance.Map<IList<UIModel_C9902>>(dJCF.ListByID("B")).AsTrackable();
            }

            string path = $@"C:\future_night\{ MagicalHats.Ocf.OCF_DATE.ToString("yyyyMMdd")}\";
            string[] check = { "50301", "50302", "50303", "52303" };
            string checkPatern;
            List<FileInfo> fileList = new List<FileInfo>();
            foreach (string item in check)
            {
                checkPatern = $"*{item}*.pdf";
                string[] files = Directory.GetFiles(path, checkPatern, SearchOption.AllDirectories);
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        fileList.Add(fileInfo);
                    }
                }
            }
            FileGridData = fileList;
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_C9902());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_C9902)item);
        }
    }

    public class UIModel_C9902 : JCF
    {
    }
}