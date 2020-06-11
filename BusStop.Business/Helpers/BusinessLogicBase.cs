using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Business.Helpers
{
    public abstract class BusinessLogicBase<TResultCode> where TResultCode : Enum
    {
        protected abstract IDictionary<TResultCode, string> ResultMessages { get; set; }

        protected IBusinessResult<T, TResultCode> GetErrorResult<T>(TResultCode resultCode)
        {
            return this.GetBusinessResult(default(T), resultCode, false);
        }

        protected IBusinessResult<T, TResultCode> GetBusinessResult<T>(T result, TResultCode resultCode, bool isSuccessful = true)
        {
            if (this.ResultMessages == null)
            {
                throw new ApplicationException($"{nameof(this.ResultMessages)} was null; cannot produce a business result.");
            }

            if (this.ResultMessages.ContainsKey(resultCode) == false)
            {
                throw new ArgumentException($"The parameter ${nameof(resultCode)} had a value of {resultCode} which could not be found in the result messages lookup");
            }

            var resultMessage = this.ResultMessages[resultCode];

            return new BusinessResult<T, TResultCode>(result, resultCode, resultMessage, isSuccessful);
        }
    }
}
