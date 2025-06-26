using System;
using System.Data;
using System.Windows.Forms;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmTrocaSenha : Form
    {
        public clLogin objCllogin;

        public frmTrocaSenha(clLogin objCllogin)
        {
            InitializeComponent();
            this.objCllogin = objCllogin;
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

                    var retorno = objCllogin.incluiLogin();

                    MessageBox.Show("A troca da senha foi efetuada com sucesso. Voltando para a tela de login.    ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // GRAVA LOG
                    clLog objclLog = new clLog();
                    objclLog.IdLogDescricao = 2; // descrição LOGIN na tabela LOGDESCRICAO
                    objclLog.IdUsuario = objCllogin.Idusuario;
                    objclLog.DescErro = "";

                    if (objclLog.incluiLogin() == false)
                    {
                        MessageBox.Show("Erro ao tentar gravar o log!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                // GRAVA LOG
                clLog objclLog = new clLog();
                objclLog.IdLogDescricao = 3; // descrição GENÉRICO na tabela LOGDESCRICAO
                objclLog.IdUsuario = 9999;
                objclLog.DescErro = ex.Message.ToString();

                if (objclLog.incluiLogin() == false)
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
    }
}
