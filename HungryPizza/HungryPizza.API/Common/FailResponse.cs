using System;

namespace HungryPizza.API.Common
{
    public class FailResponse
    {
        public FailResponse() { }

        public FailResponse(string error)
        {
            Error = error;
        }

        public FailResponse(string error, Exception e)
        {
            Error = error;
            Exception = e;
        }

        public DateTime Date => DateTime.Now;

        public string Error { get; set; }

        public Exception Exception { get; set; }
    }
}
