using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WEDLC.Banco;
using WEDLC.DataSetsReport;
using WinFormsZoom;

namespace WEDLC.Forms
{
    public partial class frmResultado : Form
    {
        DataTable dadosXML = new DataTable();

        // Create a ToolTip component
        ToolTip toolTip1 = new ToolTip();

        public Acao cAcao = Acao.CANCELAR;
        public DataTable dtGrdFolhaPaciente;
        public bool ValidaFolha = false;
        public DataTable dtComboFolha;
        public int NumeroLinha = -1; // Variável para controlar a linha do grid da folha
        public int CodGrupoFolha = 0; // Variável para controlar o código do grupo da folha
        public int IdResultado = 0; //Variável para controlar o código do resultado
        public string IdadePaciente = string.Empty; //Variável para controlar a idade do paciente

        public const int codModulo = 13; //Código do módulo

        private FormZoomHelper zoomHelper;

        // Variaveis de controle para o relatório

        private Int32 relIdpaciente = 0;
        private Int32 relIdfolha = 0;
        private Int32 relCodGrupoFolha = 0;
        private string relSigla = string.Empty;
        private string relNome = string.Empty;

        public enum Acao
        {
            INSERT = 0,
            UPDATE = 1,
            DELETE = 2,
            SAVE = 3,
            CANCELAR = 4
        }

        public enum GrupoFolha
        {
            ENG = 1,
            PEV = 2,
            PESS = 3,
            PEA = 4,
            PEGC = 5,
            PESSMED = 6,
        }

        public frmResultado()
        {
            InitializeComponent();

            // Configurações do ToolTip
            toolTip1.AutoPopDelay = 5000; // Tempo que o ToolTip permanece visível
            toolTip1.InitialDelay = 500; // Tempo antes do ToolTip aparecer
            toolTip1.ReshowDelay = 500; // Tempo entre as aparições do ToolTip
            toolTip1.ShowAlways = true; // Sempre mostrar o ToolTip
            //toolTip1.SetToolTip(txtCep, "Digite o CEP sem pontos ou traços. Exemplo: 12345678");

            zoomHelper = new FormZoomHelper(this); // Inicializa o helper de zoom
            this.FormClosed += (s, e) => zoomHelper.Dispose(); // Descarta automaticamente quando o form for fechado
        }

        private void frmResultado_Load(object sender, EventArgs e)
        {
            // Configurações iniciais do formulário, se necessário
            this.DoubleBuffered = true;
            btnLimpar_Click(sender, e); //Simula o clique no botão cancelar   

        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdDadosPessoais_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Otimização: Acessa a linha apenas uma vez
                    DataGridViewRow row = grdDadosPessoais.Rows[e.RowIndex];

                    //Determina a acao
                    cAcao = Acao.UPDATE;

                    //controlaBotao(); //libera botões 
                    //liberaCampos(true); //Libera os campos para edição

                    txtCodigoProntuario.Enabled = false; //Desabilita o campo código
                    grdDadosPessoais.Enabled = false; //Desabilita o grid de dados

                    SuspendLayout();

                    //Preenche os campos com os dados da linha selecionada
                    txtCodigoProntuario.Text = row.Cells[0].Value.ToString();
                    txtNome.Text = row.Cells[1].Value.ToString();
                    IdadePaciente = row.Cells[3].Value.ToString(); // Nascimento

                    //Busca a folha do paciente
                    if (buscaResultadoFolha() == false)
                    {
                        return;
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao selecionar um item na grid dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                ResumeLayout();
            }
        }

