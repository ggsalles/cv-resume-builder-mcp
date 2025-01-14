using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    objclLog.Idlogdescricao = 2; // descrição LOGIN na tabela LOGDESCRICAO
                    objclLog.Idusuario = Int32.Parse(objCllogin.Id.ToString());
                    objclLog.Descerrovs = "";

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
                objclLog.Idlogdescricao = 3; // descrição GENÉRICO na tabela LOGDESCRICAO
                objclLog.Idusuario = 9999;
                objclLog.Descerrovs = ex.Message.ToString();

                if (objclLog.incluiLogin() == false)
                {
                    MessageBox.Show("Erro ao tentar gravar o log!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private Boolean validacampos(string novasenha, string senha)

        {
            if (novasenha != senha)
            {
                MessageBox.Show("As senhas não são iguais. Por favor, informe-as novamente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNovaSenha.Text = "";
                txtSenha.Text = "";
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
