using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixC
{
    public class U_C9902_ViewModel : ViewModelParent<UIModel_C9902>
    {
        public IList<UIModel_C9902_File> FileGridData
        {
            get { return GetProperty(() => FileGridData); }
            set { SetProperty(() => FileGridData, value); }
        }

        public U_C9902_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_C9902>();
            FileGridData = new ObservableCollection<UIModel_C9902_File>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JCF, UIModel_C9902>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_C9902>().ToList().AsTrackable();
            FileGridData = new ObservableCollection<UIModel_C9902_File>().ToList();

            using (var das = Factory.CreateDalSession())
            {
                var dJCF = new D_JCF(das);
                MainGridData = MapperInstance.Map<IList<UIModel_C9902>>(dJCF.ListNotStartWith("B")).AsTrackable();
            }

            string[] checkItem = { "50301", "50302", "50303", "52303" };
            string checkPatern;
            var fileList = new List<UIModel_C9902_File>();

            var rowNumber = 1;
            foreach (string item in checkItem)
            {
                checkPatern = $"*{item}*.pdf";
                string[] files = Directory.GetFiles(AppSettings.LocalReportDirectory, checkPatern, SearchOption.AllDirectories);
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        var fileInfo = new FileInfo(file);
                        var uiModelFile = new UIModel_C9902_File();
                        uiModelFile.RowNumber = rowNumber++;
                        uiModelFile.DirectoryName = fileInfo.DirectoryName;
                        uiModelFile.Name = fileInfo.Name;

                        fileList.Add(uiModelFile);
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

    public class UIModel_C9902_File
    {
        public virtual int RowNumber { get; set; }
        public virtual string DirectoryName { get; set; }
        public virtual string Name { get; set; }
    }
}