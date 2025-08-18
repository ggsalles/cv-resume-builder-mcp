using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmResultadoMusculoNeuro : Form
    {
        DataTable dadosXML = new DataTable();

        // Create a ToolTip component
        ToolTip toolTip1 = new ToolTip();

        public cResultadoAvaliacaoMuscular objResultadoAvaliacaoMuscular;
        public cResultadoNeuroCondMotora objResultadoNeuroCondMotora;
        public cAtividadeInsercao objAtividadeInsercao;

        public frmResultadoMusculoNeuro()
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
            this.Text = "Folha: " + objResultadoAvaliacaoMuscular.Sigla.ToString() + " - " + objResultadoAvaliacaoMuscular.Nome.ToString();

            if (CarregaAvaliacaoMuscular() == false)
            {
                return;
            }

            if (CarregaVelocidadeNeuroCondMotora() == false)
            {
                return;
            }

            if (CarregaVelocidadeNeuroCondSensorial() == false)
            {
                return;
            }

        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
       
        public bool CarregaAvaliacaoMuscular()
        {
            try
            {
                // Define o IdFolha e IdResultado com base no objeto atual
                objResultadoAvaliacaoMuscular.IdFolha = this.objResultadoAvaliacaoMuscular.IdFolha;
                objResultadoAvaliacaoMuscular.IdPaciente = this.objResultadoAvaliacaoMuscular.IdPaciente;
                // Busca os dados da avaliação muscular
                DataTable dtGridAvaliacaoMuscular = objResultadoAvaliacaoMuscular.buscaResultadoAvaliacaoMuscular();

                grdAvaliacaoMuscular.DataSource = null;

                //Renomeia as colunas do datatable
                dtGridAvaliacaoMuscular.Columns["sigla"].ColumnName = "Sigla";
                dtGridAvaliacaoMuscular.Columns["nome"].ColumnName = "Nome";
                dtGridAvaliacaoMuscular.Columns["lado"].ColumnName = "Lado";

                grdAvaliacaoMuscular.SuspendLayout();
                grdAvaliacaoMuscular.DataSource = dtGridAvaliacaoMuscular;
                configuraGridAvaliacaoMuscular(); // Configura o grid de dados
                grdAvaliacaoMuscular.ResumeLayout();

                return true; // Sucesso ao carregar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar avaliação muscular: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao carregar os dados
            }

        }
        private void configuraGridAvaliacaoMuscular()
        {
            try
            {
                grdAvaliacaoMuscular.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // Desabilita a edição da coluna
                grdAvaliacaoMuscular.Columns[0].ReadOnly = true;
                grdAvaliacaoMuscular.Columns[1].ReadOnly = true;
                grdAvaliacaoMuscular.Columns[2].ReadOnly = true;
                grdAvaliacaoMuscular.Columns[3].ReadOnly = true;
                grdAvaliacaoMuscular.Columns[4].ReadOnly = false;

                //Oculta colunas que não são necessárias
                grdAvaliacaoMuscular.Columns["idfolha"].Visible = false; // Oculta a coluna IdFolha
                grdAvaliacaoMuscular.Columns["idresultado"].Visible = false; // Oculta a coluna IdResultado

                // Configurando outras propriedades
                grdAvaliacaoMuscular.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
                grdAvaliacaoMuscular.MultiSelect = false; // Impede seleção múltipla
                grdAvaliacaoMuscular.AllowUserToAddRows = false; // Impede adição de novas linhas
                grdAvaliacaoMuscular.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
                grdAvaliacaoMuscular.CurrentCell = null; // Desmarca a célula atual
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar configurar o grid de dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        private void grdAvaliacaoMuscular_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Verifica se é a coluna/célula específica que você quer restringir
            if (grdAvaliacaoMuscular.CurrentCell.ColumnIndex == grdAvaliacaoMuscular.Columns[4].Index) // substitua "colunaDecimal" pelo nome da sua coluna
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    // Remove handlers anteriores para evitar duplicação
                    tb.KeyPress -= new KeyPressEventHandler(tb_KeyPressADE);
                    tb.TextChanged -= new EventHandler(tb_TextChangedADE);

                    // Adiciona os novos handlers
                    tb.KeyPress += new KeyPressEventHandler(tb_KeyPressADE);
                    tb.TextChanged += new EventHandler(tb_TextChangedADE);

                    // Define o tamanho máximo do texto como 1
                    tb.MaxLength = 1;
                }
            }
        }

        private void tb_KeyPressADE(object sender, KeyPressEventArgs e)
        {
            // Converte para maiúscula automaticamente
            e.KeyChar = char.ToUpper(e.KeyChar);

            // Permite apenas A, D, E e teclas de controle (backspace, delete, etc)
            if (!char.IsControl(e.KeyChar))
            {
                if (e.KeyChar != 'A' && e.KeyChar != 'D' && e.KeyChar != 'E')
                {
                    e.Handled = true;
                }
            }
        }
        private void tb_TextChangedADE(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && tb.Text.Length > 0)
            {
                // Garante que o caractere digitado está em maiúscula
                tb.Text = tb.Text.ToUpper();
                tb.SelectionStart = 1; // Posiciona o cursor no final
            }
        }

        private void grdAvaliacaoMuscular_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == grdAvaliacaoMuscular.Columns[4].Index) // Substitua pelo índice da sua coluna
            {
                string value = e.FormattedValue.ToString().ToUpper();

                // Verifica se o valor é válido (A, D, E ou vazio)
                if (!string.IsNullOrEmpty(value) && value != "A" && value != "D" && value != "E")
                {
                    MessageBox.Show("Por favor, digite apenas um caractere: A, D ou E", "Valor inválido",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }

                // Opcional: Não permite campo vazio
                // if (string.IsNullOrEmpty(value))
                // {
                //     MessageBox.Show("Este campo é obrigatório", "Atenção", 
                //                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //     e.Cancel = true;
                // }
            }
        }

        public bool CarregaVelocidadeNeuroCondMotora()
        {
            try
            {
                // Cria uma instância do objeto cResultadoNeuroCondMotora
                cResultadoNeuroCondMotora objResultadoNeuroCondMotora = new cResultadoNeuroCondMotora();

                // Define o IdFolha e IdResultado com base no objeto atual
                objResultadoNeuroCondMotora.IdFolha = this.objResultadoAvaliacaoMuscular.IdFolha;
                objResultadoNeuroCondMotora.IdPaciente = this.objResultadoAvaliacaoMuscular.IdPaciente;

                // Busca os dados da avaliação muscular
                DataTable dtGridNeuroCondMotora = objResultadoNeuroCondMotora.buscaResultadoNeuroCondMotora();

                grdNeuroConducaoMotora.DataSource = null;

                //Renomeia as colunas do datatable
                dtGridNeuroCondMotora.Columns["sigla"].ColumnName = "Sigla";
                dtGridNeuroCondMotora.Columns["nome"].ColumnName = "Nome";
                dtGridNeuroCondMotora.Columns["velocidadedireito"].ColumnName = "Vel.Direito";
                dtGridNeuroCondMotora.Columns["velocidadeesquerdo"].ColumnName = "Vel.Esquerdo";
                dtGridNeuroCondMotora.Columns["latenciadireito"].ColumnName = "Lat.Direito";
                dtGridNeuroCondMotora.Columns["latenciaesquerdo"].ColumnName = "Lat.Esquerdo";

                grdNeuroConducaoMotora.SuspendLayout();
                grdNeuroConducaoMotora.DataSource = dtGridNeuroCondMotora;
                configuraGridNeuroCondMotora(); // Configura o grid de dados
                grdNeuroConducaoMotora.ResumeLayout();

                return true; // Sucesso ao carregar os dados

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar avaliação muscular: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao carregar os dados
            }
        }
        private void configuraGridNeuroCondMotora()
        {
            try
            {
                grdNeuroConducaoMotora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // Desabilita a edição da coluna
                grdNeuroConducaoMotora.Columns[0].ReadOnly = true;
                grdNeuroConducaoMotora.Columns[1].ReadOnly = true;
                grdNeuroConducaoMotora.Columns[2].ReadOnly = true;
                grdNeuroConducaoMotora.Columns[3].ReadOnly = true;
                grdNeuroConducaoMotora.Columns[4].ReadOnly = false;
                grdNeuroConducaoMotora.Columns[5].ReadOnly = false;
                grdNeuroConducaoMotora.Columns[6].ReadOnly = false;
                grdNeuroConducaoMotora.Columns[7].ReadOnly = false;

                //Oculta colunas que não são necessárias
                grdNeuroConducaoMotora.Columns["idfolha"].Visible = false; // Oculta a coluna IdFolha
                grdNeuroConducaoMotora.Columns["idresultado"].Visible = false; // Oculta a coluna IdResultado

                // Configurando outras propriedades
                grdNeuroConducaoMotora.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
                grdNeuroConducaoMotora.MultiSelect = false; // Impede seleção múltipla
                grdNeuroConducaoMotora.AllowUserToAddRows = false; // Impede adição de novas linhas
                grdNeuroConducaoMotora.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
                grdNeuroConducaoMotora.CurrentCell = null; // Desmarca a célula atual
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar configurar o grid de dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        private void grdNeuroConducaoMotora_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Verifica se é a coluna/célula específica que você quer restringir
            if (grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[4].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[5].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[6].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[7].Index) // substitua "colunaDecimal" pelo nome da sua coluna
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    // Remove o handler anterior para evitar múltiplas atribuições
                    tb.KeyPress -= new KeyPressEventHandler(tb_KeyPressDecimal);
                    // Adiciona o novo handler
                    tb.KeyPress += new KeyPressEventHandler(tb_KeyPressDecimal);
                    // Define o tamanho máximo do texto como 1
                    tb.MaxLength = 10;
                }
            }
        }
        private void tb_KeyPressDecimal(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            // Permite números, backspace, vírgula e controle (como Ctrl+C, Ctrl+V)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Permite apenas uma vírgula
            if (e.KeyChar == ',' && (tb.Text.Contains(",") || tb.Text.Length == 0))
            {
                e.Handled = true;
                return;
            }

            // Opcional: Não permite vírgula como primeiro caractere
            if (e.KeyChar == ',' && tb.SelectionStart == 0)
            {
                e.Handled = true;
                return;
            }
        }
        private void grdNeuroConducaoMotora_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[4].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[5].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[6].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[7].Index) // substitua "colunaDecimal" pelo nome da sua coluna
            {
                var cell = grdNeuroConducaoMotora.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                {
                    cell.Value = "0,00";
                }
                else if (decimal.TryParse(cell.Value.ToString(), out decimal valor))
                {
                    cell.Value = valor.ToString("N2");
                }
            }

        }
        public bool CarregaNeuroCondSensorial()
        {
            try
            {
                // Define o IdFolha e IdResultado com base no objeto atual
                objResultadoNeuroCondMotora.IdFolha = this.objResultadoAvaliacaoMuscular.IdFolha;
                objResultadoNeuroCondMotora.IdResultado = this.objResultadoAvaliacaoMuscular.IdResultado;
                // Busca os dados da avaliação muscular
                DataTable dtGridNeuroCondMotora = objResultadoNeuroCondMotora.buscaResultadoNeuroCondMotora();

                grdNeuroConducaoMotora.DataSource = null;

                //Renomeia as colunas do datatable
                dtGridNeuroCondMotora.Columns["sigla"].ColumnName = "Sigla";
                dtGridNeuroCondMotora.Columns["nome"].ColumnName = "Nome";
                dtGridNeuroCondMotora.Columns["velocidadedireito"].ColumnName = "Vel.Direito";
                dtGridNeuroCondMotora.Columns["velocidadeesquerdo"].ColumnName = "Vel.Esquerdo";
                dtGridNeuroCondMotora.Columns["latenciadireito"].ColumnName = "Lat.Direito";
                dtGridNeuroCondMotora.Columns["latenciaesquerdo"].ColumnName = "Lat.Esquerdo";

                grdNeuroConducaoMotora.SuspendLayout();
                grdNeuroConducaoMotora.DataSource = dtGridNeuroCondMotora;
                configuraGridNeuroCondMotora(); // Configura o grid de dados
                grdNeuroConducaoMotora.ResumeLayout();

                return true; // Sucesso ao carregar os dados

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar avaliação muscular: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao carregar os dados
            }
        }

        public bool CarregaVelocidadeNeuroCondSensorial()
        {
            try
            {
                // Cria uma instância do objeto cResultadoNeuroCondMotora
                cResultadoNeuroCondSensorial objResultadoNeuroCondSensorial = new cResultadoNeuroCondSensorial();

                // Define o IdFolha e IdResultado com base no objeto atual
                objResultadoNeuroCondSensorial.IdFolha = this.objResultadoAvaliacaoMuscular.IdFolha;
                objResultadoNeuroCondSensorial.IdPaciente = this.objResultadoAvaliacaoMuscular.IdPaciente;

                // Busca os dados da avaliação muscular
                DataTable dtGridNeuroCondSensorial = objResultadoNeuroCondSensorial.buscaResultadoNeuroCondSensorial();

                grdNeuroConducaoSensorial.DataSource = null;

                //Renomeia as colunas do datatable
                dtGridNeuroCondSensorial.Columns["sigla"].ColumnName = "Sigla";
                dtGridNeuroCondSensorial.Columns["nome"].ColumnName = "Nome";
                dtGridNeuroCondSensorial.Columns["latenciadireito"].ColumnName = "Lat.Direito";
                dtGridNeuroCondSensorial.Columns["latenciaesquerdo"].ColumnName = "Lat.Esquerdo";

                grdNeuroConducaoSensorial.SuspendLayout();
                grdNeuroConducaoSensorial.DataSource = dtGridNeuroCondSensorial;
                configuraGridNeuroCondSensorial(); // Configura o grid de dados
                grdNeuroConducaoSensorial.ResumeLayout();

                return true; // Sucesso ao carregar os dados

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar avaliação muscular: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao carregar os dados
            }
        }

        private void configuraGridNeuroCondSensorial()
        {
            try
            {
                grdNeuroConducaoSensorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // Desabilita a edição da coluna
                grdNeuroConducaoSensorial.Columns[0].ReadOnly = true;
                grdNeuroConducaoSensorial.Columns[1].ReadOnly = true;
                grdNeuroConducaoSensorial.Columns[2].ReadOnly = true;
                grdNeuroConducaoSensorial.Columns[3].ReadOnly = true;
                grdNeuroConducaoSensorial.Columns[4].ReadOnly = false;
                grdNeuroConducaoSensorial.Columns[5].ReadOnly = false;

                //Oculta colunas que não são necessárias
                grdNeuroConducaoSensorial.Columns["idfolha"].Visible = false; // Oculta a coluna IdFolha
                grdNeuroConducaoSensorial.Columns["idresultado"].Visible = false; // Oculta a coluna IdResultado

                // Configurando outras propriedades
                grdNeuroConducaoSensorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
                grdNeuroConducaoSensorial.MultiSelect = false; // Impede seleção múltipla
                grdNeuroConducaoSensorial.AllowUserToAddRows = false; // Impede adição de novas linhas
                grdNeuroConducaoSensorial.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
                grdNeuroConducaoSensorial.CurrentCell = null; // Desmarca a célula atual
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar configurar o grid de dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void grdNeuroConducaoSensorial_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Verifica se é a coluna/célula específica que você quer restringir
            if (grdNeuroConducaoSensorial.CurrentCell.ColumnIndex == grdNeuroConducaoSensorial.Columns[4].Index ||
                grdNeuroConducaoSensorial.CurrentCell.ColumnIndex == grdNeuroConducaoSensorial.Columns[5].Index)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    // Remove o handler anterior para evitar múltiplas atribuições
                    tb.KeyPress -= new KeyPressEventHandler(tb_KeyPressDecimal);
                    // Adiciona o novo handler
                    tb.KeyPress += new KeyPressEventHandler(tb_KeyPressDecimal);
                    // Define o tamanho máximo do texto como 1
                    tb.MaxLength = 10;
                }
            }
        }
        private void grdNeuroConducaoSensorial_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (grdNeuroConducaoSensorial.CurrentCell.ColumnIndex == grdNeuroConducaoSensorial.Columns[4].Index ||
                grdNeuroConducaoSensorial.CurrentCell.ColumnIndex == grdNeuroConducaoSensorial.Columns[5].Index)
            {
                var cell = grdNeuroConducaoSensorial.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                {
                    cell.Value = "0,00";
                }
                else if (decimal.TryParse(cell.Value.ToString(), out decimal valor))
                {
                    cell.Value = valor.ToString("N2");
                }
            }

        }

        private void btnAtividadeInsercao_Click(object sender, EventArgs e)
        {
            frmAtividadeInsercao objfrmAtividadeInsercao = new frmAtividadeInsercao();

            // Passa o objeto cAtividadeInsercao para o formulário B
            objfrmAtividadeInsercao.VemdeResultado = true;

            // Mostra o diálogo e verifica se foi preenchido
            if (objfrmAtividadeInsercao.ShowDialog() == DialogResult.OK)
            {
                // Acessa os dados diretamente do formulário B
                txtCodAtividade.Text = objfrmAtividadeInsercao.IdAatividadeInsercao.ToString();
                txtNomeAtividade.Text = objfrmAtividadeInsercao.Nome;
                txtSiglaAtividade.Text = objfrmAtividadeInsercao.Sigla;
                txtTextoAtividade.Text = objfrmAtividadeInsercao.Texto;
            }
        }

        private void btnPotenciaisUnidade_Click(object sender, EventArgs e)
        {
            frmPotenciais objfrmPotenciais = new frmPotenciais();

            // Passa o objeto cAtividadeInsercao para o formulário B
            objfrmPotenciais.VemdeResultado = true;

            // Mostra o diálogo e verifica se foi preenchido
            if (objfrmPotenciais.ShowDialog() == DialogResult.OK)
            {
                // Acessa os dados diretamente do formulário B
                txtCodigoPotencial.Text = objfrmPotenciais.IdAatividadeInsercao.ToString();
                txtNomePotencial.Text = objfrmPotenciais.Nome;
                txtSiglaPotencial.Text = objfrmPotenciais.Sigla;
                txtTextoPotencial.Text = objfrmPotenciais.Texto;
            }
        }

        private void btnComentario_Click(object sender, EventArgs e)
        {
            frmComentarios objfrmComentarios = new frmComentarios();

            // Passa o objeto cAtividadeInsercao para o formulário B
            objfrmComentarios.VemdeResultado = true;

            // Mostra o diálogo e verifica se foi preenchido
            if (objfrmComentarios.ShowDialog() == DialogResult.OK)
            {
                // Acessa os dados diretamente do formulário B
                txtCodigoComentario.Text = objfrmComentarios.IdAatividadeInsercao.ToString();
                txtNomeComentario.Text = objfrmComentarios.Nome;
                txtSiglaComentario.Text = objfrmComentarios.Sigla;
                txtTextoComentario.Text = objfrmComentarios.Texto;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {

        }
    }
}

