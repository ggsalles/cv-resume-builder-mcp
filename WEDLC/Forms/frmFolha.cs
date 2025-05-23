using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WEDLC.Banco;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            CANCELAR = 4
        }

        public Acao cAcao = Acao.UPDATE;

        //Código do módulo
        public const int codModulo = 1;

        public DataTable dtTipoFolha = new DataTable();
        public DataTable dtGrupo = new DataTable();

        public frmFolha()
        {
            InitializeComponent();
        }

        private void frmFolha_Load(object sender, EventArgs e)
        {
            carregaCombo();
            carregaTela();

        }

        public void carregaTela()
        {
            //Popula o grid
            this.populaGrid(0, 0, "", "");

            // Ativa a visualização do click no form
            this.KeyPreview = true;

            //Configura o grid
            configuraGrid();

            cAcao = Acao.CANCELAR;
        }

        public void carregaCombo()
        {

            carregaTipo();
            carregaGrupo(0);
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

            //Se clicou em gravar
            if (cAcao == Acao.SAVE || cAcao == Acao.CANCELAR)
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
        private void configuraGrid()
        {
            // Ajustando o tamanho das colunas e ocultando as que não são necessárias
            grdDados.Columns[0].Width = 80; //ID
            grdDados.Columns[1].Width = 120; //Sigla
            grdDados.Columns[2].Width = 350; //Nome
            grdDados.Columns[3].Visible = false; //IdTipo
            grdDados.Columns[4].Width = 80; //Tipo
            grdDados.Columns[5].Visible = false; //IdGrupo
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
        }

        private DataTable buscaTipoFolha()
        {
            try
            {
                DataTable dtAux = new DataTable();
                cFolha objcTipoFolha = new cFolha();

                dtAux = objcTipoFolha.buscaTipoFolha();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar a especialização!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
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

        private void populaGrid(int tipopesquisa, Int32 idfolha, string sigla, string nome)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = this.buscaFolha (tipopesquisa, idfolha, sigla, nome);

                grdEstudoPotenciais.DataSource = null;

                //Renomeia as colunas do datatable
                dt.Columns["idfolha"].ColumnName = "Código";
                dt.Columns["sigla"].ColumnName = "Sigla";
                dt.Columns["nome"].ColumnName = "Nome";
                dt.Columns["idtipofolha"].ColumnName = "IdTipoFolha";
                dt.Columns["descricaotipo"].ColumnName = "Tipo";
                dt.Columns["idgrupo"].ColumnName = "IdGrupo";
                dt.Columns["descricaogrupo"].ColumnName = "Grupo";

                grdDados.DataSource = dt;

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

            return true;

        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Determina a acao
            cAcao = Acao.INSERT;

            //Prepara os botões para a inclusão
            controlaBotao();

            //limpa controles
            txtCodigo.Text = string.Empty;
            txtCodigo.Enabled = false; //Desabilita o campo código
            txtSigla.Text = string.Empty;
            txtNome.Text = string.Empty;

            //Deixa o foco no nome
            txtSigla.Focus();

            //Desmarca a seleção do grid
            grdEstudoPotenciais.CurrentCell = null;

        }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            cEspecializacao objEspecializacao = new cEspecializacao();

            objEspecializacao.Nome = txtNome.Text;
            objEspecializacao.Sigla = txtSigla.Text;

            //Valida campos
            if (validaCampos() == true)
            {
                if (cAcao == Acao.INSERT)
                {
                    if (objEspecializacao.incluiEspecialidade() == true)
                    {
                        MessageBox.Show("Inclusão efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Erro ao tentar incluir!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                }
                else if (cAcao == Acao.UPDATE)
                {
                    objEspecializacao.IdEspecializacao = int.Parse(txtCodigo.Text);

                    //Solicita a confirmação do usuário para alteração
                    if (MessageBox.Show("Tem certeza que deseja alterar este dado?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (objEspecializacao.atualizaEspecializacao() == true)
                        {
                            MessageBox.Show("Alteração efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao tentar atualilzar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                }
                else if (cAcao == Acao.DELETE)
                {
                    objEspecializacao.IdEspecializacao = int.Parse(txtCodigo.Text);

                    //Solicita a confirmação do usuário para alteração
                    if (MessageBox.Show("Tem certeza que deseja excluir este dado?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (objEspecializacao.excluiEspecializacao() == true)
                        {
                            MessageBox.Show("Exclusão efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao tentar atualilzar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                //Chama o evento cancelar
                btnCancelar_Click(sender, e);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Determina a acao
            cAcao = Acao.CANCELAR;

            controlaBotao();

            //Habilita o campo código
            txtCodigo.Enabled = true;

            //Limpa os campos
            limpaControles();

            //Desmarca a seleção do grid
            grdEstudoPotenciais.CurrentCell = null;

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
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
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

                this.populaGrid(tipopesquisa, idespecializacao, "", "");
                this.configuraGrid();
            }

        }
        private void txtSigla_KeyUp(object sender, KeyEventArgs e)
        {
            int tipopesquisa = 2; //Código que pesquisa pela sigla   
            string sigla = string.Empty; //Código da especialização

            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
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

                this.populaGrid(tipopesquisa, 0, sigla, "");
                this.configuraGrid();
            }
        }
        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            int tipopesquisa = 3; //Código que pesquisa pelo nome 
            string nome = string.Empty; //Código da especialização

            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
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

                this.populaGrid(tipopesquisa, 0, "", nome);
                this.configuraGrid();
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

        private void limpaControles()
        {
            txtCodigo.Text = string.Empty;
            txtSigla.Text = string.Empty;
            txtNome.Text = string.Empty;
            cboTipo.SelectedIndex = 0;
            cboGrupo.SelectedIndex = 0;

            //Desmarca a seleção do grid
            grdEstudoPotenciais.CurrentCell = null;
        }

        private void cboTipo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FiltrarComboTipo(cboTipo.Text.ToString());
        }

        private void FiltrarComboTipo(string texto)
        {
            DataTable dt = (DataTable)cboTipo.DataSource;
            bool existe = dt.AsEnumerable().Any(row => row.Field<string>("descricao") == cboTipo.Text);
            if (existe == false)
            {
                MessageBox.Show("Selecione um item válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTipo.Focus();
            }
        }

        public void carregaTipo()
        {
            //Carrega o combo de tipo de folha
            cboTipo.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboTipo.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cFolha objcTipoFolha = new cFolha();
            dtTipoFolha = objcTipoFolha.buscaTipoFolha();
            DataRow newRow = dtTipoFolha.NewRow();
            newRow["descricao"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtTipoFolha.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição
            cboTipo.DataSource = dtTipoFolha;
            cboTipo.ValueMember = "idtipofolha";
            cboTipo.DisplayMember = "descricao";
            cboTipo.Refresh();
        }

        public void carregaGrupo(int pTipoFolha)
        {
            //Carrega o combo de tipo de folha
            cboGrupo.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
            cboGrupo.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista

            cFolha objcTipoFolha = new cFolha();
            dtGrupo = objcTipoFolha.buscaGrupoFolha(pTipoFolha);
            DataRow newRow = dtGrupo.NewRow();
            newRow["descricao"] = "Selecione..."; // Defina o valor desejado para a primeira linha
            dtGrupo.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição
            cboGrupo.DataSource = dtGrupo;
            cboGrupo.ValueMember = "idgrupo";
            cboGrupo.DisplayMember = "descricao";
            cboGrupo.Refresh();
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

        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTipo_Validated(object sender, EventArgs e)
        {
            if (cboTipo.SelectedValue.ToString() == "")
            {
                carregaGrupo(0); // carrega tudo
            }
            else
            {
                carregaGrupo(int.Parse(cboTipo.SelectedValue.ToString()));
                cboGrupo.Focus();
            }
        }

        private void CarregaComboSimNao(System.Windows.Forms.ComboBox objCombo)
        {
            objCombo.DisplayMember = "Descricao";
            objCombo.ValueMember = "Id";
            objCombo.Items.Add(new cSimNao { Id = "0", Descricao = "Nenhum" });
            objCombo.Items.Add(new cSimNao { Id = "1", Descricao = "Sim" });
            objCombo.Items.Add(new cSimNao { Id = "2", Descricao = "Não" });

            objCombo.DropDownStyle = ComboBoxStyle.DropDownList; // Define o estilo do ComboBox como DropDownList
            objCombo.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista
            objCombo.SelectedIndex = 0; // Define o índice padrão como 0 (primeiro item);
        }

        private void grdDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //Determina a acao
                cAcao = Acao.UPDATE;

                txtCodigo.Text = grdDados.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtSigla.Text = grdDados.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtNome.Text = grdDados.Rows[e.RowIndex].Cells[2].Value.ToString();
                cboTipo.SelectedValue = grdDados.Rows[e.RowIndex].Cells[3].Value.ToString();
                cboGrupo.SelectedValue = grdDados.Rows[e.RowIndex].Cells[5].Value.ToString();

                //libera os controles 
                controlaBotao();

                //Desabilita o campo código
                txtCodigo.Enabled = false;

                txtSigla.Focus();
            }

            // Desabilita a edição da coluna
            grdDados.Columns[0].ReadOnly = true;
            grdDados.Columns[1].ReadOnly = true;
            grdDados.Columns[2].ReadOnly = true;

        }
    }
}