        private void limpaFormulario()
        {
            //Limpa os campos
            txtCodigoProntuario.Text = string.Empty;
            txtNome.Text = string.Empty;
            grdDadosPessoais.DataSource = null;
            grdFolhaPaciente.DataSource = null;

            //Libera as variáveis do relatório
            relIdpaciente = 0;
            relIdfolha = 0;
            relCodGrupoFolha = 0;
            relSigla = string.Empty;
            relNome = string.Empty;

            NumeroLinha = -1; // Variável para controlar a linha do grid da folha
            CodGrupoFolha = 0; // Variável para controlar o código do grupo da folha
            IdResultado = 0; //Variável para controlar o código do resultado

            //Libera os objetos
            txtCodigoProntuario.Enabled = true; //Habilita o campo código
            grdDadosPessoais.Enabled = true; //Habilita o grid de dados

            //Deixa o foco no campo código prontuário
            txtCodigoProntuario.Focus();

            cAcao = Acao.CANCELAR;

        }

        private void txtCodigoProntuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void txtCodigoProntuario_KeyUp(object sender, KeyEventArgs e)
        {
            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
            {
                int tipopesquisa = 0; //Pesquisa pelo código   
                int idpaciente = 0; //Código do paciente

                //Limpa campos
                txtNome.Text = string.Empty;

                // Verifica a quantidade de caracteres
                if (txtCodigoProntuario.Text.Length > 0)
                {
                    tipopesquisa = 1;
                    idpaciente = int.Parse(txtCodigoProntuario.Text);
                }
                else
                {
                    tipopesquisa = 1;
                    idpaciente = 0;
                }

                this.populaGridDados(tipopesquisa, idpaciente, "");
                this.configuraGridDados();
            }
        }

