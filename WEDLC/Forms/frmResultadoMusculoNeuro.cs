using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Windows.Forms;
using WEDLC.Banco;
using WinFormsZoom;
using static WEDLC.Banco.cUtil;

namespace WEDLC.Forms
{
    public partial class frmResultadoMusculoNeuro : Form
    {
        public const int codModulo = 16; //Código do módulo

        DataTable dadosXML = new DataTable();

        // Create a ToolTip component
        ToolTip toolTip1 = new ToolTip();

        public cResultadoAvaliacaoMuscular objResultadoAvaliacaoMuscular;
        public cResultadoNeuroCondMotora objResultadoNeuroCondMotora;
        public cAtividadeInsercao objAtividadeInsercao;
        public cResultadoAtividadeInsercao objResultadoAtividadeInsercao;
        public Int32 IdResultado = 0;
        public int grupoFolha = 0;

        private FormZoomHelper zoomHelper;

        public frmResultadoMusculoNeuro()
        {
            InitializeComponent();

            // Configurações do ToolTip
            toolTip1.AutoPopDelay = 5000; // Tempo que o ToolTip permanece visível
            toolTip1.InitialDelay = 500; // Tempo antes do ToolTip aparecer
            toolTip1.ReshowDelay = 500; // Tempo entre as aparições do ToolTip
            toolTip1.ShowAlways = true; // Sempre mostrar o ToolTip

            this.FormClosed += (s, e) => zoomHelper.Dispose(); // Descarta automaticamente quando o form for fechado
            this.DoubleBuffered = true;
        }

