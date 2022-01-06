using System;

namespace CrossModel
{
    public class ExceptionCustom : Exception
    {
        public int ErrorNumber { get; set; }

        public string ErrorMessage { get; set; }

        public ExceptionCustom(string message) : base(message)
        {
        }
    }
}