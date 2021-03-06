namespace TradeFutNight.Views.Prefix5
{
    public partial class U_52303_Report : DevExpress.XtraReports.UI.XtraReport
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

        public bool HasGroupProdIdOut
        {
            set
            {
                this.GroupFooterBandProdIdOut.Visible = value;
            }
        }

        public U_52303_Report()
        {
            InitializeComponent();
        }

        private void U_52303_Report_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (RowCount == 0)
            {
                Detail.Visible = false;
                GroupFooterBandProdIdOut.Visible = false;
                ReportFooter.Visible = false;
            }
        }
    }
}