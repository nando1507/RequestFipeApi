using  System;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Data.SqlClient;

namespace CapturaFipe
{
    class Valor
    {
        public class valor
        {
            public string referencia { get; set; }
            public string fipe_codigo { get; set; }//"1992-1"
            public string name { get; set; }//"Integra GS 1.8"
            public string combustivel { get; set; }
            public string marca { get; set; }//"ACURA"
            public string ano_modelo { get; set; }
            public string preco { get; set; }
            public string key { get; set; }//"integra-1"
            public float time { get; set; }
            public string veiculo { get; set; }  //"Integra GS 1.8"
            public string id { get; set; }//"1"
        }

    }
}
