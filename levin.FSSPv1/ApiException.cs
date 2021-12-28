using System;

namespace levin.FSSPv1
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

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

}
