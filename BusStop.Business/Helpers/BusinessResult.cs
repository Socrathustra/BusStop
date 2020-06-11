using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Business.Helpers
{
    public class BusinessResult<T, TResultCode> : IBusinessResult<T, TResultCode> where TResultCode : Enum
    {
        public BusinessResult(T result, TResultCode resultCode, string resultMessage, bool isSuccessful)
        {
            this.Result = result;
            this.ResultCode = resultCode;
            this.ResultMessage = resultMessage;
            this.IsSuccessful = isSuccessful;
        }

        public T Result { get; private set; }
        public TResultCode ResultCode { get; private set; }
        public string ResultMessage { get; private set; }
        public bool IsSuccessful { get; private set; }
    }
}
