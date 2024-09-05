using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrapper
{
    //ResponseContainer
    public class ResponseWrapper<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; }
        public T Data { get; set; }

        public ResponseWrapper<T> Success(T data, string message = null)
        {
            IsSuccess = true;
            Messages = [message];
            Data = data;

            return this;
        }

        public ResponseWrapper<T> Failed(string message = null)
        {
            IsSuccess = false;
            Messages = [message];

            return this;
        }

    }
}
