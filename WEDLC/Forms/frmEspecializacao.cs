using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmEspecializacao : Form
    {

        public enum Acao
        {
            INSERT = 0,
            UPDATE = 1,
            DELETE = 2

        }

        public Acao cAcao = Acao.UPDATE;

        public frmEspecializacao()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Prepara os botões para a inclusão
            controlaBotao(true);

            //limpa controles
            txtCodigo.Text = string.Empty;
            txtNome.Text = string.Empty;

            //Desbloqueia controles
            txtNome.Enabled = true;

            //Deixa o foco no nome
            txtNome.Focus();

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;

            //Determina a acao
            cAcao = Acao.INSERT;

        }
        private void controlaBotao(bool libera)
        {
            btnNovo.Enabled = !libera;
            btnGravar.Enabled = libera;
            btnCancelar.Enabled = libera;
            btnExcluir.Enabled = !libera;

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            cEspecializacao objEspecializacao = new cEspecializacao();
            objEspecializacao.Nome = txtNome.Text;

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
                    objEspecializacao.Codigo = int.Parse(txtCodigo.Text);

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
                    objEspecializacao.Codigo = int.Parse(txtCodigo.Text);

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
            controlaBotao(false);

            //limpa controles
            txtCodigo.Text = string.Empty;
            txtNome.Text = string.Empty;

            //Bloqueia controles
            txtNome.Enabled = false;

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;

            //Determina a acao
            cAcao = Acao.UPDATE;

            //Carrega o grid
            carregaTela();
        }

        private void configuraGrid()
        {

            //Configurando as colunas manualmente
            //grdDados.ColumnCount = 2; // Define o número de colunas
            //grdDados.Columns[0].Name = "Código";
            //grdDados.Columns[1].Name = "Nome";

            // Ajustando o tamanho das colunas
            grdDados.Columns[0].Width = 80;
            grdDados.Columns[1].Width = 300;

            // Desabilita a edição da coluna
            grdDados.Columns[0].ReadOnly = true;
            grdDados.Columns[1].ReadOnly = true;

            // Configurando outras propriedades
            //grdDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Preenche automaticamente
            grdDados.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
            grdDados.MultiSelect = false; // Impede seleção múltipla
            grdDados.AllowUserToAddRows = false;

            //Deixa o grid zebrado
            grdDados.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            //// Adicionando algumas linhas de exemplo
            //string[] row1 = new string[] { "1", "Gustavo" };
            //string[] row2 = new string[] { "2", "Viviane" };

            //grdDados.Rows.Add(row1);
            //grdDados.Rows.Add(row2);

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;

        }

        private void grdDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                txtCodigo.Text = grdDados.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNome.Text = grdDados.Rows[e.RowIndex].Cells[1].Value.ToString();

                //libera os controles 
                btnGravar.Enabled = true;
                txtNome.Enabled = true;

                txtNome.Focus();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //valida codigo para exclusão
            if (txtCodigo.Text.Length == 0)
            {
                MessageBox.Show("Não existe nenhum item selecionado para exclusão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //Determina a acao
            cAcao = Acao.DELETE;

            //Chama o gravra
            btnGravar_Click(sender, e);
        }

        private void frmEspecializacao_Load(object sender, EventArgs e)
        {
            carregaTela();
        }

        private DataTable retornaEspecializacao(int codigo, string nome)
        {
            DataTable dtAux = new DataTable();
            cEspecializacao objcEspecializacao = new cEspecializacao();

            dtAux = objcEspecializacao.buscaEspecializacao(codigo, nome);

            return dtAux;
        }

        private void populaGrid()
        {
            DataTable dt = new DataTable();
            dt = this.retornaEspecializacao(0, "");

            grdDados.DataSource = null;

            dt.Columns["codigo"].ColumnName = "Código";
            dt.Columns["nome"].ColumnName = "Nome";

            grdDados.DataSource = dt;
        }

        public bool validaCampos()
        {
            if (txtNome.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Favor preencher o nome", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNome.Focus();
                return false;
            }

            return true;

        }

        public void carregaTela()
        {
            //Popula o grid
            this.populaGrid();

            //Configura o grid
            configuraGrid();
        }
    }
}
