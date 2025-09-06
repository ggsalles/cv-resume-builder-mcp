using System;
using System.Data;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;
using WEDLC.Banco;
using static Mysqlx.Notice.Frame.Types;
using static WEDLC.Banco.cUtil;

namespace WEDLC.Forms
{
    public partial class frmPotenciaisEvocados : Form
    {
        //Código do módulo
        public const int codModulo = 1;

        //Variável para identificar se a chamada vem do fomrmulário de resultado do paciente
        public int IdResultado { get; set; }
        public int IdResultadoPEV { get; set; } // Usado para identificar o resultado PEV
        public int IdResultadoPEA { get; set; } // Usado para identificar o resultado PEV
        public int IdResultadoPESS { get; set; } // Usado para identificar o resultado PESS
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

        private void frmPotenciaisEvocados_Shown(object sender, EventArgs e)
        {
            txtCaptacao.Focus();
            ValidacaoTextBox.SelecionaTextoTextBox((txtCaptacao), e);
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

                    if (CarregaDadosPea() == false)
                    {
                        throw new Exception("Erro na CarregaDadosPea");
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

                case (int)GrupoFolha.PESS:

                    tabPotenciais.TabPages.Add(tabPess);

                    RedimensionaTelaPESS();

                    if (CarregaDadosPess() == false)
                    {
                        throw new Exception("Erro na CarregaDadosPea");
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

                                if (ValidaComentarioPEV() == false)
                                {
                                    break;
                                }
                                if (GravaGrupoFolhaComentarioPev() == false)
                                {
                                    throw new Exception("Falha ao gravar comentário PEV");
                                }

                                // Se tudo ok, commit na transação
                                scope.Complete();

                                MessageBox.Show("Gravado com sucesso!", "Atenção",
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                                this.Close();
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

                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                if (GravaGrupoFolhaPotEvocadoTecnica() == false)
                                {
                                    throw new Exception("Falha ao gravar técnica PEV");
                                }

                                if (GravaGrupoFolhaPea() == false)
                                {
                                    throw new Exception("Falha ao gravar PEA");
                                }
                                if (ValidaComentarioPEV() == false)
                                {
                                    break;
                                }
                                if (GravaGrupoFolhaComentarioPev() == false)
                                {
                                    throw new Exception("Falha ao gravar comentário PEV");
                                }

                                // Se tudo ok, commit na transação
                                scope.Complete();

                                MessageBox.Show("Gravado com sucesso!", "Atenção",
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro na gravação: {ex.Message}", "Erro",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                // O rollback é automático quando scope.Complete() não é chamado
                            }
                        }

                        break;

                    case (int)GrupoFolha.PESS:

                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                if (GravaGrupoFolhaPotEvocadoTecnica() == false)
                                {
                                    throw new Exception("Falha ao gravar técnica PEV");
                                }

                                if (GravaGrupoFolhaPess() == false)
                                {
                                    throw new Exception("Falha ao gravar PEA");
                                }
                                if (ValidaComentarioPEV() == false)
                                {
                                    break;
                                }
                                if (GravaGrupoFolhaComentarioPev() == false)
                                {
                                    throw new Exception("Falha ao gravar comentário PEV");
                                }

                                // Se tudo ok, commit na transação
                                scope.Complete();

                                MessageBox.Show("Gravado com sucesso!", "Atenção",
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro na gravação: {ex.Message}", "Erro",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                // O rollback é automático quando scope.Complete() não é chamado
                            }
                        }

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
                objcPotenciaisPEV.IdResultado = this.IdResultado;

                DataTable dt = objcPotenciaisPEV.BuscaResultadoPev();

                if (dt.Rows.Count > 0)
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultado = dt.Rows[0]["idresultado"] != DBNull.Value ? Int32.Parse(dt.Rows[0]["idresultado"].ToString()) : 0;
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
                else
                {
                    // Limpa os campos de texto
                    txtN75OlhoDireito.Text = "";
                    txtN75OlhoEsquerdo.Text = "";
                    txtP100OlhoDireito.Text = "";
                    txtP100OlhoEsquerdo.Text = "";
                    txtP100Diferenca.Text = "";
                    txtN145OlhoDireito.Text = "";
                    txtN145OlhoEsquerdo.Text = "";
                    txtAmplitudeOlhoDireito.Text = "";
                    txtAmplitudeOlhoEsquerdo.Text = ""; ;
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao CarregaDadosPev: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CarregaDadosComentarioPev()
        {
            try
            {
                cPotenciaisPEV objcPotenciaisPEV = new cPotenciaisPEV();

                objcPotenciaisPEV.IdResultado = this.IdResultado;
                objcPotenciaisPEV.IdPaciente = this.IdPaciente;

                DataTable dt = objcPotenciaisPEV.BuscaResultadoComentarioPev();

                if (dt.Rows.Count > 0)
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultadoComentarioPEV = dt.Rows[0]["idresultadocomentariopev"] != DBNull.Value ? Int32.Parse(dt.Rows[0]["idresultadocomentariopev"].ToString()) : 0;
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
            grpComentario.Location = new Point(12, 360);
            grpBotoes.Location = new Point(12, 617);
            this.Size = new Size(735, 715);
        }

        private bool CarregaDadosPea()
        {
            try
            {
                cPotenciaisPEA objcPotenciaisPEA = new cPotenciaisPEA();

                objcPotenciaisPEA.IdResultado = this.IdResultado;
                objcPotenciaisPEA.IdPaciente = this.IdPaciente;

                DataTable dt = objcPotenciaisPEA.BuscaResultadoPea();

                if (dt.Rows.Count > 0)
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultado = dt.Rows[0]["idresultado"] != DBNull.Value ? Int32.Parse(dt.Rows[0]["idresultado"].ToString()) : 0;
                    this.IdResultadoPEA = Int32.Parse(dt.Rows[0]["idresultadopea"].ToString());

                    // Preenche os campos das ondas
                    txtonda1direito.Text = dt.Rows[0]["onda1ouvidodireito"] != DBNull.Value ?
                        dt.Rows[0]["onda1ouvidodireito"].ToString() : "";
                    txtonda1esquerdo.Text = dt.Rows[0]["onda1ouvidoesquerdo"] != DBNull.Value ?
                        dt.Rows[0]["onda1ouvidoesquerdo"].ToString() : "";
                    txtonda2direito.Text = dt.Rows[0]["onda2ouvidodireito"] != DBNull.Value ?
                        dt.Rows[0]["onda2ouvidodireito"].ToString() : "";
                    txtonda2esquerdo.Text = dt.Rows[0]["onda2ouvidoesquerdo"] != DBNull.Value ?
                        dt.Rows[0]["onda2ouvidoesquerdo"].ToString() : "";
                    txtonda3direito.Text = dt.Rows[0]["onda3ouvidodireito"] != DBNull.Value ?
                        dt.Rows[0]["onda3ouvidodireito"].ToString() : "";
                    txtonda3esquerdo.Text = dt.Rows[0]["onda3ouvidoesquerdo"] != DBNull.Value ?
                        dt.Rows[0]["onda3ouvidoesquerdo"].ToString() : "";
                    txtonda4direito.Text = dt.Rows[0]["onda4vouvidodireito"] != DBNull.Value ?
                        dt.Rows[0]["onda4vouvidodireito"].ToString() : "";
                    txtonda4esquerdo.Text = dt.Rows[0]["onda4ouvidoesquerdo"] != DBNull.Value ?
                        dt.Rows[0]["onda4ouvidoesquerdo"].ToString() : "";
                    txtonda5direito.Text = dt.Rows[0]["onda5ouvidodireito"] != DBNull.Value ?
                        dt.Rows[0]["onda5ouvidodireito"].ToString() : "";
                    txtonda5esquerdo.Text = dt.Rows[0]["onda5ouvidoesquerdo"] != DBNull.Value ?
                        dt.Rows[0]["onda5ouvidoesquerdo"].ToString() : "";

                    // Preenche os campos dos intervalos
                    txt1a3direito.Text = dt.Rows[0]["1a3direito"] != DBNull.Value ?
                        dt.Rows[0]["1a3direito"].ToString() : "";
                    txt1a3esquerdo.Text = dt.Rows[0]["1a3esquerdo"] != DBNull.Value ?
                        dt.Rows[0]["1a3esquerdo"].ToString() : "";
                    txt3a5direito.Text = dt.Rows[0]["3a5direito"] != DBNull.Value ?
                        dt.Rows[0]["3a5direito"].ToString() : "";
                    txt3a5esquerdo.Text = dt.Rows[0]["3a5esquerdo"] != DBNull.Value ?
                        dt.Rows[0]["3a5esquerdo"].ToString() : "";
                    txt1a4direito.Text = dt.Rows[0]["1a4direito"] != DBNull.Value ?
                        dt.Rows[0]["1a4direito"].ToString() : "";
                    txt1a4esquerdo.Text = dt.Rows[0]["1a4esquerdo"] != DBNull.Value ?
                        dt.Rows[0]["1a4esquerdo"].ToString() : "";
                }

                else
                {
                    // Limpa os campos das ondas
                    txtonda1direito.Text = "";
                    txtonda1esquerdo.Text = "";
                    txtonda2direito.Text = "";
                    txtonda2esquerdo.Text = "";
                    txtonda3direito.Text = "";
                    txtonda3esquerdo.Text = "";
                    txtonda4direito.Text = "";
                    txtonda4esquerdo.Text = "";
                    txtonda5direito.Text = "";
                    txtonda5esquerdo.Text = "";

                    // Limpa os campos dos intervalos
                    txt1a3direito.Text = "";
                    txt1a3esquerdo.Text = "";
                    txt3a5direito.Text = "";
                    txt3a5esquerdo.Text = "";
                    txt1a4direito.Text = "";
                    txt1a4esquerdo.Text = "";
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

            if (objcPotenciaisPEV.AtualizarResultadoPEV() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool GravaGrupoFolhaPea()
        {
            cPotenciaisPEA objcPotenciaisPEA = new cPotenciaisPEA();
            objcPotenciaisPEA.IdResultadoPea = this.IdResultadoPEA;
            objcPotenciaisPEA.IdResultado = this.IdResultado;
            objcPotenciaisPEA.IdPaciente = this.IdPaciente;

            // Preencher os outros campos específicos do PEA aqui
            objcPotenciaisPEA.Onda1OuvidoDireito = txtonda1direito.Text;
            objcPotenciaisPEA.Onda1OuvidoEsquerdo = txtonda1esquerdo.Text;
            objcPotenciaisPEA.Onda2OuvidoDireito = txtonda2direito.Text;
            objcPotenciaisPEA.Onda2OuvidoEsquerdo = txtonda2esquerdo.Text;
            objcPotenciaisPEA.Onda3OuvidoDireito = txtonda3direito.Text;
            objcPotenciaisPEA.Onda3OuvidoEsquerdo = txtonda3esquerdo.Text;
            objcPotenciaisPEA.Onda4OuvidoDireito = txtonda4direito.Text;
            objcPotenciaisPEA.Onda4OuvidoEsquerdo = txtonda4esquerdo.Text;
            objcPotenciaisPEA.Onda5OuvidoDireito = txtonda5direito.Text;
            objcPotenciaisPEA.Onda5OuvidoEsquerdo = txtonda5esquerdo.Text;
            objcPotenciaisPEA.Intervalo1a3Direito = txt1a3direito.Text;
            objcPotenciaisPEA.Intervalo1a3Esquerdo = txt1a3esquerdo.Text;
            objcPotenciaisPEA.Intervalo3a5Direito = txt3a5direito.Text;
            objcPotenciaisPEA.Intervalo3a5Esquerdo = txt3a5esquerdo.Text;
            objcPotenciaisPEA.Intervalo1a4Direito = txt1a4direito.Text;
            objcPotenciaisPEA.Intervalo1a4Esquerdo = txt1a4esquerdo.Text;

            if (objcPotenciaisPEA.AtualizarResultadoPEA() == false)
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
            objPotenciaisEvocadosTecnica.Estimulacao = txtEstimulacao.Text.ToUpper();
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
            cResultadoComentario.Texto = txtTextoComentario.Text.ToUpper();
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
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
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
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
            if (string.IsNullOrEmpty(txtN75OlhoEsquerdo.Text))
            {
                txtN75OlhoEsquerdo.Text = "0,0";
            }

        }
        private void txtP100OlhoDireito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);

            decimal? diferenca = CalculosTextBox.CalcularDiferencaPositiva(txtP100OlhoDireito, txtP100OlhoEsquerdo);

            if (diferenca.HasValue)
            {
                txtP100Diferenca.Text = diferenca.Value.ToString("N2");
            }

            if (string.IsNullOrEmpty(txtP100OlhoDireito.Text))
            {
                txtP100OlhoDireito.Text = "0,0";
            }

            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(txtP100Diferenca, e);
        }

        private void txtP100OlhoDireito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtP100OlhoEsquerdo_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
            decimal? diferenca = CalculosTextBox.CalcularDiferencaPositiva(txtP100OlhoDireito, txtP100OlhoEsquerdo);

            if (diferenca.HasValue)
            {
                txtP100Diferenca.Text = diferenca.Value.ToString("N2");

            }
            if (string.IsNullOrEmpty(txtP100OlhoEsquerdo.Text))
            {
                txtP100OlhoEsquerdo.Text = "0,0";
            }

            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(txtP100Diferenca, e);
        }

        private void txtP100OlhoEsquerdo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtN145OlhoDireito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);

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
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
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
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
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
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
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

        private void txtonda1direito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtonda1direito_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtonda1direito), e);
        }

