using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmAtividadeInsercao : Form
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
        public const int codModulo = 2;

        //Variável para identificar se a chamada vem do fomrmulário de resultado do paciente
        public bool VemdeResultado { get; set; } = false;
        public int IdAatividadeInsercao { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Texto { get; set; }

        public frmAtividadeInsercao()
        {
            InitializeComponent();
        }

        private void frmAtividadeInsercao_Load(object sender, EventArgs e)
        {
            carregaTela();

            // Se a chamada for do formulário de resultado, desabilita o botão novo
            if (VemdeResultado == true)
            {
                btnNovo.Enabled = false;
            }
        }

        public void carregaTela()
        {
            try
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
            grdDados.Columns[1].Width = 80; //Sigla
            grdDados.Columns[2].Width = 250; //Nome
            grdDados.Columns[3].Width = 250; //Texto

            // Desabilita a edição da coluna
            grdDados.Columns[0].ReadOnly = true;
            grdDados.Columns[1].ReadOnly = true;
            grdDados.Columns[2].ReadOnly = true;
            grdDados.Columns[3].ReadOnly = false;

            //Deixa a coluna invisível
            grdDados.Columns[3].Visible = false; //Texto

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

        private DataTable buscaAtividadeinsercao(int tipopesquisa, Int32 idatividadeinsercao, string sigla, string nome)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cAtividadeInsercao objAtividadeInsercao = new cAtividadeInsercao();

                //Configura os parâmetros de pesquisa
                objAtividadeInsercao.TipoPesquisa = tipopesquisa;
                objAtividadeInsercao.IdAatividadeInsercao = idatividadeinsercao;
                objAtividadeInsercao.Sigla = sigla;
                objAtividadeInsercao.Nome = nome;

                dtAux = objAtividadeInsercao.buscaAtividadeinsercao();

                return dtAux;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar o comentário!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private void populaGrid(int tipopesquisa, Int32 idatividadeinsercao, string sigla, string nome)

        {
            try
            {
                DataTable dt = new DataTable();
                dt = this.buscaAtividadeinsercao(tipopesquisa, idatividadeinsercao, sigla, nome);

                grdDados.DataSource = null;

                //Renomeia as colunas do datatable
                dt.Columns["idatividadeinsercao"].ColumnName = "Código";
                dt.Columns["sigla"].ColumnName = "Sigla";
                dt.Columns["nome"].ColumnName = "Nome";
                dt.Columns["texto"].ColumnName = "Texto";

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

            if (txtTexto.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("O campo texto não está preenchido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtTexto.Text = string.Empty;

            txtCodigo.Enabled = false; //Desabilita o campo código
            txtTexto.Enabled = true; //Habilita o campo texto
            grdDados.Enabled = false; //Desabilita o grid

            //Deixa o foco no nome
            txtSigla.Focus();

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;

        }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                cAtividadeInsercao objAtividadeInsercao = new cAtividadeInsercao();

                objAtividadeInsercao.Nome = txtNome.Text;
                objAtividadeInsercao.Sigla = txtSigla.Text;
                objAtividadeInsercao.Texto = txtTexto.Text;

                //Valida campos
                if (validaCampos() == true)
                {
                    if (cAcao == Acao.INSERT)
                    {
                        if (objAtividadeInsercao.IncluiAtividaDeInsercao() == true)
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
                        //Recebe o código do musculo transformado em inteiro
                        objAtividadeInsercao.IdAatividadeInsercao = int.Parse(txtCodigo.Text);

                        //Solicita a confirmação do usuário para alteração
                        if (MessageBox.Show("Tem certeza que deseja alterar este dado?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (objAtividadeInsercao.atualizaAtividaDeInsercao() == true)
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
            // Se a chamada for do formulário de resultado, apenas limpa os campos e retorna
            if (VemdeResultado == true)
            {
                //Habilita os campos
                txtCodigo.Enabled = true;
                txtNome.Enabled = true;
                txtSigla.Enabled = true;

                //Desabilita o cancelar
                btnCancelar.Enabled = false;

                //Limpa os campos
                limpaControles();

                //Habilita o grid
                grdDados.Enabled = true;

                //Desmarca a seleção do grid
                grdDados.CurrentCell = null;

                //Carrega o grid
                carregaTela();

                return;
            }

            //Determina a acao
            cAcao = Acao.CANCELAR;

            controlaBotao();

            //Habilita o campo código
            txtCodigo.Enabled = true;

            //Desabilita os campos normncs, normncm e normlmd
            txtTexto.Enabled = false;

            //Limpa os campos
            limpaControles();

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;

            //Habilita o grid
            grdDados.Enabled = true;

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
            if (VemdeResultado)
            {
                if (!string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    // Preenche as propriedades públicas com os dados do formulário
                    this.IdAatividadeInsercao = Int32.Parse(txtCodigo.Text);
                    this.Sigla = txtSigla.Text;
                    this.Nome = txtNome.Text;
                    this.Texto = txtTexto.Text;

                    // Define o resultado do diálogo como OK
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                this.Close();
            }
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
                txtTexto.Text = grdDados.Rows[e.RowIndex].Cells[3].Value.ToString();

                //libera os controles 
                controlaBotao();

                //Desabilita o campo código
                txtCodigo.Enabled = false;
                txtTexto.Enabled = true;

                //desabilita o grid
                grdDados.Enabled = false;

                txtSigla.Focus();

                // Se a chamada for do formulário de resultado, desabilita...
                if (VemdeResultado == true)
                {
                    //Desabilita botões
                    btnNovo.Enabled = false;
                    btnGravar.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnExcluir.Enabled = false;
                    //Desabilita os campos
                    txtCodigo.Enabled = false;
                    txtSigla.Enabled = false;
                    txtNome.Enabled = false;
                    txtTexto.Enabled = false;
                    //Habilita o grid
                    grdDados.Enabled = true;
                }
            }

            // Desabilita a edição da coluna
            grdDados.Columns[0].ReadOnly = true;
            grdDados.Columns[1].ReadOnly = true;
            grdDados.Columns[2].ReadOnly = true;
            grdDados.Columns[3].ReadOnly = true;
        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
            {
                int tipopesquisa = 0; //Código que retorna todo select   
                int idatividadeinsercao = 0; //Código da Atividade de Inserção

                //Limpa campos
                txtSigla.Text = string.Empty;
                txtNome.Text = string.Empty;
                txtTexto.Text = string.Empty;

                // Verifica a quantidade de caracteres
                if (txtCodigo.Text.Length == 0)
                {
                    tipopesquisa = 0;
                }
                else
                {
                    tipopesquisa = 1;
                    idatividadeinsercao = int.Parse(txtCodigo.Text);
                }

                this.populaGrid(tipopesquisa, idatividadeinsercao, "", "");
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
                txtTexto.Text = string.Empty;

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
                txtTexto.Text = string.Empty;

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
            txtTexto.Text = string.Empty;

            //Desmarca a seleção do grid
            grdDados.CurrentCell = null;
        }
    }
}
