using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Abstractions.ResultObjects
{
    public class ResponseResult<TType> where TType : class
    {
        public Guid TransactionId { get; set; }
        public string ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public string ResultType { get; set; }
        public TType Body { get; set; }
    }
}