        private void populaGridDados(int tipopesquisa, Int32 idpaciente, string nome)
        {
            try
            {
                DataTable dt = this.buscaResultadoPaciente(tipopesquisa, idpaciente, nome);

                grdDadosPessoais.DataSource = null;

                //Renomeia as colunas do datatable
                dt.Columns["idpaciente"].ColumnName = "Código";
                dt.Columns["nome"].ColumnName = "Nome";
                dt.Columns["sexo"].ColumnName = "Sexo";
                dt.Columns["nascimento"].ColumnName = "Nascimento";
                dt.Columns["telefone"].ColumnName = "Telefone";
                dt.Columns["convenio"].ColumnName = "Convênio";

                grdDadosPessoais.SuspendLayout();
                grdDadosPessoais.DataSource = dt;
                grdDadosPessoais.ResumeLayout();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar populaGrid!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void configuraGridDados()
        {
            try
            {
                grdDadosPessoais.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // Desabilita a edição da coluna
                grdDadosPessoais.Columns[0].ReadOnly = true;
                grdDadosPessoais.Columns[1].ReadOnly = true;
                grdDadosPessoais.Columns[2].ReadOnly = true;
                grdDadosPessoais.Columns[2].ReadOnly = true;
                grdDadosPessoais.Columns[3].ReadOnly = true;
                grdDadosPessoais.Columns[4].ReadOnly = true;
                grdDadosPessoais.Columns[5].ReadOnly = true;

                // Configurando outras propriedades
                grdDadosPessoais.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
                grdDadosPessoais.MultiSelect = false; // Impede seleção múltipla
                grdDadosPessoais.AllowUserToAddRows = false; // Impede adição de novas linhas
                grdDadosPessoais.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
                grdDadosPessoais.CurrentCell = null; // Desmarca a célula atual
                grdDadosPessoais.AllowUserToDeleteRows = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar configurar o grid de dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            int tipopesquisa = 2; //Código que pesquisa pelo nome
            string nome = string.Empty; //Nome da especialização

            //Determina a acao
            if (cAcao != Acao.UPDATE && cAcao != Acao.INSERT)
            {
                txtCodigoProntuario.Text = string.Empty; //Limpa o campo código resultado

                if (txtNome.Text.Length > 0)
                {
                    nome = txtNome.Text;
                }
                else
                {
                    nome = "!@#$%"; // Se não houver nome, define tipopesquisa como 0 para retornar não retornar ninguém
                }

                //Limpa campos
                txtCodigoProntuario.Text = string.Empty;
                this.populaGridDados(tipopesquisa, 0, nome);
                this.configuraGridDados();
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

        private bool buscaResultadoFolha()
        {
            try
            {
                int idpaciente = 0;

                if (txtCodigoProntuario.Text.ToString().Trim().Length > 0)
                {
                    //Busca os dados do paciente folha
                    idpaciente = int.Parse(txtCodigoProntuario.Text);
                }

                dtGrdFolhaPaciente = new DataTable();
                dtGrdFolhaPaciente = this.buscaResultadoFolha(idpaciente);
                grdFolhaPaciente.DataSource = null;

                //Renomeia as colunas do datatable
                dtGrdFolhaPaciente.Columns["idpaciente"].ColumnName = "IdPaciente";
                dtGrdFolhaPaciente.Columns["idfolha"].ColumnName = "IdFolha";
                dtGrdFolhaPaciente.Columns["sigla"].ColumnName = "Sigla";
                dtGrdFolhaPaciente.Columns["nome"].ColumnName = "Nome";
                dtGrdFolhaPaciente.Columns["idgrupofolha"].ColumnName = "Grupo";

                grdFolhaPaciente.SuspendLayout();
                grdFolhaPaciente.DataSource = dtGrdFolhaPaciente;
                grdFolhaPaciente.ResumeLayout();

                // Ajustando o tamanho das colunas e ocultando as que não são necessárias
                //grdFolha.Columns[3].Width = 80; //ID
                //grdFolha.Columns[4].Width = 350; //Nome

                grdFolhaPaciente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // Oculta ou exibe as colunas que não são necessárias
                grdFolhaPaciente.Columns[0].Visible = false; //IdPaciente
                grdFolhaPaciente.Columns[1].Visible = false; //IdFolha
                grdFolhaPaciente.Columns[4].Visible = false; //Grupo

                // Desabilita a edição da coluna
                grdFolhaPaciente.Columns[0].ReadOnly = true;
                grdFolhaPaciente.Columns[1].ReadOnly = true;
                grdFolhaPaciente.Columns[2].ReadOnly = true;
                grdFolhaPaciente.Columns[3].ReadOnly = true;
                grdFolhaPaciente.Columns[4].ReadOnly = true;

                // Configurando outras propriedades
                grdFolhaPaciente.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
                grdFolhaPaciente.MultiSelect = false; // Impede seleção múltipla
                grdFolhaPaciente.AllowUserToAddRows = false; // Impede adição de novas linhas
                grdFolhaPaciente.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
                grdFolhaPaciente.CurrentCell = null; // Desmarca a célula atual
                grdFolhaPaciente.AllowUserToDeleteRows = false;

                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar popular a folha paciente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void grdFolhaPaciente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verifica se a célula clicada é válida
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Obtém a linha selecionada
                    DataGridViewRow row = grdFolhaPaciente.Rows[e.RowIndex];
                    // Verifica se a linha contém dados
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {

                        // Obtém o ID e os textos das células necessárias
                        Int32 idpaciente = Convert.ToInt32(row.Cells[0].Value);
                        Int32 idfolha = Convert.ToInt32(row.Cells[1].Value);
                        string sigla = row.Cells[2].Value.ToString();
                        string nome = row.Cells[3].Value.ToString();
                        string idade = IdadePaciente; // Nascimento

                        // Armazena o número da linha selecionada do Grupo da folha
                        CodGrupoFolha = Convert.ToInt32(row.Cells[4].Value);

                        cResultado objResultado = new cResultado();
                        objResultado.Paciente.IdPaciente = idpaciente;
                        objResultado.Paciente.IdFolha = idfolha;

                        DataTable dtAux = new DataTable();
                        dtAux = objResultado.buscaIdResultado();
                        IdResultado = Convert.ToInt32(dtAux.Rows[0]["idresultado"]);

                        {
                            switch ((int)CodGrupoFolha)
                            {
                                case (int)GrupoFolha.ENG:

                                    // Cria um objeto para o form de troca de senhas abrir
                                    frmResultadoMusculoNeuro objResultadoMusculoNeuro = new frmResultadoMusculoNeuro();
                                    objResultadoMusculoNeuro.objResultadoAvaliacaoMuscular = new cResultadoAvaliacaoMuscular();
                                    objResultadoMusculoNeuro.objResultadoAvaliacaoMuscular.IdPaciente = idpaciente;
                                    objResultadoMusculoNeuro.objResultadoAvaliacaoMuscular.IdFolha = idfolha;
                                    objResultadoMusculoNeuro.objResultadoAvaliacaoMuscular.IdResultado = IdResultado;
                                    objResultadoMusculoNeuro.objResultadoAvaliacaoMuscular.Sigla = sigla;
                                    objResultadoMusculoNeuro.objResultadoAvaliacaoMuscular.Nome = nome;
                                    objResultadoMusculoNeuro.objResultadoAvaliacaoMuscular.Idade = idade;
                                    objResultadoMusculoNeuro.grupoFolha = CodGrupoFolha;

                                    //Abre o form de senha modal
                                    objResultadoMusculoNeuro.Show();
                                    break;

                                case (int)GrupoFolha.PEV:
                                case (int)GrupoFolha.PEA:
                                case (int)GrupoFolha.PESS:
                                case (int)GrupoFolha.PEGC:
                                case (int)GrupoFolha.PESSMED:

                                    if (cUtil.ValidaFormulario.FormularioEstaAberto<frmPotenciaisEvocados>() == true)
                                    {
                                        MessageBox.Show("O formulário de Potenciais Evocados já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                    // Cria um objeto para o form de troca de senhas abrir
                                    frmPotenciaisEvocados objPotenciaisEvocados = new frmPotenciaisEvocados();

                                    objPotenciaisEvocados.codGrupoFolha = CodGrupoFolha;
                                    objPotenciaisEvocados.IdFolha = idfolha;
                                    objPotenciaisEvocados.IdPaciente = idpaciente;
                                    objPotenciaisEvocados.IdResultado = IdResultado;
                                    objPotenciaisEvocados.sigla = sigla;
                                    objPotenciaisEvocados.nome = nome;

                                    // Altera o cursor para "espera"
                                    Cursor.Current = Cursors.WaitCursor;

                                    // Define o form pai como o form principal
                                    objPotenciaisEvocados.MdiParent = this.MdiParent;

                                    //Abre o form de especialização não modal
                                    objPotenciaisEvocados.Show();

                                    // Restaura o cursor normal
                                    Cursor.Current = Cursors.Default;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar selecionar a folha do paciente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            this.limpaFormulario();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (relIdpaciente > 0) // Verifica se um paciente foi selecionado
                {
                    cResultado objResultado = new cResultado();
                    objResultado.Paciente.IdPaciente = relIdpaciente;
                    objResultado.Paciente.IdFolha = relIdfolha;

                    DataTable dtAux = new DataTable();
                    dtAux = objResultado.buscaIdResultado();
                    IdResultado = Convert.ToInt32(dtAux.Rows[0]["idresultado"]);

                    var frm = new frmRelResultadoMusculoNeuro(relIdpaciente, relIdfolha, relCodGrupoFolha, relSigla, relNome, IdResultado); // passando ID do cliente
                    frm.ShowDialog();
                }

                else
                {
                    MessageBox.Show("Selecione uma folha para imprimir!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar imprimir o relatório!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void grdFolhaPaciente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Otimização: Acessa a linha apenas uma vez
                    DataGridViewRow row = grdFolhaPaciente.Rows[e.RowIndex];

                    SuspendLayout(); //Suspende o layout para evitar flickering

                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        // Obtém o ID e os textos das células necessárias
                        relIdpaciente = Convert.ToInt32(row.Cells[0].Value); // IdPaciente
                        relIdfolha = Convert.ToInt32(row.Cells[1].Value); // IdFolha
                        relSigla = row.Cells[2].Value.ToString(); // Sigla
                        relNome = row.Cells[3].Value.ToString(); // Nome
                        relCodGrupoFolha = Convert.ToInt32(row.Cells[4].Value); // Armazena o número da linha selecionada do Grupo da folha
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao selecionar um item na grid dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                ResumeLayout();
            }
        }
    }
}

