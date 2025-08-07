using System;
using System.Data;
using System.Drawing;
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
            //Determina a acao
            cAcao = Acao.CANCELAR;

            controlaBotao();

            //Habilita o campo código
            txtCodigoProntuario.Enabled = true;

            grdDadosPessoais.Enabled = true; //Habilita o grid de dados
            
            this.liberaCampos(false); //Libera os campos para edição

            //Carrega o grid
            //carregaTela();
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
                    mskTelefone.Text = row.Cells[8].Value.ToString();
                    cboSexo.SelectedValue = row.Cells[9].Value.ToString();
                    mskNascimento.Text = row.Cells[10].Value != null ? DateTime.ParseExact(row.Cells[10].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                    cboConvenio.SelectedValue = row.Cells[11].Value.ToString();
                    cboIndPrinc.SelectedValue = row.Cells[12].Value.ToString();
                    cboIndSec.SelectedValue = row.Cells[13].Value.ToString();
                    cboMedico.SelectedValue = row.Cells[14].Value.ToString();
                    cboBeneficente.SelectedValue = row.Cells[15].Value.ToString();
                    txtDataCadastro.Text = row.Cells[16].Value.ToString(); 
                    txtObs.Text = row.Cells[17].Value != null ? row.Cells[17].Value.ToString() : string.Empty;

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
            cboSexo.SelectedIndex = -1; // Reseta o combo de sexo
            mskNascimento.Text = DateTime.Now.ToString("dd/MM/yyyy"); // Reseta o campo data de nascimento para a data atual
            cboConvenio.SelectedIndex = -1; // Reseta o combo de convênio
            cboIndPrinc.SelectedIndex = -1; // Reseta o combo de indicação principal
            cboIndSec.SelectedIndex = -1; // Reseta o combo de indicação secundária
            cboMedico.SelectedIndex = -1; // Reseta o combo de médico
            cboBeneficente.SelectedIndex = -1; // Reseta o combo de beneficiário
            cboFolha.SelectedIndex = -1; // Reseta o combo de especialização consultório
            txtObs.Text = string.Empty; // Limpa o campo de observação


            //Habilita - Desabilita os campos dados
            //txtNome.Enabled = Ativa; //Habilita - Desabilita o campo nome
            txtCep.Enabled = Ativa; //Habilita - Desabilita o campo cep
            txtLogradouro.Enabled = Ativa; //Habilita - Desabilita o campo logradouro
            txtComplemento.Enabled = Ativa; //Habilita - Desabilita o campo complemento
            txtBairro.Enabled = Ativa; //Habilita - Desabilita o campo bairro
            txtLocalidade.Enabled = Ativa; //Habilita - Desabilita o campo localidade
            txtUf.Enabled = Ativa; //Habilita - Desabilita o campo uf
            mskTelefone.Enabled = Ativa; //Habilita - Desabilita o campo telefone
            cboSexo.Enabled = Ativa; //Habilita - Desabilita o campo sexo
            mskNascimento.Enabled = Ativa; //Habilita - Desabilita o campo data de nascimento
            cboConvenio.Enabled = Ativa; //Habilita - Desabilita o campo convênio
            cboIndPrinc.Enabled = Ativa; //Habilita - Desabilita o campo indicação principal
            cboIndSec.Enabled = Ativa; //Habilita - Desabilita o campo indicação secundária
            cboMedico.Enabled = Ativa; //Habilita - Desabilita o campo médico
            cboBeneficente.Enabled = Ativa; //Habilita - Desabilita o campo beneficiário
            txtObs.Enabled = Ativa; //Habilita - Desabilita o campo observação

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
            carregaSimNao();
            carregaFolha();
        }

        public void carregaSexo()
        {
            cboSexo.BeginUpdate(); // Desabilita redesenho durante carregamento

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

            cboSexo.EndUpdate(); // Reabilita redesenho
        }

        public void carregaConvenio()
        {
            cboConvenio.BeginUpdate(); // Desabilita redesenho durante carregamento

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

            cboConvenio.EndUpdate(); // Reabilita redesenho
        }

        public void carregaInidcacao1()
        {
            cboIndPrinc.BeginUpdate(); // Desabilita redesenho durante carregamento

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

            cboIndPrinc.EndUpdate(); // Reabilita redesenho
        }

        public void carregaInidcacao2()
        {
            cboIndSec.BeginUpdate(); // Desabilita redesenho durante carregamento 

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

            cboIndSec.EndUpdate(); // Reabilita redesenho
        }
        public void carregaMedico()
        {

            cboMedico.BeginUpdate(); // Desabilita redesenho durante carregamento

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

            cboMedico.EndUpdate(); // Reabilita redesenho
        }
        public void carregaSimNao()
        {
            cboBeneficente.BeginUpdate(); // Desabilita redesenho durante carregamento

            //Carrega o combo de tipo de folha
            cboBeneficente.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboBeneficente.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cPaciente objcPaciente = new cPaciente();

            DataTable dtSimNao = objcPaciente.SimNao.buscaSimNao();
            DataRow newRow = dtSimNao.NewRow();

            newRow["idsimnao"] = 0; // Defina o valor desejado para a primeira linha
            newRow["descricao"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtSimNao.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            cboBeneficente.DataSource = dtSimNao;
            cboBeneficente.ValueMember = "idsimnao";
            cboBeneficente.DisplayMember = "descricao";

            cboBeneficente.EndUpdate(); // Reabilita redesenho
        }

        public void carregaFolha()
        {
            cboFolha.BeginUpdate(); // Desabilita redesenho durante carregamento

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

            cboFolha.EndUpdate(); // Reabilita redesenho
        }

        private void txtCodigoProntuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void txtCodigoProntuario_KeyUp(object sender, KeyEventArgs e)
        {
            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
            {
                int tipopesquisa = 0; //Pesquisa pelo código   
                int idpaciente = 0; //Código do paciente

                //Limpa campos
                txtNome.Text = string.Empty;

                // Verifica a quantidade de caracteres
                if (txtCodigoProntuario.Text.Length > 0)
                {
                    tipopesquisa = 0;
                    idpaciente = int.Parse(txtCodigoProntuario.Text);

                    this.populaGridDados(tipopesquisa, idpaciente, "");
                    this.configuraGridDados();

                }
            }
        }

        private void populaGridDados(int tipopesquisa, Int32 idpaciente, string nome)
        {
            try
            {
                DataTable dt = this.buscaPaciente(tipopesquisa, idpaciente, nome);

                grdDadosPessoais.DataSource = null;

                //Renomeia as colunas do datatable
                dt.Columns["idpaciente"].ColumnName = "Código";
                dt.Columns["nome"].ColumnName = "Nome";
                dt.Columns["cep"].ColumnName = "CEP";
                dt.Columns["logradouro"].ColumnName = "Logradouro";
                dt.Columns["complemento"].ColumnName = "Complemento";
                dt.Columns["bairro"].ColumnName = "Bairro";
                dt.Columns["localidade"].ColumnName = "Localidade";
                dt.Columns["uf"].ColumnName = "UF";
                dt.Columns["telefone"].ColumnName = "Telefone";
                dt.Columns["idsexo"].ColumnName = "IdSexo";
                dt.Columns["nascimento"].ColumnName = "Nascimento";
                dt.Columns["idconvenio"].ColumnName = "IdConvenio";
                dt.Columns["idindicacao1"].ColumnName = "IdIndicacao1";
                dt.Columns["idindicacao2"].ColumnName = "IdIndicacao2";
                dt.Columns["idmedico"].ColumnName = "IdMedico";
                dt.Columns["idsimnao"].ColumnName = "IdSimNao";
                dt.Columns["datacadastro"].ColumnName = "DataCadastro";
                dt.Columns["observacao"].ColumnName = "Observacao";

                grdDadosPessoais.SuspendLayout();
                grdDadosPessoais.DataSource = dt;
                grdDadosPessoais.ResumeLayout();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar populaGrid!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void configuraGridDados()
        {
            try
            {
                // Ajustando o tamanho das colunas e ocultando as que não são necessárias
                grdDadosPessoais.Columns[0].Width = 80; //ID
                grdDadosPessoais.Columns[1].Width = 350; //Nome
                grdDadosPessoais.Columns[8].Width = 100; //Telefone

                // Oculta ou exibe as colunas que não são necessárias
                grdDadosPessoais.Columns[2].Visible = false; //Cep
                grdDadosPessoais.Columns[3].Visible = false; //Logradouro
                grdDadosPessoais.Columns[4].Visible = false; //Complemento
                grdDadosPessoais.Columns[5].Visible = false; //Bairro
                grdDadosPessoais.Columns[6].Visible = false; //Localidade
                grdDadosPessoais.Columns[7].Visible = false; //Uf
                grdDadosPessoais.Columns[8].Visible = true; //Telefone
                grdDadosPessoais.Columns[9].Visible = false; //IdSexo
                grdDadosPessoais.Columns[10].Visible = false; //Nascimento
                grdDadosPessoais.Columns[11].Visible = false; //IdConvenio
                grdDadosPessoais.Columns[12].Visible = false; //IdIndicacao1
                grdDadosPessoais.Columns[13].Visible = false; //IdIndicacao2
                grdDadosPessoais.Columns[14].Visible = false; //IdMedico
                grdDadosPessoais.Columns[15].Visible = false; //IdSimNao
                grdDadosPessoais.Columns[16].Visible = false; //DataCadastro
                grdDadosPessoais.Columns[17].Visible = false; //Obs

                // Desabilita a edição da coluna
                grdDadosPessoais.Columns[0].ReadOnly = true;
                grdDadosPessoais.Columns[1].ReadOnly = true;

                // Configurando outras propriedades
                grdDadosPessoais.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
                grdDadosPessoais.MultiSelect = false; // Impede seleção múltipla
                grdDadosPessoais.AllowUserToAddRows = false; // Impede adição de novas linhas
                grdDadosPessoais.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
                grdDadosPessoais.CurrentCell = null; // Desmarca a célula atual
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar configurar o grid de dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private DataTable buscaPaciente(int tipopesquisa, Int32 idpaciente, string nome)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cPaciente objcPaciente = new cPaciente();

                objcPaciente.TipoPesquisa = tipopesquisa; //Tipo de pesquisa
                objcPaciente.IdPaciente = idpaciente; //Código da especialização
                objcPaciente.Nome = nome; //Nome da especialização

                dtAux = objcPaciente.buscaPaciente();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar o paciente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            int tipopesquisa = 1; //Código que pesquisa pelo nome
            string nome = string.Empty; //Nome da especialização

            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
            {
                //Limpa campos
                txtCodigoProntuario.Text = string.Empty;
                txtNome.Text = string.Empty;

                nome = txtNome.Text;

                this.populaGridDados(tipopesquisa, 0, nome);
                this.configuraGridDados();
            }
        }
    }
}
