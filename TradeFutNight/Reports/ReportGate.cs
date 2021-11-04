using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.Caching;
using DevExpress.XtraReports.UI;
using System.Threading.Tasks;

namespace TradeFutNight.Reports
{
    internal class ReportGate
    {
        private readonly XtraReport _report;
        private readonly DocumentStorage _storage;
        private CachedReportSource _cachedReportSource;

        public ReportGate(XtraReport report) : this(report, new MemoryDocumentStorage())
        {
        }

        public ReportGate(XtraReport report, DocumentStorage storage)
        {
            _report = report;
            _storage = storage;
            _cachedReportSource = new CachedReportSource(report, storage);

            // 如果不要顯示列印跳出來的系統預設視窗的話加這行
            //_cachedReportSource.PrintingSystem.ShowPrintStatusDialog = false;
        }

        public async Task<ReportGate> CreateDocumentAsync()
        {
            await _cachedReportSource.CreateDocumentAsync();
            return this;
        }

        public async Task ExportPdf(string filePath)
        {
            new PdfStreamingExporter(_report, true).Export(filePath);
            await Task.Yield();
        }

        public async Task Print()
        {
            PrintHelper.PrintDirect(_cachedReportSource);
            await Task.Yield();
        }
    }
}