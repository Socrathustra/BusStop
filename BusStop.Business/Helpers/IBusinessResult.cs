using System;

namespace BusStop.Business.Helpers
{
    public interface IBusinessResult<T, TResultCode> where TResultCode : Enum
    {
        bool IsSuccessful { get; }
        T Result { get; }
        string ResultMessage { get; }
        TResultCode ResultCode { get; }
    }
}