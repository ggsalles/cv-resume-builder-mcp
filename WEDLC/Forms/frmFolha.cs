using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WEDLC.Banco;
using WinFormsZoom;

namespace WEDLC.Forms
{
    public partial class frmFolha : Form
    {
        public enum Acao
        {
            INSERT = 0,
            UPDATE = 1,
            DELETE = 2,
            SAVE = 3,
            CANCELAR = 4,
            COMPLEMENTO = 5

        }

        public Acao cAcao = Acao.UPDATE;

        //Código do módulo
        public const int codModulo = 6;

        public DataTable dtTipoFolha = new DataTable();
        public DataTable dtGrupo = new DataTable();
        public DataTable dtAvaliacaoMuscular = new DataTable();
        public DataTable dtNeuroCondMotora = new DataTable();
        public DataTable dtNeuroCondSenorial = new DataTable();
        public DataTable dtEstudoPotencial = new DataTable();

        private FormZoomHelper zoomHelper;

        public frmFolha()
        {
            InitializeComponent();
            zoomHelper = new FormZoomHelper(this); // Inicializa o helper de zoom
            this.FormClosed += (s, e) => zoomHelper.Dispose(); // Descarta automaticamente quando o form for fechado
        }

        private void frmFolha_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            carregaCombo();
            carregaTela();
        }
        public void carregaTela()
        {
            //Popula o grid
            this.populaGridDados(0, 0, "", "");

            // Ativa a visualização do click no form
            this.KeyPreview = true;

            //Configura o grid
            configuraGridDados();

            cAcao = Acao.CANCELAR;
        }

        private void ConfigureGridPerformance(DataGridView grid)
        {
            // Habilita double buffering para reduzir flickering
            typeof(DataGridView).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(grdEstudoPotencial, true, null);
        }

        public void carregaCombo()
        {

            carregaTipo();
            carregaGrupo(0); // carrega apenas o selecione
            CarregaComboSimNao(cboSimNaoBlink);
            CarregaComboSimNao(cboSimNaoNSPD);
            CarregaComboSimNao(cboSimNaoRBC);
            CarregaComboSimNao(cboSimNaoReflexo);

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

            //Se clicou em gravar, cancelar ou complemento...
            if (cAcao == Acao.SAVE || cAcao == Acao.CANCELAR || cAcao == Acao.COMPLEMENTO)
            {
                btnNovo.Enabled = true;
                btnGravar.Enabled = false;
                btnCancelar.Enabled = false;
                btnExcluir.Enabled = false;
            }

            //Se clicou em gravar
            if (cAcao == Acao.UPDATE)
            {
                btnNovo.Enabled = false;
                btnGravar.Enabled = true;
                btnCancelar.Enabled = true;
                btnExcluir.Enabled = true;
            }
        }
        private void configuraGridDados()
        {
            // Ajustando o tamanho das colunas e ocultando as que não são necessárias
            grdDados.Columns[0].Width = 80; //ID
            grdDados.Columns[1].Width = 120; //Sigla
            grdDados.Columns[2].Width = 350; //Nome
            grdDados.Columns[3].Visible = false; //IdTipo
            grdDados.Columns[4].Width = 80; //Tipo
            grdDados.Columns[5].Visible = false; //IdGrupoFolha
            grdDados.Columns[6].Width = 80; //Grupo

            // Desabilita a edição da coluna
            grdDados.Columns[0].ReadOnly = true;
            grdDados.Columns[1].ReadOnly = true;
            grdDados.Columns[2].ReadOnly = true;
            grdDados.Columns[4].ReadOnly = true;
            grdDados.Columns[6].ReadOnly = true;

            // Configurando outras propriedades
            grdDados.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
            grdDados.MultiSelect = false; // Impede seleção múltipla
            grdDados.AllowUserToAddRows = false; // Impede adição de novas linhas
            grdDados.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
            grdDados.CurrentCell = null; // Desmarca a célula atual
            grdDados.AllowUserToDeleteRows = false; // Impede a exclusão de linhas

        }
        private DataTable buscaFolha(int tipopesquisa, Int32 idfolha, string sigla, string nome)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cFolha objcTipoFolha = new cFolha();

