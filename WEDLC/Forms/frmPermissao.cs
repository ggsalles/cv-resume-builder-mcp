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

        public frmPermissao()
        {
            InitializeComponent();
            zoomHelper = new FormZoomHelper(this); // Inicializa o helper de zoom
            this.FormClosed += (s, e) => zoomHelper.Dispose(); // Descarta automaticamente quando o form for fechado
            this.DoubleBuffered = true;
        }
        private void frmPermissao_Load(object sender, EventArgs e)
        {
            if (!cPermissao.PodeAcessarModulo(codModulo))
            {
                MessageBox.Show("Usuário sem acesso", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Fecha de forma segura depois que o handle estiver pronto
                this.BeginInvoke(new Action(() => this.Close()));
                return;
            }

            carregaTelaInicial();

            // GRAVA LOG
            clLog objcLog = new clLog();
            objcLog.IdLogDescricao = 4; // descrição na tabela LOGDESCRICAO 
            objcLog.IdUsuario = Sessao.IdUsuario;
            objcLog.Descricao = this.Name;
            objcLog.incluiLog();
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
            //Oculta a coluna
            grdPermissao.Columns["idusuario"].Visible = false;
            grdPermissao.Columns["idmodulo"].Visible = false;
            grdPermissao.Columns["idnivel"].Visible = false;

            grdPermissao.Columns[2].ReadOnly = true; // Deixa a coluna Módulo como somente leitura

            // === 🔹 Cria a coluna ComboBox (Nível) ===
            DataGridViewComboBoxColumn colNivel = new DataGridViewComboBoxColumn();
            colNivel.HeaderText = "Nível";
            colNivel.Name = "Nível";
            colNivel.DataPropertyName = "idnivel"; // valor que será salvo

            // 🔹 Busca níveis direto do banco
            DataTable dtNiveis = new cPermissao().BuscaNivelPermissao();
            colNivel.DataSource = dtNiveis;
            colNivel.DisplayMember = "descricao";  // texto visível (ajuste o nome conforme o retorno)
            colNivel.ValueMember = "idnivel";      // id real
            colNivel.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            colNivel.FlatStyle = FlatStyle.Flat;
            colNivel.ReadOnly = false;

            grdPermissao.Columns.Add(colNivel);

            // === ⚙️ Outras configurações do grid ===
            grdPermissao.SelectionMode = DataGridViewSelectionMode.CellSelect;
            grdPermissao.MultiSelect = false;
            grdPermissao.AllowUserToAddRows = false;
            grdPermissao.AllowUserToDeleteRows = false;
            grdPermissao.EnableHeadersVisualStyles = false;
            //grdPermissao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // === 🟦 Cores alternadas (zebrado) ===
            grdPermissao.RowsDefaultCellStyle.BackColor = Color.White;
            grdPermissao.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // === ⚙️ Corrige a atualização da combo ===
            grdPermissao.CurrentCellDirtyStateChanged -= grdPermissao_CurrentCellDirtyStateChanged;
            grdPermissao.CurrentCellDirtyStateChanged += grdPermissao_CurrentCellDirtyStateChanged;

            grdPermissao.CellValueChanged -= grdPermissao_CellValueChanged;
            grdPermissao.CellValueChanged += grdPermissao_CellValueChanged;
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
                if (!cPermissao.PodeGravarModulo(codModulo))
                {
                    MessageBox.Show("Você não tem permissão para gravar neste módulo","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                cPermissao objPermissao = new cPermissao();

                foreach (DataGridViewRow row in grdPermissao.Rows)
                {

                    int idUsuario = Convert.ToInt32(row.Cells["idusuario"].Value);
                    int idModulo = Convert.ToInt32(row.Cells["idmodulo"].Value);
                    int idNivel = 0;

                    if (row.Cells["Nível"] is DataGridViewComboBoxCell comboCell)
                    {
                        idNivel = Convert.ToInt32(comboCell.Value);
                    }
                    else if (row.Cells["idnivel"].Value != null)
                    {
                        idNivel = Convert.ToInt32(row.Cells["idnivel"].Value);
                    }

                    // instancia o objeto de permissão (ou usa o seu existente)
                    objPermissao.IdUsuario = idUsuario;
                    objPermissao.IdModulo = idModulo;
                    objPermissao.IdNivel = idNivel;

                    // chama a procedure de update
                    objPermissao.atualizaPermissao();
                }

                MessageBox.Show("Atualização efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                carregaTelaInicial();

                // GRAVA LOG
                clLog objcLog = new clLog();
                objcLog.IdLogDescricao = 5; // descrição na tabela LOGDESCRICAO 
                objcLog.IdUsuario = Sessao.IdUsuario;
                objcLog.Descricao = this.Name;
                objcLog.incluiLog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar permissões: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // GRAVA LOG
                clLog objcLog = new clLog();
                objcLog.IdLogDescricao = 3; // descrição na tabela LOGDESCRICAO 
                objcLog.IdUsuario = Sessao.IdUsuario;
                objcLog.Descricao = this.Name + " - " + ex.Message;
                objcLog.incluiLog();
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
                grdUsuario.DataSource = null;
                grdPermissao.DataSource = null;
                btnGravar.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao limpar os controles!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //dtPermissao.Columns["descricao"].ColumnName = "Nível";

                    grdPermissao.SuspendLayout();
                    grdPermissao.DataSource = null;
                    grdPermissao.Rows.Clear();
                    grdPermissao.Columns.Clear();
                    grdPermissao.DataSource = dtPermissao;
                    configuraGridPermissao();
                    grdPermissao.ResumeLayout();
                    // força o autoajuste (garante em todos os reloads)
                    this.BeginInvoke(new Action(() =>
                    {
                        grdPermissao.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    }));

                    btnGravar.Enabled = true; //Habilita o botão gravar
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar selecionar o usuário.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void carregaTelaInicial()
        {
            limpaControles();

            int codigo = 0; //Código da especialização
            this.populaGridUsuario(codigo, "");
        }

        private void grdPermissao_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdPermissao.IsCurrentCellDirty)
                grdPermissao.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void grdPermissao_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && grdPermissao.Columns[e.ColumnIndex].Name == "Nível")
            {
                var idNivel = grdPermissao.Rows[e.RowIndex].Cells["Nível"].Value;
                var modulo = grdPermissao.Rows[e.RowIndex].Cells["Módulo"].Value;
            }
        }    
    }
}
