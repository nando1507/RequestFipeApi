using System;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Data.SqlClient;

namespace CapturaFipe.Class
{
    class Connection
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-jqkenas;Initial Catalog=Fipe;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();

    }
}
