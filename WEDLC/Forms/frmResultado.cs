using System;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmResultado : Form
    {
        DataTable dadosXML = new DataTable();

        // Create a ToolTip component
        ToolTip toolTip1 = new ToolTip();

        public Acao cAcao = Acao.CANCELAR;
        public DataTable dtGrdPacienteFolha;
        public bool ValidaFolha = false;
        public DataTable dtComboFolha;
        public int NumeroLinha = -1; // Variável para controlar a linha do grid da folha

        public enum Acao
        {
            INSERT = 0,
            UPDATE = 1,
            DELETE = 2,
            SAVE = 3,
            CANCELAR = 4,
        }

        public frmResultado()
        {
            InitializeComponent();

            // Configurações do ToolTip
            toolTip1.AutoPopDelay = 5000; // Tempo que o ToolTip permanece visível
            toolTip1.InitialDelay = 500; // Tempo antes do ToolTip aparecer
            toolTip1.ReshowDelay = 500; // Tempo entre as aparições do ToolTip
            toolTip1.ShowAlways = true; // Sempre mostrar o ToolTip
            //toolTip1.SetToolTip(txtCep, "Digite o CEP sem pontos ou traços. Exemplo: 12345678");
        }

        private void frmPaciente_Load(object sender, EventArgs e)
        {
            // Configurações iniciais do formulário, se necessário
            this.DoubleBuffered = true;
            carregaCombo(); // Carrega os combos 
            btnCancelar_Click(sender, e); //Simula o clique no botão cancelar   

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
            txtCodigoResultado.Enabled = true;

            //Reseta grid Dados Pessoais
            grdDadosPessoais.Enabled = true; //Habilita o grid de dados
            grdDadosPessoais.DataSource = null; //Limpa o grid de dados

            this.liberaCampos(false); //Libera os campos para edição
            txtCodigoResultado.Focus(); //Foca no campo código prontuário

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

                    txtCodigoResultado.Enabled = false; //Desabilita o campo código
                    grdDadosPessoais.Enabled = false; //Desabilita o grid de dados

                    SuspendLayout();

                    //Preenche os campos com os dados da linha selecionada
                    txtCodigoResultado.Text = row.Cells[0].Value.ToString();
                    txtNome.Text = row.Cells[1].Value.ToString();


                    if (buscaDadosPacienteFolha() == false)
                    {
                        return;
                    }

                    txtNome.Focus(); //Foca no campo nome

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
            txtCodigoResultado.Text = string.Empty;
            txtNome.Text = string.Empty;
          
            //Habilita - Desabilita os campos dados
            //txtCep.Enabled = Ativa; //Habilita - Desabilita o campo cep
            //txtLogradouro.Enabled = Ativa; //Habilita - Desabilita o campo logradouro
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Determina a acao
            cAcao = Acao.INSERT;

            controlaBotao(); //libera botões 
            liberaCampos(true); //Libera os campos para edição
            txtCodigoResultado.Enabled = false; //Desabilita o campo código
            grdDadosPessoais.Enabled = false; //Desabilita o grid de dados
            grdDadosPessoais.DataSource = null;

            if (buscaDadosPacienteFolha() == false)
            {
                return;
            }
            txtNome.Focus(); //Foca no campo nome
        }

        public void carregaCombo()
        {
            carregaFolha();
        }
      
        public void carregaFolha()
        {
            //cboFolha.BeginUpdate(); // Desabilita redesenho durante carregamento

            ////Carrega o combo de tipo de folha
            //cboFolha.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            //cboFolha.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            //// Corrigido: obtém DataTable corretamente
            //dtComboFolha = new cPaciente().Folha.buscaFolha(4, 0, "", ""); // Busca todas as folhas

            //DataRow newRow = dtComboFolha.NewRow();

            //newRow["idfolha"] = 0; // Defina o valor desejado para a primeira linha
            //newRow["siglanome"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            //dtComboFolha.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            //cboFolha.DataSource = dtComboFolha;
            //cboFolha.ValueMember = "idfolha";
            //cboFolha.DisplayMember = "siglanome";
            //cboFolha.SelectedIndex = 0; // Seleciona a primeira linha (opção "Selecione...")

            //cboFolha.EndUpdate(); // Reabilita redesenho
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
                if (txtCodigoResultado.Text.Length > 0)
                {
                    tipopesquisa = 0;
                    idpaciente = int.Parse(txtCodigoResultado.Text);
                }
                else
                {
                    tipopesquisa = 0;
                    idpaciente = 0;
                }

                this.populaGridDados(tipopesquisa, idpaciente, "");
                this.configuraGridDados();
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
                txtCodigoResultado.Text = string.Empty; //Limpa o campo código prontuário

                if (txtNome.Text.Length > 0)
                {
                    nome = txtNome.Text;
                }
                else
                {
                    nome = "!@#$%"; // Se não houver nome, define tipopesquisa como 0 para retornar não retornar ninguém
                }

                //Limpa campos
                txtCodigoResultado.Text = string.Empty;
                this.populaGridDados(tipopesquisa, 0, nome);
                this.configuraGridDados();
            }
        }

        private DataTable buscaPacienteFolha(Int32 idpaciente)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cPaciente objcPaciente = new cPaciente();

                objcPaciente.IdPaciente = idpaciente; //Código do paciente
                dtAux = objcPaciente.buscaPacienteFolha();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar a folha do paciente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private bool buscaDadosPacienteFolha()
        {
            //try
            //{
            //    int idpaciente = 0;
            
            //    if (txtCodigoProntuario.Text.ToString().Trim().Length > 0)
            //    {
            //        //Busca os dados do paciente folha
            //        idpaciente = int.Parse(txtCodigoProntuario.Text);
            //    }

            //    dtGrdPacienteFolha = new DataTable();
            //    dtGrdPacienteFolha = this.buscaPacienteFolha(idpaciente);

            //    grdFolha.DataSource = null;

            //    //Renomeia as colunas do datatable
            //    dtGrdPacienteFolha.Columns["idpacientefolha"].ColumnName = "IdPacienteFolha";
            //    dtGrdPacienteFolha.Columns["idpaciente"].ColumnName = "IdPaciente";
            //    dtGrdPacienteFolha.Columns["idfolha"].ColumnName = "IdFolha";
            //    dtGrdPacienteFolha.Columns["sigla"].ColumnName = "Sigla";
            //    dtGrdPacienteFolha.Columns["nome"].ColumnName = "Nome";

            //    grdFolha.SuspendLayout();
            //    grdFolha.DataSource = dtGrdPacienteFolha;
            //    grdFolha.ResumeLayout();

            //    // Ajustando o tamanho das colunas e ocultando as que não são necessárias
            //    grdFolha.Columns[3].Width = 80; //ID
            //    grdFolha.Columns[4].Width = 350; //Nome

            //    // Oculta ou exibe as colunas que não são necessárias
            //    grdFolha.Columns[0].Visible = false; //IdPacienteFolha
            //    grdFolha.Columns[1].Visible = false; //IdPaciente
            //    grdFolha.Columns[2].Visible = false; //IdFolha

            //    // Desabilita a edição da coluna
            //    grdFolha.Columns[3].ReadOnly = true;
            //    grdFolha.Columns[4].ReadOnly = true;

            //    // Configurando outras propriedades
            //    grdFolha.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
            //    grdFolha.MultiSelect = false; // Impede seleção múltipla
            //    grdFolha.AllowUserToAddRows = false; // Impede adição de novas linhas
            //    grdFolha.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
            //    grdFolha.CurrentCell = null; // Desmarca a célula atual

                return true;
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Erro ao tentar popular a folha do paciente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
        }

        private void btnIncluiFolha_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //Valida dados a ser inserido

            //    if (ValidaFolha == true)
            //    {
            //        //Se tiver pelo menos uma linha no grid especialidades...
            //        if (grdFolha.Rows.Count > 0)
            //        {
            //            // Verifica se já existe a especialização no grid
            //            bool duplicado = ValidaDuplicidadePacienteFolha(cboFolha.SelectedValue.ToString());

            //            // Verifica se a especialização já foi adicionada'
            //            if (duplicado)
            //            {
            //                MessageBox.Show("Folha já foi adicionada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return;
            //            }
            //        }

            //        // Adicionando uma nova linha
            //        DataRow novaLinha = dtGrdPacienteFolha.NewRow();

            //        //Como ainda não tem o ID...
            //        if (cAcao == Acao.INSERT)
            //        {
            //            novaLinha["idpaciente"] = grdFolha.Rows.Count + 1; // Atribui um ID temporário baseado na contagem de linhas
            //        }
            //        else
            //        {
            //            novaLinha["idpaciente"] = txtCodigoProntuario.Text;
            //        }
            //        novaLinha["idfolha"] = dtComboFolha.Rows[cboFolha.SelectedIndex][0]; //id folha
            //        novaLinha["sigla"] = dtComboFolha.Rows[cboFolha.SelectedIndex][1]; //sigla
            //        novaLinha["nome"] = dtComboFolha.Rows[cboFolha.SelectedIndex][2]; //nome

            //        dtGrdPacienteFolha.Rows.Add(novaLinha);
            //        grdFolha.DataSource = dtGrdPacienteFolha;
            //    }

            //    else
            //    {
            //        MessageBox.Show("Selecione um item válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }


            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Erro ao tentar inserir especialização!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
        }

        private void btnExcluiFolha_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtComboFolha.Rows.Count == 1)
                {
                    MessageBox.Show("Não é permitido deixar a folha do paciente sem pelo menos uma folha!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verifica se uma linha foi selecionada
                if (NumeroLinha >= 0)
                {
                    // Obter a linha que deseja remover (por exemplo, a linha selecionada em um DataGridView)
                    DataRow rowToDelete = dtGrdPacienteFolha.Rows[NumeroLinha]; // Remove a terceira linha
                    dtGrdPacienteFolha.Rows.Remove(rowToDelete);
                    //grdFolha.DataSource = dtGrdPacienteFolha;
                }
                else
                {
                    MessageBox.Show("Selecione uma especialização para excluir!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar excluir especialização!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private bool FiltrarComboFolha(string texto)
        {
            //Se infomrou alguma coisa e for diferente de selecione...
            if (texto.Length > 0 && texto != "SELECIONE...")
            {
                //DataTable dt = (DataTable)cboFolha.DataSource;
                //ValidaFolha = dt.AsEnumerable().Any(row => row.Field<string>("siglanome") == texto);
                if (ValidaFolha == false)
                {
                    MessageBox.Show("Selecione um item válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //cboFolha.Focus();
                    //cboFolha.Text = string.Empty;
                    return false; // Retorna falso se o item não existir
                }
            }

            else
            {
                ValidaFolha = false; // Se não informou nada, não valida
                //cboFolha.SelectedValue = 0; // Reseta o valor selecionado para 0 (Selecione...)
            }

            return true; // Retorna verdadeiro se o item existir
        }

        private bool ValidaDuplicidadePacienteFolha(string pacientefolha)
        {
            if (dtGrdPacienteFolha.Rows.Count > 0)
            {
                // Verifica se a especialidade já existe no DataTable
                foreach (DataRow row in dtGrdPacienteFolha.Rows)
                {
                    if (row["idfolha"].ToString() == pacientefolha)
                    {
                        return true; // A especialidade já existe
                    }
                }
            }
            else
            {
                return false;
            }

            return false;
        }

        private void cboFolha_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Determina a acao e valida se vai efetuar a pesquisa
            if (cAcao == Acao.UPDATE || cAcao == Acao.INSERT)
            {
                //bool retorno = FiltrarComboFolha(cboFolha.Text.ToString().ToUpper());
            }
        }

        private void grdFolha_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                NumeroLinha = -1;

                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    NumeroLinha = e.RowIndex; // Armazena o número da linha selecionada
                }
                else
                {
                    MessageBox.Show("Selecione uma folha para excluir!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao selecionar uma folha para excluir!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida os campos
                if (ValidaCampos() == false)
                {
                    return; // Se não for válido, sai do método
                }
                cPaciente objcPaciente = new cPaciente();
                //Se for novo paciente
                if (cAcao == Acao.INSERT)
                {
                    // sequence
                    Int32 sequence = 0;

                    //Preenche os dados do paciente
                    if (PreencheDadosPaciente(objcPaciente) == false)
                    {
                        return; // Se não for possível preencher os dados, sai do método
                    }

                    //Grava o paciente
                    if (objcPaciente.incluiPaciente(out sequence)    == true)
                    {
                        foreach (DataRow row in dtGrdPacienteFolha.Rows)
                        {
                            // Acessar os valores das colunas
                            objcPaciente.IdPaciente = sequence;
                            objcPaciente.Folha.IdFolha = Convert.ToInt32(row["idfolha"]);

                            // Inclui a folha do paciente
                            if (objcPaciente.incluiPacienteFolha() == false)
                            {
                                MessageBox.Show("Erro ao tentar incluir a folha do paciente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        MessageBox.Show("Inclusão efetuada com sucesso!. Código gerado: " + sequence, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancelar_Click(sender, e); // Chama o método de cancelar para limpar os campos e voltar ao estado inicial    
                        return;
                    }

                    else
                    {
                        MessageBox.Show("Erro ao tentar incluir!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                //Se for atualização de paciente
                if (cAcao == Acao.UPDATE)
                {

                    //Preenche os dados do paciente
                    if (PreencheDadosPaciente(objcPaciente) == false)
                    {
                        return; // Se não for possível preencher os dados, sai do método
                    }

                    // limpa o contador
                    int contador = 0;

                    foreach (DataRow row in dtGrdPacienteFolha.Rows)
                    {

                        // Acessar os valores das colunas
                        objcPaciente.IdPaciente = int.Parse(row["idpaciente"].ToString());
                        objcPaciente.IdFolha = int.Parse(row["idfolha"].ToString());

                        //Se for a primeira linha do loop da folha, o sistema entende que é necesseário apagar a tabela de especializacao do medico
                        if (contador == 0)
                        {
                            objcPaciente.Apaga = true;
                        }
                        //Se não... apagar não é necessário
                        else
                        {
                            objcPaciente.Apaga = false;
                        }
                        // Atualiza a especialização do médico
                        if (objcPaciente.atualizaPaciente() == true)
                        {
                            //incrementa contador
                            contador++;
                        }
                        else
                        {
                            MessageBox.Show("Erro ao tentar atualizar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    MessageBox.Show("Alteração efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancelar_Click(sender, e); // Chama o método de cancelar para limpar os campos e voltar ao estado inicial    
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar gravar o paciente: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidaCampos()
        {
            //Valida os campos obrigatórios
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("O campo Nome é obrigatório!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }
            //if (cboSexo.SelectedIndex == 0)
            //{
            //    MessageBox.Show("Selecione o Sexo do paciente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cboSexo.Focus();
            //    return false;
            //}

            //Se chegou aqui, todos os campos obrigatórios estão preenchidos
            return true;
        }

        private bool PreencheDadosPaciente(cPaciente objcPaciente)
        {
            try
            {
                //Preenche os dados do paciente
                if (string.IsNullOrWhiteSpace(txtCodigoResultado.Text))
                {
                    objcPaciente.IdPaciente = 0;
                }
                else
                {
                    objcPaciente.IdPaciente = int.Parse(txtCodigoResultado.Text);
                }
                objcPaciente.Nome = txtNome.Text.ToUpper();
                //objcPaciente.IdConvenio = int.Parse(cboConvenio.SelectedValue.ToString());

                return true; // Retorna verdadeiro se os dados foram preenchidos corretamente
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar preencher os dados do paciente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Retorna falso se houve erro ao preencher os dados
            }

        }
    }
}
