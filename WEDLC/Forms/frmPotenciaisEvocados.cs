using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WEDLC.Banco;
using static WEDLC.Banco.cUtil;
using System.Transactions;

namespace WEDLC.Forms
{
    public partial class frmPotenciaisEvocados : Form
    {
        //Código do módulo
        public const int codModulo = 1;

        //Variável para identificar se a chamada vem do fomrmulário de resultado do paciente
        public int IdResultado { get; set; }
        public int IdResultadoPEV { get; set; } // Usado para identificar o resultado PEV
        public int IdResultadoComentarioPEV { get; set; } // Usado para identificar o resultado PEV
        public int IdFolha { get; set; }
        public int IdPaciente { get; set; }

        public int codGrupoFolha; //Código do grupo de folha (PEV, PESS, PEA, PEGC, PESSMED)

        public string sigla;

        public string nome;

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
            // Configurações iniciais do formulário, se necessário
            this.DoubleBuffered = true;
            this.Text = "Folha: " + sigla + " - " + nome;
            iniciaTela();
        }

        public void iniciaTela()
        {
            try
            {
                //De acordo com o grupo de folha, formata a tela
                formataCarregaTela();

                // Ativa a visualização do click no form
                this.KeyPreview = true;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar a tela!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void formataCarregaTela()
        {
            // Primeiro, remove todas as abas
            tabPotenciais.TabPages.Clear();

            switch ((int)codGrupoFolha)
            {
                case (int)GrupoFolha.PEV:

                    // Adiciona apenas a aba correspondente ao grupo
                    tabPotenciais.TabPages.Add(tabPevi);

                    // Redimensiona a tela para PEV
                    RedimensionaTelaPEV();

                    if (CarregaDadosPev() == false)
                    {
                        throw new Exception("Erro na CarregaDadosPev");
                    }

                    if (CarregaDadosPotEvocadoTecnica() == false)
                    {
                        throw new Exception("Erro na CarregaDadosPotEvocadoTecnica"); ;
                    }

                    if (CarregaDadosComentarioPev() == false)
                    {
                        throw new Exception("Erro na CarregaDadosComentarioPev"); ;
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
            return true;
        }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                switch ((int)codGrupoFolha)
                {
                    case (int)GrupoFolha.PEV:

                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                if (GravaGrupoFolhaPotEvocadoTecnica() == false)
                                {
                                    throw new Exception("Falha ao gravar técnica PEV");
                                }

                                if (GravaGrupoFolhaPev() == false)
                                {
                                    throw new Exception("Falha ao gravar PEV");
                                }

                                if (GravaGrupoFolhaComentarioPev() == false)
                                {
                                    throw new Exception("Falha ao gravar comentário PEV");
                                }

                                // Se tudo ok, commit na transação
                                scope.Complete();

                                MessageBox.Show("Gravado com sucesso!", "Atenção",
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro na gravação: {ex.Message}", "Erro",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                // O rollback é automático quando scope.Complete() não é chamado
                            }
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
                objcPotenciaisPEV.IdPaciente = this.IdPaciente;

                DataTable dt = objcPotenciaisPEV.BuscaResultadoPotEvocadoTecnica();

                if (dt.Rows.Count > 0)
                {
                    // Preenche os campos com os dados retornados
                    txtCodigo.Text = dt.Rows[0]["idresultadopotevoctecnica"].ToString();
                    txtCaptacao.Text = dt.Rows[0]["captacao"].ToString();
                    txtSensibilidade.Text = dt.Rows[0]["sensibilidade"].ToString();
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
                    txtSensibilidade.Text = string.Empty;
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

                objcPotenciaisPEV.IdPaciente = this.IdPaciente;

                DataTable dt = objcPotenciaisPEV.BuscaResultadoPev();

                // Preenche os campos com os dados retornados
                this.IdResultado = Int32.Parse(dt.Rows[0]["idresultado"].ToString());
                this.IdResultadoPEV = Int32.Parse(dt.Rows[0]["idresultadopev"].ToString());
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
                //objcPotenciaisPEV.IdFolha = this.IdFolha;
                objcPotenciaisPEV.IdPaciente = this.IdPaciente;

                DataTable dt = objcPotenciaisPEV.BuscaResultadoComentarioPev();

                if (dt.Rows.Count > 0)
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultadoComentarioPEV = Int32.Parse(dt.Rows[0]["idresultadocomentariopev"].ToString());
                    txtCodigoComentario.Text = dt.Rows[0]["idcomentario"].ToString();
                    txtSiglaComentario.Text = dt.Rows[0]["sigla"].ToString();
                    txtNomeComentario.Text = dt.Rows[0]["nome"].ToString();
                    txtTextoComentario.Text = dt.Rows[0]["descricao"].ToString();
                }
                else
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultadoComentarioPEV = 0;
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

        private void RedimensionaTelaPEV()
        {
            tabPotenciais.Size = new Size(684, 179);
            grpBoxDados.Size = new Size(699, 344);
            groupBox6.Location = new Point(12, 360);
            grpBotoes.Location = new Point(12, 617);
            this.Size = new Size(735, 715);
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

        private bool GravaGrupoFolhaPev()
        {
            cPotenciaisPEV objcPotenciaisPEV = new cPotenciaisPEV();
            objcPotenciaisPEV.IdResultadoPev = this.IdResultadoPEV;
            objcPotenciaisPEV.IdResultado = this.IdResultado;
            objcPotenciaisPEV.IdPaciente = this.IdPaciente;
            objcPotenciaisPEV.N75OlhoDireito = txtN75OlhoDireito.Text;
            objcPotenciaisPEV.N75OlhoEsquerdo = txtN75OlhoEsquerdo.Text;
            objcPotenciaisPEV.P100OlhoDireito = txtP100OlhoDireito.Text;
            objcPotenciaisPEV.P100OlhoEsquerdo = txtP100OlhoEsquerdo.Text;
            objcPotenciaisPEV.P100Diferenca = txtP100Diferenca.Text;
            objcPotenciaisPEV.N145OlhoDireito = txtN145OlhoDireito.Text;
            objcPotenciaisPEV.N145OlhoEsquerdo = txtN145OlhoEsquerdo.Text;
            objcPotenciaisPEV.AmplitudeOlhoDireito = txtAmplitudeOlhoDireito.Text;
            objcPotenciaisPEV.AmplitudeOlhoEsquerdo = txtAmplitudeOlhoEsquerdo.Text;
            //objcPotenciaisPEV.objComentario.IdComentario = string.IsNullOrEmpty(txtCodigoComentario.Text) ? 0 : Convert.ToInt32(txtCodigoComentario.Text);
            //objcPotenciaisPEV.objComentario.Sigla = txtSiglaComentario.Text;
            //objcPotenciaisPEV.objComentario.Nome = txtNomeComentario.Text;
            //objcPotenciaisPEV.objComentario.Texto = txtTextoComentario.Text;
            if (objcPotenciaisPEV.AtualizarResultadoPEV() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool GravaGrupoFolhaPotEvocadoTecnica()
        {
            cPotenciaisEvocadosTecnica objPotenciaisEvocadosTecnica = new cPotenciaisEvocadosTecnica();
            objPotenciaisEvocadosTecnica.IdResultadoPotEvocTecnica = Convert.ToInt32(txtCodigo.Text);
            objPotenciaisEvocadosTecnica.IdResultado = this.IdResultado;
            objPotenciaisEvocadosTecnica.IdPaciente = this.IdPaciente;
            objPotenciaisEvocadosTecnica.Sensibilidade = txtSensibilidade.Text.ToUpper();
            objPotenciaisEvocadosTecnica.Captacao = txtCaptacao.Text.ToUpper();
            objPotenciaisEvocadosTecnica.UvDivTempo = string.IsNullOrEmpty(txtUvDivTempo.Text) ? (int?)null : Convert.ToInt32(txtUvDivTempo.Text);
            objPotenciaisEvocadosTecnica.Filtros = txtFiltros.Text.ToUpper();
            objPotenciaisEvocadosTecnica.Estimulacao = txtEstimulacao.Text;
            objPotenciaisEvocadosTecnica.FreqEstimada = string.IsNullOrEmpty(txtFreqEstim.Text) ? (int?)null : Convert.ToInt32(txtFreqEstim.Text);
            objPotenciaisEvocadosTecnica.NEstimulo = string.IsNullOrEmpty(txtNEstimulo.Text) ? (int?)null : Convert.ToInt32(txtNEstimulo.Text);
            objPotenciaisEvocadosTecnica.Intensidade = txtItensidade.Text.ToUpper();
            if (objPotenciaisEvocadosTecnica.AtualizaResultadoPotEvocadoTecnica() == false)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private bool GravaGrupoFolhaComentarioPev()
        {
            if (string.IsNullOrEmpty(txtCodigoComentario.Text))
            {
                // Se não houver comentário selecionado, nada a gravar
                return true;
            }

            cResultadoComentario cResultadoComentario = new cResultadoComentario();
            cResultadoComentario.IdResultadoComentario = this.IdResultadoComentarioPEV;
            cResultadoComentario.IdResultado = this.IdResultado;
            cResultadoComentario.IdPaciente = this.IdPaciente;
            //cResultadoComentario.IdFolha = this.IdFolha;
            cResultadoComentario.IdComentario = Convert.ToInt32(txtCodigoComentario.Text);
            cResultadoComentario.Texto = txtTextoComentario.Text;
            if (cResultadoComentario.AtualizarResultadoComentarioPEV() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void txtN75OlhoDireito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtN75OlhoDireito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFoco(sender, e);
            if (string.IsNullOrEmpty(txtN75OlhoDireito.Text))
            {
                txtN75OlhoDireito.Text = "0,0";
            }

        }

        private void txtN75OlhoEsquerdo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtN75OlhoEsquerdo_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFoco(sender, e);
            if (string.IsNullOrEmpty(txtN75OlhoEsquerdo.Text))
            {
                txtN75OlhoEsquerdo.Text = "0,0";
            }

        }

        private void txtP100OlhoDireito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFoco(sender, e);

            decimal? diferenca = CalculosTextBox.CalcularDiferencaPositiva(txtP100OlhoDireito, txtP100OlhoEsquerdo);

            if (diferenca.HasValue)
            {
                txtP100Diferenca.Text = diferenca.Value.ToString("N2");
            }

            if (string.IsNullOrEmpty(txtP100OlhoDireito.Text))
            {
                txtP100OlhoDireito.Text = "0,0";
            }

            ValidacaoTextBox.FormatarAoPerderFoco(txtP100Diferenca, e);
        }

        private void txtP100OlhoDireito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtP100OlhoEsquerdo_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFoco(sender, e);
            decimal? diferenca = CalculosTextBox.CalcularDiferencaPositiva(txtP100OlhoDireito, txtP100OlhoEsquerdo);

            if (diferenca.HasValue)
            {
                txtP100Diferenca.Text = diferenca.Value.ToString("N2");

            }
            if (string.IsNullOrEmpty(txtP100OlhoEsquerdo.Text))
            {
                txtP100OlhoEsquerdo.Text = "0,0";
            }

            ValidacaoTextBox.FormatarAoPerderFoco(txtP100Diferenca, e);
        }

        private void txtP100OlhoEsquerdo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtN145OlhoDireito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFoco(sender, e);

            if (string.IsNullOrEmpty(txtN145OlhoDireito.Text))
            {
                txtN145OlhoDireito.Text = "0,0";
            }
        }
        private void txtN145OlhoDireito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtN145OlhoEsquerdo_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFoco(sender, e);
            if (string.IsNullOrEmpty(txtN145OlhoEsquerdo.Text))
            {
                txtN145OlhoEsquerdo.Text = "0,0";
            }
        }

        private void txtN145OlhoEsquerdo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtAmplitudeOlhoDireito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFoco(sender, e);
            if (string.IsNullOrEmpty(txtAmplitudeOlhoDireito.Text))
            {
                txtAmplitudeOlhoDireito.Text = "0,0";
            }
        }

        private void txtAmplitudeOlhoDireito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtAmplitudeOlhoEsquerdo_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFoco(sender, e);
            if (string.IsNullOrEmpty(txtAmplitudeOlhoEsquerdo.Text))
            {
                txtAmplitudeOlhoEsquerdo.Text = "0,0";
            }
        }

        private void txtAmplitudeOlhoEsquerdo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtN75OlhoDireito_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtN75OlhoDireito), e);
        }

        private void txtN75OlhoEsquerdo_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtN75OlhoEsquerdo), e);
        }

        private void txtP100OlhoDireito_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtP100OlhoDireito), e);
        }

        private void txtP100OlhoEsquerdo_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtP100OlhoEsquerdo), e);
        }

        private void txtN145OlhoDireito_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtN145OlhoDireito), e);
        }

        private void txtN145OlhoEsquerdo_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtN145OlhoEsquerdo), e);
        }

        private void txtAmplitudeOlhoDireito_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtAmplitudeOlhoDireito), e);
        }

        private void txtAmplitudeOlhoEsquerdo_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtAmplitudeOlhoEsquerdo), e);
        }

        private void frmPotenciaisEvocados_Shown(object sender, EventArgs e)
        {
            txtCaptacao.Focus();
            ValidacaoTextBox.SelecionaTextoTextBox((txtCaptacao), e);
        }
    }
}