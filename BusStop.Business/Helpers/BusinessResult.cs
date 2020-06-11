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
            this.IsSuccessful = isSuccessful;
        }

        public T Result { get; set; }
        public TResultCode ResultCode { get; private set; }
        public bool IsSuccessful { get; private set; }
    }
}
