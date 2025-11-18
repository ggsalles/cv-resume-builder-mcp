using System;
using System.Data;
using System.Transactions;
using System.Windows.Forms;
using WEDLC.Banco;
using WinFormsZoom;
using static WEDLC.Banco.cUtil;

namespace WEDLC.Forms
{
    public partial class frmAVJ : Form
    {
        //Código do módulo
        public const int codModulo = 15;

        //Variável para identificar se a chamada vem do fomrmulário de resultado do paciente
        public int IdResultado { get; set; }
        public int IdResultadoAVJ { get; set; } // Usado para identificar o resultado PEV
        public int IdFolha { get; set; }
        public int IdPaciente { get; set; }

        public int codGrupoFolha; //Código do grupo de folha (PEV, PESS, PEA, PEGC, PESSMED)
        public int IdResultadoComentarioAVJ { get; set; } // Usado para identificar o resultado PEV

        public bool jaIniciou = false;

        public string sigla;

        public string nome;

        private FormZoomHelper zoomHelper;
        public enum GrupoFolha
        {
            AVJ = 7
        }

        public frmAVJ()
        {
            InitializeComponent();
            this.FormClosed += (s, e) => zoomHelper.Dispose(); // Descarta automaticamente quando o form for fechado
            this.DoubleBuffered = true;
        }

