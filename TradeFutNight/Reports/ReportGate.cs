using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.Caching;
using DevExpress.XtraReports.UI;
using System.Threading.Tasks;

namespace TradeFutNight.Reports
{
    public class ReportGate
    {
        private readonly XtraReport _report;
        private readonly DocumentStorage _storage;
        private CachedReportSource _cachedReportSource;
        private CompositeLink _compositeLink;
        private PrintType _printType;

        public ReportGate(XtraReport report) : this(report, new MemoryDocumentStorage())
        {
        }

        public ReportGate(XtraReport report, DocumentStorage storage)
        {
            _printType = PrintType.XtraReport;
            _report = report;
            _storage = storage;
            _cachedReportSource = new CachedReportSource(report, storage);

            // 如果不要顯示列印跳出來的系統預設視窗的話加這行
            _cachedReportSource.PrintingSystem.ShowPrintStatusDialog = false;
        }

        public ReportGate(CompositeLink compositeLink)
        {
            _printType = PrintType.CompositeLink;
            _compositeLink = compositeLink;
        }

        public async Task<ReportGate> CreateDocumentAsync()
        {
            switch (_printType)
            {
                case PrintType.XtraReport:
                    await _cachedReportSource.CreateDocumentAsync();
                    break;

                case PrintType.CompositeLink:
                    _compositeLink.CreateDocument();
                    break;

                default:
                    break;
            }

            return this;
        }

        public async Task ExportPdf(string filePath)
        {
            switch (_printType)
            {
                case PrintType.XtraReport:
                    _cachedReportSource.PrintingSystem.ExportToPdf(filePath);
                    break;

                case PrintType.CompositeLink:
                    _compositeLink.ExportToPdf(filePath);
                    break;

                default:
                    break;
            }

            await Task.Yield();
        }

        public async Task Print()
        {
            switch (_printType)
            {
                case PrintType.XtraReport:
                    PrintHelper.PrintDirect(_cachedReportSource);
                    break;

                case PrintType.CompositeLink:
                    _compositeLink.PrintDirect();
                    break;

                default:
                    break;
            }

            // 等待一下，因為如果不等待一下的話，後續有呼叫MessageBoxExService裡面ThemedMessageBox.Show的跳出視窗然後再按下是的話，整個程式視窗會被縮小
            await Task.Delay(500);
            await Task.Yield();
        }
    }

    public enum PrintType
    {
        XtraReport,
        CompositeLink
    }
}