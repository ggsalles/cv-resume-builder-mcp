using System;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmResultadoMusculoNeuro : Form
    {
        DataTable dadosXML = new DataTable();

        // Create a ToolTip component
        ToolTip toolTip1 = new ToolTip();

        public Acao cAcao = Acao.CANCELAR;
        public cResultadoAvaliacaoMuscular objResultadoAvaliacaoMuscular;

        public enum Acao
        {
            INSERT = 0,
            UPDATE = 1,
            DELETE = 2,
            SAVE = 3,
            CANCELAR = 4,
        }

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
            
            if (CarregaAvaliacaoMuscular()==false)
            {
                return;
            }

            if (CarregaVelocidadeNeuroCondMotora() == false)
            {
                return;
            }

        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Determina a acao
            cAcao = Acao.CANCELAR;

            controlaBotao();

            this.liberaCampos(false); //Libera os campos para edição

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

            //Se clicou no grid
            if (cAcao == Acao.UPDATE)
            {
                btnNovo.Enabled = false;
                btnGravar.Enabled = true;
                btnCancelar.Enabled = true;
                btnExcluir.Enabled = false;
            }

            //Se clicou no grid
            if (cAcao == Acao.CANCELAR)
            {
                btnNovo.Enabled = true;
                btnGravar.Enabled = false;
                btnCancelar.Enabled = false;
                btnExcluir.Enabled = false;
            }
        }

        private void liberaCampos(bool Ativa)
        {
            //Limpa os campos

            //Habilita - Desabilita os campos dados
            //txtCep.Enabled = Ativa; //Habilita - Desabilita o campo cep
            //txtLogradouro.Enabled = Ativa; //Habilita - Desabilita o campo logradouro
        }

        private DataTable buscaResultadoPaciente(int tipopesquisa, Int32 idpaciente, string nome)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cResultado objcResultado = new cResultado();

                objcResultado.TipoPesquisa = tipopesquisa; //Tipo de pesquisa
                objcResultado.Paciente.IdPaciente = idpaciente; //Código da especialização
                objcResultado.Paciente.Nome = nome; //Nome da especialização

                dtAux = objcResultado.buscaResultadoPaciente();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar o paciente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }


        private DataTable buscaResultadoFolha(Int32 idpaciente)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cResultado objcResultado = new cResultado();

                objcResultado.Paciente.IdPaciente = idpaciente; //Código do paciente
                dtAux = objcResultado.buscaResultadoFolha();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar a folha do paciente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida os campos

                cPaciente objcPaciente = new cPaciente();
                //Se for novo paciente
                if (cAcao == Acao.INSERT)
                {
                    // sequence
                    //Int32 sequence = 0;

                    //Preenche os dados do paciente


                    //Grava o paciente
                    //if (objcPaciente.incluiPaciente(out sequence)    == true)
                    //{
                    //    foreach (DataRow row in dtGrdPacienteFolha.Rows)
                    //    {
                    //        // Acessar os valores das colunas
                    //        objcPaciente.IdPaciente = sequence;
                    //        objcPaciente.Folha.IdFolha = Convert.ToInt32(row["idfolha"]);

                    //        // Inclui a folha do paciente
                    //        if (objcPaciente.incluiPacienteFolha() == false)
                    //        {
                    //            MessageBox.Show("Erro ao tentar incluir a folha do paciente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //            return;
                    //        }
                    //    }

                    //    MessageBox.Show("Inclusão efetuada com sucesso!. Código gerado: " + sequence, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    btnCancelar_Click(sender, e); // Chama o método de cancelar para limpar os campos e voltar ao estado inicial    
                    //    return;
                    //}

                    //else
                    //{
                    //    MessageBox.Show("Erro ao tentar incluir!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                }

                //Se for atualização de paciente
                if (cAcao == Acao.UPDATE)
                {

                    // limpa o contador
                    //int contador = 0;

                    //foreach (DataRow row in dtGrdPacienteFolha.Rows)
                    //{

                    //    // Acessar os valores das colunas
                    //    objcPaciente.IdPaciente = int.Parse(row["idpaciente"].ToString());
                    //    objcPaciente.IdFolha = int.Parse(row["idfolha"].ToString());

                    //    //Se for a primeira linha do loop da folha, o sistema entende que é necesseário apagar a tabela de especializacao do medico
                    //    if (contador == 0)
                    //    {
                    //        objcPaciente.Apaga = true;
                    //    }
                    //    //Se não... apagar não é necessário
                    //    else
                    //    {
                    //        objcPaciente.Apaga = false;
                    //    }
                    //    // Atualiza a especialização do médico
                    //    if (objcPaciente.atualizaPaciente() == true)
                    //    {
                    //        //incrementa contador
                    //        contador++;
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Erro ao tentar atualizar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        return;
                    //    }
                    //}

                    MessageBox.Show("Alteração efetuada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancelar_Click(sender, e); // Chama o método de cancelar para limpar os campos e voltar ao estado inicial    
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar gravar o paciente: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool CarregaAvaliacaoMuscular()
        {
            try
            {
                // Define o IdFolha e IdResultado com base no objeto atual
                objResultadoAvaliacaoMuscular.IdFolha = this.objResultadoAvaliacaoMuscular.IdFolha;
                objResultadoAvaliacaoMuscular.IdResultado = this.objResultadoAvaliacaoMuscular.IdResultado;
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
                // Define o IdFolha e IdResultado com base no objeto atual
                objResultadoAvaliacaoMuscular.IdFolha = this.objResultadoAvaliacaoMuscular.IdFolha;
                objResultadoAvaliacaoMuscular.IdResultado = this.objResultadoAvaliacaoMuscular.IdResultado;
                // Busca os dados da avaliação muscular
                DataTable dtGridNeuroCondMotora = objResultadoAvaliacaoMuscular.buscaResultadoNeuroCondMotora();

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
    }
}

