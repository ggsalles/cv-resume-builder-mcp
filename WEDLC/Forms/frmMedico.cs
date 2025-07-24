using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmMedico : Form
    {
        public enum Acao
        {
            INSERT = 0,
            UPDATE = 1,
            DELETE = 2,
            SAVE = 3,
            CANCELAR = 4,
        }

        // Variável para armazenar a ação atual do formulário
        public Acao cAcao = Acao.CANCELAR;

        // Datatable para armazenar os dados do XML
        DataTable dadosXML = new DataTable();

        // Create a ToolTip component
        ToolTip toolTip1 = new ToolTip();


        public frmMedico()
        {
            InitializeComponent();

            // Configurações do ToolTip
            toolTip1.AutoPopDelay = 5000; // Tempo que o ToolTip permanece visível
            toolTip1.InitialDelay = 500; // Tempo antes do ToolTip aparecer
            toolTip1.ReshowDelay = 500; // Tempo entre as aparições do ToolTip
            toolTip1.ShowAlways = true; // Sempre mostrar o ToolTip
            toolTip1.SetToolTip(txtCep, "Digite o CEP sem pontos ou traços. Exemplo: 12345678");
            toolTip1.SetToolTip(txtCepConsultorio, "Digite o CEP sem pontos ou traços. Exemplo: 12345678");
            toolTip1.SetToolTip(txtMediaConsultorio, "Digite o valor médio da consulta. Exemplo: 150,00");
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
                if (txtCep.Text.ToString().Trim().Length > 0 && txtCep.Text.ToString().Trim().Length == 8)
                {
                    string xmlUrl = "https://viacep.com.br/ws/" + txtCep.Text + "/xml/";
                    dadosXML = await GetXmlToDataTable(xmlUrl);
                    if (dadosXML.Rows.Count > 1)
                    {
                        txtLogradouro.Text = dadosXML.Rows[1][0].ToString();
                        txtComplemento.Text = dadosXML.Rows[2][0].ToString();
                        txtBairro.Text = dadosXML.Rows[4][0].ToString();
                        txtLocalidade.Text = dadosXML.Rows[5][0].ToString();
                        txtUf.Text = dadosXML.Rows[6][0].ToString();
                    }
                    else
                    {
                        txtLogradouro.Text = string.Empty;
                        txtComplemento.Text = string.Empty;
                        txtBairro.Text = string.Empty;
                        txtLocalidade.Text = string.Empty;
                        txtUf.Text = string.Empty;

                        MessageBox.Show("CEP não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
            if (decimal.TryParse(txtMediaConsultorio.Text, out decimal valor))
            {
                txtMediaConsultorio.Text = valor.ToString("N2");
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
        private async void txtCepConsultorio_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (txtCepConsultorio.Text.ToString().Trim().Length > 0 && txtCepConsultorio.Text.ToString().Trim().Length == 8)
                {
                    string xmlUrl = "https://viacep.com.br/ws/" + txtCepConsultorio.Text + "/xml/";
                    dadosXML = await GetXmlToDataTable(xmlUrl);
                    if (dadosXML.Rows.Count > 1)
                    {
                        txtLogradouroConsultorio.Text = dadosXML.Rows[1][0].ToString();
                        txtComplementoConsultorio.Text = dadosXML.Rows[2][0].ToString();
                        txtBairroConsultorio.Text = dadosXML.Rows[4][0].ToString();
                        txtLocalidadeConsultorio.Text = dadosXML.Rows[5][0].ToString();
                        txtUfConsultorio.Text = dadosXML.Rows[6][0].ToString();
                    }
                    else
                    {
                        txtLogradouroConsultorio.Text = string.Empty;
                        txtComplementoConsultorio.Text = string.Empty;
                        txtBairroConsultorio.Text = string.Empty;
                        txtLocalidadeConsultorio.Text = string.Empty;
                        txtUfConsultorio.Text = string.Empty;

                        MessageBox.Show("CEP não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != (char)Keys.Back &&
                e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true; // Rejeita o caractere
            }

            // Não permite ponto
            if (e.KeyChar == '.')
            {
                e.Handled = true;
            }

        }

        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            int tipopesquisa = 2; //Código que pesquisa pelo nome  
            string nome = string.Empty; //Código da especialização

            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
            {

                //Limpa campo
                txtCodigoMedico.Text = string.Empty;

                if (txtNome.Text.Length > 0)
                {
                    tipopesquisa = 2;
                    nome = txtNome.Text;
                }
                else
                {
                    tipopesquisa = 2;
                    nome = "!@#$%"; // Se não houver nome, define tipopesquisa como 0 para retornar não retornar ninguém
                }

                this.populaGrid(tipopesquisa, 0, nome);
            }
        }

        private void txtCodigoMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void txtCodigoMedico_KeyUp(object sender, KeyEventArgs e)
        {
            //Determina a acao e valida se vai efetuar a pesquisa
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
            {
                int tipopesquisa = 0; //Código que retorna todo select   
                int idmedico = 0; //Código da especialização

                //Limpa campo
                txtNome.Text = string.Empty;

                // Verifica a quantidade de caracteres
                if (txtCodigoMedico.Text.Length > 0)
                {
                    tipopesquisa = 1;
                    idmedico = int.Parse(txtCodigoMedico.Text);
                }
                else
                {
                    tipopesquisa = 1;
                    idmedico = 0; // Se não houver código, define tipopesquisa como 0 para retornar não retornar ninguém
                }
                this.populaGrid(tipopesquisa, idmedico, null);
            }
        }

        private void grdDadosPessoais_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se a tecla pressionada é a barra de espaço
            if (e.KeyChar == (char)Keys.Space)
            {
                // Cancela o evento para impedir que a barra de espaço seja registrada
                e.Handled = true;
            }
        }

        private void populaGrid(int tipopesquisa, int idmedico, string nome)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = this.buscaMedico(tipopesquisa, idmedico, nome);

                grdDadosPessoais.DataSource = null;

                //Renomeia as colunas do datatable
                dt.Columns["idmedico"].ColumnName = "Código";
                dt.Columns["nome"].ColumnName = "Nome";
                dt.Columns["cep"].ColumnName = "CEP";
                dt.Columns["logradouro"].ColumnName = "Logradouro";
                dt.Columns["complemento"].ColumnName = "Complemento";
                dt.Columns["bairro"].ColumnName = "Bairro";
                dt.Columns["localidade"].ColumnName = "Localidade";
                dt.Columns["uf"].ColumnName = "UF";
                dt.Columns["pais"].ColumnName = "País";
                dt.Columns["telefone"].ColumnName = "Telefone";

                grdDadosPessoais.SuspendLayout();
                grdDadosPessoais.DataSource = dt;
                configuraGridDados();
                grdDadosPessoais.ResumeLayout();

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar populaGrid!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private DataTable buscaMedico(int tipopesquisa, int idmedico, string nome)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cMedico objMedico = new cMedico();
                objMedico.IdMedico = idmedico;
                objMedico.Nome = nome;
                objMedico.TipoPesquisa = tipopesquisa;

                dtAux = objMedico.buscaMedico();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar a especialização!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private void configuraGridDados()
        {
            //// Ajustando o tamanho das colunas e ocultando as que não são necessárias
            //grdDadosPessoais.Columns[0].Width = 80; //ID
            //grdDadosPessoais.Columns[1].Width = 200; //Nome
            //grdDadosPessoais.Columns[2].Width = 50; //CEP
            //grdDadosPessoais.Columns[3].Width = 200; //Logradouro
            //grdDadosPessoais.Columns[4].Width = 100; //Complemento
            //grdDadosPessoais.Columns[5].Width = 100; //Bairro
            //grdDadosPessoais.Columns[6].Width = 100; //Localidade
            //grdDadosPessoais.Columns[7].Width = 50; //UF
            //grdDadosPessoais.Columns[8].Width = 100; //País
            //grdDadosPessoais.Columns[9].Width = 100; //Telefone

            grdDadosPessoais.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Desabilita a edição da coluna
            grdDadosPessoais.Columns[0].ReadOnly = true;
            grdDadosPessoais.Columns[1].ReadOnly = true;
            grdDadosPessoais.Columns[2].ReadOnly = true;
            grdDadosPessoais.Columns[3].ReadOnly = true;
            grdDadosPessoais.Columns[4].ReadOnly = true;
            grdDadosPessoais.Columns[5].ReadOnly = true;
            grdDadosPessoais.Columns[6].ReadOnly = true;
            grdDadosPessoais.Columns[7].ReadOnly = true;
            grdDadosPessoais.Columns[8].ReadOnly = true;
            grdDadosPessoais.Columns[9].ReadOnly = true;

            grdDadosPessoais.Columns[10].Visible = false;
            grdDadosPessoais.Columns[11].Visible = false;
            grdDadosPessoais.Columns[12].Visible = false;
            grdDadosPessoais.Columns[13].Visible = false;
            grdDadosPessoais.Columns[14].Visible = false;
            grdDadosPessoais.Columns[15].Visible = false;
            grdDadosPessoais.Columns[16].Visible = false;
            grdDadosPessoais.Columns[17].Visible = false;
            grdDadosPessoais.Columns[18].Visible = false;
            grdDadosPessoais.Columns[19].Visible = false;

            // Configurando outras propriedades
            grdDadosPessoais.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
            grdDadosPessoais.MultiSelect = false; // Impede seleção múltipla
            grdDadosPessoais.AllowUserToAddRows = false; // Impede adição de novas linhas
            grdDadosPessoais.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
            grdDadosPessoais.CurrentCell = null; // Desmarca a célula atual
        }

          private void controlaBotao()
        {
            //Se clicou em novo
            if (cAcao == Acao.INSERT)
            {
                btnNovo.Enabled = false;
                btnGravar.Enabled = true;
                btnCancelar.Enabled = true;
                btnExcluir.Enabled = false;
            }

            //Se clicou no grid
            if (cAcao == Acao.UPDATE)
            {
                btnNovo.Enabled = false;
                btnGravar.Enabled = true;
                btnCancelar.Enabled = true;
                btnExcluir.Enabled = false;
            }

            //Se clicou no grid
            if (cAcao == Acao.CANCELAR)
            {
                btnNovo.Enabled = true;
                btnGravar.Enabled = false;
                btnCancelar.Enabled = false;
                btnExcluir.Enabled = false;
            }

        }

        private void liberaCampos(bool Ativa)
        {
            //Limpa os campos
            txtCodigoMedico.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCep.Text = string.Empty;
            txtLogradouro.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtLocalidade.Text = string.Empty;
            txtUf.Text = string.Empty;
            txtPais.Text = string.Empty;
            mskTelefone.Text = string.Empty;

            //Habilita os campos
            txtNome.Enabled = Ativa; //Habilita o campo nome
            txtCep.Enabled = Ativa; //Habilita o campo cep
            txtLogradouro.Enabled = Ativa; //Habilita o campo logradouro
            txtComplemento.Enabled = Ativa; //Habilita o campo complemento
            txtBairro.Enabled = Ativa; //Habilita o campo bairro
            txtLocalidade.Enabled = Ativa; //Habilita o campo localidade
            txtUf.Enabled = Ativa; //Habilita o campo uf
            txtPais.Enabled = Ativa; //Habilita o campo pais
            mskTelefone.Enabled = Ativa; //Habilita o campo telefone

        }

        private void grdDadosPessoais_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //Determina a acao
                    cAcao = Acao.UPDATE;

                    controlaBotao(); //libera botões 
                    liberaCampos(true); //Libera os campos para edição

                    txtCodigoMedico.Enabled = false; //Desabilita o campo código
                    grdDadosPessoais.Enabled = false; //Desabilita o grid de dados

                    txtCodigoMedico.Text = grdDadosPessoais.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtNome.Text = grdDadosPessoais.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtCep.Text = grdDadosPessoais.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtLogradouro.Text = grdDadosPessoais.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtComplemento.Text = grdDadosPessoais.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtBairro.Text = grdDadosPessoais.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtLocalidade.Text = grdDadosPessoais.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtUf.Text = grdDadosPessoais.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtPais.Text = grdDadosPessoais.Rows[e.RowIndex].Cells[8].Value.ToString();
                    mskTelefone.Text = grdDadosPessoais.Rows[e.RowIndex].Cells[9].Value.ToString();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao selecionar um item na grid dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Determina a acao
            cAcao = Acao.CANCELAR;

            controlaBotao();
            liberaCampos(false); //Libera os campos para edição

            txtCodigoMedico.Enabled = true; //Habilita o campo código
            txtNome.Enabled = true; // Habilita o campo nome
            grdDadosPessoais.Enabled = true; //Habilita o grid de dados
            grdDadosPessoais.DataSource = null; //Limpa o grid de dados

            //Desmarca a seleção do grid
            grdDadosPessoais.CurrentCell = null;

        }
    }
}
