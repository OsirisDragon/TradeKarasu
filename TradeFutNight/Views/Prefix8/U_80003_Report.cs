using DevExpress.XtraPrinting;
using System.Collections;

namespace TradeFutNight.Views.Prefix8
{
    public partial class U_80003_Report : DevExpress.XtraReports.UI.XtraReport
    {
        public bool PageHeaderVisible
        {
            set
            {
                this.PageHeader.Visible = value;
            }
        }

        public bool TableFooterVisible
        {
            set
            {
                this.tableFooter.Visible = value;
            }
        }

        public bool HasHandlePerson
        {
            set
            {
                this.tableCellHandlePerson.Visible = value;
            }
        }

        public bool HasConfirmPerson
        {
            set
            {
                this.tableCellConfirmPerson.Visible = value;
            }
        }

        public bool HasManagerPerson
        {
            set
            {
                this.tableCellManagerPerson.Visible = value;
            }
        }

        public U_80003_Report()
        {
            InitializeComponent();
        }

        private void U_80003_Report_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            PrintingSystem.EditingFieldChanged += PrintingSystem_EditingFieldChanged;
        }

        private void PrintingSystem_EditingFieldChanged(object sender, EditingFieldEventArgs e)
        {
            var obj = ((IList)this.DataSource)[0];

            if (e.EditingField.ID != "")
            {
                obj.GetType().GetProperty(e.EditingField.ID).SetValue(obj, e.EditingField.EditValue);
            }

            //var value = obj.GetType().GetProperty("UPF_USER_ID").GetValue(obj, null);

            // Check if the field corresponds to a data-aware report control
            //if (e.EditingField.Brick.TextValue != null)
            //{
            //    e.EditingField.Brick.TextValueFormatString = "{0:c2}";
            //    e.EditingField.Brick.Text = string.Format(e.EditingField.Brick.TextValueFormatString, e.EditingField.Brick.TextValue);
            //}
            //else
            //{
            //    e.EditingField.Brick.Text = string.Format("{0:c2}", System.Convert.ToDouble(e.EditingField.Brick.Text));
            //}
        }
    }
}