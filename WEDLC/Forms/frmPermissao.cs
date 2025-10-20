using Nest;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WEDLC.Banco;
using WinFormsZoom;

namespace WEDLC.Forms
{
    public partial class frmPermissao : Form
    {

        public const int codModulo = 14; //Código do módulo
        private FormZoomHelper zoomHelper;

        private Int32 idUsuario;
        private int idNivel;
        private int idModulo;
        public frmPermissao()
        {
            InitializeComponent();
            zoomHelper = new FormZoomHelper(this); // Inicializa o helper de zoom
            this.FormClosed += (s, e) => zoomHelper.Dispose(); // Descarta automaticamente quando o form for fechado
        }
        private void frmPermissão_Load(object sender, EventArgs e)
        {
            carregaTelaInicial();
        }
        public void limparTela()
        {
            try
            {
                // Limpara os controles
                txtCodigo.Text = string.Empty;
                txtNome.Text = string.Empty;
                grdUsuario.DataSource = null;
                grdPermissao.DataSource = null;
                txtModulo.Text = string.Empty;
                cboPermissao.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao limpar a tela!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void configuraGridUsuario()
        {

            grdUsuario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Desabilita a edição da coluna
            grdUsuario.Columns[0].ReadOnly = true;
            grdUsuario.Columns[1].ReadOnly = true;

            // Configurando outras propriedades
            //grdDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Preenche automaticamente
            grdUsuario.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
            grdUsuario.MultiSelect = false; // Impede seleção múltipla
            grdUsuario.AllowUserToAddRows = false;
            grdUsuario.AllowUserToDeleteRows = false; // Impede a exclusão de linhas

            //Deixa o grid zebrado
            grdUsuario.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            //Desmarca a seleção do grid
            grdUsuario.CurrentCell = null;
        }

        private void configuraGridPermissao()
        {
            grdPermissao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //Oculta a coluna
            grdPermissao.Columns["idusuario"].Visible = false;
            grdPermissao.Columns["idmodulo"].Visible = false;
            grdPermissao.Columns["idnivel"].Visible = false;

            // Desabilita a edição da coluna
            grdPermissao.Columns[0].ReadOnly = true;
            grdPermissao.Columns[1].ReadOnly = true;
            grdPermissao.Columns[2].ReadOnly = true;
            grdPermissao.Columns[3].ReadOnly = true;
            grdPermissao.Columns[4].ReadOnly = true;

            // Configurando outras propriedades
            //grdDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Preenche automaticamente
            grdPermissao.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
            grdPermissao.MultiSelect = false; // Impede seleção múltipla
            grdPermissao.AllowUserToAddRows = false;
            grdPermissao.AllowUserToDeleteRows = false; // Impede a exclusão de linhas

            //Deixa o grid zebrado
            grdPermissao.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            //Desmarca a seleção do grid
            grdPermissao.CurrentCell = null;
        }

        private DataTable buscaUsuario(int idUsuario, string Nome)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cPermissao objPermissao = new cPermissao();

                //Configura os parâmetros de pesquisa
                objPermissao.IdUsuario = idUsuario;
                objPermissao.Nome = Nome;

                dtAux = objPermissao.BuscaUsuario();

                return dtAux;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar o comentário!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private DataTable buscaPermissao(int idUsuario)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cPermissao objPermissao = new cPermissao();

                //Configura os parâmetros de pesquisa
                objPermissao.IdUsuario = idUsuario;

                dtAux = objPermissao.BuscaUsuarioPermissao();

                return dtAux;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar o comentário!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }
        private void populaGridUsuario(int IdUsuario, string nome)

        {
            try
            {
                DataTable dt = new DataTable();
                dt = this.buscaUsuario(IdUsuario, nome);

                grdUsuario.DataSource = null;

                //Renomeia as colunas do datatable
                dt.Columns["idusuario"].ColumnName = "Código";
                dt.Columns["nome"].ColumnName = "Nome";

                grdUsuario.SuspendLayout();
                grdUsuario.DataSource = dt;
                configuraGridUsuario();
                grdUsuario.ResumeLayout();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar populaGrid!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                cPermissao objPermissao = new cPermissao();
                objPermissao.IdUsuario = idUsuario;
                objPermissao.IdModulo = idModulo;
                objPermissao.IdNivel = cboPermissao.SelectedIndex;

                if (objPermissao.IdNivel <= 0)
                {
                    MessageBox.Show("Selecione um nível!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (objPermissao.atualizaPermissao() == true)
                {
                    MessageBox.Show("Atualizãção efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaTelaInicial();
                }
                else
                {
                    MessageBox.Show("Erro ao tentar gravar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar gravar!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            //Limpa campo
            txtNome.Text = string.Empty;
            int codigo = 0; //Código da especialização

            if (txtCodigo.Text.Length > 0)
            {
                codigo = Convert.ToInt16(txtCodigo.Text); //Código da especialização
                this.populaGridUsuario(codigo, "");
            }
            else
            {
                codigo = 0; //Código da especialização
                this.populaGridUsuario(codigo, "");
            }
        }

        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            string nome = string.Empty; //Código da especialização

            //Limpa campos
            txtCodigo.Text = string.Empty;

            nome = txtNome.Text;

            this.populaGridUsuario(0, nome);
            this.configuraGridUsuario();
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
            try
            {
                txtCodigo.Text = string.Empty;
                txtNome.Text = string.Empty;
                txtModulo.Text = string.Empty;
                CarregaComboPermissao();
                grdUsuario.DataSource = null;
                grdPermissao.DataSource = null;
                btnGravar.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao limpar os controles!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CarregaComboPermissao()
        {
            try
            {
                //Carrega o combo de classe
                DataTable dtPermissao = new cPermissao().BuscaNivelPermissao();
                DataRow newRow = dtPermissao.NewRow();
                newRow["idnivel"] = 0; // Defina o valor desejado para a primeira linha
                newRow["descricao"] = "Selecione..."; // Defina o valor desejado para a primeira linha
                dtPermissao.Rows.InsertAt(newRow, 0); // Insere a nova linha na primeira posição

                //Carrega o combo de tipo de folha
                cboPermissao.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Sugestão automática
                cboPermissao.AutoCompleteSource = AutoCompleteSource.ListItems; // Fonte das sugestões: itens existentes na lista
                cboPermissao.ValueMember = "idnivel";
                cboPermissao.DisplayMember = "descricao";
                cboPermissao.DataSource = dtPermissao;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar o combo de permissão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            carregaTelaInicial();
        }

        private void grdUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataTable dtPermissao = new DataTable();
                    dtPermissao = this.buscaPermissao(Convert.ToInt32(grdUsuario.Rows[e.RowIndex].Cells[0].Value.ToString()));

                    //Renomeia as colunas do datatable
                    dtPermissao.Columns["descmodulo"].ColumnName = "Módulo";
                    dtPermissao.Columns["descricao"].ColumnName = "Nível";

                    grdPermissao.SuspendLayout();
                    grdPermissao.DataSource = dtPermissao;
                    configuraGridPermissao();
                    grdPermissao.ResumeLayout();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar selecionar o usuário.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void grdPermissao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Otimização: Acessa a linha apenas uma vez
                    DataGridViewRow row = grdPermissao.Rows[e.RowIndex];

                    idUsuario = Convert.ToInt32(row.Cells[0].Value.ToString());
                    idModulo = Convert.ToInt32(row.Cells[1].Value.ToString());

                    // Preenche as TextBoxes com os valores da linha selecionada
                    txtModulo.Text = row.Cells[2].Value.ToString();
                    cboPermissao.SelectedValue = row.Cells[3].Value.ToString();
                    cboPermissao.Enabled = true;
                    cboPermissao.Focus();

                    btnGravar.Enabled = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar selecionar a permissão.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void carregaTelaInicial()
        {
            limpaControles();
            CarregaComboPermissao();

            int codigo = 0; //Código da especialização
            this.populaGridUsuario(codigo, "");
        }
    }
}
