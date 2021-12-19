using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TANGOCCONG.ANUIShop.API.Objects
{
    public class ResponseObject
    {
        /// <summary>
        /// Error: 
        ///     0: thanh cong; 
        ///     1: that bai
        /// </summary>
        public int Error { get; set; }
        public string Msg { get; set; }
    }

    public class ResponseData<T>
    {
        /// <summary>
        /// Error: 
        ///     0: thanh cong; 
        ///     1: that bai
        /// </summary>
        public int Error { get; set; }
        public string Msg { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
    }

    public class ErrorResponseData<T> : ResponseData<T>
    {
        public ErrorResponseData() { }

        public ErrorResponseData(string msg)
        {
            Error = 1;
            Msg = msg;
        }

        public ErrorResponseData(string msg, int code)
        {
            Error = 1;
            Msg = msg;
            Code = code;
        }
    }

    public class SuccessResponseData<T> : ResponseData<T>
    {
        public SuccessResponseData() { }

        public SuccessResponseData(string msg)
        {
            Error = 0;
            Msg = msg;
        }

        //public SuccessResponseData(string msg, int code)
        //{
        //    Error = 0;
        //    Msg = msg;
        //    Code = code;
        //}

        public SuccessResponseData(string msg, T data)
        {
            Error = 0;
            Msg = msg;
            Data = data;
        }
    }

    public class PaginationResult<T>
    {
        public List<T> items { get; set; }
        public int Page { get; set; }
        public int TotalRecord { get; set; }
        public int Limit { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecord / Limit);
            }
        }

        public PaginationResult(int page, int total, int limit, List<T> records)
        {
            Page = page;
            TotalRecord = total;
            Limit = limit;
            items = records;
        }
    }


    public class BaseRequest
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string Keyword { get; set; }
    }
}
