using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class ServiceResponse
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int Code { get; set; }
    }
}
