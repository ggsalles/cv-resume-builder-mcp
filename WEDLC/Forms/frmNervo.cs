using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmNervo : Form
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
        public const int codModulo = 9;

        public frmNervo()
        {
            InitializeComponent();
        }

        private void frmNervo_Load(object sender, EventArgs e)
        {
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

            txtCodigo.Focus();
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

            //Configurando as colunas manualmente
            //grdDados.ColumnCount = 2; // Define o número de colunas
            //grdDados.Columns[0].Name = "Código";
            //grdDados.Columns[1].Name = "Nome";

            // Ajustando o tamanho das colunas
            grdDados.Columns[0].Width = 80; //ID
            grdDados.Columns[1].Width = 80; //Sigla
            grdDados.Columns[2].Width = 250; //Nome
            grdDados.Columns[3].Width = 80; //Norm LMD
            grdDados.Columns[4].Width = 80; //Norm NCM
            grdDados.Columns[5].Width = 80; //Norm NCS

            // Desabilita a edição da coluna
            grdDados.Columns[0].ReadOnly = true;
            grdDados.Columns[1].ReadOnly = true;
            grdDados.Columns[2].ReadOnly = true;
            grdDados.Columns[3].ReadOnly = true;
            grdDados.Columns[4].ReadOnly = true;
            grdDados.Columns[5].ReadOnly = true;

            // Configurando outras propriedades
            //grdDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Preenche automaticamente
            grdDados.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
            grdDados.MultiSelect = false; // Impede seleção múltipla
            grdDados.AllowUserToAddRows = false;
            grdDados.AllowUserToDeleteRows = false; // Impede a exclusão de linhas

            //Deixa o grid zebrado
            grdDados.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            grdDados.KeyPress += new KeyPressEventHandler(grdDados_KeyPress);

            //// Adicionando algumas linhas de exemplo
            //string[] row1 = new string[] { "1", "Gustavo" };
            //string[] row2 = new string[] { "2", "Viviane" };

            //grdDados.Rows.Add(row1);
            //grdDados.Rows.Add(row2);

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;
        }

        private DataTable buscaNervo(int tipopesquisa, int idnervo, string sigla, string nome)
        {
            DataTable dtAux = new DataTable();
            cNervo objcNervo = new cNervo();
            objcNervo.TipoPesquisa = tipopesquisa;
            objcNervo.IdNervo = idnervo;
            objcNervo.Sigla = sigla;
            objcNervo.Nome = nome;

            dtAux = objcNervo.buscaNervo();

            return dtAux;
        }

        private void populaGrid(int tipopesquisa, int idnervo, string sigla, string nome)

        {
            DataTable dt = new DataTable();
            dt = this.buscaNervo(tipopesquisa, idnervo, sigla, nome);

            grdDados.DataSource = null;

            //Renomeia as colunas do datatable
            dt.Columns["idnervo"].ColumnName = "Código";
            dt.Columns["sigla"].ColumnName = "Sigla";
            dt.Columns["nome"].ColumnName = "Nome";
            dt.Columns["normlmd"].ColumnName = "Norm LMD";
            dt.Columns["normncm"].ColumnName = "Norm NCM";
            dt.Columns["normncs"].ColumnName = "Nome NCS";
            grdDados.DataSource = dt;
            this.configuraGrid();

        }

        public bool validaCampos()
        {

            if (txtCodigo.Text.ToString().Trim().Length == 0 && cAcao != Acao.INSERT)
            {
                MessageBox.Show("O campo código não está preenchido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigo.Focus();
                return false;
            }

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
            txtSigla.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtNormNcs.Text = string.Empty;
            txtNormNcm.Text = string.Empty;
            txtNormLmd.Text = string.Empty;

            txtCodigo.Enabled = false; //Desabilita o campo código
            grdDados.Enabled = false; //Desabilita o grid

            txtNormNcs.Enabled = true; //Habilita o campo normncs
            txtNormNcm.Enabled = true; //Habilita o campo normncm
            txtNormLmd.Enabled = true; //Habilita o campo normlmd

            //Deixa o foco no nome
            txtSigla.Focus();

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;

        }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            cNervo objNervo = new cNervo();

            objNervo.Nome = txtNome.Text;
            objNervo.Sigla = txtSigla.Text;
            objNervo.NormLmd = txtNormLmd.Text;
            objNervo.NormNcm = txtNormNcm.Text;
            objNervo.NormNcs = txtNormNcs.Text;

            //Valida campos
            if (validaCampos() == true)
            {
                if (cAcao == Acao.INSERT)
                {
                    if (objNervo.incluiNervo() == true)
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
                    objNervo.IdNervo = int.Parse(txtCodigo.Text);

                    //Solicita a confirmação do usuário para alteração
                    if (MessageBox.Show("Tem certeza que deseja alterar este dado?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (objNervo.AtualizaNervo() == true)
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
                    objNervo.IdNervo = int.Parse(txtCodigo.Text);

                    //Solicita a confirmação do usuário para alteração
                    if (MessageBox.Show("Tem certeza que deseja excluir este dado?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (objNervo.ExcluiNervo() == true)
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

            //Habilita o grid
            grdDados.Enabled = true;

            //Desabilita os campos normncs, normncm e normlmd
            txtNormNcs.Enabled = false;
            txtNormNcm.Enabled = false;
            txtNormLmd.Enabled = false;

            //Limpa os campos
            limpaControles();

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;

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

        private void grdDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //Determina a acao
                cAcao = Acao.UPDATE;

                txtCodigo.Text = grdDados.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtSigla.Text = grdDados.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtNome.Text = grdDados.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtNormLmd.Text = grdDados.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtNormNcm.Text = grdDados.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtNormNcs.Text = grdDados.Rows[e.RowIndex].Cells[5].Value.ToString();

                //libera os controles 
                controlaBotao();

                //Desabilita o campo código
                txtCodigo.Enabled = false;
                txtNormNcs.Enabled = true;
                txtNormNcm.Enabled = true;
                txtNormLmd.Enabled = true;

                grdDados.Enabled = false; //Desabilita o grid


                txtSigla.Focus();
            }

            // Desabilita a edição da coluna
            grdDados.Columns[0].ReadOnly = true;
            grdDados.Columns[1].ReadOnly = true;
            grdDados.Columns[2].ReadOnly = true;
            grdDados.Columns[3].ReadOnly = true;
            grdDados.Columns[4].ReadOnly = true;
            grdDados.Columns[5].ReadOnly = true;
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
                txtNormLmd.Text = string.Empty;
                txtNormNcm.Text = string.Empty;
                txtNormNcs.Text = string.Empty;

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
                txtNormLmd.Text = string.Empty;
                txtNormNcm.Text = string.Empty;
                txtNormNcs.Text = string.Empty;

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
                txtNormLmd.Text = string.Empty;
                txtNormNcm.Text = string.Empty;
                txtNormNcs.Text = string.Empty;

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

        private void grdDados_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se a tecla pressionada é a barra de espaço
            if (e.KeyChar == (char)Keys.Space)
            {
                // Cancela o evento para impedir que a barra de espaço seja registrada
                e.Handled = true;
            }
        }

        private void limpaControles()
        {
            txtCodigo.Text = string.Empty;
            txtSigla.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtNormLmd.Text = string.Empty;
            txtNormNcm.Text = string.Empty;
            txtNormNcs.Text = string.Empty;

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;
        }
    }
}
