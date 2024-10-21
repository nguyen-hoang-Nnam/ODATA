using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.Model.Models.DTO.Response
{
    public class ResponseDTO
    {
        public bool IsSucceed { get; set; } = false;
        public string Message { get; set; } = "";
        public object Data { get; set; }
    }
}
