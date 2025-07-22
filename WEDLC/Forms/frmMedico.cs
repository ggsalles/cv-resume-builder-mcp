using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WEDLC.Forms
{
    public partial class frmMedico : Form
    {
        DataTable dadosXML = new DataTable();

        public frmMedico()
        {
            InitializeComponent();
        }

        private void frmPaciente_Load(object sender, EventArgs e)
        {
            // Configurações iniciais do formulário, se necessário
            this.DoubleBuffered = true;
        }

        private async void txtCep_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                string xmlUrl = "https://viacep.com.br/ws/" + txtCep.Text + "/xml/";
                dadosXML = await GetXmlToDataTable(xmlUrl);
                if (dadosXML.Rows.Count > 0)
                {
                    txtLogradouro.Text = dadosXML.Rows[1][0].ToString();
                    txtComplemento.Text = dadosXML.Rows[2][0].ToString();
                    txtBairro.Text = dadosXML.Rows[4][0].ToString();
                    txtLocalidade.Text = dadosXML.Rows[5][0].ToString();
                    txtUf.Text = dadosXML.Rows[6][0].ToString();
                }
                else
                {
                    MessageBox.Show("CEP não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtValor.Text, out decimal valor))
            {
                txtValor.Text = valor.ToString("N2");
            }

        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite:
            // - Dígitos numéricos (0-9)
            // - Vírgula (para decimais)
            // - Backspace (para apagar)
            // - Delete (tecla Del)

            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != ',' &&
                e.KeyChar != (char)Keys.Back &&
                e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true; // Rejeita o caractere
            }

            // Permite apenas uma vírgula
            TextBox textBox = (TextBox)sender;
            if (e.KeyChar == ',' && textBox.Text.Contains(','))
            {
                e.Handled = true;
            }

            // Não permite ponto
            if (e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }
    }
}
