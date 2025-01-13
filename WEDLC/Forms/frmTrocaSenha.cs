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
            Sair();
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

                    MessageBox.Show("Troca de senha efetuado com sucesso. Voltando para a tela de login.    ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Sair();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void Sair()
        {
            frmLogin objLogin = new frmLogin();
            objLogin.ShowDialog();
            Close();
        }
    }
}
