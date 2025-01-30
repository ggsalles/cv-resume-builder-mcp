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

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {

                if (validaDados() == false)
                {
                    return;
                }

                DataTable dtAux = new DataTable();
                clLogin objclLogin = new clLogin();
                clLog objclLog = new clLog();

                string pCripto = "";
                byte[] pCifrado;

                // Rotina que critpografa a senha
                pCripto = objclLogin.critptografiaSenha(txtSenha.Text.ToString(), txtUsuario.Text.ToString(), out pCifrado);

                //pDescripto = objclLogin.descritptografiaSenha(txtSenha.Text.ToString(), pCifrado);

                dtAux = objclLogin.buscaUsuarioLogin(txtUsuario.Text.ToString());

                // Se não econtrou ninguém...
                if (dtAux.Rows.Count == 0)
                {
                    MessageBox.Show("Usuário não cadastrado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtAux.Dispose();
                    return;
                }

                // Se encontrou e for troca de senha...
                if (dtAux.Rows.Count == 1 && dtAux.Rows[0]["nome"].ToString() == txtUsuario.Text.ToString() && dtAux.Rows[0]["trocasenha"].ToString() == "1")
                {

                    MessageBox.Show("Você será redirecionado para o formulário de troca de senha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    objclLogin.Id = Int16.Parse(dtAux.Rows[0]["id"].ToString());
                    objclLogin.Nome = txtUsuario.Text.ToString();
                    objclLogin.Senha = txtSenha.Text.ToString();

                    // Deixa o form de senha invisivel
                    this.Visible = false;

                    // Cria um objeto para o form de troca de senhas abrir
                    frmTrocaSenha objTrocaSenha = new frmTrocaSenha(objclLogin);

                    //Abre o form de senha modal
                    objTrocaSenha.ShowDialog();

                    // Deixa o form da senha visivel novamente
                    this.Visible = true;

                    txtSenha.Text = "";

                    return;

                }

                if (pCripto != dtAux.Rows[0]["password"].ToString())
                {
                    MessageBox.Show("Senha Inválida!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtAux.Dispose();
                    return;
                }
                else
                {
                    MessageBox.Show("Usuário " + txtUsuario.Text.ToString().ToUpper() + " conectado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // GRAVA LOG
                    objclLog.Idlogdescricao = 1; // descrição LOGIN na tabela LOGDESCRICAO
                    objclLog.Idusuario = Int32.Parse(dtAux.Rows[0]["id"].ToString());
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
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool validaDados()
        {
            if (txtUsuario.Text.ToString().Length == 0 || txtSenha.Text.ToString().Length == 0)
            {
                MessageBox.Show("O preenchimento dos campos usuário e senha são obrigatórios.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }

}
