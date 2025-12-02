using System;
using System.Data;
using System.Transactions;
using System.Windows.Forms;
using WEDLC.Banco;
using WinFormsZoom;
using static WEDLC.Banco.cUtil;

namespace WEDLC.Forms
{
    public partial class frmComentarioG : Form
    {
        //Código do módulo
        public const int codModulo = 15;

        //Variável para identificar se a chamada vem do fomrmulário de resultado do paciente
        public Int32 IdResultado { get; set; }
        public Int32 IdResultadoG { get; set; }
        public Int32 IdFolha { get; set; }
        public Int32 IdPaciente { get; set; }

        public int codGrupoFolha; //Código do grupo de folha (ComentarioG)
        public Int32 IdResultadoComentarioG { get; set; } // Usado para identificar o resultado PEV

        public bool jaIniciou = false;

        public string sigla;

        public string nome;

        private FormZoomHelper zoomHelper;
        public enum GrupoFolha
        {
            ComentarioG = 8
        }

        public frmComentarioG()
        {
            InitializeComponent();
            this.FormClosed += (s, e) => zoomHelper.Dispose(); // Descarta automaticamente quando o form for fechado
            this.DoubleBuffered = true;
        }

        private void frmComentarioG_Load(object sender, EventArgs e)
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
            if (CarregaDadosG() == false)
            {
                throw new Exception("Erro na CarregaDadosG");
            }


            if (CarregaDadosComentarioG() == false)
            {
                throw new Exception("Erro na CarregaDadosComentarioG"); ;
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
                    if (ValidaComentarioG() == false)
                    {
                        return;
                    }

                    if (GravaGrupoFolhaComentarioG() == false)
                    {
                        throw new Exception("Falha ao gravar comentário G");
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

        private bool CarregaDadosG()
        {
            try
            {
                cComentarioG objComnetarioG = new cComentarioG();

                objComnetarioG.IdPaciente = this.IdPaciente;
                objComnetarioG.IdResultado = this.IdResultado;
                objComnetarioG.IdFolha = this.IdFolha;
                objComnetarioG.IdResultadoG = this.IdResultadoG;

                DataTable dt = objComnetarioG.BuscaResultadoG();

                if (dt.Rows.Count > 0)
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultadoG = Convert.ToInt32(dt.Rows[0]["IdResultadoG"].ToString());
                    txtCodigo.Text = dt.Rows[0]["IdResultadoG"].ToString();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao CarregaDadosG: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool GravaGrupoFolhaComentarioG()
        {
            if (string.IsNullOrEmpty(txtCodigoComentario.Text))
            {
                // Se não houver comentário selecionado, nada a gravar
                return true;
            }

            cComentarioG objcComentarioG = new cComentarioG();

            objcComentarioG.IdResultadoComentarioG = this.IdResultadoComentarioG;
            objcComentarioG.IdResultado = this.IdResultado;
            objcComentarioG.IdPaciente = this.IdPaciente;
            objcComentarioG.IdComentario = Convert.ToInt32(txtCodigoComentario.Text);
            objcComentarioG.Texto = txtTextoComentario.Text.ToUpper();

            if (objcComentarioG.AtualizarResultadoComentarioG() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CarregaDadosComentarioG()
        {
            try
            {
                cComentarioG objcComentarioG = new cComentarioG();

                objcComentarioG.IdResultado = this.IdResultado;
                objcComentarioG.IdPaciente = this.IdPaciente;
                objcComentarioG.IdFolha = this.IdFolha;

                DataTable dt = objcComentarioG.BuscaResultadoComentarioG();

                if (dt.Rows.Count > 0)
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultadoComentarioG = dt.Rows[0]["idresultadocomentariog"] != DBNull.Value ? Int32.Parse(dt.Rows[0]["idresultadocomentariog"].ToString()) : 0;
                    txtCodigoComentario.Text = dt.Rows[0]["idcomentario"].ToString();
                    txtSiglaComentario.Text = dt.Rows[0]["sigla"].ToString();
                    txtNomeComentario.Text = dt.Rows[0]["nome"].ToString();
                    txtTextoComentario.Text = dt.Rows[0]["descricao"].ToString();
                }
                else
                {
                    // Preenche os campos com os dados retornados
                    this.IdResultadoComentarioG = 0;
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
        private bool ValidaComentarioG()
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

            // Indica que a chamada é para resultado
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


