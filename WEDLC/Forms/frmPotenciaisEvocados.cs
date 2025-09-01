using System;
using System.Activities.Statements;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmPotenciaisEvocados : Form
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
        public const int codModulo = 1;

        //Variável para identificar se a chamada vem do fomrmulário de resultado do paciente
        public int IdAatividadeInsercao { get; set; }
        public int IdResultado { get; set; }
        public int IdFolha { get; set; }
        public int IdPaciente { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Texto { get; set; }

        public int codGrupoFolha; //Código do grupo de folha (PEV, PESS, PEA, PEGC, PESSMED)

        public enum GrupoFolha
        {
            ENG = 1,
            PEV = 2,
            PESS = 3,
            PEA = 4,
            PEGC = 5,
            PESSMED = 6,
        }

        public frmPotenciaisEvocados()
        {
            InitializeComponent();
        }

        private void frmPotenciaisEvocados_Load(object sender, EventArgs e)
        {
            carregaTela();
        }

        public void carregaTela()
        {
            try
            {
                //De acordo com o grupo de folha, formata a tela
                formataTela();

                // Ativa a visualização do click no form
                this.KeyPreview = true;

                cAcao = Acao.CANCELAR;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar a tela!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void formataTela()
        {
            // Primeiro, remove todas as abas
            tabPotenciais.TabPages.Clear();

            // Adiciona apenas a aba correspondente ao grupo
            switch ((int)codGrupoFolha)
            {
                case (int)GrupoFolha.PEV:
                    tabPotenciais.TabPages.Add(tabPevi);
                    if (CarregaDadosPev() == false)
                    {
                        break;
                    }
                    if (CarregaDadosComentarioPev() == false)
                    {
                        break;
                    }
                    if (CarregaDadosPotEvocadoTecnica() == false)
                    {
                        break;
                    }
                    break;
                case (int)GrupoFolha.PEA:
                    tabPotenciais.TabPages.Add(tabPea);
                    break;
                case (int)GrupoFolha.PESS:
                    tabPotenciais.TabPages.Add(tabPess);
                    break;
                case (int)GrupoFolha.PEGC:
                    tabPotenciais.TabPages.Add(tabPegc);
                    break;
                case (int)GrupoFolha.PESSMED:
                    tabPotenciais.TabPages.Add(tabPessMed);
                    break;
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

            if (txtCaptacao.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("O campo sigla não está preenchido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCaptacao.Focus();
                return false;
            }

            if (txtUvDivTempo.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("O campo nome não está preenchido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUvDivTempo.Focus();
                return false;
            }

            return true;

        }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {

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


        private bool CarregaDadosPotEvocadoTecnica()
        {
            try
            {
                cPotenciaisPEV objcPotenciaisPEV = new cPotenciaisPEV();

                objcPotenciaisPEV.IdResultado = this.IdResultado;
                objcPotenciaisPEV.IdFolha = this.IdFolha;
                objcPotenciaisPEV.IdPaciente = this.IdPaciente;

                DataTable dt = objcPotenciaisPEV.BuscaResultadoPotEvocadoTecnica();

                if (dt.Rows.Count > 0)
                {
                    // Preenche os campos com os dados retornados
                    txtCodigo.Text = dt.Rows[0]["idresultadopotevoctecnica"].ToString();
                    txtCaptacao.Text = dt.Rows[0]["captacao"].ToString();
                    txtUvDivTempo.Text = dt.Rows[0]["uvdivtempo"].ToString();
                    txtFiltros.Text = dt.Rows[0]["filtros"].ToString();
                    txtEstimulacao.Text = dt.Rows[0]["estimulacao"].ToString();
                    txtFreqEstim.Text = dt.Rows[0]["freqestimada"].ToString();
                    txtNEstimulo.Text = dt.Rows[0]["nestimulo"].ToString();
                    txtItensidade.Text = dt.Rows[0]["intensidade"].ToString();
                }
                else
                {
                    // Preenche os campos com os dados retornados
                    txtCodigo.Text = string.Empty;
                    txtCaptacao.Text = string.Empty;
                    txtUvDivTempo.Text = string.Empty;
                    txtFiltros.Text = string.Empty;
                    txtEstimulacao.Text = string.Empty;
                    txtFreqEstim.Text = string.Empty;
                    txtNEstimulo.Text = string.Empty;
                    txtItensidade.Text = string.Empty;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CarregaDadosPev()
        {
            try
            {
                cPotenciaisPEV objcPotenciaisPEV = new cPotenciaisPEV();

                objcPotenciaisPEV.IdResultado = this.IdResultado;
                objcPotenciaisPEV.IdFolha = this.IdFolha;
                objcPotenciaisPEV.IdPaciente = this.IdPaciente;

                    DataTable dt = objcPotenciaisPEV.BuscaResultadoPev();

                // Preenche os campos com os dados retornados
                txtN75OlhoDireito.Text = dt.Rows[0]["N75OlhoDireito"].ToString();
                txtN75OlhoEsquerdo.Text = dt.Rows[0]["N75OlhoEsquerdo"].ToString();
                txtP100OlhoDireito.Text = dt.Rows[0]["P100OlhoDireito"].ToString();
                txtP100OlhoEsquerdo.Text = dt.Rows[0]["P100OlhoEsquerdo"].ToString();
                txtP100Diferenca.Text = dt.Rows[0]["P100Diferenca"].ToString();
                txtN145OlhoDireito.Text = dt.Rows[0]["N145OlhoDireito"].ToString();
                txtN145OlhoEsquerdo.Text = dt.Rows[0]["N145OlhoEsquerdo"].ToString();
                txtAmplitudeOlhoDireito.Text = dt.Rows[0]["AmplitudeOlhoDireito"].ToString();
                txtAmplitudeOlhoEsquerdo.Text = dt.Rows[0]["AmplitudeOlhoEsquerdo"].ToString();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CarregaDadosComentarioPev()
        {
            try
            {
                cPotenciaisPEV objcPotenciaisPEV = new cPotenciaisPEV();

                objcPotenciaisPEV.IdResultado = this.IdResultado;
                objcPotenciaisPEV.IdFolha = this.IdFolha;
                objcPotenciaisPEV.IdPaciente = this.IdPaciente;

                DataTable dt = objcPotenciaisPEV.BuscaResultadoComentarioPev();

                if (dt.Rows.Count > 0 )
                {
                    // Preenche os campos com os dados retornados
                    txtCodigoComentario.Text = dt.Rows[0]["idcomentario"].ToString();
                    txtSiglaComentario.Text = dt.Rows[0]["sigla"].ToString();
                    txtNomeComentario.Text = dt.Rows[0]["nome"].ToString();
                    txtTextoComentario.Text = dt.Rows[0]["descricao"].ToString();

                }
                else
                {
                    // Preenche os campos com os dados retornados
                    txtCodigoComentario.Text = string.Empty;
                    txtSiglaComentario.Text = string.Empty;
                    txtNomeComentario.Text = string.Empty;   
                    txtTextoComentario.Text = string.Empty;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}