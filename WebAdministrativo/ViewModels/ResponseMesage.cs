using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdministrativo.ViewModels
{
    public class ResponseMesage
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<Data> data { get; set; }
    }

    public class Data
    {
        public string pro1 { get; set; }
        public string pro2 { get; set; }
        public int tipoUser { get; set; }

    }
}