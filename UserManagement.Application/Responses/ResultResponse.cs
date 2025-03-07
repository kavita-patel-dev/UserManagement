using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Responses
{
    public class ResultResponse<T>
    {
        public T Data { get; set; }
        public int TotalCount { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

    }
}
