using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WEDLC.Forms
{
    public partial class testecep : Form
    {
        public testecep()
        {
            InitializeComponent();
        }

        DataTable dadosXML = new DataTable();
        
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

        public async Task<DataTable> GetXmlToDataTable(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                // Faz o download do XML
                string xmlContent = await client.GetStringAsync(url);

                // Cria um novo DataTable
                DataTable dataTable = new DataTable();

                // Carrega o XML
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent);

                // Obtém os nós (ajuste conforme sua estrutura XML)
                XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;

                // Cria colunas baseadas no primeiro nó (se necessário)
                if (nodeList.Count > 0)
                {
                    foreach (XmlNode node in nodeList[0].ChildNodes)
                    {
                        dataTable.Columns.Add(node.Name);
                    }

                    // Preenche as linhas
                    foreach (XmlNode node in nodeList)
                    {
                        DataRow row = dataTable.NewRow();
                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            row[childNode.Name] = childNode.InnerText;
                        }
                        dataTable.Rows.Add(row);
                    }
                }

                return dataTable;
            }
        }

        private async void button1_Click_1(object sender, System.EventArgs e)
        {
            try
            {
                string xmlUrl = "https://viacep.com.br/ws/" + txtcep.Text + "/xml/";
                dadosXML = await GetXmlToDataTable(xmlUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}