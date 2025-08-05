using System;
using System.Data;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmPaciente : Form
    {
        DataTable dadosXML = new DataTable();

        // Create a ToolTip component
        ToolTip toolTip1 = new ToolTip();

        public Acao cAcao = Acao.CANCELAR;

        public enum Acao
        {
            INSERT = 0,
            UPDATE = 1,
            DELETE = 2,
            SAVE = 3,
            CANCELAR = 4,
        }

        public frmPaciente()
        {
            InitializeComponent();

            // Configurações do ToolTip
            toolTip1.AutoPopDelay = 5000; // Tempo que o ToolTip permanece visível
            toolTip1.InitialDelay = 500; // Tempo antes do ToolTip aparecer
            toolTip1.ReshowDelay = 500; // Tempo entre as aparições do ToolTip
            toolTip1.ShowAlways = true; // Sempre mostrar o ToolTip
            toolTip1.SetToolTip(txtCep, "Digite o CEP sem pontos ou traços. Exemplo: 12345678");
        }

        private void frmPaciente_Load(object sender, EventArgs e)
        {
            // Configurações iniciais do formulário, se necessário
            this.DoubleBuffered = true;
            carregaCombo(); // Carrega os combos 

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void grdDadosPessoais_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {

                    // Otimização: Acessa a linha apenas uma vez
                    DataGridViewRow row = grdDadosPessoais.Rows[e.RowIndex];

                    //Determina a acao
                    cAcao = Acao.UPDATE;

                    controlaBotao(); //libera botões 
                    liberaCampos(true); //Libera os campos para edição

                    txtCodigoProntuario.Enabled = false; //Desabilita o campo código
                    grdDadosPessoais.Enabled = false; //Desabilita o grid de dados

                    SuspendLayout();

                    //Preenche os campos com os dados da linha selecionada
                    txtCodigoProntuario.Text = row.Cells[0].Value.ToString();
                    txtNome.Text = row.Cells[1].Value.ToString();
                    txtCep.Text = row.Cells[2].Value.ToString();
                    txtLogradouro.Text = row.Cells[3].Value.ToString();
                    txtComplemento.Text = row.Cells[4].Value.ToString();
                    txtBairro.Text = row.Cells[5].Value.ToString();
                    txtLocalidade.Text = row.Cells[6].Value.ToString();
                    txtUf.Text = row.Cells[7].Value.ToString();
                    mskTelefone.Text = row.Cells[9].Value.ToString();

                    cboSexo.SelectedIndex = cboSexo.FindStringExact(row.Cells[8].Value.ToString());

                    // Verifica se o campo "Data de Nascimento" não é nulo ou vazio antes de converter
                    if (row.Cells[10].Value != null && !string.IsNullOrEmpty(row.Cells[10].Value.ToString()))
                    {
                        mskNascimento.Text = DateTime.ParseExact(row.Cells[10].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                    }
                    //else
                    //{
                    //    mskNascimento.Text = DateTime.Now.ToString("dd/MM/yyyy"); // Define uma data padrão se estiver vazia
                    //}

                    // Carrega combo folha
                    //CarregaComboEspecialidadeMedico();

                    // Popular grid folha
                    //this.populaGridMedicoEspecialidade(int.Parse(txtCodigoMedico.Text));

                    //Determina a acao
                    cAcao = Acao.UPDATE;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao selecionar um item na grid dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                ResumeLayout();
            }
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
                btnIncluiFolha.Enabled = true;
                btnExcluiFolha.Enabled = true;
            }

            //Se clicou no grid
            if (cAcao == Acao.UPDATE)
            {
                btnNovo.Enabled = false;
                btnGravar.Enabled = true;
                btnCancelar.Enabled = true;
                btnExcluir.Enabled = false;
                btnIncluiFolha.Enabled = true;
                btnExcluiFolha.Enabled = true;
            }

            //Se clicou no grid
            if (cAcao == Acao.CANCELAR)
            {
                btnNovo.Enabled = true;
                btnGravar.Enabled = false;
                btnCancelar.Enabled = false;
                btnExcluir.Enabled = false;
                btnIncluiFolha.Enabled = false;
                btnExcluiFolha.Enabled = false;
            }
        }

        private void liberaCampos(bool Ativa)
        {
            //Limpa os campos
            txtCodigoProntuario.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCep.Text = string.Empty;
            txtLogradouro.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtLocalidade.Text = string.Empty;
            txtUf.Text = string.Empty;
            mskTelefone.Text = string.Empty;

            //Habilita - Desabilita os campos dados
            //txtNome.Enabled = Ativa; //Habilita - Desabilita o campo nome
            txtCep.Enabled = Ativa; //Habilita - Desabilita o campo cep
            txtLogradouro.Enabled = Ativa; //Habilita - Desabilita o campo logradouro
            txtComplemento.Enabled = Ativa; //Habilita - Desabilita o campo complemento
            txtBairro.Enabled = Ativa; //Habilita - Desabilita o campo bairro
            txtLocalidade.Enabled = Ativa; //Habilita - Desabilita o campo localidade
            txtUf.Enabled = Ativa; //Habilita - Desabilita o campo uf
            mskTelefone.Enabled = Ativa; //Habilita - Desabilita o campo telefone

            //Habilita - Desabilita Especialização
            cboFolha.Enabled = Ativa; //Habilita - Desabilita o campo especialização consultório
            cboFolha.SelectedIndex = -1; // Reseta o combo de especialização consultório
            grdFolha.Enabled = Ativa; //Habilita - Desabilita o grid de especialização consultório
            grdFolha.DataSource = null; //Limpa o grid de especialização consultório

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Determina a acao
            cAcao = Acao.INSERT;

            controlaBotao(); //libera botões 
            liberaCampos(true); //Libera os campos para edição
            txtCodigoProntuario.Enabled = false; //Desabilita o campo código
            grdDadosPessoais.Enabled = false; //Desabilita o grid de dados
            grdDadosPessoais.DataSource = null;
            //populaGridMedicoEspecialidade(0); // Cria a estrutura do grid de especialização consultório
            //CarregaComboEspecialidadeMedico(); //Carrega o combo de especialização consultório
            txtNome.Focus(); //Foca no campo nome
        }

        public void carregaCombo()
        {
            carregaSexo();
            carregaConvenio();
            carregaInidcacao1();
            carregaInidcacao2();
            carregaMedico();
            carregaFolha();
        }

        public void carregaSexo()
        {
            //Carrega o combo de tipo de folha
            cboSexo.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboSexo.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cPaciente objcPaciente = new cPaciente();
            DataTable dtSexo = objcPaciente.Sexo.buscaSexo();
            DataRow newRow = dtSexo.NewRow();

            newRow["idsexo"] = 0; // Defina o valor desejado para a primeira linha
            newRow["descricao"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtSexo.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            cboSexo.DataSource = dtSexo;
            cboSexo.ValueMember = "idsexo";
            cboSexo.DisplayMember = "descricao";
        }

        public void carregaConvenio()
        {
            //Carrega o combo de tipo de folha
            cboConvenio.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboConvenio.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cPaciente objcPaciente = new cPaciente();
            objcPaciente.Convenio.TipoPesquisa = 4;  // Pesquisa buscando sigla + nome concatenado
            objcPaciente.Convenio.IdConvenio = 0; // ID 0 para buscar todos os convênios
            DataTable dtConvenio = objcPaciente.Convenio.buscaConvenio();
            DataRow newRow = dtConvenio.NewRow();

            newRow["idconvenio"] = 0; // Defina o valor desejado para a primeira linha
            newRow["nome"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtConvenio.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            cboConvenio.DataSource = dtConvenio;
            cboConvenio.ValueMember = "idconvenio";
            cboConvenio.DisplayMember = "nome";
        }

        public void carregaInidcacao1()
        {
            //Carrega o combo de tipo de folha
            cboIndPrinc.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboIndPrinc.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cPaciente objcPaciente = new cPaciente();
            objcPaciente.Indicacao.TipoPesquisa = 0;  // Pesquisa buscando sigla + nome concatenado
            objcPaciente.Indicacao.IdIndicacao = 0; // ID 0 para buscar todos os convênios'
            objcPaciente.Indicacao.Nome = string.Empty; // Nome vazio para buscar todos os convênios    

            DataTable dtIndicacao = objcPaciente.Indicacao.buscaIndicacao();
            DataRow newRow = dtIndicacao.NewRow();

            newRow["idindicacao"] = 0; // Defina o valor desejado para a primeira linha
            newRow["nome"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtIndicacao.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            cboIndPrinc.DataSource = dtIndicacao;
            cboIndPrinc.ValueMember = "idindicacao";
            cboIndPrinc.DisplayMember = "nome";
        }

        public void carregaInidcacao2()
        {
            //Carrega o combo de tipo de folha
            cboIndSec.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboIndSec.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cPaciente objcPaciente = new cPaciente();
            objcPaciente.Indicacao.TipoPesquisa = 0;  // Pesquisa buscando sigla + nome concatenado
            objcPaciente.Indicacao.IdIndicacao = 0; // ID 0 para buscar todos os convênios'
            objcPaciente.Indicacao.Nome = string.Empty; // Nome vazio para buscar todos os convênios    

            DataTable dtIndicacao = objcPaciente.Indicacao.buscaIndicacao();
            DataRow newRow = dtIndicacao.NewRow();

            newRow["idindicacao"] = 0; // Defina o valor desejado para a primeira linha
            newRow["nome"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtIndicacao.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            cboIndSec.DataSource = dtIndicacao;
            cboIndSec.ValueMember = "idindicacao";
            cboIndSec.DisplayMember = "nome";
        }
        public void carregaMedico()
        {
            //Carrega o combo de tipo de folha
            cboMedico.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboMedico.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cPaciente objcPaciente = new cPaciente();
            objcPaciente.Medico.TipoPesquisa = 3;  // Pesquisa o médico sem where
            objcPaciente.Medico.IdMedico = 0; 
            objcPaciente.Medico.Nome = string.Empty; // 

            DataTable dtMedico = objcPaciente.Medico.buscaMedico();
            DataRow newRow = dtMedico.NewRow();

            newRow["idmedico"] = 0; // Defina o valor desejado para a primeira linha
            newRow["nome"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtMedico.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            cboMedico.DataSource = dtMedico;
            cboMedico.ValueMember = "idmedico";
            cboMedico.DisplayMember = "nome";
        }
        public void carregaSimNao()
        {
            //Carrega o combo de tipo de folha
            cboBeneficente.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboBeneficente.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cPaciente objcPaciente = new cPaciente();
            objcPaciente.Medico.TipoPesquisa = 0;  // Pesquisa buscando sigla + nome concatenado
            objcPaciente.Medico.IdMedico = 0; // ID 0 para buscar todos os convênios'
            objcPaciente.Medico.Nome = string.Empty; // Nome vazio para buscar todos os convênios    

            DataTable dtSimNao = objcPaciente.SimNao.buscaSimNao();
            DataRow newRow = dtSimNao.NewRow();

            newRow["idsimnao"] = 0; // Defina o valor desejado para a primeira linha
            newRow["descricao"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtSimNao.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            cboBeneficente.DataSource = dtSimNao;
            cboBeneficente.ValueMember = "idsimnao";          
            cboBeneficente.DisplayMember = "descricao";
        }

        public void carregaFolha()
        {
            //Carrega o combo de tipo de folha
            cboFolha.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboFolha.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cPaciente objcPaciente = new cPaciente();
            DataTable dtFolha = objcPaciente.Folha.buscaFolha(4, 0, "", "");
            DataRow newRow = dtFolha.NewRow();

            newRow["idfolha"] = 0; // Defina o valor desejado para a primeira linha
            newRow["nome"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtFolha.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            cboFolha.DataSource = dtFolha;
            cboFolha.ValueMember = "idfolha";
            cboFolha.DisplayMember = "nome";
        }
    }
}
