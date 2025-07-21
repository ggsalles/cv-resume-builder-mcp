using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Windows.Forms;

namespace WEDLC.Forms
{
    public partial class testecep : Form
    {
        public testecep()
        {
            InitializeComponent();
        }

        public DataTable GetJsonToDataTable(string url)
        {
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                DataTable dt = new DataTable();
                foreach (var key in dict.Keys)
                    dt.Columns.Add(key);

                var row = dt.NewRow();
                foreach (var kvp in dict)
                    row[kvp.Key] = kvp.Value ?? "";
                dt.Rows.Add(row);

                return dt;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            DataTable dtAux = new DataTable();
            string url = "https://viacep.com.br/ws/" + txtcep.Text + "/json/";

            dtAux = GetJsonToDataTable(url);
            if (dtAux.Rows.Count > 0)
            {
                txtlogradouro.Text = dtAux.Rows[0]["logradouro"].ToString();
                txtbairro.Text = dtAux.Rows[0]["bairro"].ToString();
                txtcidade.Text = dtAux.Rows[0]["localidade"].ToString();
                txtuf.Text = dtAux.Rows[0]["uf"].ToString();
            }
            else
            {
                MessageBox.Show("CEP não encontrado.");
            }
        }
    }
}