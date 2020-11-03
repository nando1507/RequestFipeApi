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
    class Marca
    {
        public class marca
        {
            public string name { get; set; }
            public string fipe_name { get; set; }
            public string order { get; set; }
            public string key { get; set; }
            public string id { get; set; }
            /*{"name": "AUDI", "fipe_name": "Audi", "order": 2, "key": "audi-6", "id": 6}*/
        }

        SqlConnection conn = new SqlConnection(@"Data Source=desktop-jqkenas;Initial Catalog=Fipe;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();

    }
}
