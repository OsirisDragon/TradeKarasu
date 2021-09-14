using System;

namespace CrossModel
{
    public class ResultItem
    {
        private string _errorMessage = null;

        public string ErrorMessage
        {
            get
            {
                if (_errorMessage != null)
                {
                    return _errorMessage.TrimEnd('\r', '\n');
                }
                return _errorMessage;
            }
            set { _errorMessage = value; }
        }

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