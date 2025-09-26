using System;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;
using WEDLC.Banco;
using WinFormsZoom;

namespace WEDLC.Forms
{
    public partial class frmIndicacao : Form
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

        public const int codModulo = 7; //Código do módulo

        private FormZoomHelper zoomHelper;
        public frmIndicacao()
        {
            InitializeComponent();
            zoomHelper = new FormZoomHelper(this); // Inicializa o helper de zoom
        }
        private void frmIndicacao_Load(object sender, EventArgs e)
        {
            carregaTela();
        }
        public void carregaTela()
        {
            try
            {
                //Popula o grid
                this.populaGrid(0, 0, "");

                // Ativa a visualização do click no form
                this.KeyPreview = true;

                //Configura o grid
                configuraGrid();

                cAcao = Acao.CANCELAR;

                txtCodigo.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar a tela!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            grdDados.Columns[1].Width = 250; //Nome

            // Desabilita a edição da coluna
            grdDados.Columns[0].ReadOnly = true;
            grdDados.Columns[1].ReadOnly = true;

            // Configurando outras propriedades
            //grdDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Preenche automaticamente
            grdDados.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
            grdDados.MultiSelect = false; // Impede seleção múltipla
            grdDados.AllowUserToAddRows = false;
            grdDados.AllowUserToDeleteRows = false; // Impede a exclusão de linhas

            //Deixa o grid zebrado
            grdDados.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            grdDados.KeyPress += new KeyPressEventHandler(grdDados_KeyPress);

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;
        }
        private DataTable buscaIndicacao(int tipopesquisa, int idindicacao, string nome)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cIndicacao objIndicacao = new cIndicacao();

                //Configura os parâmetros de pesquisa
                objIndicacao.TipoPesquisa = tipopesquisa;
                objIndicacao.IdIndicacao = idindicacao;
                objIndicacao.Nome = nome;

                dtAux = objIndicacao.buscaIndicacao();

                return dtAux;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar o indicação!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }
        private void populaGrid(int tipopesquisa, int idindicacao, string nome)

        {
            try
            {
                DataTable dt = new DataTable();
                dt = this.buscaIndicacao(tipopesquisa, idindicacao, nome);

                grdDados.DataSource = null;

                //Renomeia as colunas do datatable
                dt.Columns["idindicacao"].ColumnName = "Código";
                dt.Columns["nome"].ColumnName = "Nome";

                grdDados.DataSource = dt;
                this.configuraGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar populaGrid!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        public bool validaCampos()
        {

            if (txtCodigo.Text.ToString().Trim().Length == 0 && cAcao != Acao.INSERT)
            {
                MessageBox.Show("O campo código não está preenchido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigo.Focus();
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
            txtNome.Text = string.Empty;

            txtCodigo.Enabled = false; //Desabilita o campo código
            grdDados.Enabled = false; //Desabilita o grid de dados

            txtNome.Focus(); //Coloca o foco no campo nome

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;

        }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                cIndicacao objIndicacao = new cIndicacao();

                objIndicacao.Nome = txtNome.Text;

                //Valida campos
                if (validaCampos() == true)
                {
                    if (cAcao == Acao.INSERT)
                    {
                        if (objIndicacao.incluiIndicacao() == true)
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
                        //Recebe o código do convenio transformado em inteiro
                        objIndicacao.IdIndicacao = int.Parse(txtCodigo.Text);

                        //Solicita a confirmação do usuário para alteração
                        if (MessageBox.Show("Tem certeza que deseja alterar este dado?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (objIndicacao.atualizaIndicacao() == true)
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
                        //objMusculo.IdMusculo = int.Parse(txtCodigo.Text);

                        ////Solicita a confirmação do usuário para alteração
                        //if (MessageBox.Show("Tem certeza que deseja excluir este dado?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        //    if (objMusculo.excluiMusculo() == true)
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
                    btnCancelar_Click(sender, e);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar gravar!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //if (txtCodigo.Text.Length == 0 || txtSigla.Text.Length == 0 || txtNome.Text.Length == 0)
            //{
            //    MessageBox.Show("Não existe nenhum item selecionado para exclusão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            ////Determina a acao
            //cAcao = Acao.DELETE;

            ////Chama o gravra
            //btnGravar_Click(sender, e);
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void grdDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //Determina a acao
                    cAcao = Acao.UPDATE;

                    txtCodigo.Text = grdDados.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtNome.Text = grdDados.Rows[e.RowIndex].Cells[1].Value.ToString();

                    //libera os controles 
                    controlaBotao();

                    //Desabilita o campo código
                    txtCodigo.Enabled = false;

                    txtNome.Focus();
                }

                // Desabilita a edição da coluna
                grdDados.Columns[0].ReadOnly = true;
                grdDados.Columns[1].ReadOnly = true;

                grdDados.Enabled = false; //Desabilita o grid de dados
            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao tentar selecionar o item!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retorna sem fazer nada se houver um erro
            }

        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
            {
                int tipopesquisa = 0; //Código que retorna todo select   
                int idindicacao = 0; //Código da especialização

                //Limpa campos
                txtNome.Text = string.Empty;

                // Verifica a quantidade de caracteres
                if (txtCodigo.Text.Length == 0)
                {
                    tipopesquisa = 0;
                }
                else
                {
                    tipopesquisa = 1;
                    idindicacao = int.Parse(txtCodigo.Text);
                }

                this.populaGrid(tipopesquisa, idindicacao, "");
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

                if (txtNome.Text.Length == 0)
                {
                    tipopesquisa = 0;
                }
                else
                {
                    tipopesquisa = 3;
                    nome = txtNome.Text;
                }

                this.populaGrid(tipopesquisa, 0, nome);
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
            txtNome.Text = string.Empty;

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;
        }
    }
}
