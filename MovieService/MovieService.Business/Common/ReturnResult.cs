using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.Common
{
    public class ReturnResult
    {
        public bool IsSuccess { get; set; }

        public bool IsUpdatedSuccess { get; set; }

        public string Message { get; set; }

        public object data { get; set; }
    }
}