        private void frmResultadoMusculoNeuro_Load(object sender, EventArgs e)
        {
            this.Text = "Folha: " + objResultadoAvaliacaoMuscular.Sigla.ToString() + " - " + objResultadoAvaliacaoMuscular.Nome.ToString();
            zoomHelper = new FormZoomHelper(this); // Inicializa o helper de zoom
            //int var = grupoFolha; // Apenas para evitar o aviso de variável não utilizada
            IdResultado = objResultadoAvaliacaoMuscular.IdResultado;

            //Verifica permissão de acesso
            if (!cPermissao.PodeAcessarModulo(codModulo))
            {
                MessageBox.Show("Usuário sem acesso", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Fecha de forma segura depois que o handle estiver pronto
                this.BeginInvoke(new Action(() => this.Close()));
                return;
            }

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

            if (CarregaReflexoH() == false)
            {
                return;
            }

            if (CarregaAtividadeInsercao() == false)
            {
                return;
            }

            if (CarregaPotenciaisUnidade() == false)
            {
                return;
            }

            if (CarregaComentario() == false)
            {
                return;
            }

            // GRAVA LOG
            clLog objcLog = new clLog();
            objcLog.IdLogDescricao = 4; // descrição na tabela LOGDESCRICAO 
            objcLog.IdUsuario = Sessao.IdUsuario;
            objcLog.Descricao = this.Name;
            objcLog.incluiLog();
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
                objResultadoAvaliacaoMuscular.IdResultado = IdResultado;

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
                grdAvaliacaoMuscular.Columns[4].ReadOnly = true;
                grdAvaliacaoMuscular.Columns[5].ReadOnly = true;
                grdAvaliacaoMuscular.Columns[6].ReadOnly = false;
                //Oculta colunas que não são necessárias
                grdAvaliacaoMuscular.Columns["idresultadoavaliacao"].Visible = false; // Oculta a coluna IdMusculo
                grdAvaliacaoMuscular.Columns["idmusculo"].Visible = false; // Oculta a coluna IdMusculo
                grdAvaliacaoMuscular.Columns["idfolha"].Visible = false; // Oculta a coluna IdFolha
                grdAvaliacaoMuscular.Columns["idresultado"].Visible = false; // Oculta a coluna IdResultado

                // Configurando outras propriedades
                grdAvaliacaoMuscular.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
                grdAvaliacaoMuscular.MultiSelect = false; // Impede seleção múltipla
                grdAvaliacaoMuscular.AllowUserToAddRows = false; // Impede adição de novas linhas
                grdAvaliacaoMuscular.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
                grdAvaliacaoMuscular.CurrentCell = null; // Desmarca a célula atual
                grdAvaliacaoMuscular.AllowUserToDeleteRows = false;
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
            if (grdAvaliacaoMuscular.CurrentCell.ColumnIndex == grdAvaliacaoMuscular.Columns[6].Index) // substitua "colunaDecimal" pelo nome da sua coluna
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
            if (e.ColumnIndex == grdAvaliacaoMuscular.Columns[6].Index) // Substitua pelo índice da sua coluna
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
                grdNeuroConducaoMotora.Columns[4].ReadOnly = true;
                grdNeuroConducaoMotora.Columns[5].ReadOnly = true;
                grdNeuroConducaoMotora.Columns[6].ReadOnly = false;
                grdNeuroConducaoMotora.Columns[7].ReadOnly = false;
                grdNeuroConducaoMotora.Columns[8].ReadOnly = false;
                grdNeuroConducaoMotora.Columns[9].ReadOnly = false;

                //Oculta colunas que não são necessárias
                grdNeuroConducaoMotora.Columns["idresultadovelocneurocondmotora"].Visible = false; // Oculta a coluna IdFolha
                grdNeuroConducaoMotora.Columns["idnervo"].Visible = false; // Oculta a coluna IdFolha
                grdNeuroConducaoMotora.Columns["idfolha"].Visible = false; // Oculta a coluna IdFolha
                grdNeuroConducaoMotora.Columns["idresultado"].Visible = false; // Oculta a coluna IdResultado

                // Configurando outras propriedades
                grdNeuroConducaoMotora.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
                grdNeuroConducaoMotora.MultiSelect = false; // Impede seleção múltipla
                grdNeuroConducaoMotora.AllowUserToAddRows = false; // Impede adição de novas linhas
                grdNeuroConducaoMotora.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
                grdNeuroConducaoMotora.CurrentCell = null; // Desmarca a célula atual
                grdNeuroConducaoMotora.AllowUserToDeleteRows = false; // Impede a exclusão de linhas
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
            if (grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[6].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[7].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[8].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[9].Index) // substitua "colunaDecimal" pelo nome da sua coluna
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
            if (grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[6].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[7].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[8].Index ||
                grdNeuroConducaoMotora.CurrentCell.ColumnIndex == grdNeuroConducaoMotora.Columns[9].Index) // substitua "colunaDecimal" pelo nome da sua coluna
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

        public bool CarregaReflexoH()
        {
            try
            {
                // Cria uma instância do objeto cResultadoNeuroCondMotora
                cResultadoReflexoH objResultadoReflexoH = new cResultadoReflexoH();

                // IdResultado com base no objeto atual
                objResultadoReflexoH.IdResultado = this.objResultadoAvaliacaoMuscular.IdResultado;

                // Busca os dados da avaliação muscular
                DataTable dtResultadoReflexoH = objResultadoReflexoH.buscaResultadoReflexoH();

                // Popula os campos de texto com os dados retornados
                if (dtResultadoReflexoH.Rows.Count > 0)
                {
                    DataRow row = dtResultadoReflexoH.Rows[0];
                    txtIdade.Text = row["idade"].ToString();
                    txtComprimentoPerna.Text = row["comprimentoperna"].ToString();
                    txtLatenciaDireita.Text = row["latenciadireita"].ToString();
                    txtLatenciaEsquerda.Text = row["latenciaesquerda"].ToString();
                    txtLatenciaEsperada.Text = row["latenciaesperada"].ToString();
                    txtLimiteInferior.Text = row["limiteinferior"].ToString();
                    txtLimiteSuperior.Text = row["limitesuperior"].ToString();
                    txtDiferenca.Text = row["diferencalados"].ToString();
                }
                else
                {
                    LimpaCamposReflexoH();
                }

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
                grdNeuroConducaoSensorial.Columns[4].ReadOnly = true;
                grdNeuroConducaoSensorial.Columns[5].ReadOnly = true;
                grdNeuroConducaoSensorial.Columns[6].ReadOnly = false;
                grdNeuroConducaoSensorial.Columns[7].ReadOnly = false;

                //Oculta colunas que não são necessárias
                grdNeuroConducaoSensorial.Columns["idresultadoneurocondsensorial"].Visible = false;
                grdNeuroConducaoSensorial.Columns["idnervo"].Visible = false;
                grdNeuroConducaoSensorial.Columns["idfolha"].Visible = false;
                grdNeuroConducaoSensorial.Columns["idresultado"].Visible = false;

                // Configurando outras propriedades
                grdNeuroConducaoSensorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
                grdNeuroConducaoSensorial.MultiSelect = false; // Impede seleção múltipla
                grdNeuroConducaoSensorial.AllowUserToAddRows = false; // Impede adição de novas linhas
                grdNeuroConducaoSensorial.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
                grdNeuroConducaoSensorial.CurrentCell = null; // Desmarca a célula atual
                grdNeuroConducaoSensorial.AllowUserToDeleteRows = false; // Impede a exclusão de linhas
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
            if (grdNeuroConducaoSensorial.CurrentCell.ColumnIndex == grdNeuroConducaoSensorial.Columns[6].Index ||
                grdNeuroConducaoSensorial.CurrentCell.ColumnIndex == grdNeuroConducaoSensorial.Columns[7].Index)
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
            if (grdNeuroConducaoSensorial.CurrentCell.ColumnIndex == grdNeuroConducaoSensorial.Columns[6].Index ||
                grdNeuroConducaoSensorial.CurrentCell.ColumnIndex == grdNeuroConducaoSensorial.Columns[7].Index)
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
                txtCodigoPotencial.Text = objfrmPotenciais.IdPotencial.ToString();
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
                txtCodigoComentario.Text = objfrmComentarios.IdComentario.ToString();
                txtNomeComentario.Text = objfrmComentarios.Nome;
                txtSiglaComentario.Text = objfrmComentarios.Sigla;
                txtTextoComentario.Text = objfrmComentarios.Texto;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (GravaMusculoExaminado() == false)
                    {
                        return;
                    }

                    if (GravaNeuroConducaoMotora() == false)
                    {
                        return;
                    }

                    if (GravaNeuroConducaoSensorial() == false)
                    {
                        return;
                    }


                    if (GravaReflexoH() == false)
                    {
                        return;
                    }

                    if (GravaAtividadeInsercao() == false)
                    {
                        return;
                    }

                    if (GravaPotencialUnidade() == false)
                    {
                        return;
                    }

                    if (GravaComentario() == false)
                    {
                        return;
                    }

                    // GRAVA LOG
                    clLog objcLog = new clLog();
                    objcLog.IdLogDescricao = 5; // descrição na tabela LOGDESCRICAO 
                    objcLog.IdUsuario = Sessao.IdUsuario;
                    objcLog.Descricao = this.Name;
                    objcLog.incluiLog();

                    // Se tudo ok, commit na transação
                    scope.Complete();

                    MessageBox.Show("Dados gravados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Close();

                }
                catch (Exception ex)
                {
                    // GRAVA LOG
                    clLog objcLog = new clLog();
                    objcLog.IdLogDescricao = 3; // descrição na tabela LOGDESCRICAO 
                    objcLog.IdUsuario = Sessao.IdUsuario;
                    objcLog.Descricao = this.Name + " - " + ex.Message;
                    objcLog.incluiLog();
                }
            }
        }

        private bool GravaMusculoExaminado()
        {
            try
            {
                //verifica se existe alguma linha no grid
                if (grdAvaliacaoMuscular.Rows.Count > 0)
                {
                    // Cria uma instância do objeto cResultadoAvaliacaoMuscular
                    cResultadoAvaliacaoMuscular objResultadoAvaliacaoMuscular = new cResultadoAvaliacaoMuscular();

                    // Percorre as linhas do DataGridView e grava os dados
                    foreach (DataGridViewRow row in grdAvaliacaoMuscular.Rows)
                    {
                        if (row.IsNewRow) continue; // Ignora a linha de novo registro

                        objResultadoAvaliacaoMuscular.IdResultadoAvaliacao = Int32.Parse(row.Cells["idresultadoavaliacao"].Value.ToString());
                        objResultadoAvaliacaoMuscular.Lado = row.Cells["lado"].Value.ToString();

                        // Chama o método para gravar os dados
                        if (!objResultadoAvaliacaoMuscular.gravaResultadoAvaliacaoMuscular())
                        {
                            MessageBox.Show("Erro ao gravar avaliação muscular.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false; // Falha ao gravar os dados
                        }
                    }
                }

                return true; // Sucesso ao gravar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar gravaResultadoAvaliacaoMuscular: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao gravar os dados
            }
        }

        private bool GravaNeuroConducaoMotora()
        {
            try
            {
                //verifica se existe alguma linha no grid
                if (grdNeuroConducaoMotora.Rows.Count > 0)
                {
                    // Cria uma instância do objeto cResultadoAvaliacaoMuscular
                    cResultadoNeuroCondMotora objResultadoNeuroCondMotora = new cResultadoNeuroCondMotora();

                    // Percorre as linhas do DataGridView e grava os dados
                    foreach (DataGridViewRow row in grdNeuroConducaoMotora.Rows)
                    {
                        if (row.IsNewRow) continue; // Ignora a linha de novo registro

                        objResultadoNeuroCondMotora.IdResultadoVelocNeuroCondMotora = Int32.Parse(row.Cells["idresultadovelocneurocondmotora"].Value.ToString());
                        objResultadoNeuroCondMotora.VelocidadeDireito = row.Cells["vel.direito"].Value.ToString();
                        objResultadoNeuroCondMotora.VelocidadeEsquerdo = row.Cells["vel.esquerdo"].Value.ToString();
                        objResultadoNeuroCondMotora.LatenciaDireito = row.Cells["lat.direito"].Value.ToString();
                        objResultadoNeuroCondMotora.LatenciaEsquerdo = row.Cells["lat.esquerdo"].Value.ToString();

                        // Chama o método para gravar os dados
                        if (!objResultadoNeuroCondMotora.gravaResultadoNeuroConducaoMotora())
                        {
                            MessageBox.Show("Erro ao gravar Resultado Neuro Condução Motora.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false; // Falha ao gravar os dados
                        }
                    }
                }

                return true; // Sucesso ao gravar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar gravaResultadoNeuroConducaoMotora: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao gravar os dados
            }
        }

        private bool GravaNeuroConducaoSensorial()
        {
            try
            {
                //verifica se existe alguma linha no grid
                if (grdNeuroConducaoSensorial.Rows.Count > 0)
                {
                    // Cria uma instância do objeto cResultadoAvaliacaoMuscular
                    cResultadoNeuroCondSensorial objResultadoNeuroCondSensorial = new cResultadoNeuroCondSensorial();

                    // Percorre as linhas do DataGridView e grava os dados
                    foreach (DataGridViewRow row in grdNeuroConducaoSensorial.Rows)
                    {
                        if (row.IsNewRow) continue; // Ignora a linha de novo registro

                        objResultadoNeuroCondSensorial.IdResultadoNeuroCondSensorial = Int32.Parse(row.Cells["idresultadoneurocondsensorial"].Value.ToString());
                        objResultadoNeuroCondSensorial.LatenciaDireito = row.Cells["lat.direito"].Value.ToString();
                        objResultadoNeuroCondSensorial.LatenciaEsquerdo = row.Cells["lat.esquerdo"].Value.ToString();

                        // Chama o método para gravar os dados
                        if (!objResultadoNeuroCondSensorial.gravaResultadoNeuroConducaoSensorial())
                        {
                            MessageBox.Show("Erro ao gravar Resultado Neuro Condução Sensorial.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false; // Falha ao gravar os dados
                        }
                    }
                }

                return true; // Sucesso ao gravar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar gravaResultadoNeuroConducaoSensorial: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao gravar os dados
            }
        }

        private bool GravaReflexoH()
        {
            try
            {
                //verifica se o comprimento da perna foi informado
                if (!string.IsNullOrEmpty(txtComprimentoPerna.Text))
                {
                    // Cria uma instância do objeto 
                    cResultadoReflexoH objResultadoReflexoH = new cResultadoReflexoH();
                    objResultadoReflexoH.IdResultado = this.objResultadoAvaliacaoMuscular.IdResultado;
                    objResultadoReflexoH.idade = txtIdade.Text;
                    objResultadoReflexoH.comprimentoperna = txtComprimentoPerna.Text;
                    objResultadoReflexoH.latenciadireita = txtLatenciaDireita.Text;
                    objResultadoReflexoH.latenciaesquerda = txtLatenciaEsquerda.Text;
                    objResultadoReflexoH.latenciaesperada = txtLatenciaEsperada.Text;
                    objResultadoReflexoH.limiteinferior = txtLimiteInferior.Text;
                    objResultadoReflexoH.limitesuperior = txtLimiteSuperior.Text;
                    objResultadoReflexoH.diferencalados = txtDiferenca.Text;

                    // Chama o método para gravar os dados
                    if (!objResultadoReflexoH.gravaResultadoReflexoH())
                    {
                        MessageBox.Show("Erro ao gravar Resultado do ReflexoH.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; // Falha ao gravar os dados
                    }
                }

                return true; // Sucesso ao gravar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar gravaResultadoReflexoH: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao gravar os dados
            }
        }

        public bool CarregaAtividadeInsercao()
        {
            try
            {
                cResultadoAtividadeInsercao objResultadoAtividadeInsercao = new cResultadoAtividadeInsercao();
                objResultadoAtividadeInsercao.IdResultado = IdResultado;
                DataTable dtAtividadeInsercao = objResultadoAtividadeInsercao.buscaResultadoAtividadeInsercao();

                // Verifica se o DataTable retornou dados
                if (dtAtividadeInsercao.Rows.Count > 0)
                {
                    //popula campos Atividade Inserção
                    txtCodAtividade.Text = dtAtividadeInsercao.Rows[0]["idatividadeinsercao"].ToString();
                    txtNomeAtividade.Text = dtAtividadeInsercao.Rows[0]["nome"].ToString();
                    txtSiglaAtividade.Text = dtAtividadeInsercao.Rows[0]["sigla"].ToString();
                    txtTextoAtividade.Text = dtAtividadeInsercao.Rows[0]["texto"].ToString();

                }
                else
                {
                    // Se não houver dados, limpa os campos
                    txtCodAtividade.Text = string.Empty;
                    txtNomeAtividade.Text = string.Empty;
                    txtSiglaAtividade.Text = string.Empty;
                    txtTextoAtividade.Text = string.Empty;
                    //return false; // Falha ao carregar os dados
                }

                return true; // Sucesso ao carregar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na CarregaAtividadeInsercao: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao carregar os dados
            }

        }

        public bool CarregaPotenciaisUnidade()
        {
            try
            {
                cResultadoPotenciaisUnidadeMotora objResultadoPotenciaisUnidadeMotora = new cResultadoPotenciaisUnidadeMotora();
                objResultadoPotenciaisUnidadeMotora.IdResultado = IdResultado;
                DataTable dtPotenciaisUnidade = objResultadoPotenciaisUnidadeMotora.buscaResultadoUnidadePotencial();

                //Verifica se o DataTable retornou dados
                if (dtPotenciaisUnidade.Rows.Count > 0)
                {
                    //popula campos Atividade Inserção
                    txtCodigoPotencial.Text = dtPotenciaisUnidade.Rows[0]["idpotenciaisunidade"].ToString();
                    txtNomePotencial.Text = dtPotenciaisUnidade.Rows[0]["nome"].ToString();
                    txtSiglaPotencial.Text = dtPotenciaisUnidade.Rows[0]["sigla"].ToString();
                    txtTextoPotencial.Text = dtPotenciaisUnidade.Rows[0]["texto"].ToString();

                }
                else
                {
                    // Se não houver dados, limpa os campos
                    txtCodigoPotencial.Text = string.Empty;
                    txtNomePotencial.Text = string.Empty;
                    txtSiglaPotencial.Text = string.Empty;
                    txtTextoPotencial.Text = string.Empty;
                }

                return true; // Sucesso ao carregar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na CarregaPotenciaisUnidade: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao carregar os dados
            }
        }

        public bool CarregaComentario()
        {
            try
            {
                cResultadoComentario objResultadoComentario = new cResultadoComentario();
                objResultadoComentario.IdResultado = IdResultado;
                DataTable dtComentario = objResultadoComentario.buscaResultadoComentario();

                //Verifica se o DataTable retornou dados
                if (dtComentario.Rows.Count > 0)
                {
                    //popula campos Atividade Inserção
                    txtCodigoComentario.Text = dtComentario.Rows[0]["idcomentario"].ToString();
                    txtNomeComentario.Text = dtComentario.Rows[0]["nome"].ToString();
                    txtSiglaComentario.Text = dtComentario.Rows[0]["sigla"].ToString();
                    txtTextoComentario.Text = dtComentario.Rows[0]["texto"].ToString();
                }
                else
                {
                    // Se não houver dados, limpa os campos
                    txtCodigoComentario.Text = string.Empty;
                    txtNomeComentario.Text = string.Empty;
                    txtSiglaComentario.Text = string.Empty;
                    txtTextoComentario.Text = string.Empty;
                }

                return true; // Sucesso ao carregar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na CarregaComentario: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao carregar os dados
            }
        }

        private bool GravaAtividadeInsercao()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCodAtividade.Text))
                {
                    // Cria uma instância do objeto cResultadoAvaliacaoMuscular
                    cResultadoAtividadeInsercao objResultadoAtividadeInsercao = new cResultadoAtividadeInsercao();

                    objResultadoAtividadeInsercao.IdAtividadeInsercao = txtCodAtividade.Text != string.Empty ? Int32.Parse(txtCodAtividade.Text) : 0;
                    objResultadoAtividadeInsercao.IdResultado = this.IdResultado;
                    objResultadoAtividadeInsercao.Texto = txtTextoAtividade.Text;

                    // Chama o método para gravar os dados
                    if (!objResultadoAtividadeInsercao.gravaResultadoAtividadeInsercao())
                    {
                        MessageBox.Show("Erro ao gravar gravaResultadoAtividadeInsercao.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; // Falha ao gravar os dados
                    }
                }

                return true; // Sucesso ao gravar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar gravaResultadoAtividadeInsercao: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao gravar os dados
            }
        }

        private bool GravaPotencialUnidade()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCodigoPotencial.Text))
                {
                    // Cria uma instância do objeto cResultadoAvaliacaoMuscular
                    cResultadoPotenciaisUnidadeMotora objResultadoPotenciaisUnidadeMotora = new cResultadoPotenciaisUnidadeMotora();

                    objResultadoPotenciaisUnidadeMotora.IdPotenciaisUnidade = txtCodigoPotencial.Text != string.Empty ? Int32.Parse(txtCodigoPotencial.Text) : 0;
                    objResultadoPotenciaisUnidadeMotora.IdResultado = IdResultado;
                    objResultadoPotenciaisUnidadeMotora.Texto = txtTextoPotencial.Text;

                    // Chama o método para gravar os dados
                    if (!objResultadoPotenciaisUnidadeMotora.gravaResultadoPotencialUnidade())
                    {
                        MessageBox.Show("Erro ao gravar gravaResultadoPotencialUnidade.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; // Falha ao gravar os dados
                    }
                }

                return true; // Sucesso ao gravar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar gravaResultadoPotencialUnidade: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao gravar os dados
            }
        }