                dtAux = objcTipoFolha.buscaFolha(tipopesquisa, idfolha, sigla, nome);

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar a folha!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }
        private DataTable buscaAvalicaoMuscular(Int32 idfolha)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cFolha objcTipoFolha = new cFolha();

                dtAux = objcTipoFolha.buscaAvalicaoMuscular(idfolha);

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar a avaliação muscular!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }
        private DataTable buscaNeuroConducaoMotora(Int32 idfolha)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cFolha objcTipoFolha = new cFolha();

                dtAux = objcTipoFolha.buscaNeuroConducaoMotora(idfolha);

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar a neuro condução motora !", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }
        private DataTable buscaNeuroConducaoSensorial(Int32 idfolha)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cFolha objcTipoFolha = new cFolha();

                dtAux = objcTipoFolha.buscaNeuroConducaoSensorial(idfolha);

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar a neuro condução sensorial!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }
        private void populaGridDados(int tipopesquisa, Int32 idfolha, string sigla, string nome)
        {
            try
            {
                DataTable dt = this.buscaFolha(tipopesquisa, idfolha, sigla, nome);

                grdDados.DataSource = null;
                ConfigureGridPerformance(grdDados); // Aplica otimizações de performance    

                //Renomeia as colunas do datatable
                dt.Columns["idfolha"].ColumnName = "Código";
                dt.Columns["sigla"].ColumnName = "Sigla";
                dt.Columns["nome"].ColumnName = "Nome";
                dt.Columns["idtipofolha"].ColumnName = "IdTipoFolha";
                dt.Columns["descricaotipo"].ColumnName = "Tipo";
                dt.Columns["idgrupofolha"].ColumnName = "IdGrupoFolha";
                dt.Columns["descricaogrupo"].ColumnName = "Grupo";

                grdDados.SuspendLayout();
                grdDados.DataSource = dt;
                grdDados.ResumeLayout();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar populaGrid!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public bool validaCampos()
        {
            if (txtSigla.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("O campo sigla não está preenchido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSigla.Focus();
                return false;
            }

            if (txtNome.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("O campo nome não está preenchido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNome.Focus();
                return false;
            }

            if (cboTipo.SelectedValue == null || cboTipo.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("O campo tipo não está preenchido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboTipo.Focus();
                return false;
            }
            if (cboGrupo.SelectedValue == null || cboGrupo.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("O campo grupo não está preenchido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboGrupo.Focus();
                return false;
            }

            return true;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {

                //long proximoId = new cSequence().GetNextSequenceValue("folha_sequence");
                //txtCodigo.Text = proximoId.ToString();

                this.carregaTela(); // Carrega a tela para limpar os dados anteriores

                //Determina a acao
                cAcao = Acao.INSERT;

                //Prepara os botões para a inclusão
                controlaBotao();

                this.controlaComplemento(); // Reseta o grupo de complemento

                //limpa controles e desbloqueia os campos
                this.controlaDadosFolha(true);

                grdDados.CurrentCell = null; //Desmarca a seleção do grid
                grdDados.Enabled = false; //Desabilita o grid de dados

                //Deixa o foco no nome
                txtSigla.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }

        }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            //Valida campos
            if (validaCampos() == true)
            {
                Int32 ultimoIdFolha = 0; // Inicializa o ID da folha
                cFolha objFolha = new cFolha();

                objFolha.Sigla = txtSigla.Text;
                objFolha.Nome = txtNome.Text;
                objFolha.IdTipoFolha = int.Parse(cboTipo.SelectedValue.ToString());
                objFolha.IdGrupoFolha = int.Parse(cboGrupo.SelectedValue.ToString());

                if (cAcao == Acao.INSERT)
                {
                    if (objFolha.incluiFolha(objFolha, out ultimoIdFolha))
                    {
                        MessageBox.Show("Folha " + ultimoIdFolha + " foi incluida com sucesso! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (cboTipo.SelectedIndex == 1) //ENMG
                        {
                            cTestesEspeciais objTestesEspeciais = new cTestesEspeciais();

                            objTestesEspeciais.IdFolha = ultimoIdFolha;
                            objTestesEspeciais.blinkreflex = cboSimNaoBlink.SelectedIndex == 0 ? 1 : 2;
                            objTestesEspeciais.rbc = cboSimNaoRBC.SelectedIndex == 0 ? 1 : 2;
                            objTestesEspeciais.reflexoh = cboSimNaoReflexo.SelectedIndex == 0 ? 1 : 2;
                            objTestesEspeciais.nspd = cboSimNaoNSPD.SelectedIndex == 0 ? 1 : 2;
                            objTestesEspeciais.IncluiTestesEspeciais();

                        }

                        txtCodigo.Text = ultimoIdFolha.ToString(); // Atualiza o campo de código com o ID da nova folha
                        carregaTela(); // Recarrega a tela para mostrar a nova folha incluída
                        cAcao = Acao.SAVE; // Muda a ação para SAVE após inclusão bem-sucedida
                        controlaBotao(); // Atualiza os botões para o estado de SAVE
                        grdDados.Enabled = true; // Habilita o grid de dados
                        btnComplemento.Enabled = true; // Habilita o botão de complemento
                        btnComplemento_Click(sender, e); // Chama o evento de complemento para carregar os dados adicionais

                    }
                    else
                    {
                        MessageBox.Show("Erro ao tentar incluir!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (cAcao == Acao.UPDATE)
                {
                    objFolha.IdFolha = int.Parse(txtCodigo.Text);

                    //Solicita a confirmação do usuário para alteração
                    if (MessageBox.Show("Tem certeza que deseja alterar este dado?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (objFolha.atualizaFolha() == true)
                        {
                            MessageBox.Show("Alteração efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao tentar atualilzar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        btnCancelar_Click(sender, e);
                    }
                }
                else if (cAcao == Acao.DELETE)
                {
                    objFolha.IdFolha = int.Parse(txtCodigo.Text);

                    ////Solicita a confirmação do usuário para alteração
                    //if (MessageBox.Show("Tem certeza que deseja excluir este dado?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    if (objEspecializacao.excluiEspecializacao() == true)
                    //    {
                    //        MessageBox.Show("Exclusão efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Erro ao tentar atualilzar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        return;
                    //    }
                    //}
                }

                //Chama o evento cancelar
                //btnCancelar_Click(sender, e);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Determina a acao
            cAcao = Acao.CANCELAR;

            controlaBotao();

            //Habilita o campo código
            txtCodigo.Enabled = true;

            grdDados.Enabled = true; //Habilita o grid de dados
            btnComplemento.Enabled = false; // Desabilita o botão de complemento

            //limpa controles e desbloqueia os campos
            this.controlaDadosFolha(false);

            limpaComboGridAvaliacaoMuscular();
            limpaComboGridNeuroCondMotora();
            limpaComboGridNeuroCondSensorial();
            limpaComboGridEstudoPotencial();

            //Desmarca a seleção do grid
            grdEstudoPotencial.CurrentCell = null;

            //Carrega o grid
            carregaTela();
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //valida codigo para exclusão
            if (txtCodigo.Text.Length == 0 || txtSigla.Text.Length == 0 || txtNome.Text.Length == 0)
            {
                MessageBox.Show("Não existe nenhum item selecionado para exclusão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Determina a acao
            cAcao = Acao.DELETE;

            //Chama o gravra
            btnGravar_Click(sender, e);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT && cAcao != Acao.COMPLEMENTO)   
            {
                int tipopesquisa = 0; //Código que retorna todo select   
                int idespecializacao = 0; //Código da especialização

                //Limpa campos
                txtSigla.Text = string.Empty;
                txtNome.Text = string.Empty;

                // Verifica a quantidade de caracteres
                if (txtCodigo.Text.Length == 0)
                {
                    tipopesquisa = 0;
                }
                else
                {
                    tipopesquisa = 1;
                    idespecializacao = int.Parse(txtCodigo.Text);
                }

                this.populaGridDados(tipopesquisa, idespecializacao, "", "");
                this.configuraGridDados();
            }
        }

        private void txtSigla_KeyUp(object sender, KeyEventArgs e)
        {
            int tipopesquisa = 2; //Código que pesquisa pela sigla   
            string sigla = string.Empty; //Código da especialização

            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT && cAcao != Acao.COMPLEMENTO)
            {

                //Limpa campos
                txtCodigo.Text = string.Empty;
                txtNome.Text = string.Empty;

                if (txtSigla.Text.Length == 0)
                {
                    tipopesquisa = 0;
                }
                else
                {
                    tipopesquisa = 2;
                    sigla = txtSigla.Text;
                }

                this.populaGridDados(tipopesquisa, 0, sigla, "");
                this.configuraGridDados();
            }
        }
        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            int tipopesquisa = 3; //Código que pesquisa pelo nome 
            string nome = string.Empty; //Código da especialização

            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT && cAcao != Acao.COMPLEMENTO)
            {

                //Limpa campos
                txtCodigo.Text = string.Empty;
                txtSigla.Text = string.Empty;

                if (txtNome.Text.Length == 0)
                {
                    tipopesquisa = 0;
                }
                else
                {
                    tipopesquisa = 3;
                    nome = txtNome.Text;
                }

                this.populaGridDados(tipopesquisa, 0, "", nome);
                this.configuraGridDados();
            }
        }
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void cboTipo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cboTipo.SelectedValue == null || !int.TryParse(cboTipo.SelectedValue.ToString(), out int tipoId))
            {
                carregaGrupo(0); // carrega tudo
            }
            else
            {
                carregaGrupo(tipoId);
                cboGrupo.Focus();
            }

            //Determina a acao e valida se vai efetuar a pesquisa
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
            {
                bool retorno = FiltrarComboTipo(cboTipo.Text.ToString());
            }
        }

        private bool FiltrarComboTipo(string texto)
        {
            DataTable dt = (DataTable)cboTipo.DataSource;
            bool existe = dt.AsEnumerable().Any(row => row.Field<string>("descricao") == cboTipo.Text);
            if (existe == false)
            {
                MessageBox.Show("Selecione um item válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTipo.Focus();
                return false; // Retorna falso se o item não existir
            }

            return true; // Retorna verdadeiro se o item existir
        }

        public void carregaTipo()
        {
            cboTipo.BeginUpdate(); // Reduz flickering

            //Carrega o combo de tipo de folha
            cboTipo.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboTipo.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cFolha objcTipoFolha = new cFolha();
            dtTipoFolha = objcTipoFolha.buscaTipoFolha();
            DataRow newRow = dtTipoFolha.NewRow();
            newRow["idtipofolha"] = 0; // Defina o valor desejado para a primeira linha
            newRow["descricao"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtTipoFolha.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição
            cboTipo.DataSource = dtTipoFolha;
            cboTipo.ValueMember = "idtipofolha";
            cboTipo.DisplayMember = "descricao";

            cboTipo.EndUpdate();
        }

        public void carregaGrupo(int pTipoFolha)
        {

            dtGrupo = new cFolha().buscaGrupoFolha(pTipoFolha);
            DataRow newRow = dtGrupo.NewRow();
            newRow["idgrupofolha"] = 0; // Defina o valor desejado para a primeira linha
            newRow["descricao"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtGrupo.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            cboGrupo.BeginUpdate(); // Reduz flickering
            //Carrega o combo de tipo de folha
            cboGrupo.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboGrupo.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista
            cboGrupo.ValueMember = "idgrupofolha";
            cboGrupo.DisplayMember = "descricao";
            cboGrupo.DataSource = dtGrupo;
            cboGrupo.EndUpdate();

        }

        public void carregaComboAvaliacaoMuscular(int pTipoFolha)
        {
            try
            {
                // Considerar injetar cFolha como dependência se usado frequentemente
                dtAvaliacaoMuscular = new cFolha().carregaComboAvaliacaoMuscular(pTipoFolha);

                if (dtAvaliacaoMuscular != null)
                {
                    // Adiciona item padrão
                    DataRow newRow = dtAvaliacaoMuscular.NewRow();
                    newRow["idmusculo"] = 0;
                    newRow["descricao"] = "Selecione...";
                    dtAvaliacaoMuscular.Rows.InsertAt(newRow, 0);

                    // Configura ComboBox
                    cboAvaliacaoMuscular.BeginUpdate(); // Reduz flickering
                    cboAvaliacaoMuscular.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cboAvaliacaoMuscular.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cboAvaliacaoMuscular.DataSource = dtAvaliacaoMuscular;
                    cboAvaliacaoMuscular.ValueMember = "idmusculo";
                    cboAvaliacaoMuscular.DisplayMember = "descricao";
                    cboAvaliacaoMuscular.EndUpdate();
                }
            }
            catch (Exception)
            {
                // Logar erro e/ou mostrar mensagem ao usuário
                //Logger.Error("Erro ao carregar combo avaliação muscular", ex);
                MessageBox.Show("Não foi possível carregar Combo Avaliacao Muscular.");
            }
        }

        public void carregaComboNeuroCondMotora(int pTipoFolha)
        {

            dtNeuroCondMotora = new cFolha().carregaComboNeuroConducaoMotora(pTipoFolha);
            DataRow newRow = dtNeuroCondMotora.NewRow();
            newRow["idnervo"] = 0; // Defina o valor desejado para a primeira linha
            newRow["descricao"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtNeuroCondMotora.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            //Carrega o combo conducao motora
            cboNeuroConducaoMotora.BeginUpdate(); // Reduz flickering
            cboNeuroConducaoMotora.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboNeuroConducaoMotora.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista
            cboNeuroConducaoMotora.DataSource = dtNeuroCondMotora;
            cboNeuroConducaoMotora.ValueMember = "idnervo";
            cboNeuroConducaoMotora.DisplayMember = "descricao";
            cboNeuroConducaoMotora.EndUpdate();

        }

        public void carregaComboNeuroCondSensorial(int pTipoFolha)
        {

            dtNeuroCondSenorial = new cFolha().carregaComboNeuroConducaoSensorial(pTipoFolha);
            DataRow newRow = dtNeuroCondSenorial.NewRow();
            newRow["idnervo"] = 0; // Defina o valor desejado para a primeira linha
            newRow["descricao"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtNeuroCondSenorial.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

            //Carrega o combo de tipo de folha
            cboNeuroConducaoSensorial.BeginUpdate(); // Reduz flickering
            cboNeuroConducaoSensorial.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboNeuroConducaoSensorial.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista
            cboNeuroConducaoSensorial.DataSource = dtNeuroCondSenorial;
            cboNeuroConducaoSensorial.ValueMember = "idnervo";
            cboNeuroConducaoSensorial.DisplayMember = "descricao";
            cboNeuroConducaoSensorial.EndUpdate();

        }

        private void FiltrarComboGrupo(string texto)
        {
            DataTable dt = (DataTable)cboGrupo.DataSource;
            bool existe = dt.AsEnumerable().Any(row => row.Field<string>("descricao") == cboGrupo.Text);
            if (existe == false)
            {
                MessageBox.Show("Selecione um item válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGrupo.Focus();
            }
        }

        private void cboGrupo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FiltrarComboGrupo(cboGrupo.Text.ToString());
        }

        private void CarregaComboSimNao(System.Windows.Forms.ComboBox objCombo)
        {
            objCombo.BeginUpdate(); // Reduz flickering
            objCombo.DisplayMember = "Descricao";
            objCombo.ValueMember = "Id";
            objCombo.Items.Add(new cSimNao { IdSimNao = "1", Descricao = "Sim" });
            objCombo.Items.Add(new cSimNao { IdSimNao = "2", Descricao = "Não" });
            objCombo.DropDownStyle = ComboBoxStyle.DropDownList; // Define o estilo do ComboBox como DropDownList
            objCombo.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista
            objCombo.SelectedIndex = 1; // Define o índice padrão como Não;
            objCombo.EndUpdate();
        }

        private void grdDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    txtAguarde.Visible = true; // Exibe o botão de aguarde
                    txtAguarde.Text = "Processando...";
                    txtAguarde.Refresh();

                    //Determina a acao
                    cAcao = Acao.UPDATE;

                    txtCodigo.Text = grdDados.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtSigla.Text = grdDados.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtNome.Text = grdDados.Rows[e.RowIndex].Cells[2].Value.ToString();
                    cboTipo.SelectedValue = grdDados.Rows[e.RowIndex].Cells[3].Value.ToString();
                    carregaGrupo(int.Parse(cboTipo.SelectedValue.ToString()));
                    cboGrupo.SelectedValue = grdDados.Rows[e.RowIndex].Cells[5].Value.ToString();
                    grpBoxDados.Refresh();


                    //libera os controles 
                    controlaBotao();

                    txtCodigo.Enabled = false; //Desabilita o campo código
                    grdDados.Enabled = false; //Desabilita o grid de dados
                    grpComplemento.Enabled = false; //Desabilita o grupo de complemento
                    btnComplemento.Enabled = true; // Habilita o botão de complemento

                    //Carrega grid avaliacao muscular
                    carregaGridAvaliacaoMuscular();

                    //Carrega grid neuro condução motora
                    carregaGridNeuroCondMotora();

                    //Carrega grid neuro condução sensorial
                    carregaGridNeuroCondSensorial();

                    //Carrega grid estudo potencial
                    carregaGridEstudoPotencial();

                    grpComplemento.Refresh();

                    if (cboTipo.SelectedIndex == 1) //se for do tipo ENMG
                    {
                        carregaTestesEspeciais(int.Parse(txtCodigo.Text));
                    }

                    txtSigla.Focus();
                }

                // Desabilita a edição da coluna
                grdDados.Columns[0].ReadOnly = true;
                grdDados.Columns[1].ReadOnly = true;
                grdDados.Columns[2].ReadOnly = true;

                txtAguarde.Visible = false; // Oculta o botão de aguarde

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao selecionar um item na grid dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAguarde.Visible = false; // Oculta o botão de aguarde
                return;
            }

        }

        private void configuraObjGrid(DataGridView objGrid)
        {
            // Ajustando o tamanho das colunas e ocultando as que não são necessárias
            objGrid.Columns[0].Visible = false; //idAvalicao
            objGrid.Columns[1].Visible = false; //idMusculo
            objGrid.Columns[2].Width = 200; //Descricao

            // Desabilita a edição da coluna
            objGrid.Columns[0].ReadOnly = true;
            objGrid.Columns[1].ReadOnly = true;
            objGrid.Columns[2].ReadOnly = true;

            // Configurando outras propriedades
            objGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
            objGrid.MultiSelect = false; // Impede seleção múltipla
            objGrid.AllowUserToAddRows = false; // Impede adição de novas linhas
            objGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
            objGrid.CurrentCell = null; // Desmarca a célula atual
            objGrid.AllowUserToDeleteRows = false; // Impede a exclusão de linhas

        }

        private void populaGridAvaliacaoMuscular(Int32 idfolha)
        {
            try
            {
                DataTable dt = this.buscaAvalicaoMuscular(idfolha);

                grdAvaliacaoMuscular.DataSource = null;

                ConfigureGridPerformance(grdAvaliacaoMuscular); // Aplica otimizações de performance

                //Renomeia as colunas do datatable
                dt.Columns["idavaliacaomuscular"].ColumnName = "IdAvaliacaoMuscular";
                dt.Columns["idmusculo"].ColumnName = "IdMusculo";
                dt.Columns["descricao"].ColumnName = "Descricao";

                grdAvaliacaoMuscular.SuspendLayout();
                grdAvaliacaoMuscular.DataSource = dt;
                grdAvaliacaoMuscular.ResumeLayout();

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na populaGridAvaliacaoMuscular!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void populaGridNeuroConducaoMotora(Int32 idfolha)
        {
            try
            {
                DataTable dt = this.buscaNeuroConducaoMotora(idfolha);

                grdNeuroCondMotora.DataSource = null;
                ConfigureGridPerformance(grdNeuroCondMotora); // Aplica otimizações de performance

                //Renomeia as colunas do datatable
                dt.Columns["idneurocondmotora"].ColumnName = "IdNeuroCondMotora";
                dt.Columns["idnervo"].ColumnName = "IdNervo";
                dt.Columns["descricao"].ColumnName = "Descricao";

                grdNeuroCondMotora.SuspendLayout(); //otimiza a atualização do grid
                grdNeuroCondMotora.DataSource = dt;
                grdNeuroCondMotora.ResumeLayout(); //otimiza a atualização do grid

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na populaGridAvaliacaoMuscular!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void populaGridNeuroConducaoSensorial(Int32 idfolha)
        {
            try
            {
                DataTable dt = this.buscaNeuroConducaoSensorial(idfolha);

                grdNeuroCondSensorial.DataSource = null;
                ConfigureGridPerformance(grdNeuroCondSensorial); // Aplica otimizações de performance

                //Renomeia as colunas do datatable
                dt.Columns["idneurocondsensorial"].ColumnName = "IdNeuroCondSensorial";
                dt.Columns["idnervo"].ColumnName = "IdNervo";
                dt.Columns["descricao"].ColumnName = "Descricao";

                grdNeuroCondSensorial.SuspendLayout(); //otimiza a atualização do grid
                grdNeuroCondSensorial.DataSource = dt;
                grdNeuroCondSensorial.ResumeLayout(); //otimiza a atualização do grid

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na populaGridNeuroConduçãoSensorial!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void populaGridEstudoPotencial(Int32 idfolha)
        {
            try
            {
                DataTable dtEstudoPotencial = this.buscaEstudoPotenEvocado(idfolha);

                grdEstudoPotencial.DataSource = null;
                ConfigureGridPerformance(grdEstudoPotencial); // Aplica otimizações de performance

                //Renomeia as colunas do datatable
                dtEstudoPotencial.Columns["idestudopotenevocado"].ColumnName = "IdEstudoPotenEvocado";
                dtEstudoPotencial.Columns["idfolha"].ColumnName = "IdFolha";
                dtEstudoPotencial.Columns["descricao"].ColumnName = "Descricao";

                grdEstudoPotencial.SuspendLayout();
                grdEstudoPotencial.DataSource = dtEstudoPotencial;
                grdEstudoPotencial.ResumeLayout();

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na populaGridEstudoPotencial!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private DataTable buscaEstudoPotenEvocado(Int32 idfolha)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cFolha objcTipoFolha = new cFolha();

                dtAux = objcTipoFolha.buscaEstudoPotenEvocado(idfolha);

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar a neuro condução sensorial!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private void limpaComboGridAvaliacaoMuscular()
        {
            cboAvaliacaoMuscular.DataSource = null;
            cboAvaliacaoMuscular.Items.Clear();
            cboAvaliacaoMuscular.Text = string.Empty;
            cboAvaliacaoMuscular.SelectedIndex = -1;

            grdAvaliacaoMuscular.DataSource = null;

            grpAvaliacaoMuscular.Enabled = false;
        }
        private void limpaComboGridNeuroCondMotora()
        {
            cboNeuroConducaoMotora.DataSource = null;
            cboNeuroConducaoMotora.Items.Clear();
            cboNeuroConducaoMotora.Text = string.Empty;
            cboNeuroConducaoMotora.SelectedIndex = -1;

            grdNeuroCondMotora.DataSource = null;

            grpNeuroCondMotora.Enabled = false;
        }
        private void limpaComboGridNeuroCondSensorial()
        {
            cboNeuroConducaoSensorial.DataSource = null;
            cboNeuroConducaoSensorial.Items.Clear();
            cboNeuroConducaoSensorial.Text = string.Empty;
            cboNeuroConducaoSensorial.SelectedIndex = -1;

            grdNeuroCondSensorial.DataSource = null;

            grpNeuroCondSensorial.Enabled = false;
        }
        private void limpaComboGridEstudoPotencial()
        {
            grdEstudoPotencial.DataSource = null;
            grpEstudoPotenEvocado.Enabled = false;

        }

        private void btnIncluiAvaliacaoMuscular_Click(object sender, EventArgs e)
        {
            try
            {
                //valida inclusão
                if (cboAvaliacaoMuscular.SelectedValue == null || cboAvaliacaoMuscular.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Selecione um item válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboAvaliacaoMuscular.Focus();
                    return;
                }
                //verifica se existe itens no combo para inlcusão
                if (cboAvaliacaoMuscular.Items.Count == 0 || cboAvaliacaoMuscular.SelectedIndex == 0)
                {
                    MessageBox.Show("Não existem itens para inclusão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboAvaliacaoMuscular.Focus();
                    return;
                }

                //chama a procedure de inclusão da avaliação muscular
                cAvaliacaoMuscular objAvaliacaoMuscular = new cAvaliacaoMuscular();
                objAvaliacaoMuscular.IdFolha = int.Parse(txtCodigo.Text);
                objAvaliacaoMuscular.IdMusculo = int.Parse(cboAvaliacaoMuscular.SelectedValue.ToString());
                if (objAvaliacaoMuscular.incluiAvaliacaoMuscular() == true)
                {
                    MessageBox.Show("Avaliação muscular incluída com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Carrega o grid
                    this.populaGridAvaliacaoMuscular(int.Parse(txtCodigo.Text));
                    cboAvaliacaoMuscular.Focus();
                }
                else
                {
                    MessageBox.Show("Erro ao tentar incluir avaliação muscular!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                carregaComboAvaliacaoMuscular(int.Parse(txtCodigo.Text));
                carregaGridAvaliacaoMuscular();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar incluir uma avaliação muscular!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void carregaGridAvaliacaoMuscular()
        {
            try
            {
                //Carrega grid avaliacao muscular
                this.populaGridAvaliacaoMuscular(int.Parse(txtCodigo.Text));
                this.configuraObjGrid(grdAvaliacaoMuscular);
                this.carregaComboAvaliacaoMuscular(int.Parse(txtCodigo.Text));
                grpAvaliacaoMuscular.Enabled = true;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar a grid de avaliação muscular!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void carregaGridNeuroCondMotora()
        {
            try
            {
                //Carrega grid neuro condução motora
                this.populaGridNeuroConducaoMotora(int.Parse(txtCodigo.Text));
                this.configuraObjGrid(grdNeuroCondMotora);
                this.carregaComboNeuroCondMotora(int.Parse(txtCodigo.Text));
                grpNeuroCondMotora.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar a grid de neuro condução motora!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void carregaGridNeuroCondSensorial()
        {
            try
            {
                //Carrega grid neuro condução sensorial
                this.populaGridNeuroConducaoSensorial(int.Parse(txtCodigo.Text));
                this.configuraObjGrid(grdNeuroCondSensorial);
                this.carregaComboNeuroCondSensorial(int.Parse(txtCodigo.Text));
                grpNeuroCondSensorial.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar a grid de neuro condução sensorial!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void carregaGridEstudoPotencial()
        {
            try
            {
                //Carrega grid estudo potencial
                this.populaGridEstudoPotencial(int.Parse(txtCodigo.Text));
                this.configuraObjGrid(grdEstudoPotencial);
                grpEstudoPotenEvocado.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar a grid de estudo potencial!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnExcluiAvaliacaoMuscular_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida se existe item no grid
                if (grdAvaliacaoMuscular.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecione um item para exclusão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Verifica se o item selecionado é válido
                if (grdAvaliacaoMuscular.SelectedRows[0].Cells[0].Value == null || grdAvaliacaoMuscular.SelectedRows[0].Cells[0].Value.ToString() == "0")
                {
                    MessageBox.Show("Selecione um item válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Solicita a confirmação do usuário para exclusão
                if (MessageBox.Show("Tem certeza que deseja excluir este item?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cAvaliacaoMuscular objAvaliacaoMuscular = new cAvaliacaoMuscular();
                    objAvaliacaoMuscular.IdAvaliacaoMuscular = int.Parse(grdAvaliacaoMuscular.SelectedRows[0].Cells[0].Value.ToString());
                    if (objAvaliacaoMuscular.excluiAvaliacaoMuscular() == true)
                    {
                        MessageBox.Show("Avaliação muscular excluída com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Carrega o grid
                        this.populaGridAvaliacaoMuscular(int.Parse(txtCodigo.Text));
                    }

                    carregaComboAvaliacaoMuscular(int.Parse(txtCodigo.Text));
                    carregaGridAvaliacaoMuscular();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar excluir avaliação muscular!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnComplemento_Click(object sender, EventArgs e)
        {
            grpComplemento.Enabled = true; // Habilita o grupo de complemento
            cAcao = Acao.COMPLEMENTO; // Define a ação como complemento
            controlaBotao(); // Controla os botões de ação
            habilitaDesabilitaComplementos();
            grdDados.Enabled = true; // Habilita o grid de dados

        }

        private void controlaComplemento()
        {
            btnComplemento.Enabled = false; // Desabilita o botão de complemento
            grpComplemento.Enabled = false; // Desabilita o grupo de complemento
            grpTestesEspeciais.Enabled = false; // Desabilita o grupo de testes especiais

            carregaComboAvaliacaoMuscular(0);
            carregaComboNeuroCondMotora(0);
            carregaComboNeuroCondSensorial(0);
            txtEstudoPotencial.Text = string.Empty; // Limpa o campo de estudo potencial

            grdAvaliacaoMuscular.DataSource = null; // Limpa o grid de avaliação muscular
            grdNeuroCondMotora.DataSource = null; // Limpa o grid de neuro condução motora
            grdNeuroCondSensorial.DataSource = null; // Limpa o grid de neuro condução sensorial
            grdEstudoPotencial.DataSource = null; // Limpa o grid de estudo potencial

            CarregaComboSimNao(cboSimNaoBlink); // Carrega o combo de sim/não para blink 
            CarregaComboSimNao(cboSimNaoRBC); // Carrega o combo de sim/não para RBC
            CarregaComboSimNao(cboSimNaoReflexo); // Carrega o combo de sim/não para reflexo
            CarregaComboSimNao(cboSimNaoNSPD); // Carrega o combo de sim/não para NSPD

        }
        private void controlaDadosFolha(Boolean pEnabled)
        {
            txtCodigo.Text = string.Empty;
            txtCodigo.Enabled = !pEnabled; //Desabilita o campo código
            txtSigla.Text = string.Empty;
            txtNome.Text = string.Empty;

            cboTipo.SelectedIndex = 0; // Reseta o combo de tipo
            cboGrupo.SelectedIndex = 0; // Reseta o combo de grupo
            cboTipo.Enabled = pEnabled; //Habilita o combo de tipo
            cboGrupo.Enabled = pEnabled; //Habilita o combo de grupo

            grdEstudoPotencial.CurrentCell = null; //Desmarca a seleção do grid
        }

        private void btnIncluiNeuroCondMotora_Click(object sender, EventArgs e)
        {
            try
            {
                //valida inclusão
                if (cboNeuroConducaoMotora.SelectedValue == null || cboNeuroConducaoMotora.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Selecione um item válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNeuroConducaoMotora.Focus();
                    return;
                }
                //verifica se existe itens no combo para inlcusão
                if (cboNeuroConducaoMotora.Items.Count == 0 || cboNeuroConducaoMotora.SelectedIndex == 0)
                {
                    MessageBox.Show("Não existem itens para inclusão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNeuroConducaoMotora.Focus();
                    return;
                }
                //chama a procedure de inclusão da avaliação muscular
                cNeuroConducaoMotora objNeuroConducaoMotora = new cNeuroConducaoMotora();
                objNeuroConducaoMotora.IdFolha = int.Parse(txtCodigo.Text);
                objNeuroConducaoMotora.IdNervo = int.Parse(cboNeuroConducaoMotora.SelectedValue.ToString());
                if (objNeuroConducaoMotora.IncluiNeuroConducaoMotora() == true)
                {
                    MessageBox.Show("Neuro condução motora incluída com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Carrega o grid
                    this.populaGridNeuroConducaoMotora(int.Parse(txtCodigo.Text));
                    cboNeuroConducaoMotora.Focus();
                }
                else
                {
                    MessageBox.Show("Erro ao tentar incluir uma neuro condução motora!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                carregaComboNeuroCondMotora(int.Parse(txtCodigo.Text));
                carregaGridNeuroCondMotora();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar incluir uma neuro condução motora!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnExcluiNeuroCondMotora_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida se existe item no grid
                if (grdNeuroCondMotora.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecione um item para exclusão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Verifica se o item selecionado é válido
                if (grdNeuroCondMotora.SelectedRows[0].Cells[0].Value == null || grdNeuroCondMotora.SelectedRows[0].Cells[0].Value.ToString() == "0")
                {
                    MessageBox.Show("Selecione um item válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Solicita a confirmação do usuário para exclusão
                if (MessageBox.Show("Tem certeza que deseja excluir este item?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cNeuroConducaoMotora objNeuroConducaoMotora = new cNeuroConducaoMotora();
                    objNeuroConducaoMotora.IdNeuroCondMotora = int.Parse(grdNeuroCondMotora.SelectedRows[0].Cells[0].Value.ToString());
                    if (objNeuroConducaoMotora.ExcluiNeuroConducaoMotora() == true)
                    {
                        MessageBox.Show("Neuro condução motora excluída com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Carrega o grid
                        this.populaGridNeuroConducaoMotora(int.Parse(txtCodigo.Text));
                    }

                    carregaComboNeuroCondMotora(int.Parse(txtCodigo.Text));
                    carregaGridNeuroCondMotora();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar excluir a neuro condução motora!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void habilitaDesabilitaComplementos()
        {
            btnComplemento.Enabled = false; // Desabilita o botão de complemento
            grpTestesEspeciais.Enabled = false; // Desabilita o grupo de testes especiais

            if (cboTipo.SelectedIndex == 1) //ENMG
            {
                grpAvaliacaoMuscular.Enabled = true; // Habilita o grupo de avaliação muscular
                grpNeuroCondMotora.Enabled = true; // Habilita o grupo de neuro condução motora
                grpNeuroCondSensorial.Enabled = true; // Habilita o grupo de neuro condução sensorial
                grpEstudoPotenEvocado.Enabled = false; // Desabilita o grupo de estudo potencial
                grpTestesEspeciais.Enabled = true; // Habilita o grupo de testes especiais

            }
            else if (cboTipo.SelectedIndex == 2) //FIXO
            {
                grpAvaliacaoMuscular.Enabled = false; // Habilita o grupo de avaliação muscular
                grpNeuroCondMotora.Enabled = false; // Habilita o grupo de neuro condução motora
                grpNeuroCondSensorial.Enabled = false; // Habilita o grupo de neuro condução sensorial
                grpEstudoPotenEvocado.Enabled = false; // Desabilita o grupo de estudo potencial
                btnComplemento.Enabled = true;
                MessageBox.Show("Para este tipo de folha, não existe complemento.","Aviso",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (cboTipo.SelectedIndex == 3) //RAIZ
            {
                grpAvaliacaoMuscular.Enabled = false; // Habilita o grupo de avaliação muscular
                grpNeuroCondMotora.Enabled = false; // Habilita o grupo de neuro condução motora
                grpNeuroCondSensorial.Enabled = false; // Habilita o grupo de neuro condução sensorial
                grpEstudoPotenEvocado.Enabled = true; // Habilita o grupo de estudo potencial
                //btnComplemento.Enabled = false; // Desabilita o botão de complemento, pois não há complementos para este tipo
            }

        }

        private void btnIncluiNeuroCondSensorial_Click(object sender, EventArgs e)
        {
            try
            {
                //valida inclusão
                if (cboNeuroConducaoSensorial.SelectedValue == null || cboNeuroConducaoSensorial.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Selecione um item válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNeuroConducaoSensorial.Focus();
                    return;
                }
                //verifica se existe itens no combo para inlcusão
                if (cboNeuroConducaoSensorial.Items.Count == 0 || cboNeuroConducaoSensorial.SelectedIndex == 0)
                {
                    MessageBox.Show("Não existem itens para inclusão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNeuroConducaoSensorial.Focus();
                    return;
                }
                //chama a procedure de inclusão da avaliação muscular
                cNeuroConducaoSensorial objNeuroConducaoSensorial = new cNeuroConducaoSensorial();
                objNeuroConducaoSensorial.IdFolha = int.Parse(txtCodigo.Text);
                objNeuroConducaoSensorial.IdNervo = int.Parse(cboNeuroConducaoSensorial.SelectedValue.ToString());
                if (objNeuroConducaoSensorial.IncluiNeuroConducaoSensorial() == true)
                {
                    MessageBox.Show("Neuro condução sensorial incluida com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Carrega o grid
                    this.populaGridNeuroConducaoSensorial(int.Parse(txtCodigo.Text));
                    cboNeuroConducaoSensorial.Focus();
                }
                else
                {
                    MessageBox.Show("Erro ao tentar incluir uma neuro condução sensorial!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                carregaComboNeuroCondSensorial(int.Parse(txtCodigo.Text));
                carregaGridNeuroCondSensorial();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar incluir uma neuro condução sensorial!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnExcluiNeuroCondSensorial_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida se existe item no grid
                if (grdNeuroCondSensorial.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecione um item para exclusão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Verifica se o item selecionado é válido
                if (grdNeuroCondSensorial.SelectedRows[0].Cells[0].Value == null || grdNeuroCondSensorial.SelectedRows[0].Cells[0].Value.ToString() == "0")
                {
                    MessageBox.Show("Selecione um item válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Solicita a confirmação do usuário para exclusão
                if (MessageBox.Show("Tem certeza que deseja excluir este item?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cNeuroConducaoSensorial objNeuroConducaoSensorial = new cNeuroConducaoSensorial();
                    objNeuroConducaoSensorial.IdNeuroCondSensorial = int.Parse(grdNeuroCondSensorial.SelectedRows[0].Cells[0].Value.ToString());
                    if (objNeuroConducaoSensorial.ExcluiNeuroConducaoSensorial() == true)
                    {
                        MessageBox.Show("Neuro condução sensorial excluída com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Carrega o grid
                        this.populaGridNeuroConducaoSensorial(int.Parse(txtCodigo.Text));
                    }
                    else
                    {
                        MessageBox.Show("Neuro condução sensorial excluída com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    carregaComboNeuroCondSensorial(int.Parse(txtCodigo.Text));
                    carregaGridNeuroCondSensorial();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar excluir a neuro condução motora!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnIncluiEstudoPotencial_Click(object sender, EventArgs e)
        {
            try
            {
                //valida inclusão
                if (txtEstudoPotencial.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Informe um valor válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEstudoPotencial.Focus();
                    return;
                }

                DataView dv = new DataView((DataTable)grdEstudoPotencial.DataSource);
                dv.RowFilter = "Descricao = " + "'" + txtEstudoPotencial.Text.ToString().Trim() + "'";
                if (dv.Count > 0)
                {
                    MessageBox.Show("Já existe um estudo potencial com esta descrição!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEstudoPotencial.Focus();
                    return;
                }

                //chama a procedure de inclusão da avaliação muscular
                cEstudoPotencialEvocado objEstudoPotencialEvocado = new cEstudoPotencialEvocado();
                objEstudoPotencialEvocado.IdFolha = int.Parse(txtCodigo.Text);
                objEstudoPotencialEvocado.Descricao = txtEstudoPotencial.Text.Trim();
                if (objEstudoPotencialEvocado.IncluiEstudoPotencialEvocado() == true)
                {
                    MessageBox.Show("Estudo potencial incluido com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Carrega o grid
                    this.populaGridEstudoPotencial(int.Parse(txtCodigo.Text));
                    txtEstudoPotencial.Text = string.Empty; // Limpa o campo de estudo potencial
                    txtEstudoPotencial.Focus();
                }
                else
                {
                    MessageBox.Show("Erro ao tentar incluir um Estudo Potêncial!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                carregaGridEstudoPotencial();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar incluir um Estudo Potêncial!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnExcluiEstudoPotencial_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida se existe item no grid
                if (grdEstudoPotencial.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecione um item para exclusão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Verifica se o item selecionado é válido
                if (grdEstudoPotencial.SelectedRows[0].Cells[0].Value == null || grdEstudoPotencial.SelectedRows[0].Cells[0].Value.ToString() == "0")
                {
                    MessageBox.Show("Selecione um item válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Solicita a confirmação do usuário para exclusão
                if (MessageBox.Show("Tem certeza que deseja excluir este item?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cEstudoPotencialEvocado objEstudoPotencialEvocado = new cEstudoPotencialEvocado();
                    objEstudoPotencialEvocado.IdEstudoPotenEvocado = int.Parse(grdEstudoPotencial.SelectedRows[0].Cells[0].Value.ToString());
                    if (objEstudoPotencialEvocado.ExcluiEstudoPotencialEvocado() == true)
                    {
                        MessageBox.Show("Estudo potencial evocado excluído com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Carrega o grid
                        this.populaGridEstudoPotencial(int.Parse(txtCodigo.Text));
                        txtEstudoPotencial.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao tentar excluir estudo potencial evocado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    carregaGridEstudoPotencial();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar excluir o estudo potencial evocado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void carregaTestesEspeciais(int pIdFolha)
        {
            try
            {
                string valorDesejado = ""; // Valor que você deseja selecionar no combo
                int index = 0; // Variável para armazenar o índice do item encontrado

                // Reseta os valores dos combos de testes especiais para (não)
                cboSimNaoBlink.SelectedIndex = 1;
                cboSimNaoNSPD.SelectedIndex = 1;
                cboSimNaoRBC.SelectedIndex = 1;
                cboSimNaoReflexo.SelectedIndex = 1;

                cTestesEspeciais objTestesEspeciais = new cTestesEspeciais();
                objTestesEspeciais.IdFolha = pIdFolha;
                DataTable dt = objTestesEspeciais.carregaTestesEspeciais(pIdFolha);
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][2].ToString()) == 1)
                    {
                        valorDesejado = "Sim";
                    }
                    else
                    {
                        { valorDesejado = "Não"; }
                    }
                    index = cboSimNaoBlink.FindStringExact(valorDesejado);
                    cboSimNaoBlink.SelectedIndex = index;

                    if (int.Parse(dt.Rows[0][3].ToString()) == 1)
                    {
                        valorDesejado = "Sim";
                    }
                    else
                    {
                        { valorDesejado = "Não"; }
                    }
                    index = cboSimNaoRBC.FindStringExact(valorDesejado);
                    cboSimNaoRBC.SelectedIndex = index;
                    if (int.Parse(dt.Rows[0][4].ToString()) == 1)
                    {
                        valorDesejado = "Sim";
                    }
                    else
                    {
                        { valorDesejado = "Não"; }
                    }
                    index = cboSimNaoReflexo.FindStringExact(valorDesejado);
                    cboSimNaoReflexo.SelectedIndex = index;
                    if (int.Parse(dt.Rows[0][5].ToString()) == 1)
                    {
                        valorDesejado = "Sim";
                    }
                    else
                    {
                        { valorDesejado = "Não"; }
                    }
                    index = cboSimNaoNSPD.FindStringExact(valorDesejado);
                    cboSimNaoNSPD.SelectedIndex = index;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar testes especiais!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cboSimNaoBlink_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaTestesEspeciais();
        }

        private void cboSimNaoRBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaTestesEspeciais();
        }

        private void cboSimNaoReflexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaTestesEspeciais();
        }

        private void cboSimNaoNSPD_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaTestesEspeciais();
        }

        private void AtualizaTestesEspeciais()
        {
            try
            {
                if (cAcao == Acao.COMPLEMENTO && cboTipo.SelectedIndex == 1) // Se for ENMG
                {
                    cTestesEspeciais objTestesEspeciais = new cTestesEspeciais();

                    objTestesEspeciais.IdFolha = int.Parse(txtCodigo.Text);
                    objTestesEspeciais.blinkreflex = cboSimNaoBlink.SelectedIndex == 0 ? 1 : 2;
                    objTestesEspeciais.rbc = cboSimNaoRBC.SelectedIndex == 0 ? 1 : 2;
                    objTestesEspeciais.reflexoh = cboSimNaoReflexo.SelectedIndex == 0 ? 1 : 2;
                    objTestesEspeciais.nspd = cboSimNaoNSPD.SelectedIndex == 0 ? 1 : 2;

                    if (objTestesEspeciais.atualizaTestesEspeciais() == false)
                    {
                        MessageBox.Show("Erro ao tentar atualizar os testes especiais!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar atualizar os testes especiais!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
