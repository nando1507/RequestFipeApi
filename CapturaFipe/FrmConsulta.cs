using System;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Data.SqlClient;
using System.ComponentModel;

namespace CapturaFipe
{
    public partial class FrmConsulta : Form
    {
        public FrmConsulta()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=desktop-jqkenas;Initial Catalog=Fipe;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();

        /*
         * Necessidade:
         * Capturar dados da tabela fipe
         * Como fazer:
         * Modelos http://fipeapi.appspot.com/api/1/carros/marcas.json
         * Veiculos http://fipeapi.appspot.com/api/1/carros/veiculos/1.json
         * periodo http://fipeapi.appspot.com/api/1/carros/veiculo/1/2.json         
         * Valores http://fipeapi.appspot.com/api/1/carros/veiculo/21/4828/2013-1.json
         */
        string URI = string.Empty;



        public class fipeMarcas
        {
            public string name { get; set; }
            public string fipe_name { get; set; }
            public string order { get; set; }
            public string key { get; set; }
            public string id { get; set; }
            /*{"name": "AUDI", "fipe_name": "Audi", "order": 2, "key": "audi-6", "id": 6}*/
        }

        public class fipeModelos
        {
            public string fipe_marca { get; set; }//"Acura"
            public string name { get; set; }//"Integra GS 1.8"
            public string marca { get; set; }//"ACURA"
            public string key { get; set; }//"integra-1"
            public string id { get; set; }//"1"
            public string fipe_name { get; set; }  //"Integra GS 1.8"

        }

        public class fipePeriodo
        {
            public string fipe_marca { get; set; }//"Acura"
            public string fipe_codigo { get; set; }//"1992-1"
            public string name { get; set; }//"Integra GS 1.8"
            public string marca { get; set; }//"ACURA"
            public string key { get; set; }//"integra-1"
            public string veiculo { get; set; }  //"Integra GS 1.8"
            public string id { get; set; }//"1"

        }

        public class fipeValores
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

            /*
            "referencia": "novembro de 2020", 
            "fipe_codigo": "037001-0", 
            "name": "Hummer Hard-Top 6.5 4x4 Diesel TB", 
            "combustivel": "Diesel", 
            "marca": "AM Gen", 
            "ano_modelo": "1999", 
            "preco": "R$ 187.557,00", 
            "key": "hummer-1999", 
            "time": 0.04000000000007731, 
            "veiculo": "Hummer Hard-Top 6.5 4x4 Diesel TB", 
            "id": "1999"
            */
        }



