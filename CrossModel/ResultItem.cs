using System;

namespace CrossModel
{
    public class ResultItem
    {
        public string ErrorMessage { get; set; }

        public ResultItem()
        {
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