        private bool GravaComentario()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCodigoComentario.Text))
                {
                    // Cria uma instância do objeto cResultadoAvaliacaoMuscular
                    cResultadoComentario objResultadoComentario = new cResultadoComentario();

                    objResultadoComentario.IdComentario = txtCodigoComentario.Text != string.Empty ? Int32.Parse(txtCodigoComentario.Text) : 0;
                    objResultadoComentario.IdResultado = IdResultado;
                    objResultadoComentario.Texto = txtTextoComentario.Text;

                    // Chama o método para gravar os dados
                    if (!objResultadoComentario.gravaResultadoComentario())
                    {
                        MessageBox.Show("Erro ao gravar GravaComentario.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; // Falha ao gravar os dados
                    }
                }

                return true; // Sucesso ao gravar os dados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar GravaComentario: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Falha ao gravar os dados
            }
        }

        private void txtComprimentoPerna_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtComprimentoPerna), e);
        }

        private void txtComprimentoPerna_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtComprimentoPerna.Text))
            {
                LimpaCamposReflexoH();
                return;
            }

            //Calcula a latência esperada
            var comprimentoPerna = txtComprimentoPerna.Text != string.Empty ? Double.Parse(txtComprimentoPerna.Text) : 0;
            var idade = txtIdade.Text != string.Empty ? Int32.Parse(txtIdade.Text) : 0;
            var latenciaesperada = 9.14 + (0.46 * comprimentoPerna) + (0.1 * idade);
            txtLatenciaEsperada.Text = latenciaesperada.ToString("N1");

            // Calcula a diferença do limite superior e do limite inferior
            txtLimiteInferior.Text = Convert.ToString(latenciaesperada - 5.5);
            txtLimiteSuperior.Text = Convert.ToString(latenciaesperada + 5.5);
        }

        private void txtComprimentoPerna_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void txtLatenciaDireita_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtLatenciaDireita), e);
        }

        private void txtLatenciaDireita_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLatenciaDireita.Text))
            {
                txtLatenciaDireita.Text = "0,0";
            }

            var diferenca = Math.Abs(Decimal.Parse(txtLatenciaDireita.Text) - Decimal.Parse(txtLatenciaEsquerda.Text));
            txtDiferenca.Text = diferenca.ToString();

            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
        }

        private void txtLatenciaDireita_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtLatenciaEsquerda_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtLatenciaEsquerda), e);
        }

        private void txtLatenciaEsquerda_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLatenciaEsquerda.Text))
            {
                txtLatenciaEsquerda.Text = "0,0";
            }

            var diferenca = Math.Abs(Decimal.Parse(txtLatenciaDireita.Text) - Decimal.Parse(txtLatenciaEsquerda.Text));
            txtDiferenca.Text = diferenca.ToString();

            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
        }

        private void txtLatenciaEsquerda_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void LimpaCamposReflexoH()
        {
            txtIdade.Text = cUtil.DataNascimentoValidator.IdadeCalculator.CalcularIdade(DateTime.Parse(objResultadoAvaliacaoMuscular.Idade)).ToString();
            txtComprimentoPerna.Text = string.Empty;
            txtLatenciaDireita.Text = "0,0";
            txtLatenciaEsquerda.Text = "0,0";
            txtLatenciaEsperada.Text = "0,0";
            txtLimiteInferior.Text = string.Empty;
            txtLimiteSuperior.Text = string.Empty;
            txtDiferenca.Text = string.Empty;
        }
    }
}

