using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasgemMicroservices.Shared.Dtos
{
    //geriye dönüş tipleri response üzerinden 
    public class Response<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }
        public static Response<T> Success(T data, int StatusCode)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = StatusCode,
                IsSuccessful = true
            };
        }
        public static Response<T> Success(int StatusCode)
        {
            return new Response<T>
            {
                Data = default(T),
                StatusCode = StatusCode,
                IsSuccessful = true
            };
        }
        public static Response<T> Fail(List<string> erors, int statusCode)
        {
            return new Response<T>
            {
                Errors = erors,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }
        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T>
            {
                Errors = new List<string>() { error },
                IsSuccessful = false,
                StatusCode = statusCode
            };
        }
    }
}