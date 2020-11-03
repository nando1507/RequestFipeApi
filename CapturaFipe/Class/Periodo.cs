using System;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Data.SqlClient;
namespace CapturaFipe
{
    class Periodo
    {
        public class periodo
        {
            public string fipe_marca { get; set; }//"Acura"
            public string fipe_codigo { get; set; }//"1992-1"
            public string name { get; set; }//"Integra GS 1.8"
            public string marca { get; set; }//"ACURA"
            public string key { get; set; }//"integra-1"
            public string veiculo { get; set; }  //"Integra GS 1.8"
            public string id { get; set; }//"1"

        }


    }
}