        private void Form1_Load(object sender, EventArgs e)
        {



        }
        private async void GetAllMarcas()
        {

            lblLinhas.Text = "0";
            URI = "http://fipeapi.appspot.com/api/1/carros/marcas.json";
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        dgvDados.DataSource = JsonConvert.DeserializeObject<fipeMarcas[]>(ProdutoJsonString).ToList();
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível obter o produto : " + response.StatusCode);
                    }
                }
            }

            lblLinhas.Text = dgvDados.Rows.Count.ToString();

            //dgvDados.Sort(dgvDados.Columns["id"], ListSortDirection.Ascending);

            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Marcas]  ([idMarca]
                                                                           ,[NmMarca]
                                                                           ,[NmMarcaFipe]
                                                                           ,[NmKey])
                                                                     VALUES
                                                                           (@idMarca
                                                                           ,@NmMarca
                                                                           ,@NmMarcaFipe
                                                                           ,@NmKey
		                                                                   )", conn);


            //importa as marcas para o banco
            for (int i = 0; i < dgvDados.Rows.Count; i++)
            {
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@idMarca", dgvDados.Rows[i].Cells["id"].Value.ToString());
                cmd.Parameters.AddWithValue("@NmMarca", dgvDados.Rows[i].Cells["name"].Value.ToString());
                cmd.Parameters.AddWithValue("@NmMarcaFipe", dgvDados.Rows[i].Cells["fipe_name"].Value.ToString());
                cmd.Parameters.AddWithValue("@NmKey", dgvDados.Rows[i].Cells["key"].Value.ToString());


                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace.ToString(), ex.Message.ToString());
                    conn.Close();
                }
            }


            dgvDados.DataSource = null;


        }

        private async void GetAllModelos()
        {
            lblLinhas.Text = "0";
            SqlCommand cmdMarcas = new SqlCommand(@"Select Distinct idMarca from Marcas where cast(DtCapturaMarcas as date) = cast(getdate() as date) Order by idMarca", conn);
            da.SelectCommand = cmdMarcas;
            da.Fill(ds, "Marcas");

            for (int i = 0; i < ds.Tables["Marcas"].Rows.Count; i++)
            {
                int id = Convert.ToInt32(ds.Tables["Marcas"].Rows[i].ItemArray[0].ToString());
                URI = $"http://fipeapi.appspot.com/api/1/carros/veiculos/{id}.json";
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(URI))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                            dgvDados.DataSource = JsonConvert.DeserializeObject<fipeModelos[]>(ProdutoJsonString).ToList();
                        }
                        else
                        {
                            MessageBox.Show(string.Concat("Não foi possível obter o produto : ", response.StatusCode.ToString(), i.ToString()));
                        }
                    }
                }

                SqlCommand iModelos = new SqlCommand(@"INSERT INTO [dbo].[Modelos]
                                                                               ([IdModelo]
                                                                               ,[NmModelo]
                                                                               ,[NmModeloFipe]
                                                                               ,[NmKey]
                                                                               ,[CdMarca])
                                                                         VALUES
                                                                               (@IdModelo
                                                                               ,@NmModelo
                                                                               ,@NmModeloFipe
                                                                               ,@NmKey
                                                                               ,@CdMarca)", conn);
                for (int j = 0; j < dgvDados.Rows.Count; j++)
                {
                    iModelos.Parameters.Clear();

                    iModelos.Parameters.AddWithValue("@IdModelo", dgvDados.Rows[j].Cells["id"].Value.ToString());
                    iModelos.Parameters.AddWithValue("@NmModelo", dgvDados.Rows[j].Cells["name"].Value.ToString());
                    iModelos.Parameters.AddWithValue("@NmModeloFipe", dgvDados.Rows[j].Cells["fipe_marca"].Value.ToString());
                    iModelos.Parameters.AddWithValue("@NmKey", dgvDados.Rows[j].Cells["key"].Value.ToString());
                    iModelos.Parameters.AddWithValue("@CdMarca", id);
                    try
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        iModelos.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.StackTrace.ToString(), ex.Message.ToString());
                        conn.Close();
                    }

                }
                lblLinhas.Text = Convert.ToString(Convert.ToInt32(lblLinhas.Text) + (int)dgvDados.Rows.Count);

                dgvDados.DataSource = null;
                //Thread.Sleep(2000);
            }

        }

        private async void GetAllPeriodo()
        {
            lblLinhas.Text = "0";
            SqlCommand cmdMarcas = new SqlCommand(@"Select Distinct IdModelo, CdMarca from Modelos With (nolock) Order by  IdModelo, CdMarca", conn);
            da.SelectCommand = cmdMarcas;
            da.Fill(ds, "Modelos");

            for (int i = 0; i < ds.Tables["Modelos"].Rows.Count; i++)
            {
                int idMarca = Convert.ToInt32(ds.Tables["Modelos"].Rows[i].ItemArray[1].ToString());
                int idModelo = Convert.ToInt32(ds.Tables["Modelos"].Rows[i].ItemArray[0].ToString());
                URI = $"http://fipeapi.appspot.com/api/1/carros/veiculo/{idMarca}/{idModelo}.json";
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(URI))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                            dgvDados.DataSource = JsonConvert.DeserializeObject<fipePeriodo[]>(ProdutoJsonString).ToList();
                        }
                        else
                        {
                            MessageBox.Show(string.Concat("Não foi possível obter o produto : ", response.StatusCode.ToString(), i.ToString()));
                        }
                    }
                }

                SqlCommand iPeriodo = new SqlCommand(@"INSERT INTO [dbo].[Periodo]
                                                                               ([idPeriodo]
                                                                               ,[NmPeriodo]
                                                                               ,[NmKey]
                                                                               ,[cdModelo]
                                                                               ,[cdMarca])
                                                                         VALUES
                                                                               (@idPeriodo
                                                                               ,@NmPeriodo
                                                                               ,@NmKey
                                                                               ,@cdModelo
                                                                               ,@cdMarca)", conn);
                for (int j = 0; j < dgvDados.Rows.Count; j++)
                {
                    iPeriodo.Parameters.Clear();

                    iPeriodo.Parameters.AddWithValue("@idPeriodo", String.IsNullOrEmpty(dgvDados.Rows[j].Cells["id"].Value.ToString()) ? Convert.DBNull : dgvDados.Rows[j].Cells["id"].Value.ToString());
                    iPeriodo.Parameters.AddWithValue("@NmPeriodo", String.IsNullOrEmpty(dgvDados.Rows[j].Cells["name"].Value.ToString()) ? Convert.DBNull : dgvDados.Rows[j].Cells["name"].Value.ToString());
                    //iPeriodo.Parameters.AddWithValue("@NmCodigoFipe", String.IsNullOrEmpty(dgvDados.Rows[j].Cells["fipe_marca"].Value) ? Convert.DBNull : dgvDados.Rows[j].Cells["fipe_marca"].Value.ToString());
                    iPeriodo.Parameters.AddWithValue("@NmKey", String.IsNullOrEmpty(dgvDados.Rows[j].Cells["key"].Value.ToString()) ? Convert.DBNull : dgvDados.Rows[j].Cells["key"].Value.ToString());
                    iPeriodo.Parameters.AddWithValue("@cdModelo", idModelo);
                    iPeriodo.Parameters.AddWithValue("@cdMarca", idMarca);
                    try
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        iPeriodo.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.StackTrace.ToString(), ex.Message.ToString());
                        conn.Close();
                    }

                }
                lblLinhas.Text = Convert.ToString(Convert.ToInt32(lblLinhas.Text) + (int)dgvDados.Rows.Count);

                dgvDados.DataSource = null;
                Thread.Sleep(1000);
            }
        }


        private async void GetAllValores()
        {
            lblLinhas.Text = "0";
            SqlCommand cmdMarcas = new SqlCommand(@"Select A.cdMarca, cdModelo, idPeriodo from Periodo A with (nolock)
                                                    left join Modelos B with (nolock)
	                                                    on A.cdModelo = b.IdModelo
                                                    left join Marcas C With (Nolock)
	                                                    on A.cdMarca = c.idMarca
                                                    Order by A.cdMarca, cdModelo", conn);
            cmdMarcas.CommandTimeout = 5000;
            da.SelectCommand = cmdMarcas;
            da.Fill(ds, "Valores");

            for (int i = 0; i < ds.Tables["Valores"].Rows.Count; i++)
            {
                int idMarca = Convert.ToInt32(ds.Tables["Valores"].Rows[i].ItemArray[0].ToString());
                int idModelo = Convert.ToInt32(ds.Tables["Valores"].Rows[i].ItemArray[1].ToString());
                String idPeriodo = ds.Tables["Valores"].Rows[i].ItemArray[2].ToString();
                URI = $"http://fipeapi.appspot.com/api/1/carros/veiculo/{idMarca}/{idModelo}/{idPeriodo}.json";
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(URI))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                            var aux = JsonConvert.DeserializeObject<fipeValores[]>(ProdutoJsonString).ToList();
                            dgvDados.DataSource = JsonConvert.DeserializeObject<fipeValores[]>(ProdutoJsonString).ToList();
                        }
                        else
                        {
                            MessageBox.Show(string.Concat("Não foi possível obter o produto : ", response.StatusCode.ToString(), i.ToString()));
                        }
                    }
                }

                SqlCommand iPeriodo = new SqlCommand(@"INSERT INTO [dbo].[ValorPeriodo]    ([idValorPeriodo]
                                                                                           ,[NmReferencia]
                                                                                           ,[NmFipeCodigo]
                                                                                           ,[NmModelo]
                                                                                           ,[NmCombustivel]
                                                                                           ,[NmMarca]
                                                                                           ,[NuAnoModelo]
                                                                                           ,[VlrPrecoPeriodo]
                                                                                           ,[NmKey]
                                                                                           ,[HrValorPeriodo]
                                                                                           ,[NmVeiculo]
                                                                                           ,[CdPeriodo]
                                                                                           ,[cdModelo]
                                                                                           ,[cdMarca])
                                                                                     VALUES
                                                                                           (@idValorPeriodo
                                                                                           ,@NmReferencia
                                                                                           ,@NmFipeCodigo
                                                                                           ,@NmModelo
                                                                                           ,@NmCombustivel
                                                                                           ,@NmMarca
                                                                                           ,@NuAnoModelo
                                                                                           ,@VlrPrecoPeriodo
                                                                                           ,@NmKey
                                                                                           ,@HrValorPeriodo
                                                                                           ,@NmVeiculo
                                                                                           ,@CdPeriodo
                                                                                           ,@cdModelo
                                                                                           ,@cdMarca)", conn);
                for (int j = 0; j < dgvDados.Rows.Count; j++)
                {
                    iPeriodo.Parameters.Clear();

                    iPeriodo.Parameters.AddWithValue("@idPeriodo", String.IsNullOrEmpty(dgvDados.Rows[j].Cells["id"].Value.ToString()) ? Convert.DBNull : dgvDados.Rows[j].Cells["id"].Value.ToString());
                    iPeriodo.Parameters.AddWithValue("@NmPeriodo", String.IsNullOrEmpty(dgvDados.Rows[j].Cells["name"].Value.ToString()) ? Convert.DBNull : dgvDados.Rows[j].Cells["name"].Value.ToString());
                    //iPeriodo.Parameters.AddWithValue("@NmCodigoFipe", String.IsNullOrEmpty(dgvDados.Rows[j].Cells["fipe_marca"].Value) ? Convert.DBNull : dgvDados.Rows[j].Cells["fipe_marca"].Value.ToString());
                    iPeriodo.Parameters.AddWithValue("@NmKey", String.IsNullOrEmpty(dgvDados.Rows[j].Cells["key"].Value.ToString()) ? Convert.DBNull : dgvDados.Rows[j].Cells["key"].Value.ToString());
                    iPeriodo.Parameters.AddWithValue("@cdModelo", idModelo);
                    iPeriodo.Parameters.AddWithValue("@cdMarca", idMarca);
                    try
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        iPeriodo.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.StackTrace.ToString(), ex.Message.ToString());
                        conn.Close();
                    }

                }
                lblLinhas.Text = Convert.ToString(Convert.ToInt32(lblLinhas.Text) + (int)dgvDados.Rows.Count);

                dgvDados.DataSource = null;
                Thread.Sleep(2000);
            }


        }



        private void btnMarca_Click(object sender, EventArgs e)
        {
            this.GetAllMarcas();
        }

        private void btnModelo_Click(object sender, EventArgs e)
        {
            this.GetAllModelos();
        }

        private void btnPeriodo_Click(object sender, EventArgs e)
        {
            this.GetAllPeriodo();
        }

        private void btnValores_Click(object sender, EventArgs e)
        {
            GetAllValores();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}