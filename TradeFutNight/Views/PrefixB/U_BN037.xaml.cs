using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixB
{
    /// <summary>
    /// U_BN037.xaml 的互動邏輯
    /// </summary>
    public partial class U_BN037 : UserControlParent, IViewSword
    {
        private U_BN037_ViewModel _vm;

        public U_BN037()
        {
            InitializeComponent();
            _vm = (U_BN037_ViewModel)DataContext;
        }

        public async Task<bool> IsCanRun()
        {
            var task = Task.Run(() =>
            {
                var isCanRun = IsCanRunProgram();
                DbLog(MessageConst.IsCanRun + ":" + isCanRun.ToString().ToUpper());
                return isCanRun;
            });
            await task;

            return task.Result;
        }

        public override void ToolButtonSetting()
        {
            base.ToolButtonSetting();
        }

        public async Task Open()
        {
            ToolButtonSetting();

            var task = Task.Run(() =>
            {
                _vm.Open();

                using (var das = Factory.CreateDalSession())
                {
                    //das.Begin();

                    try
                    {
                        var dSp = new D_StoredProcedure<DEFAULT>(das);
                        DbLog("中文", das);
                        DbLog("開始執行" + nameof(dSp.proc_AH_settle_price), das);
                        dSp.proc_AH_settle_price("U", 10, 0);
                        DbLog("結束執行" + nameof(dSp.proc_AH_settle_price), das);

                        UpdateAccessPermission(ProgramID, das);
                        DbLog(MessageConst.Completed, das);

                        //das.Commit();
                    }
                    catch (Exception ex)
                    {
                        //das.Rollback();
                        throw ex;
                    }
                }
            });
            await task;

            MessageBoxExService.Instance().Error(MessageConst.ExecuteSuccess);

            CloseWindow();
        }

        public void Insert()
        {
        }

        public void Delete()
        {
        }

        public async Task<bool> CheckField()
        {
            var task = Task.Run(() =>
            {
                return true;
            });
            await task;

            return task.Result;
        }

        public async Task Save()
        {
            var task = Task.Run(() =>
           {
           });
            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            return null;
        }

        public async Task Export()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task Print()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task PrintIndex()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task PrintStock()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }
    }
}