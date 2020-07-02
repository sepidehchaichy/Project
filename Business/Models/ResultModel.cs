using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ResultModel
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }

    public class ResultModel<TData>: ResultModel
    {
        public TData Result { get; set; }
    }

}