        private void txtonda1direito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoDuasCasasDecimais(sender, e);
            if (string.IsNullOrEmpty(txtonda1direito.Text))
            {
                txtonda1direito.Text = "0,00";
            }
            CalculaDiferençaPEA();
        }

        private void txtonda1esquerdo_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtonda1esquerdo), e);
        }

        private void txtonda1esquerdo_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoDuasCasasDecimais(sender, e);
            if (string.IsNullOrEmpty(txtonda1esquerdo.Text))
            {
                txtonda1esquerdo.Text = "0,00";
            }
            CalculaDiferençaPEA();
        }

        private void txtonda1esquerdo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtonda2direito_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtonda2direito), e);
        }

        private void txtonda2direito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoDuasCasasDecimais(sender, e);
            if (string.IsNullOrEmpty(txtonda2direito.Text))
            {
                txtonda2direito.Text = "0,00";
            }
            CalculaDiferençaPEA();
        }

        private void txtonda2direito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtonda2esquerdo_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtonda2esquerdo), e);
        }

        private void txtonda2esquerdo_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoDuasCasasDecimais(sender, e);
            if (string.IsNullOrEmpty(txtonda2esquerdo.Text))
            {
                txtonda2esquerdo.Text = "0,00";
            }
            CalculaDiferençaPEA();
        }

        private void txtonda2esquerdo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtonda3direito_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtonda3direito), e);
        }

        private void txtonda3direito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoDuasCasasDecimais(sender, e);
            if (string.IsNullOrEmpty(txtonda3direito.Text))
            {
                txtonda3direito.Text = "0,00";
            }
            CalculaDiferençaPEA();
        }

        private void txtonda3direito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtonda3esquerdo_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtonda3esquerdo), e);
        }

        private void txtonda3esquerdo_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoDuasCasasDecimais(sender, e);
            if (string.IsNullOrEmpty(txtonda3esquerdo.Text))
            {
                txtonda3esquerdo.Text = "0,00";
            }
            CalculaDiferençaPEA();
        }

        private void txtonda3esquerdo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtonda4direito_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtonda4direito), e);
        }

        private void txtonda4direito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoDuasCasasDecimais(sender, e);
            if (string.IsNullOrEmpty(txtonda4direito.Text))
            {
                txtonda4direito.Text = "0,00";
            }
            CalculaDiferençaPEA();
        }

        private void txtonda4direito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtonda4esquerdo_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtonda4esquerdo), e);
        }

        private void txtonda4esquerdo_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoDuasCasasDecimais(sender, e);
            if (string.IsNullOrEmpty(txtonda4esquerdo.Text))
            {
                txtonda4esquerdo.Text = "0,00";
            }
            CalculaDiferençaPEA();
        }

        private void txtonda4esquerdo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtonda5direito_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtonda5direito), e);
        }

        private void txtonda5direito_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoDuasCasasDecimais(sender, e);
            if (string.IsNullOrEmpty(txtonda5direito.Text))
            {
                txtonda5direito.Text = "0,00";
            }
            CalculaDiferençaPEA();
        }

        private void txtonda5direito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void txtonda5esquerdo_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtonda5esquerdo), e);
        }

        private void txtonda5esquerdo_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoDuasCasasDecimais(sender, e);
            if (string.IsNullOrEmpty(txtonda5esquerdo.Text))
            {
                txtonda5esquerdo.Text = "0,00";
            }
            CalculaDiferençaPEA();
        }

        private void txtonda5esquerdo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacaoTextBox.PermitirDecimaisPositivosNegativos((TextBox)sender, e);
        }

        private void CalculaDiferençaPEA()
        {
            decimal? diferenca = CalculosTextBox.CalcularDiferencaPositiva(txtonda1direito, txtonda3direito);
            txt1a3direito.Text = diferenca.HasValue ? diferenca.Value.ToString("N2") : string.Empty;

            diferenca = CalculosTextBox.CalcularDiferencaPositiva(txtonda1esquerdo, txtonda3esquerdo);
            txt1a3esquerdo.Text = diferenca.HasValue ? diferenca.Value.ToString("N2") : string.Empty;

            diferenca = CalculosTextBox.CalcularDiferencaPositiva(txtonda3direito, txtonda5direito);
            txt3a5direito.Text = diferenca.HasValue ? diferenca.Value.ToString("N2") : string.Empty;

            diferenca = CalculosTextBox.CalcularDiferencaPositiva(txtonda3esquerdo, txtonda5esquerdo);
            txt3a5esquerdo.Text = diferenca.HasValue ? diferenca.Value.ToString("N2") : string.Empty;

            diferenca = CalculosTextBox.CalcularDiferencaPositiva(txtonda1direito, txtonda4direito);
            txt1a4direito.Text = diferenca.HasValue ? diferenca.Value.ToString("N2") : string.Empty;

            diferenca = CalculosTextBox.CalcularDiferencaPositiva(txtonda1esquerdo, txtonda4esquerdo);
            txt1a4esquerdo.Text = diferenca.HasValue ? diferenca.Value.ToString("N2") : string.Empty;
        }

        private void txtUvDivTempo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void txtFreqEstim_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void txtNEstimulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private bool ValidaComentarioPEV()
        {
            if (string.IsNullOrEmpty(txtCodigoComentario.Text))
            {
                MessageBox.Show("Por favor, selecione um tipo de comentário.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool CarregaDadosPess()
        {
            try
            {
                cPotenciaisPESS objcPotenciaisPESS = new cPotenciaisPESS();

                objcPotenciaisPESS.IdPaciente = this.IdPaciente;
                objcPotenciaisPESS.IdResultado = this.IdResultado;
                objcPotenciaisPESS.IdFolha = this.IdFolha;

                DataTable dt = objcPotenciaisPESS.BuscaResultadoPess();

                // Preenche os campos com os dados retornados
                this.IdResultado = dt.Rows[0]["idresultado"] != DBNull.Value ? Int32.Parse(dt.Rows[0]["idresultado"].ToString()) : 0;
                this.IdResultadoPESS = Int32.Parse(dt.Rows[0]["idresultadopess"].ToString());

                grdDadosPess.DataSource = dt;

                configuraGridDadosPess();

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao CarregaDadosPev: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void configuraGridDadosPess()
        {
            try
            {
                grdDadosPess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // Desabilita a edição da coluna
                grdDadosPess.Columns[0].ReadOnly = true;
                grdDadosPess.Columns[1].ReadOnly = true;
                grdDadosPess.Columns[2].ReadOnly = true;
                grdDadosPess.Columns[2].ReadOnly = true;
                grdDadosPess.Columns[3].ReadOnly = true;
                grdDadosPess.Columns[4].ReadOnly = true;
                grdDadosPess.Columns[5].ReadOnly = true;
                grdDadosPess.Columns[6].ReadOnly = true;
                grdDadosPess.Columns[7].ReadOnly = true;
                grdDadosPess.Columns[8].ReadOnly = true;
                grdDadosPess.Columns[9].ReadOnly = true;
                grdDadosPess.Columns[10].ReadOnly = true;
                grdDadosPess.Columns[11].ReadOnly = true;

                // Deixando as colunas ocultas
                grdDadosPess.Columns["idfolha"].Visible = false;
                grdDadosPess.Columns["idresultado"].Visible = false;
                grdDadosPess.Columns["idestudopotenevocado"].Visible = false;
                grdDadosPess.Columns["idresultadopess"].Visible = false;
                grdDadosPess.Columns["idpaciente"].Visible = false;

                // Renomeando os cabeçalhos
                grdDadosPess.Columns["descricao"].HeaderText = "Descrição";
                grdDadosPess.Columns["p1n1ladodireitorespobt"].HeaderText = "P1-N1 Direito Obtida";
                grdDadosPess.Columns["p1n1ladoesquerdorespobt"].HeaderText = "P1-N1 Esquerdo Obtida";
                grdDadosPess.Columns["diferencaobitida"].HeaderText = "Diferença Obtida";
                grdDadosPess.Columns["p1n1ladodireitorespoesp"].HeaderText = "P1-N1 Direito Esperada";
                grdDadosPess.Columns["p1n1ladoesquerdorespoesp"].HeaderText = "P1-N1 Esquerdo Esperada";
                grdDadosPess.Columns["diferencaesperada"].HeaderText = "Diferença Esperada";

                // Configurando outras propriedades
                grdDadosPess.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona linha inteira
                grdDadosPess.MultiSelect = false; // Impede seleção múltipla
                grdDadosPess.AllowUserToAddRows = false; // Impede adição de novas linhas
                grdDadosPess.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Cor de fundo das linhas alternadas
                grdDadosPess.CurrentCell = null; // Desmarca a célula atual
                grdDadosPess.AllowUserToDeleteRows = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar configurar o grid de dados pess!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void grdDadosPess_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdDadosPess.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = grdDadosPess.SelectedRows[0];

                    // Preenche as TextBoxes com os valores da linha selecionada
                    txtP1Ni1DireitoObitida.Text = row.Cells["p1n1ladodireitorespobt"].Value != DBNull.Value ? row.Cells["p1n1ladodireitorespobt"].Value.ToString() : "";
                    txtP1Ni1EsquerdoObidida.Text = row.Cells["p1n1ladoesquerdorespobt"].Value != DBNull.Value ? row.Cells["p1n1ladoesquerdorespobt"].Value.ToString() : "";
                    txtDiferencaObitida.Text = row.Cells["diferencaobitida"].Value != DBNull.Value ? row.Cells["diferencaobitida"].Value.ToString() : "";
                    txtP1Ni1DireitoEsperada.Text = row.Cells["p1n1ladodireitorespoesp"].Value != DBNull.Value ? row.Cells["p1n1ladodireitorespoesp"].Value.ToString() : "";
                    txtP1Ni1EsquerdoEsperada.Text = row.Cells["p1n1ladoesquerdorespoesp"].Value != DBNull.Value ? row.Cells["p1n1ladoesquerdorespoesp"].Value.ToString() : "";
                    txtDiferencaEsperada.Text = row.Cells["diferencaesperada"].Value != DBNull.Value ? row.Cells["diferencaesperada"].Value.ToString() : "";
                    txtP1Ni1DireitoObitida.Focus();

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void AtualizaValoresGridPessTXT()
        {
            DataGridViewRow row = grdDadosPess.SelectedRows[0];

            // Atualiza os valores da linha com os valores das TextBoxes
            row.Cells["p1n1ladodireitorespobt"].Value = txtP1Ni1DireitoObitida.Text;
            row.Cells["p1n1ladodireitorespoesp"].Value = txtP1Ni1DireitoEsperada.Text;
            row.Cells["p1n1ladoesquerdorespobt"].Value = txtP1Ni1EsquerdoObidida.Text;
            row.Cells["p1n1ladoesquerdorespoesp"].Value = txtP1Ni1EsquerdoEsperada.Text;
            row.Cells["diferencaobitida"].Value = Convert.ToString(CalculosTextBox.CalcularDiferencaPositiva(txtP1Ni1DireitoObitida, txtP1Ni1EsquerdoObidida));
            row.Cells["diferencaesperada"].Value = txtDiferencaEsperada.Text;

            txtDiferencaObitida.Text = Convert.ToString(CalculosTextBox.CalcularDiferencaPositiva(txtP1Ni1DireitoObitida, txtP1Ni1EsquerdoObidida));
        }
        private void txtP1Ni1DireitoObitida_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);

            if (string.IsNullOrEmpty(txtP1Ni1DireitoObitida.Text))
            {
                txtP1Ni1DireitoObitida.Text = "0,0";
            }

            AtualizaValoresGridPessTXT();
        }

        private void txtP1Ni1DireitoObitida_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtP1Ni1DireitoObitida), e);
        }

        private void txtP1Ni1DireitoObitida_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void txtP1Ni1DireitoEsperada_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtP1Ni1DireitoEsperada), e);
        }

        private void txtP1Ni1DireitoEsperada_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
            if (string.IsNullOrEmpty(txtP1Ni1DireitoEsperada.Text))
            {
                txtP1Ni1DireitoEsperada.Text = "0,0";
            }
            AtualizaValoresGridPessTXT();
        }

        private void txtP1Ni1DireitoEsperada_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void txtP1Ni1EsquerdoObidida_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtP1Ni1EsquerdoObidida), e);
        }

        private void txtP1Ni1EsquerdoObidida_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
            if (string.IsNullOrEmpty(txtP1Ni1EsquerdoObidida.Text))
            {
                txtP1Ni1EsquerdoObidida.Text = "0,0";
            }
            AtualizaValoresGridPessTXT();
        }

        private void txtP1Ni1EsquerdoObidida_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void txtP1Ni1EsquerdoEsperada_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtP1Ni1EsquerdoEsperada), e);
        }

        private void txtP1Ni1EsquerdoEsperada_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
            if (string.IsNullOrEmpty(txtP1Ni1EsquerdoEsperada.Text))
            {
                txtP1Ni1EsquerdoEsperada.Text = "0,0";
            }
            AtualizaValoresGridPessTXT();
        }

        private void txtP1Ni1EsquerdoEsperada_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private void txtDiferencaEsperada_Enter(object sender, EventArgs e)
        {
            ValidacaoTextBox.SelecionaTextoTextBox((txtDiferencaEsperada), e);
        }

        private void txtDiferencaEsperada_Leave(object sender, EventArgs e)
        {
            ValidacaoTextBox.FormatarAoPerderFocoUmaCasaDecimal(sender, e);
            if (string.IsNullOrEmpty(txtDiferencaEsperada.Text))
            {
                txtDiferencaEsperada.Text = "0,0";
            }
            AtualizaValoresGridPessTXT();
        }

        private void txtDiferencaEsperada_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se o caractere digitado é um número (e.Control para permitir teclas como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela o evento, impedindo que o caractere não-numérico seja inserido
                e.Handled = true;
            }
        }

        private bool GravaGrupoFolhaPess()

        {
            try
            {
                cPotenciaisPESS objPotenciaisPESS = new cPotenciaisPESS();
                objPotenciaisPESS.IdResultado = this.IdResultado;
                objPotenciaisPESS.IdPaciente = this.IdPaciente;
                objPotenciaisPESS.IdFolha = this.IdFolha;

                // Validar grid
                if (grdDadosPess.Rows.Count == 0 ||
                    (grdDadosPess.Rows.Count == 1 && grdDadosPess.Rows[0].IsNewRow))
                {
                    MessageBox.Show("Nenhum dado válido para processar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }

                // Processar cada linha
                for (int i = 0; i < grdDadosPess.Rows.Count; i++)
                {
                    if (grdDadosPess.Rows[i].IsNewRow) continue;

                    var rowView = grdDadosPess.Rows[i].DataBoundItem as DataRowView;
                    if (rowView == null) continue;

                    PopularObjetoFromDataRowView(objPotenciaisPESS, rowView);

                    if (!objPotenciaisPESS.AtualizarResultadoPESS())
                    {
                        // Reverter transação em caso de erro
                        MessageBox.Show($"Erro ao processar linha {i + 1}. Operação cancelada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void PopularObjetoFromDataRowView(cPotenciaisPESS obj, DataRowView rowView)
        {
            try
            {
                obj.IdResultadoPess = ObterValorIntSeguro(rowView, "IdResultadoPess");
                obj.IdEstudopotenevocado = ObterValorIntSeguro(rowView, "IdEstudopotenevocado");
                obj.P1N1LadoDireitoRespObt = ObterValorStringSeguro(rowView, "P1N1LadoDireitoRespObt");
                obj.P1N1LadoDireitoRespOEsp = ObterValorStringSeguro(rowView, "P1N1LadoDireitoRespOEsp");
                obj.P1N1LadoEsquerdoRespObt = ObterValorStringSeguro(rowView, "P1N1LadoEsquerdoRespObt");
                obj.P1N1LadoEsquerdoRespOEsp = ObterValorStringSeguro(rowView, "P1N1LadoEsquerdoRespOEsp");
                obj.DiferencaObitida = ObterValorStringSeguro(rowView, "DiferencaObitida");
                obj.DiferencaEsperada = ObterValorStringSeguro(rowView, "DiferencaEsperada");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao popular objeto: {ex.Message}");
            }
        }

        private int ObterValorIntSeguro(DataRowView rowView, string coluna, int valorPadrao = 0)
        {
            try
            {
                if (rowView.Row.Table.Columns.Contains(coluna))
                {
                    object valor = rowView[coluna];
                    return (valor == null || valor == DBNull.Value) ? valorPadrao : Convert.ToInt32(valor);
                }
                return valorPadrao;
            }
            catch
            {
                return valorPadrao;
            }
        }

        private string ObterValorStringSeguro(DataRowView rowView, string coluna, string valorPadrao = "")
        {
            try
            {
                if (rowView.Row.Table.Columns.Contains(coluna))
                {
                    object valor = rowView[coluna];
                    return (valor == null || valor == DBNull.Value) ? valorPadrao : valor.ToString();
                }
                return valorPadrao;
            }
            catch
            {
                return valorPadrao;
            }
        }

        private void RedimensionaTelaPESS()
        {
            tabPotenciais.Size = new Size(684, 268);
            grpBoxDados.Size = new Size(699, 438);
            grpComentario.Location = new Point(12, 451);
            grpBotoes.Location = new Point(12, 704);
            this.Size = new Size(735, 802);
        }
    }
}


