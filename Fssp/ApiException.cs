using System;

namespace Fssp
{

    public class ApiException : Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public ApiException(string message, int statusCode, string response) : base(message)
        {
            StatusCode = statusCode;
            Response = response;
        }

        public ApiException(string message) : base(message)
        {

        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

}
