using MySql.Data.MySqlClient;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {


        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtAux = new DataTable();
                clLogin objclLogin = new clLogin();

                string pCripto = "";
                string pDescripto = "";
                byte[] pCifrado;

                pCripto = objclLogin.critptografiaSenha(txtSenha.Text.ToString(), txtUsuario.Text.ToString(), out pCifrado);
                pDescripto = objclLogin.descritptografiaSenha(txtSenha.Text.ToString(), pCifrado);

                dtAux = objclLogin.buscaUsuarioLogin(txtUsuario.Text.ToString());

                // Se não econtrou ninguém...
                if (dtAux.Rows.Count == 0)
                {
                    MessageBox.Show("Usuário não cadastrado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Se encontrou e for troca de senha...
                if (dtAux.Rows.Count == 1 && dtAux.Rows[0]["nome"].ToString() == txtUsuario.Text.ToString() && dtAux.Rows[0]["trocasenha"].ToString() == "1")
                {

                    objclLogin.Nome = txtUsuario.Text.ToString();
                    objclLogin.Senha = txtSenha.Text.ToString();

                    // Deixa o form de senha invisivél
                    this.Hide();

                    // Cria um objeto para o form de troca de senhas abrir
                    frmTrocaSenha objTrocaSenha = new frmTrocaSenha(objclLogin);
                    //objTrocaSenha.objCllogin = objclLogin;
                    //Abre o form de senha modal
                    objTrocaSenha.ShowDialog();

                }

                if (pDescripto != txtUsuario.Text.ToString())
                {
                    MessageBox.Show("Senha Inválida!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else
                {
                    MessageBox.Show("Usuário " + txtUsuario.Text.ToString() +  "conectado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
