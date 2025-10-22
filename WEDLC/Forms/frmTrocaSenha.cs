using System;
using System.Data;
using System.Windows.Forms;
using WEDLC.Banco;
using WinFormsZoom;

namespace WEDLC.Forms
{
    public partial class frmTrocaSenha : Form
    {
        public clLogin objCllogin;
        private FormZoomHelper zoomHelper;

        public frmTrocaSenha(clLogin objCllogin)
        {
            InitializeComponent();
            this.objCllogin = objCllogin;
            zoomHelper = new FormZoomHelper(this); // Inicializa o helper de zoom
            this.FormClosed += (s, e) => zoomHelper.Dispose(); // Descarta automaticamente quando o form for fechado
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Atenção. O processo de troca de senha não foi efetuado. Redirecionado o sistema para a tela de login.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                bool bRet = false;
                bRet = validacampos(txtNovaSenha.Text.ToString(), txtSenha.Text.ToString());

                if (bRet == false)
                {
                    return;
                }
                if (bRet == true)
                {
                    DataTable dtAux = new DataTable();

                    string pCripto = "";
                    byte[] pCifrado;

                    pCripto = objCllogin.critptografiaSenha(txtSenha.Text.ToString(), objCllogin.Nome, out pCifrado);
                    objCllogin.Senha = pCripto;

                    if (objCllogin.incluiLogin() == true)
                    {
                        MessageBox.Show("A troca da senha foi efetuada com sucesso. Voltando para a tela de login.    ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao tentar efetuar a troca da senha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // GRAVA LOG
                    clLog objcLog = new clLog();
                    objcLog.IdLogDescricao = 2; // descrição na tabela LOGDESCRICAO 
                    objcLog.IdUsuario = Sessao.IdUsuario;
                    objcLog.Descricao = this.Name;
                    objcLog.incluiLog();

                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                // GRAVA LOG
                clLog objclLog = new clLog();
                objclLog.IdLogDescricao = 3; // descrição GENÉRICO na tabela LOGDESCRICAO
                objclLog.IdUsuario = Sessao.IdUsuario;
                objclLog.Descricao = ex.Message.ToString();

                if (objclLog.incluiLog() == false)
                {
                    MessageBox.Show("Erro ao tentar gravar o log!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private Boolean validacampos(string novasenha, string senha)

        {
            if (txtNovaSenha.Text.ToString().Length == 0 || txtSenha.Text.ToString().Length == 0)
            {
                MessageBox.Show("Os campos Nova Senha e Senha devem ser preenchidos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNovaSenha.Focus();
                return false;
            }

            if (novasenha != senha)
            {
                MessageBox.Show("As senhas não são iguais. Por favor, informe-as novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNovaSenha.Text = "";
                txtSenha.Text = "";
                txtNovaSenha.Focus();
                return false;
            }
            return true;
        }

        private void frmTrocaSenha_Shown(object sender, EventArgs e)
        {
            txtNovaSenha.Focus();
        }

        private void frmTrocaSenha_Load(object sender, EventArgs e)
        {
            // GRAVA LOG
            clLog objcLog = new clLog();
            objcLog.IdLogDescricao = 4; // descrição na tabela LOGDESCRICAO 
            objcLog.IdUsuario = Sessao.IdUsuario;
            objcLog.Descricao = this.Name;
            objcLog.incluiLog();
        }
    }
}
