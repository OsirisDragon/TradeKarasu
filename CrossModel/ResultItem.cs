using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CrossModel
{
    public class ResultItem
    {
        private List<ResultItemDetail> _ItemDetails = new List<ResultItemDetail>();
        private bool _IsRedirectToCompleteContent = true;
        private bool _IsNeedToDownloadFile = true;
        private bool _IsNeedPrint = true;
        private int _ErrorRowIndex = -1;

        public IEnumerable MainData { get; set; }
        public IEnumerable<string> TableKeys { get; set; }
        public int ErrorRowIndex { get => _ErrorRowIndex; set => _ErrorRowIndex = value; }
        public string ErrorMessage { get; set; }
        public string ConfirmFlag { get; set; }
        public string ConfirmMessage { get; set; }
        public string WarningMessage { get; set; }
        public string ZipFileUrl { get; set; }
        public string ZipFileName { get; set; }
        public string ZipLocalFilePath { get; set; }
        public List<ResultItemDetail> ItemDetails { get => _ItemDetails; set => _ItemDetails = value; }

        /// <summary>
        /// 作完作業後是否要導向至完成的頁面
        /// </summary>
        public bool IsRedirectToCompleteContent { get => _IsRedirectToCompleteContent; set => _IsRedirectToCompleteContent = value; }

        /// <summary>
        /// 作業完成後是否要下載檔案
        /// </summary>
        public bool IsNeedToDownloadFile { get => _IsNeedToDownloadFile; set => _IsNeedToDownloadFile = value; }

        public bool IsNeedPrint { get => _IsNeedPrint; set => _IsNeedPrint = value; }

        public ResultItem()
        {
        }

        public ResultItem(IEnumerable<string> tableKeys, bool isNeedToDownloadFile, bool isRedirectToCompleteContent)
        {
            TableKeys = tableKeys;
            IsNeedToDownloadFile = isNeedToDownloadFile;
            IsRedirectToCompleteContent = isRedirectToCompleteContent;
        }

        public void Add(string filePath, bool isPrint)
        {
            ResultItemDetail detail = new ResultItemDetail();
            detail.ServerFilePath = filePath;
            detail.FileName = Path.GetFileName(filePath);
            detail.IsPrint = isPrint;

            ItemDetails.Add(detail);
        }

        public void AppendErrorMessage(string inputStr)
        {
            ErrorMessage += inputStr + Environment.NewLine;
        }

        public bool HasError
        {
            get
            {
                return ErrorMessage != null;
            }
        }
    }
}