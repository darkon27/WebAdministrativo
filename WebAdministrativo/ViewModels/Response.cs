using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdministrativo.ViewModels
{
    public class Response
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}