        private void frmAVJ_Load(object sender, EventArgs e)
        {
            //Verifica permissão de acesso
            if (!cPermissao.PodeAcessarModulo(codModulo))
            {
                MessageBox.Show("Usuário sem acesso", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Fecha de forma segura depois que o handle estiver pronto
                this.BeginInvoke(new Action(() => this.Close()));
                return;
            }

            iniciaTela();
            this.Text = "Folha: " + sigla + " - " + nome;
            zoomHelper = new FormZoomHelper(this); // Inicializa o helper de zoom

            // GRAVA LOG
            clLog objcLog = new clLog();
            objcLog.IdLogDescricao = 4; // descrição na tabela LOGDESCRICAO 
            objcLog.IdUsuario = Sessao.IdUsuario;
            objcLog.Descricao = this.Name;
            objcLog.incluiLog();
        }

        private void frmAVJ_Shown(object sender, EventArgs e)
        {
            txtEstimulacao.Focus();
            ValidacaoTextBox.SelecionaTextoTextBox((txtEstimulacao), e);
        }
        public void iniciaTela()
        {
            try
            {
                //De acordo com o grupo de folha, formata a tela
                CarregaTela();

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar a tela!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void CarregaTela()
        {
            if (CarregaDadosAVJ() == false)
            {
                throw new Exception("Erro na CarregaDadosPev");
            }


            if (CarregaDadosComentarioAVJ() == false)
            {
                throw new Exception("Erro na CarregaDadosComentarioAVJ"); ;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            //Verifica permissão de gravação
            if (!cPermissao.PodeGravarModulo(codModulo))
            {
                MessageBox.Show("Você não tem permissão para gravar neste módulo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var scope = new TransactionScope())
                {
                    if (GravaGrupoFolhaAVJ() == false)
                    {
                        throw new Exception("Falha ao gravar AVJ");
                    }

                    if (ValidaComentarioPEV() == false)
                    {
                        return;
                    }

                    if (GravaGrupoFolhaComentarioAVJ() == false)
                    {
                        throw new Exception("Falha ao gravar comentário AVJ");
                    }

                    // GRAVA LOG
                    clLog objcLog = new clLog();
                    objcLog.IdLogDescricao = 5; // descrição na tabela LOGDESCRICAO 
                    objcLog.IdUsuario = Sessao.IdUsuario;
                    objcLog.Descricao = this.Name;
                    objcLog.incluiLog();

                    // Se tudo ok, commit na transação
                    scope.Complete();

                    MessageBox.Show("Gravado com sucesso!", "Atenção",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar gravar!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        private bool CarregaDadosAVJ()
        {
            try
            {
                cAVJ objAvj = new cAVJ();

                objAvj.IdPaciente = this.IdPaciente;
                objAvj.IdResultado = this.IdResultado;
                objAvj.IdFolha = this.IdFolha;
                objAvj.IdResultadoAVJ = this.IdResultadoAVJ;

                DataTable dt = objAvj.BuscaResultadoAVJ();

                if (dt.Rows.Count > 0)
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultadoAVJ = Convert.ToInt32(dt.Rows[0]["IdResultadoAVJ"].ToString());
                    txtCodigo.Text = dt.Rows[0]["IdResultadoAVJ"].ToString();
                    txtEstimulacao.Text = dt.Rows[0]["Estimulacao"].ToString();
                    txtNervo.Text = dt.Rows[0]["Nervo"].ToString();
                    txtMusculo.Text = dt.Rows[0]["Musculo"].ToString();
                    txtAntes.Text = dt.Rows[0]["AntesdoExercicio"].ToString();
                    txtLogo.Text = dt.Rows[0]["LogoAposExercicio"].ToString();
                    txttres.Text = dt.Rows[0]["TresMinutosApos"].ToString();
                    return true;
                }
                else
                {
                    txtEstimulacao.Text = "";
                    txtNervo.Text = "";
                    txtMusculo.Text = "";
                    txtAntes.Text = "";
                    txtLogo.Text = "";
                    txttres.Text = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao CarregaDadosAVJ: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool GravaGrupoFolhaAVJ()
        {
            cAVJ objcAVJ = new cAVJ();

            objcAVJ.IdResultadoAVJ = this.IdResultadoAVJ;
            objcAVJ.IdResultado = this.IdResultado;
            objcAVJ.IdPaciente = this.IdPaciente;
            objcAVJ.Estimulacao = txtEstimulacao.Text;
            objcAVJ.Nervo = txtNervo.Text;
            objcAVJ.Musculo = txtMusculo.Text;
            objcAVJ.AntesExercicio = txtAntes.Text;
            objcAVJ.LogoAposExercicio = txtLogo.Text;
            objcAVJ.TresMinutosApos = txttres.Text;

            if (objcAVJ.AtualizarResultadoAVJ() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private bool GravaGrupoFolhaComentarioAVJ()
        {
            if (string.IsNullOrEmpty(txtCodigoComentario.Text))
            {
                // Se não houver comentário selecionado, nada a gravar
                return true;
            }

            cAVJ objcAVJ= new cAVJ();

            objcAVJ.IdResultadoComentario = this.IdResultadoComentarioAVJ;
            objcAVJ.IdResultado = this.IdResultado;
            objcAVJ.IdPaciente = this.IdPaciente;
            objcAVJ.IdComentario = Convert.ToInt32(txtCodigoComentario.Text);
            objcAVJ.Texto = txtTextoComentario.Text.ToUpper();

            if (objcAVJ.AtualizarResultadoComentarioAVJ() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CarregaDadosComentarioAVJ()
        {
            try
            {
                cAVJ objcAVJ = new cAVJ();

                objcAVJ.IdResultado = this.IdResultado;
                objcAVJ.IdPaciente = this.IdPaciente;

                DataTable dt = objcAVJ.BuscaResultadoComentarioAVJ();

                if (dt.Rows.Count > 0)
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultadoComentarioAVJ = dt.Rows[0]["idresultadocomentarioavj"] != DBNull.Value ? Int32.Parse(dt.Rows[0]["idresultadocomentarioavj"].ToString()) : 0;
                    txtCodigoComentario.Text = dt.Rows[0]["idcomentario"].ToString();
                    txtSiglaComentario.Text = dt.Rows[0]["sigla"].ToString();
                    txtNomeComentario.Text = dt.Rows[0]["nome"].ToString();
                    txtTextoComentario.Text = dt.Rows[0]["descricao"].ToString();
                }
                else
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultadoComentarioAVJ = 0;
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
        private bool ValidaComentarioPEV()
        {
            if (string.IsNullOrEmpty(txtCodigoComentario.Text))
            {
                MessageBox.Show("Por favor, selecione um tipo de comentário.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
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
    }
}


